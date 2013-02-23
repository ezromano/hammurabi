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
using Hammurabi;
using NUnit.Framework;

namespace Hammurabi.UnitTests.CoreFcns
{
    [TestFixture]
    public class Math : H
    {
        // ADDITION
        
        [Test]
        public void Test1 ()
        {
            Tnum res = new Tnum(8) + new Tnum(9);
            Assert.AreEqual("Time.DawnOf 17 ", res.TestOutput);        
        }

        [Test]
        public void Test2 ()
        {
            Tnum res = new Tnum(8) + new Tnum(-9);
            Assert.AreEqual("Time.DawnOf -1 ", res.TestOutput);        
        }
        
        [Test]
        public void Test3 ()
        {
            Tnum res = new Tnum(8) + -9;
            Assert.AreEqual("Time.DawnOf -1 ", res.TestOutput);        
        }
        
        [Test]
        public void Test4 ()
        {
            Tnum res = new Tnum(8.001) + -9;
            Assert.AreEqual("Time.DawnOf -0.999 ", res.TestOutput);        
        }
        
        [Test]
        public void Test5 ()
        {
            Tnum res = new Tnum(8.001) + -9 + 3;
            Assert.AreEqual("Time.DawnOf 2.001 ", res.TestOutput);        
        }
        
        [Test]
        public void Test6 ()
        {
            Tnum res = new Tnum(8) + new Tnum(9) + new Tnum(10);
            Assert.AreEqual("Time.DawnOf 27 ", res.TestOutput);        
        }
        
        [Test]
        public void Test7 ()
        {
            Tnum res = new Tnum(8) + new Tnum(0.10) + new Tnum(10);
            Assert.AreEqual("Time.DawnOf 18.1 ", res.TestOutput);        
        }
        
        // SUBTRACTION
        
        [Test]
        public void Test11 ()
        {
            Tnum res = new Tnum(8) - new Tnum(9);
            Assert.AreEqual("Time.DawnOf -1 ", res.TestOutput);        
        }

        [Test]
        public void Test12 ()
        {
            Tnum res = new Tnum(8) - new Tnum(-9);
            Assert.AreEqual("Time.DawnOf 17 ", res.TestOutput);        
        }
        
        [Test]
        public void Test13 ()
        {
            Tnum res = new Tnum(8) - -9;
            Assert.AreEqual("Time.DawnOf 17 ", res.TestOutput);        
        }
        
        [Test]
        public void Test14 ()
        {
            Tnum res = new Tnum(8.001) - -9;
            Assert.AreEqual("Time.DawnOf 17.001 ", res.TestOutput);        
        }
        
        [Test]
        public void Test15 ()
        {
            Tnum res = new Tnum(8) - -9 - 3;
            Assert.AreEqual("Time.DawnOf 14 ", res.TestOutput);        
        }
        
        [Test]
        public void Test16 ()
        {
            Tnum res = new Tnum(8) - new Tnum(9) - new Tnum(10);
            Assert.AreEqual("Time.DawnOf -11 ", res.TestOutput);        
        }
        
        // MULTIPLICATION
        
        [Test]
        public void Test21 ()
        {
            Tnum res = new Tnum(8) * new Tnum(9);
            Assert.AreEqual("Time.DawnOf 72 ", res.TestOutput);        
        }

        [Test]
        public void Test22 ()
        {
            Tnum res = new Tnum(8) * new Tnum(-9);
            Assert.AreEqual("Time.DawnOf -72 ", res.TestOutput);        
        }
        
        [Test]
        public void Test23 ()
        {
            Tnum res = new Tnum(8) * -9;
            Assert.AreEqual("Time.DawnOf -72 ", res.TestOutput);        
        }
        
        [Test]
        public void Test24 ()
        {
            Tnum res = new Tnum(8.001) * 9;
            Assert.AreEqual("Time.DawnOf 72.009 ", res.TestOutput);        
        }
        
        [Test]
        public void Test25 ()
        {
            Tnum res = new Tnum(8) * -9 * 3;
            Assert.AreEqual("Time.DawnOf -216 ", res.TestOutput);        
        }
        
        [Test]
        public void Test26 ()
        {
            Tnum res = new Tnum(8) * new Tnum(9) * new Tnum(10);
            Assert.AreEqual("Time.DawnOf 720 ", res.TestOutput);        
        }
        
        [Test]
        public void Test27 ()
        {
            Tnum res = new Tnum(8) * new Tnum(0.10) * new Tnum(10);
            Assert.AreEqual("Time.DawnOf 8.0 ", res.TestOutput);        
        }
        
        // DIVISION
        
        [Test]
        public void Test31 ()
        {
            Tnum res = new Tnum(12) / new Tnum(3);
            Assert.AreEqual("Time.DawnOf 4 ", res.TestOutput);        
        }

        [Test]
        public void Test32 ()
        {
            Tnum res = new Tnum(12) / new Tnum(-3);
            Assert.AreEqual("Time.DawnOf -4 ", res.TestOutput);        
        }
        
        [Test]
        public void Test33 ()
        {
            Tnum res = new Tnum(12) / -2;
            Assert.AreEqual("Time.DawnOf -6 ", res.TestOutput);        
        }
        
        [Test]
        public void Test34 ()
        {
            Tnum res = new Tnum(8.001) / 9;
            Assert.AreEqual("Time.DawnOf 0.889 ", res.TestOutput);        
        }
        
        [Test]
        public void Test35_Div_by_zero_issue ()
        {
            Tnum res = new Tnum(8) / 0;
            Assert.AreEqual("Time.DawnOf Uncertain ", res.TestOutput);        
        }
        
        [Test]
        public void Test36 ()
        {
            Tnum res = 8 / new Tnum(2);
            Assert.AreEqual("Time.DawnOf 4 ", res.TestOutput);        
        }
        
        [Test]
        public void Test39 ()
        {
            Tnum res = -0.10 / new Tnum(2);
            Assert.AreEqual("Time.DawnOf -0.05 ", res.TestOutput);        
        }

        [Test]
        public void Test39a ()
        {
            Tnum res = new Tnum(0) / 7;
            Assert.AreEqual("Time.DawnOf 0 ", res.TestOutput);        
        }

        [Test]
        public void Test39b ()
        {
            Tnum t = new Tnum(0);
            t.AddState(new DateTime(2000,1,1), 7);

            Tnum res = t / 7;
            Assert.AreEqual("Time.DawnOf 0 1/1/2000 12:00:00 AM 1 ", res.TestOutput);        
        }

        // MODULO
        
        [Test]
        public void Test40 ()
        {
            Tnum res = new Tnum(12) % new Tnum(2);
            Assert.AreEqual("Time.DawnOf 0 ", res.TestOutput);        
        }
        
        [Test]
        public void Test41 ()
        {
            Tnum res = new Tnum(13) % new Tnum(2);
            Assert.AreEqual("Time.DawnOf 1 ", res.TestOutput);        
        }
        
        [Test]
        public void Test45 ()
        {
            Tnum res = new Tnum(12.01) % 2;
            Assert.AreEqual("Time.DawnOf 0.01 ", res.TestOutput);        
        }
        
        // MAXIMUM
        
        [Test]
        public void Test50 ()
        {
            Tnum res = Max(new Tnum(12), new Tnum(2), new Tnum(99));
            Assert.AreEqual("Time.DawnOf 99 ", res.TestOutput);        
        }
        
        [Test]
        public void Test51 ()
        {
            Tnum res = Max(new Tnum(12), new Tnum(2), new Tnum(-99));
            Assert.AreEqual("Time.DawnOf 12 ", res.TestOutput);        
        }
        
        [Test]
        public void Test52 ()
        {
            Tnum res = Max(new Tnum(12), new Tnum(0), new Tnum(-99));
            Assert.AreEqual("Time.DawnOf 12 ", res.TestOutput);        
        }
        
        [Test]
        public void Test53 ()
        {
            Tnum res = Max(12, 0, -99);
            Assert.AreEqual("Time.DawnOf 12 ", res.TestOutput);        
        }
        
        [Test]
        public void Test60 ()
        {
            Tnum res = Max(new Tnum(12), new Tnum(0.01), new Tnum(-99));
            Assert.AreEqual("Time.DawnOf 12 ", res.TestOutput);        
        }
        
        // MINIMUM
        
        [Test]
        public void Test70 ()
        {
            Tnum res = Min(new Tnum(12), new Tnum(2), new Tnum(99));
            Assert.AreEqual("Time.DawnOf 2 ", res.TestOutput);        
        }
        
        [Test]
        public void Test71 ()
        {
            Tnum res = Min(new Tnum(12), new Tnum(2), new Tnum(-99));
            Assert.AreEqual("Time.DawnOf -99 ", res.TestOutput);        
        }
        
        [Test]
        public void Test72 ()
        {
            Tnum res = Min(new Tnum(12), new Tnum(0), new Tnum(-99));
            Assert.AreEqual("Time.DawnOf -99 ", res.TestOutput);        
        }
        
        [Test]
        public void Test73 ()
        {
            Tnum res = Min(12, 0, -99);
            Assert.AreEqual("Time.DawnOf -99 ", res.TestOutput);        
        }
        
        [Test]
        public void Test80 ()
        {
            Tnum res = Min(new Tnum(12), new Tnum(0.01), new Tnum(-99));
            Assert.AreEqual("Time.DawnOf -99 ", res.TestOutput);        
        }
        
        // ABSOLUTE VALUE
        
        [Test]
        public void Test81 ()
        {
            Assert.AreEqual("Time.DawnOf 88 ", new Tnum(88).Abs.TestOutput);        
        }
        
        [Test]
        public void Test82 ()
        {
            Assert.AreEqual("Time.DawnOf 88 ", new Tnum(-88).Abs.TestOutput);        
        }

        // Temporal
        
        [Test]
        public void TemporalMath1 ()
        {
            Tnum x = new Tnum(10);
            x.AddState(new DateTime(2000,1,1), 1);
            Assert.AreEqual("Time.DawnOf 11 1/1/2000 12:00:00 AM 2 ", (x+1).TestOutput );    
        }
        
        [Test]
        public void TemporalMath2 ()
        {
            Tnum x = new Tnum(10);
            x.AddState(new DateTime(2000,1,1), 1);
            Assert.AreEqual("Time.DawnOf 9 1/1/2000 12:00:00 AM 0 ", (x-1).TestOutput );    
        }
        
    }
}
