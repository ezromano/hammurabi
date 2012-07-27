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

namespace Hammurabi
{
    public partial class Facts
    {
        /// <summary>
        /// List of facts that are unknown and needed to resolve a goal (method)
        /// that has been called.
        /// </summary>
        public static List<Factlet> Unknowns = new List<Factlet>();
        
        /// <summary>
        /// When true, allows facts to be added to UnknownFacts.
        /// Needs to be false by default or UnknownFacts would devour all memory.
        /// </summary>
        public static bool GetUnknowns = false;

        /// <summary>
        /// Add a (two-entity) factlet to UnknownFacts.
        /// </summary>
        public static void AddUnknown(string rel, Thing e1, Thing e2, Thing e3)
        {
            // Keep list from devouring the entire universe
            if (Unknowns.Count < 500)
            {
                // Ignore duplicates
                if (!IsUnknown(rel, e1, e2, e3))
                { 
                    Unknowns.Add(new Factlet(rel, e1, e2, e3));
                }
            }
        }

        /// <summary>
        /// Indicates whether Facts.Unknowns contains a given factlet.
        /// </summary>
        /// <remarks>
        /// Note that this is distinct from whether a fact HasBeenAsserted.
        /// </remarks>
        public static bool IsUnknown(string rel, Thing e1, Thing e2, Thing e3)
        {
            foreach (Factlet t in Unknowns)
            {
                if (t.relationship == rel &&
                    t.subject == e1 &&
                    t.object1 == e2 &&
                    t.object2 == e3)
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Returns a string showing all factlets in Facts.Unknowns.
        /// </summary>
        public static string ShowUnknowns()
        {
            string result = "";
            
            foreach (Facts.Factlet f in Facts.Unknowns)
            {
                if (f.object2 != null)
                {
                    result += f.subject.Id + " " + f.relationship + " " + f.object1.Id + " " + f.object2.Id+ "\n";
                }
                else if (f.object1 != null)
                {
                    result += f.subject.Id + " " + f.relationship + " " + f.object1.Id + "\n";
                }
                else
                {
                    result += f.subject.Id + " " + f.relationship + "\n";
                }
            }
            
            return result;
        }

        /// <summary>
        /// A Factlet object represents a fact that has no value because it
        /// is needed (unstated).
        /// </summary>
        public class Factlet
        {
            public string relationship;
            public Thing subject;
            public Thing object1;
            public Thing object2;

            /// <summary>
            /// Factlet that relates to two legal entities.  
            /// </summary>
            public Factlet(string rel, Thing subj, Thing obj1, Thing obj2)
            {
                subject = subj;
                relationship = rel;
                object1 = obj1;
                object2 = obj2;
            }
        }
    }
}