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
using System;
using System.Collections.Generic;

namespace Hammurabi.UnitTests.CoreFcns
{
    [TestFixture]
    public class SetHigherOrder : H
    {        
        /*
             * Three assets whose values and 
             * ownership changes over time:
             * 
             * M "Owns" A        ----------
             * M "Owns" B        -----
             * M "Owns" C           -------
             * A "ValueOf"       1111111111
             * B "ValueOf"       2222222222 
             * C "ValueOf"       3333333344 
             * 
             * value M's assts = 3336644455
             */
        
        // Some legal entities
        private static Thing M = new Thing("M");
        private static Thing A = new Thing("A");
        private static Thing B = new Thing("B");
        private static Thing C = new Thing("C");
        private static Thing c = new Thing("corp");
        
        // Set up a new test
        private static void NewTest()
        {
            Tnum valA = new Tnum(1);
            
            Tnum valB = new Tnum(2);
            
            Tnum valC = new Tnum(3);
            valC.AddState(new DateTime(2011,1,14), 4);
            
            Tbool ownA = new Tbool(true);
            
            Tbool ownB = new Tbool(true);
            ownB.AddState(new DateTime(2008,1,1), false);
            
            Tbool ownC = new Tbool(false);
            ownC.AddState(new DateTime(2005,1,1), true);
            
            Facts.Clear();
            Facts.Assert(A, "ValueOf", valA);
            Facts.Assert(B, "ValueOf", valB);
            Facts.Assert(C, "ValueOf", valC);
            Facts.Assert(M, "Owns", A, ownA);
            Facts.Assert(M, "Owns", B, ownB);
            Facts.Assert(M, "Owns", C, ownC);
        }
        
        // "Pretend" functions for testing purposes
        private static Tnum AssetValue(Thing asset)
        {
            return Facts.QueryTvar<Tnum>("ValueOf", asset);
        }
        private static Tbool AssetValueLessThan4(Thing asset)    
        { 
            return AssetValue(asset) < 4;
        }
        private static Tbool AssetValueIndeterminacy(Thing asset)    
        { 
            return new Tbool(Hstate.Unstated);        // used to test unknowns
        }
        private static Tbool Owns(Thing p, Thing r)
        {
            return Facts.QueryTvar<Tbool>("Owns", p, r);
        }
        private static Tbool IsParentOf(Thing p1, Thing p2)
        {
            return Facts.QueryTvar<Tbool>("IsParentOf", p1, p2);
        }
        private static Tset TheThings()
        {
            return new Tset(A,B,C);
        }
        
        // Filter
    
        [Test]
        public void Filter1 ()
        {
            NewTest();
            Tset theAssets = TheThings().Filter(_ => Owns (M,_));
            Assert.AreEqual("Time.DawnOf A, B 1/1/2005 12:00:00 AM A, B, C 1/1/2008 12:00:00 AM A, C ", 
                            theAssets.TestOutput);        
        }
        
        [Test]
        public void Filter2 ()
        {
            Facts.Clear();
            
            Thing P1 = new Thing("P1");
            Thing P3 = new Thing("P3");
            Thing P4 = new Thing("P4");
            
            Facts.Assert(P1, "IsParentOf", P3, true);
            Facts.Assert(P1, "IsParentOf", P4, true);
            Facts.Assert(P1, "IsParentOf", P1, false);  // An absurd thing to have to assert

            Tset people = new Tset(P1,P3,P4);
            Tset result = people.Filter( _ => IsParentOf(P1,_));
            
            Assert.AreEqual("Time.DawnOf P3, P4 ", 
                            result.TestOutput);
        }
        
        [Test]
        public void Filter3 ()
        {
            Facts.Clear();

            Thing P1 = new Thing("P1");
            Thing P3 = new Thing("P3");
            Thing P4 = new Thing("P4");

            Tbool tb1 = new Tbool(false);
            tb1.AddState(new DateTime(2005,12,20),true);
            
            Tbool tb2 = new Tbool(false);
            tb2.AddState(new DateTime(2008,3,8),true);
            
            Facts.Assert(P1, "IsParentOf", P3, tb1);
            Facts.Assert(P1, "IsParentOf", P4, tb2);
            Facts.Assert(P1, "IsParentOf", P1, false);  // An absurd thing to have to assert

            Tset people = new Tset(P1,P3,P4);
            Tset result = people.Filter( _ => IsParentOf(P1,_));
            
            Assert.AreEqual("Time.DawnOf 12/20/2005 12:00:00 AM P3 3/8/2008 12:00:00 AM P3, P4 ", 
                            result.TestOutput);
        }
        
        [Test]
        public void Filter4 ()
        {
            Facts.Clear();
            
            Thing P1 = new Thing("P1");
            Thing P3 = new Thing("P3");
            Thing P4 = new Thing("P4");
            
            Facts.Assert(P1, "IsParentOf", P3, true);
            Facts.Assert(P1, "IsParentOf", P4, true);
            Facts.Assert(P4, "IsParentOf", P3, false);
            Facts.Assert(P3, "IsParentOf", P3, false);

            Tset people = new Tset(P1,P3,P4);
            Tset result = people.Filter( _ => IsParentOf(_,P3));
            Assert.AreEqual("Time.DawnOf P1 ", result.TestOutput);
        }
        
        [Test]
        public void Filter5a ()
        {
            Facts.Clear();
            Thing P1 = new Thing("P1");
            Tset people = new Tset(P1);
            Tset result = people.Filter( _ => IsParentOf(_,P1));
            Assert.AreEqual("Time.DawnOf Unstated ", result.TestOutput); // Compare with the test Filter5b below
        }

        [Test]
        public void Filter5b ()
        {
            Facts.Clear();
            Thing P1 = new Thing("P1");
            Tset people = new Tset(P1);
            Facts.Assert(P1, "IsParentOf", P1, false);
            Tset result = people.Filter( _ => IsParentOf(_,P1));
            Assert.AreEqual("Time.DawnOf ", result.TestOutput);
        }

        [Test]
        public void Filter6 ()
        {
            NewTest();
            Tset theAssets = TheThings().Filter( _ => Owns (M,_));
            Tset cheapAssets = theAssets.Filter(x => AssetValueLessThan4((Thing)x));            
            Assert.AreEqual("Time.DawnOf A, B 1/1/2005 12:00:00 AM A, B, C 1/1/2008 12:00:00 AM A, C 1/14/2011 12:00:00 AM A ", 
                            cheapAssets.TestOutput);        
        }

        [Test]
        public void Filter7_Unknown ()
        {
            NewTest();
            Tbool areAnyCheapAssets = TheThings().Exists(x => AssetValueIndeterminacy(x));
            Assert.AreEqual("Time.DawnOf Unstated ", areAnyCheapAssets.TestOutput);
        }

        // Tset.Count

        [Test]
        public void CountUnknown1 ()
        {
            Thing P1 = new Thing("P1");
            Tset tsv = new Tset(Hstate.Stub);
            tsv.AddState(Date(2000,01,01),P1);
            tsv.AddState(Date(2001,01,01),Hstate.Uncertain);
            Assert.AreEqual("Time.DawnOf Stub 1/1/2000 12:00:00 AM 1 1/1/2001 12:00:00 AM Uncertain ", tsv.Count.TestOutput);
        }

        // EntitiesAsOf

        [Test]
        public void EntitiesAsOf1 ()
        {
            Thing P1 = new Thing("P1");
            Tset tsv = new Tset(Hstate.Stub);
            tsv.AddState(Date(2000,01,01),P1);
            tsv.AddState(Date(2001,01,01),Hstate.Uncertain);
            Assert.AreEqual(Hstate.Stub, tsv.EntitiesAsOf(Date(1999,01,01)).Val);
        }

        [Test]
        public void EntitiesAsOf2 ()
        {
            Thing P1 = new Thing("P1");
            Tset tsv = new Tset(Hstate.Stub);
            tsv.AddState(Date(2000,01,01),P1);
            tsv.AddState(Date(2001,01,01),Hstate.Uncertain);
            Assert.AreEqual(Hstate.Uncertain, tsv.EntitiesAsOf(Date(2002,02,01)).Val);
        }

        // AllKnownPeople
        
        [Test]
        public void AllKnownPeople1 ()
        {
            Facts.Clear();
            
            Thing P1 = new Thing("P1");
            Thing P3 = new Thing("P3");
            Thing P4 = new Thing("P4");

            Facts.Assert(P1, "IsParentOf", P3, new Tbool(true));
            Facts.Assert(P1, "IsParentOf", P4, new Tbool(true));

            Assert.AreEqual("Time.DawnOf P1, P3, P4 ", 
                            Facts.AllKnownPeople().TestOutput);
        }
        
        [Test]
        public void AllKnownPeople2 ()
        {
            Facts.Clear();
            Tset result = Facts.AllKnownPeople();
            Assert.AreEqual("Time.DawnOf ", result.TestOutput);
        }
        
        [Test]
        public void AllKnownPeople3 ()
        {
            Facts.Clear();
            
            Thing P1 = new Thing("P1");
            Thing P2 = new Thing("P2");
            Thing P3 = new Thing("P3");

            Facts.Assert(P1, "IsMarriedTo", P2, true);
            Facts.Assert(P3, "IsPermanentlyAndTotallyDisabled", new Tbool(false));
            Tset result = Facts.AllKnownPeople();
            Assert.AreEqual("Time.DawnOf P1, P2, P3 ", result.TestOutput);
        }
        
        [Test]
        public void AllKnownPeople4 ()
        {
            Facts.Clear();
            Thing P1 = new Thing("P1");
            Facts.Assert(P1,"Gender", "Male");
            Tset result = Facts.AllKnownPeople();
            Assert.AreEqual("Time.DawnOf P1 ", result.TestOutput);
        }
        
        // Sum
    
        [Test]
        public void Set_Sum1 ()
        {
            NewTest();
            Tset theAssets = TheThings().Filter( _ => Owns (M,_));
            Tnum sumOfAssets = theAssets.Sum(x => AssetValue((Thing)x));
            Assert.AreEqual("Time.DawnOf 3 1/1/2005 12:00:00 AM 6 1/1/2008 12:00:00 AM 4 1/14/2011 12:00:00 AM 5 ", 
                            sumOfAssets.TestOutput);        
        }
        
        [Test]
        public void Set_Sum2 ()
        {
            NewTest();
            Tset theAssets = TheThings().Filter( _ => Owns (M,_));
            Tnum sumOfAssets = theAssets.Sum(x => AssetValue((Thing)x));
            Assert.AreEqual("Time.DawnOf 3 1/1/2005 12:00:00 AM 6 1/1/2008 12:00:00 AM 4 1/14/2011 12:00:00 AM 5 ", 
                            sumOfAssets.TestOutput);        
        }
        
        [Test]
        public void Set_Sum_Unknown_1 ()
        {
            NewTest();
            Tset theAssets = new Tset(Hstate.Unstated);
            Tnum sumOfAssets = theAssets.Sum(x => AssetValue((Thing)x));
            Assert.AreEqual("Time.DawnOf Unstated ", sumOfAssets.TestOutput);        
        }
        
        [Test]
        public void Set_Sum_Unknown_2 ()
        {
            NewTest();
            Tset theAssets = TheThings().Filter( _ => Owns (M,_));
            Tnum sumOfAssets = theAssets.Sum(x => NullFcn(x) );
            Assert.AreEqual("Time.DawnOf Stub ", sumOfAssets.TestOutput);    
        }
        private static Tnum NullFcn(Thing p)
        {
            return new Tnum(Hstate.Stub);
        }
        
        // Exists
    
        [Test]
        public void Exists_1 ()
        {
            NewTest();
            Tset theAssets = TheThings().Filter( _ => Owns (M,_));
            Tbool areAnyCheapAssets = theAssets.Exists(x => AssetValueLessThan4(x));
            Assert.AreEqual("Time.DawnOf True ", areAnyCheapAssets.TestOutput);        
        }
        
        [Test]
        public void Exists_2_Unknown ()
        {
            NewTest();
            Tset theAssets = new Tset(Hstate.Unstated);
            Tbool areAnyCheapAssets = theAssets.Exists(x => AssetValueLessThan4(x));
            Assert.AreEqual("Time.DawnOf Unstated ", areAnyCheapAssets.TestOutput);        
        }
        
        [Test]
        public void Exists_3_Unknown ()
        {
            NewTest();
            Tset theAssets = TheThings().Filter( _ => Owns (M,_));
            Tbool areAnyCheapAssets = theAssets.Exists(x => AssetValueIndeterminacy(x));
            Assert.AreEqual("Time.DawnOf Unstated ", areAnyCheapAssets.TestOutput);
        }
        
        // ForAll
    
        [Test]
        public void ForAll1 ()
        {
            NewTest();
            Tset theAssets = TheThings().Filter( _ => Owns (M,_));
            Tbool allAssetsAreCheap = theAssets.ForAll( x => AssetValueLessThan4((Thing)x));
            Assert.AreEqual("Time.DawnOf True 1/14/2011 12:00:00 AM False ", allAssetsAreCheap.TestOutput);        
        }
  
        // Functions compiled from Akkadian to C#
        
        [Test]
        public void Compiled1a ()
        {
            Facts.Clear();
            Tbool result = AllAreMale(new Tset(new List<Thing>()));
            Assert.AreEqual("Time.DawnOf True ", result.TestOutput);        
        }
        
        [Test]
        public void Compiled1b ()
        {
            Facts.Clear();
            Thing p = new Thing("p");
            Facts.Assert(p,"Gender","Male");
            Tbool result = AllAreMale(new Tset(p));
            Assert.AreEqual("Time.DawnOf True ", result.TestOutput);        
        }
        
        [Test]
        public void Compiled1c ()
        {
            Facts.Clear();
            Thing p1 = new Thing("p1");
            Thing p2 = new Thing("p2");
            Facts.Assert(p1,"Gender","Male");
            Facts.Assert(p2,"Gender","Male");
            Tbool result = AllAreMale(new Tset(p1,p2));
            Assert.AreEqual("Time.DawnOf True ", result.TestOutput);        
        }
        
        [Test]
        public void Compiled1d ()
        {
            Facts.Clear();
            Thing p1 = new Thing("p1");
            Thing p2 = new Thing("p2");
            Facts.Assert(p1,"Gender","Male");
            Facts.Assert(p2,"Gender","Female");
            Tbool result = AllAreMale(new Tset(p1,p2));
            Assert.AreEqual("Time.DawnOf False ", result.TestOutput);        
        }
        
        private static Tbool AllAreMale(Tset theSet)
        {
            return theSet.ForAll( _ => IsMale(_));
        }
        
        // ForAll using a filter method with two parameters
        
        [Test]
        public void Compiled2a ()
        {
            Facts.Clear();
            Thing c = new Thing("corp");
            Tbool result = SomeoneWorksAt(c, new Tset(new List<Thing>()));
            Assert.AreEqual("Time.DawnOf False ", result.TestOutput);        
        }
        
        [Test]
        public void Compiled2b ()
        {
            // What conclusion to draw when no relationship is expressed? "Unstated"
            Facts.Clear();
            Thing c = new Thing("corp");
            Thing p = new Thing("p");
            Tbool result = SomeoneWorksAt(c, new Tset(p));
            Assert.AreEqual("Time.DawnOf Unstated ", result.TestOutput);        
        }
        
        [Test]
        public void Compiled2c ()
        {
            Facts.Clear();
            Thing c = new Thing("corp");
            Thing p = new Thing("p");
            Facts.Assert(p, "Econ.EmploymentRelationship", c, "Employee");
            Tbool result = SomeoneWorksAt(c, new Tset(p));
            Assert.AreEqual("Time.DawnOf True ", result.TestOutput);        
        }
        
        [Test]
        public void Compiled2d ()
        {
            Facts.Clear();
            Thing c = new Thing("corp");
            Thing p = new Thing("p");
            Facts.Assert(p, "Econ.EmploymentRelationship", c, "Intern");
            Tbool result = SomeoneWorksAt(c, new Tset(p));
            Assert.AreEqual("Time.DawnOf False ", result.TestOutput);        
        }

        [Test]
        public void Compiled2g ()
        {
            Facts.Clear();
            Thing p = new Thing("p");
            Thing c = new Thing("c");
            Thing c2 = new Thing("c2");
            Facts.Assert(p, "Econ.EmploymentRelationship", c2, "Employee");
            Tbool result = Econ.IsEmployedBy(p,c2);
            Assert.AreEqual("Time.DawnOf True ", result.TestOutput);        
        }
        
        [Test]
        public void Compiled2h ()
        {
            Facts.Clear();
            Thing p = new Thing("person");
            Thing c = new Thing("c");
            Thing c2 = new Thing("c2");
            Facts.Assert(p, "Econ.EmploymentRelationship", c2, "Employee");
            Facts.Assert(p, "Econ.EmploymentRelationship", c, "Intern");
            Tbool result = SomeoneWorksAt(c, new Tset(p));  
            Assert.AreEqual("Time.DawnOf False ", result.TestOutput);        
        }
        
        private static Tbool SomeoneWorksAt(Thing c, Tset theSet)
        {
            return theSet.Exists( _ => Econ.IsEmployedBy(_,c));
        }
        
    }
}
