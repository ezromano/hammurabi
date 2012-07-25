// Copyright (c) 2012 Hammura.bi LLC
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Text.RegularExpressions;

namespace Akkadian
{
    public class Meth
    {
        public static string wrd = @"[a-zA-Z0-9_]+";

        public static string QueryTvarTransform(string line)
        {
            string typs = @"(Tbool|Tnum|Tstr|Tset|Tdate|Date|Person)";

            // People (rule condition)
            if (line.Trim().StartsWith("PersonIn"))
            {
                // Currently only handles one argument...
                line = Regex.Replace(line, @"PersonIn (?<fcn>"+wrd+@")\((?<arg>"+wrd+@")\)", "Facts.QueryPerson(\"${fcn}\", ${arg})", RegexOptions.IgnoreCase);  
            }

            // Rule conclusion line 
            else if (Util.IsInputRule(line))  // Starts w/ TvarIn at indent 0...
            {
                // TboolInSym (always has two arguments)
                line = Regex.Replace(line, 
                          @"TboolInSym (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@"), ?(?<argtyp2>"+wrd+@" )(?<arg2>"+wrd+@")\)",
                          "        public static Tbool ${fcn}(${argtyp1} ${arg1}, ${argtyp2} ${arg2})\r\n        {\r\n            return Facts.Sym(${arg1}, \"${fcn}\", ${arg2});");  
    
                // Functions with three arguments
                line = Regex.Replace(line, 
                          @"(?<type>"+typs+@")In (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@"), ?(?<argtyp2>"+wrd+@" )(?<arg2>"+wrd+@"), ?(?<argtyp3>"+wrd+@" )(?<arg3>"+wrd+@")\)",
                          "        public static ${type} ${fcn}(${argtyp1} ${arg1}, ${argtyp2} ${arg2}, ${argtyp3} ${arg3})\r\n        {\r\n            return Facts.QueryTvar<${type}>(\"${fcn}\", ${arg1}, ${arg2}, ${arg3});");  


                // Functions with two arguments
                line = Regex.Replace(line, 
                          @"(?<type>"+typs+@")In (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@"), ?(?<argtyp2>"+wrd+@" )(?<arg2>"+wrd+@")\)",
                          "        public static ${type} ${fcn}(${argtyp1} ${arg1}, ${argtyp2} ${arg2})\r\n        {\r\n            return Facts.QueryTvar<${type}>(\"${fcn}\", ${arg1}, ${arg2});");  

                // Functions with one argument
                line = Regex.Replace(line, 
                          @"(?<type>"+typs+@")In (?<fcn>"+wrd+@")\((?<argtyp>"+wrd+@" )(?<arg>"+wrd+@")\)",
                          "        public static ${type} ${fcn}(${argtyp} ${arg})\r\n        {\r\n            return Facts.QueryTvar<${type}>(\"${fcn}\", ${arg});");  
            }

            // Is rule condition line, not rule conclusion
            else                
            {
                // TboolInSym (always has two arguments)
                line = Regex.Replace(line, @"TboolInSym (?<fcn>"+wrd+@")\((?<arg1>[a-zA-Z0-9 ]+),(?<arg2>[a-zA-Z0-9 ]+)\)", 
                                     "Facts.Sym(${arg1}, \"${fcn}\", ${arg2})");  

                // Functions with three arguments
                line = Regex.Replace(line, @"(?<type>"+typs+@")In (?<fcn>"+wrd+@")\((?<arg1>[a-zA-Z0-9 ]+),(?<arg2>[a-zA-Z0-9 ]+),(?<arg3>[a-zA-Z0-9 ]+)\)", 
                                     "Facts.QueryTvar<${type}>(\"${fcn}\", ${arg1}, ${arg2}, ${arg3})", RegexOptions.IgnoreCase);  

                // Functions with two arguments
                line = Regex.Replace(line, @"(?<type>"+typs+@")In (?<fcn>"+wrd+@")\((?<arg1>[a-zA-Z0-9 ]+),(?<arg2>[a-zA-Z0-9 ]+)\)", 
                                     "Facts.QueryTvar<${type}>(\"${fcn}\", ${arg1}, ${arg2})", RegexOptions.IgnoreCase);  

                // Functions with one argument
                line = Regex.Replace(line, @"(?<type>"+typs+@")In (?<fcn>"+wrd+@")\((?<arg>"+wrd+@")\)", "Facts.QueryTvar<${type}>(\"${fcn}\", ${arg})", RegexOptions.IgnoreCase);  
            }
            
            return line;
        }
        
        /// <summary>
        /// Handles intermediate facts (facts asserted mid-tree)
        /// </summary>
        public static string CreateIntermediateAssertion(string line)
        {
            // Symmetrical facts, e.g. TboolInSym Fcn(Person p1, Person p2) =
            if (line.StartsWith("TboolInSym"))
            {
                line = Regex.Replace(line, 
                    @"TboolInSym (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@"), ?(?<argtyp2>"+wrd+@" )(?<arg2>"+wrd+@")\) =",
                     "        public static Tbool ${fcn}(${argtyp1} ${arg1}, ${argtyp2} ${arg2})\r\n" +
                     "        {\r\n" +
                     "            if (EntityArgIsUnknown(${arg1},${arg2})) return new Tbool(Hstate.Unstated);\r\n" +
                     "            if (Facts.HasBeenAssertedSym(${arg1}, \"${fcn}\", ${arg2}))\r\n" +
                     "            {\r\n" +
                     "                return Facts.Sym(${arg1}, \"${fcn}\", ${arg2});\r\n" +
                     "            }\r\n\r\n");
            }
            else
            {
                // Three arguments
                line = Regex.Replace(line, 
                    @"(?<typ>(Tbool|Tnum|Tstr|Tdate|Tset|Person))In (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@"), ?(?<argtyp2>"+wrd+@" )(?<arg2>"+wrd+@"), ?(?<argtyp3>"+wrd+@" )(?<arg3>"+wrd+@")\) =",
                     "        public static ${typ} ${fcn}(${argtyp1} ${arg1}, ${argtyp2} ${arg2}, ${argtyp3} ${arg3})\r\n" +
                     "        {\r\n" +
                     "            if (EntityArgIsUnknown(${arg1},${arg2},${arg3})) return new ${typ}(Hstate.Unstated);\r\n" +
                     "            if (Facts.HasBeenAsserted(\"${fcn}\", ${arg1}, ${arg2}, ${arg3}))\r\n" +
                     "            {\r\n" +
                     "                return Facts.QueryTvar<${typ}>(\"${fcn}\", ${arg1}, ${arg2}, ${arg3});\r\n" +
                     "            }\r\n\r\n");

                // Two arguments
                line = Regex.Replace(line, 
                    @"(?<typ>(Tbool|Tnum|Tstr|Tdate|Tset|Person))In (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@"), ?(?<argtyp2>"+wrd+@" )(?<arg2>"+wrd+@")\) =",
                     "        public static ${typ} ${fcn}(${argtyp1} ${arg1}, ${argtyp2} ${arg2})\r\n" +
                     "        {\r\n" +
                     "            if (EntityArgIsUnknown(${arg1},${arg2})) return new ${typ}(Hstate.Unstated);\r\n" +
                     "            if (Facts.HasBeenAsserted(\"${fcn}\", ${arg1}, ${arg2}))\r\n" +
                     "            {\r\n" +
                     "                return Facts.QueryTvar<${typ}>(\"${fcn}\", ${arg1}, ${arg2});\r\n" +
                     "            }\r\n\r\n");

                // One argument
                line = Regex.Replace(line, 
                    @"(?<typ>(Tbool|Tnum|Tstr|Tdate|Tset|Person))In (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@")\) =",
                     "        public static ${typ} ${fcn}(${argtyp1} ${arg1})\r\n" +
                     "        {\r\n" +
                     "            if (EntityArgIsUnknown(${arg1})) return new ${typ}(Hstate.Unstated);\r\n" +
                     "            if (Facts.HasBeenAsserted(\"${fcn}\", ${arg1}))\r\n" +
                     "            {\r\n" +
                     "                return Facts.QueryTvar<${typ}>(\"${fcn}\", ${arg1});\r\n" +
                     "            }\r\n\r\n");
            }

            return line;
        }

        /// <summary>
        /// Handles intermediate facts (facts asserted mid-tree)
        /// </summary>
        public static string CreateMainRule(string line)
        {
            // Three arguments
            line = Regex.Replace(line, 
                @"(?<typ>(Tbool|Tnum|Tstr|Tdate|Tset)) (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@"), ?(?<argtyp2>"+wrd+@" )(?<arg2>"+wrd+@"), ?(?<argtyp3>"+wrd+@" )(?<arg3>"+wrd+@")\) =",
                 "        public static ${typ} ${fcn}(${argtyp1} ${arg1}, ${argtyp2} ${arg2}, ${argtyp3} ${arg3})\r\n" +
                 "        {\r\n" +
                 "            if (EntityArgIsUnknown(${arg1},${arg2},${arg3})) return new ${typ}(Hstate.Unstated);\r\n\r\n");

            // Two arguments
            line = Regex.Replace(line, 
                @"(?<typ>(Tbool|Tnum|Tstr|Tdate|Tset)) (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@"), ?(?<argtyp2>"+wrd+@" )(?<arg2>"+wrd+@")\) =",
                 "        public static ${typ} ${fcn}(${argtyp1} ${arg1}, ${argtyp2} ${arg2})\r\n" +
                 "        {\r\n" +
                 "            if (EntityArgIsUnknown(${arg1},${arg2})) return new ${typ}(Hstate.Unstated);\r\n\r\n");

            // One argument
            line = Regex.Replace(line, 
                @"(?<typ>(Tbool|Tnum|Tstr|Tdate|Tset)) (?<fcn>"+wrd+@")\((?<argtyp1>"+wrd+@" )(?<arg1>"+wrd+@")\) =",
                 "        public static ${typ} ${fcn}(${argtyp1} ${arg1})\r\n" +
                 "        {\r\n" +
                 "            if (EntityArgIsUnknown(${arg1})) return new ${typ}(Hstate.Unstated);\r\n\r\n");

            // Otherwise, no need to check the arguments for uncertainty...
            string word = @"[-!\+\*/A-Za-z0-9\.;\(\),""'_<>=&| ]+";
            line = Regex.Replace(line, @"(?<dec>(Tbool|Tnum|Tstr|Tdate|Tset|Person|Entity|DateTime|bool)"+word+") =",
                                      "        public static ${dec}\r\n        {\r\n");  // TODO: Needs EntityArgIsUnknown() line

            return line;
        }

        /// <summary>
        /// Determines whether a string is a valid number
        /// </summary>
        public static bool IsNumber(string s)
        {
            try
            {
                double.Parse(s);
                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}