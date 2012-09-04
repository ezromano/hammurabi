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

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Akkadian
{
    /// <summary>
    /// Processes assumptions that are declared in the Akkadian rules.
    /// </summary>
    public class Assumptions
    {
        /// <summary>
        /// Converts any assumptions on the specified line to C#.
        /// </summary>
        public static string Process(string line)
        {
            if (line.Contains(" assumes "))
            {

                // Regex for a relationship
                string relRegex = "[a-zA-Z0-9_,()= \"]+";

                Match match = Regex.Match(line, "(?<rel1>" + relRegex + ") assumes (?<rel2>" + relRegex + ")");

                if (match.Success)
                {
                    string leftNode = NodeString(match.Groups[1].Value.Trim());
                    string rightNode = NodeString(match.Groups[2].Value.Trim());

                    // Assemble full line...
                    string fullLine = "(" + leftNode + ", " + rightNode + ")";
//                    Console.WriteLine(fullLine);
//                    Console.ReadLine();
                }

                // Return a blank line so this doesn't interfere with rule compilation
                return "";
            }
            else
            {
                // Do nothing
                return line;
            }
        }

        /// <summary>
        /// For a given relationship, returns a C# string declaring an AssumptionPoint.
        /// </summary>
        private static string NodeString(string node)
        {
            string result = "";
            string quote = "\"";

            // Analyze the relationship...         1             2        3         4        5        7       8                   9
            Match match = Regex.Match(node, @"([a-zA-Z0-9_]+)\(([0-9]),?([0-9])?,?([0-9])?\)( = )?((false)|([0-9]+)|("+quote+"[a-zA-Z0-9 ]+"+quote+"))?");
            if (match.Success)
            {
                // Get the relationship and the arguments
                string rel = match.Groups[1].Value.Trim();
                string arg1 = match.Groups[2].Value != "" ? match.Groups[2].Value : "0";
                string arg2 = match.Groups[3].Value != "" ? match.Groups[3].Value : "0";
                string arg3 = match.Groups[4].Value != "" ? match.Groups[4].Value : "0";
                result = "new AssumptionPoint(\""+rel+"\", "+arg1+", "+arg2+", "+arg3+", ";

                // Create the string for the proper type of Tvar
                if (match.Groups[5].Value == "") result += "new Tbool(true)";
                else if (match.Groups[7].Value == "false") result += "new Tbool(false)";
                else if (match.Groups[8].Value != "") result += "new Tnum("+match.Groups[8].Value+")";
                else if (match.Groups[9].Value != "") result += "new Tstr("+match.Groups[9].Value+")";
                result += ")";
            }

            return result;
        }
    }
}