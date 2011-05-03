// Copyright (c) 2011 The Hammurabi Project
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
    
    /*
     * These test cases verify that unknown facts are being added to the
     * Facts.Unknowns list in the desired order.  The desired order is one
     * that mimics a backwards-chaining expert system.  Specifically, facts
     * are added:
     *  
     *      1. In left-to-right order as they are written in an expression, and
     *  
     *      2. Such that facts on irrelevant branches are omitted.
     * 
     * These criteria will enable the unknown facts list to suggest the next
     * question that should be asked in a user-facing interview.  Questions 
     * will occur in a sensible order and with unnecessary questions left out.
     * 
     * Currently, criteria (1) is satisfied but (2) is not (because arguments
     * to a function are evaluated before the funtion itself).
     */
     
    [TestFixture]
    public class UnknownFacts : H
    {
        // Some people
        private static Person p1 = new Person("p1");
        private static Person p2 = new Person("p2");
        
        // Some functions that ask for input facts
        private static Tbool A()
        {
            return Facts.InputTbool(p1, "A", p2);
        }
        
        private static Tbool B()
        {
            return Facts.InputTbool(p1, "B", p2);
        }
        
        private static Tbool C()
        {
            return Facts.InputTbool(p1, "C", p2);
        }
        
        private static Tbool D()
        {
            return Facts.InputTbool(p1, "D", p2);
        }
        
        private static Tnum X()
        {
            return Facts.InputTnum(p1, "X", p2);
        } 
        
        private static Tnum Y()
        {
            return Facts.InputTnum(p1, "Y", p2);
        } 
        
        // And()
        
        [Test]
        public void FactOrder1a ()
        {
            // Tell system to list unknown facts when a rule is invoked
            Facts.GetUnknowns = true;
            
            // Clear the lists of known and unknown facts
            Facts.Clear();
            Facts.Unknowns.Clear();
            
            // Invoke a rule
            Tbool theRule = A() & B() & C();
            
            // Check the order in which the unknown facts are added to the list
            Assert.AreEqual("A B C", Facts.ShowUnknownTest());           
        }
        
        [Test]
        public void FactOrder1b ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "A", p2);
            Tbool theRule = A() & B() & C();
            Assert.AreEqual("B C", Facts.ShowUnknownTest());           
        }
        
        [Test]
        public void FactOrder1c ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "B", p2);
            Tbool theRule = A() & B() & C();
            Assert.AreEqual("A C", Facts.ShowUnknownTest());           
        }
        
        [Test]
        public void FactOrder1d ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "C", p2);
            Tbool theRule = A() & B() & C();
            Assert.AreEqual("A B", Facts.ShowUnknownTest());           
        }
        
        [Test]
        public void FactOrder1e ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "A", p2, false);
            Tbool theRule = A() & B() & C();
            Assert.AreEqual("", Facts.ShowUnknownTest());   // currently returns "B C" - not AND short-circuiting        
        }
        
        [Test]
        public void FactOrder1f ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "B", p2, false);
            Tbool theRule = A() & B() & C();
            Assert.AreEqual("", Facts.ShowUnknownTest());   // currently returns "A C" - not AND short-circuiting        
        }
        
        [Test]
        public void FactOrder1g ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "C", p2, false);
            Tbool theRule = A() & B() & C();
            Assert.AreEqual("", Facts.ShowUnknownTest());   // currently returns "A B" - not AND short-circuiting        
        }
        
        // IfThen()
        
        [Test]
        public void FactOrder2a ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Tbool theRule = IfThen(A(), B());
            Assert.AreEqual("A B", Facts.ShowUnknownTest());         
        }
        
        [Test]
        public void FactOrder2b ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "A", p2, false);
            Tbool theRule = IfThen(A(), B());
            Assert.AreEqual("", Facts.ShowUnknownTest());    // currently returns "B" - not OR short-circuiting
        }
        
        [Test]
        public void FactOrder2c ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "B", p2, false);
            Tbool theRule = IfThen(A(), B());
            Assert.AreEqual("", Facts.ShowUnknownTest());    // currently returns "A" - not OR short-circuiting
        }
        
        // Not()
        
        [Test]
        public void FactOrder3a ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Tbool theRule = A() & !B();
            Assert.AreEqual("A B", Facts.ShowUnknownTest());         
        }
        
        [Test]
        public void FactOrder3b ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "B", p2, false);
            Tbool theRule = A() & !B();
            Assert.AreEqual("A", Facts.ShowUnknownTest());         
        }
        
        [Test]
        public void FactOrder3c ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "B", p2);
            Tbool theRule = A() & !B();
            Assert.AreEqual("", Facts.ShowUnknownTest());    // currently returns "A" - not AND short-circuiting      
        }
        
        // Nested And() and Or()
        
        [Test]
        public void FactOrder4a ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Tbool theRule = A() & (B() | C());
            Assert.AreEqual("A B C", Facts.ShowUnknownTest());      
        }
        
        [Test]
        public void FactOrder4b ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Tbool theRule = (A() & B()) | C();
            Assert.AreEqual("A B C", Facts.ShowUnknownTest());    
        }
        
        // Switch()
        
        [Test]
        public void FactOrder5a ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Tnum theRule = Switch(A(),X(),
                                  B(),Y());
            Assert.AreEqual("A B", Facts.ShowUnknownTest());      // currently returns "A X B Y" - should not investigate Tnums unless relevant
        }
        
        [Test]
        public void FactOrder5b ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "A", p2);
            Tnum theRule = Switch(A(),X(),
                                  B(),Y());
            Assert.AreEqual("A X", Facts.ShowUnknownTest());      // currently hits "index out of range" error
        }
        
        [Test]
        public void FactOrder5c ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "A", p2, false);
            Tnum theRule = Switch(A(),X(),
                                  B(),Y());
            Assert.AreEqual("B", Facts.ShowUnknownTest());      // currently hits "index out of range" error
        }
        
        [Test]
        public void FactOrder5d ()
        {
            Facts.GetUnknowns = true;
            Facts.Clear();
            Facts.Unknowns.Clear();
            Facts.Assert(p1, "A", p2, false);
            Facts.Assert(p1, "B", p2);
            Tnum theRule = Switch(A(),X(),
                                  B(),Y());
            Assert.AreEqual("Y", Facts.ShowUnknownTest());      // currently hits "index out of range" error
        }
        
    }
    
    #pragma warning restore 219
}