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
using System.Linq;

namespace Hammurabi
{
	// TODO: Review accessibility level of these methods
	
	public partial class Facts
	{
		/// <summary>
		/// This is the main global data structure.
		/// It's where all the asserted facts live.
		/// </summary>
		private static List<Fact> FactBase = new List<Fact>();
        
		/// <summary>
		/// A Fact object represents a fact that is stored in the knowledge
		/// base.
		/// </summary>
		public class Fact
		{
            public string relationship;
            public object Arg1;
            public object Arg2;
            public object Arg3;
			public Tvar v;

            /// <summary>
            /// Sets a Tvar fact that establishes a relation between legal entities.
            /// </summary>
            public Fact(string rel, object arg1, object arg2, object arg3, Tvar val)
            {
                relationship = rel;
                Arg1 = arg1;
                Arg2 = arg2;
                Arg3 = arg3;
                v = val;
            }

            public Fact(string rel, object arg1, object arg2, object arg3)
            {
                relationship = rel;
                Arg1 = arg1;
                Arg2 = arg2;
                Arg3 = arg3;
                v = null;
            }
		}
		
        /// <summary>
        /// Counts how many known facts there are. 
        /// </summary>
        public static int Count()
        {
            return FactBase.Count;
        }
        
		/// <summary>
		/// Retracts all facts in the factbase
		/// </summary>
		public static void Clear()
		{
			FactBase.Clear();
            ThingBase.Clear();
		}
        
        /// <summary>
        /// Restores factbase to its virgin state.
        /// </summary>
        public static void Reset()
        {
            FactBase.Clear();
            Facts.Unknowns.Clear();
            Facts.GetUnknowns = false;
        }

        /// <summary>
        /// Returns true if a symmetrical fact has been assserted.
        /// </summary>
        public static bool HasBeenAssertedSym(object e1, string rel, object e2)
        {
            return HasBeenAsserted(rel, e1, e2) ||
                HasBeenAsserted(rel, e2, e1);
        }

        /// <summary>
        /// Returns true if a fact has been assserted - 1 entity.
        /// </summary>
        public static bool HasBeenAsserted(string rel, object e1)
        {
            return HasBeenAsserted(rel, e1, null, null);
        }
        
        /// <summary>
        /// Returns true if a fact has been assserted - 2 entities.
        /// </summary>
        public static bool HasBeenAsserted(string rel, object e1, object e2)
        {
            return HasBeenAsserted(rel, e1, e2, null);
        }
        
        /// <summary>
        /// Returns true if a fact has been assserted - 3 entities.
        /// </summary>
        public static bool HasBeenAsserted(string rel, object e1, object e2, object e3)
        {
            // Look up fact in table of facts
            foreach (Fact f in FactBase)
            {
                if (f.relationship == rel && f.Arg1 == e1 && f.Arg2 == e2 && f.Arg3 == e3)
                {
                    return true;
                }
            }
            
            // If fact is not found...
            return false;
        }

        /// <summary>
        /// Displays a crude list of all facts that have been asserted.
        /// To be used for diagnostic purposes only.
        /// </summary>
        public static string AssertedFacts()
        {
            string result = "";

            foreach (Fact f in FactBase)
            {
                result += ((Thing)f.Arg1).Id + " " + f.relationship;

                if ((Thing)f.Arg2 != null)
                {
                    result += " " + ((Thing)f.Arg2).Id;
                }

                result += " = " + f.v.TestOutput + "\n";
            }

            return result;
        }
	}
}