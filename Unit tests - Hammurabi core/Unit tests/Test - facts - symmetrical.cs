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

using Hammurabi;
using NUnit.Framework;

namespace Hammurabi.UnitTests.CoreFcns
{
    #pragma warning disable 219
    
    [TestFixture]
    public class SymmetricalFacts : H
    {
        private static Person p1 = new Person("P1");
        private static Person p2 = new Person("P2");
        
        // .Sym combos
        
        [Test]
        public void SymTT ()
        {
            Facts.Clear();
            Facts.Assert(p1, "IsMarriedTo", p2);
            Facts.Assert(p2, "IsMarriedTo", p1);
            Tbool result = Facts.Sym(p1, "IsMarriedTo", p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM True ", result.TestOutput);       
        }
        
        [Test]
        public void SymTF ()
        {
            Facts.Clear();
            Facts.Assert(p1, "IsMarriedTo", p2);
            Facts.Assert(p2, "IsMarriedTo", p1, false);                         // contradictory assertion
            Tbool result = Facts.Sym(p1, "IsMarriedTo", p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM True ", result.TestOutput);    // what is desired here? (or forbid contradictions)
        }
        
        [Test]
        public void SymTU ()
        {
            Facts.Clear();
            Facts.Assert(p1, "IsMarriedTo", p2);
            Tbool result = Facts.Sym(p1, "IsMarriedTo", p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM True ", result.TestOutput);       
        }
        
        [Test]
        public void SymFF ()
        {
            Facts.Clear();
            Facts.Assert(p1, "IsMarriedTo", p2, false);
            Facts.Assert(p2, "IsMarriedTo", p1, false);
            Tbool result = Facts.Sym(p1, "IsMarriedTo", p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM False ", result.TestOutput);       
        }  
        
        [Test]
        public void SymFU ()
        {
            Facts.Clear();
            Facts.Assert(p1, "IsMarriedTo", p2, false);
            Tbool result = Facts.Sym(p1, "IsMarriedTo", p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM False ", result.TestOutput);       
        }
        
        [Test]
        public void SymUU ()
        {
            Facts.Clear();
            Tbool result = Facts.Sym(p1, "IsMarriedTo", p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM Unstated ", result.TestOutput);       
        }
        
        // .Either - correct result?
        
        [Test]
        public void Either_Result_1 ()
        {
            Facts.Clear();
            Facts.Assert(p1, "FamilyRelationship", p2, "Biological parent");
            Tbool result = Fam.IsBiologicalParentOf(p1, p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM True ", result.TestOutput);       
        }
        
        [Test]
        public void Either_Result_2 ()
        {
            Facts.Clear();
            Facts.Assert(p2, "FamilyRelationship", p1, "Biological child");
            Tbool result = Fam.IsBiologicalParentOf(p1, p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM True ", result.TestOutput);       
        }
        
        [Test]
        public void Either_Result_3 ()
        {
            Facts.Clear();
            Tbool result = Fam.IsBiologicalParentOf(p1, p2);
            Assert.AreEqual("1/1/0001 12:00:00 AM Unstated ", result.TestOutput);       
        }
        
        // .Either - correct identification of unknown facts?
        
        [Test]
        public void Either_Unknowns_1 ()
        {
            // After first fact is established, the second one should not be added
            // to the Facts.Unknowns list.
            Facts.Reset();
            Facts.Assert(p1, "FamilyRelationship", p2, "Biological parent");
            Facts.GetUnknowns = true;
            Tbool result = Fam.IsBiologicalParentOf(p1, p2);
            Facts.GetUnknowns = false;
            bool f = Facts.IsUnknown(p2, "FamilyRelationship", p1);
            Assert.AreEqual(false, f);       
        }
        
        [Test]
        public void Either_Unknowns_2 ()
        {
            // Same as above, but input fact is false
            Facts.Reset();
            Facts.Assert(p1, "FamilyRelationship", p2, "Grandchild");
            Facts.GetUnknowns = true;
            Tbool result = Fam.IsBiologicalParentOf(p1, p2);
            Facts.GetUnknowns = false;
            bool f = Facts.IsUnknown(p2, "FamilyRelationship", p1);
            Assert.AreEqual(false, f);       
        }
        
//        [Test]
//        public void Either_Unknowns_3 ()
//        {
//            // After reverse fact is established, the fwd-fact should not be added
//            // to the Facts.Unknowns list.
//            Facts.Reset();
//            Facts.Assert(p2, "FamilyRelationship", p1, "Biological parent");
//            Facts.GetUnknowns = true;
//            Tbool result = Fam.IsParentOf(p1, p2);
//            Facts.GetUnknowns = false;
//            bool f = Facts.IsUnknown(p1, "FamilyRelationship", p2);
//            Assert.AreEqual(false, f);       
//        }
        
        [Test]
        public void Either_Unknowns_4 ()
        {
            // After reverse fact is established, the fwd-fact should not be added
            // to the Facts.Unknowns list.
            Facts.Reset();
            Facts.Assert(p2, "FamilyRelationship", p1, "Biological parent");
            Facts.GetUnknowns = true;
            Tbool result = Fam.IsBiologicalParentOf(p1, p2);
            Facts.GetUnknowns = false;
            bool f = Facts.IsUnknown(p1, "FamilyRelationship", p2);
            Assert.AreEqual(false, f);       
        }
        
        // Some family relationship inputs
        
        [Test]
        public void Fam_1 ()
        {
            Facts.Reset();
            Facts.Assert(p1, "FamilyRelationship", p2, "Partner by civil union");
            bool result = Fam.InCivilUnion(p1, p2).IsTrue;
            Assert.AreEqual(true, result);       
        }
        
        [Test]
        public void Fam_2 ()
        {
            Facts.Reset();
            Facts.Assert(p2, "FamilyRelationship", p1, "Partner by civil union");
            bool result = Fam.InCivilUnion(p1, p2).IsTrue;
            Assert.AreEqual(true, result);       
        }
        
        [Test]
        public void Fam_3 ()
        {
            Facts.Reset();
            Facts.Assert(p1, "FamilyRelationship", p2, "Something other than civil union...");
            bool result = Fam.InCivilUnion(p1, p2).IsTrue;
            Assert.AreEqual(false, result);       
        }
        
        [Test]
        public void Fam_4 ()
        {
            Facts.Reset();
            Facts.Assert(p2, "FamilyRelationship", p1, "Something other than civil union...");
            bool result = Fam.InCivilUnion(p1, p2).IsTrue;
            Assert.AreEqual(false, result);       
        }
        
    }
    
    #pragma warning restore 219
}
