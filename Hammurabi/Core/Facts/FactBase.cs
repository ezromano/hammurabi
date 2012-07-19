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
		private class Fact
		{
            public string relationship;
			public LegalEntity subject;
			public LegalEntity directObject;
            public LegalEntity directObject2;
			public Tvar v;
            
            /// <summary>
            /// Sets a Tvar fact that establishes a relation between legal entities (1)
            /// </summary>
            public Fact(string rel, LegalEntity subj, Tvar val)
            {
                relationship = rel;
                subject = subj;
                directObject = null;
                directObject2 = null;
                v = val;
            }

            /// <summary>
            /// Sets a Tvar fact that establishes a relation between legal entities (2)
            /// </summary>
            public Fact(string rel, LegalEntity subj, LegalEntity obj1, Tvar val)
            {
                relationship = rel;
                subject = subj;
                directObject = obj1;
                directObject2 = null;
                v = val;
            }

            /// <summary>
            /// Sets a Tvar fact that establishes a relation between legal entities (3) 
            /// </summary>
            public Fact(string rel, LegalEntity subj, LegalEntity obj1, LegalEntity obj2, Tvar val)
            {
                relationship = rel;
                subject = subj;
                directObject = obj1;
                directObject2 = obj2;
                v = val;
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
        public static bool HasBeenAssertedSym(LegalEntity e1, string rel, LegalEntity e2)
        {
            return HasBeenAsserted(e1, rel, e2) ||
                   HasBeenAsserted(e2, rel, e1);
        }

        /// <summary>
        /// Returns true if a fact has been assserted
        /// </summary>
        public static bool HasBeenAsserted(LegalEntity e1, string rel, LegalEntity e2)
        {
            // Look up fact in table of facts
            foreach (Fact f in FactBase)
            {
                if (f.subject == e1 && f.relationship == rel && f.directObject == e2)
                {
                    return true;
                }
            }

            // If fact is not found...
            return false;
        }
        
        /// <summary>
        /// Returns true if a fact has been assserted
        /// </summary>
        public static bool HasBeenAsserted(LegalEntity e1, string rel)
        {
            foreach (Fact f in FactBase)
            {
                if (f.subject == e1 && f.relationship == rel)
                {
                    return true;
                }
            }

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
                result += f.subject.Id + " " + f.relationship;

                if (f.directObject != null)
                    result += " " + f.directObject.Id;

                result += " = " + f.v.TestOutput + "\n";
            }

            return result;
        }

	}
}