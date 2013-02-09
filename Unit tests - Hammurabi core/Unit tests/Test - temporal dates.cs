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
    [TestFixture]
    public class TemporalDate : H
    {
        // .Lean
        
        [Test]
        public void Tdate_Lean_1 ()
        {
            Tdate td = new Tdate();
            td.AddState(Time.DawnOf, Date(2011,01,01));
            td.AddState(Time.DawnOf.AddYears(2), Date(2011,01,01));
            Assert.AreEqual("Time.DawnOf 1/1/2011 12:00:00 AM ", td.Lean.TestOutput);        
        }
        
        // .AsOf
        
        [Test]
        public void Tdate_AsOf_1 ()
        {
            Tdate td = new Tdate();
            td.AddState(Time.DawnOf, Date(2011,01,01));
            td.AddState(Time.DawnOf.AddYears(2), Date(2012,01,01));
            Assert.AreEqual("Time.DawnOf 1/1/2012 12:00:00 AM ", td.AsOf(Time.DawnOf.AddYears(3)).TestOutput);        
        }
        
        [Test]
        public void Tdate_AsOf_2 ()
        {
            Tdate td = new Tdate();
            td.AddState(Time.DawnOf, Date(2011,01,01));
            td.AddState(Time.DawnOf.AddYears(2), Date(2012,01,01));
            Assert.AreEqual("Time.DawnOf 1/1/2011 12:00:00 AM ", td.AsOf(Time.DawnOf.AddYears(1)).TestOutput);        
        }
                
        // Equals
        
        [Test]
        public void Tdate_Equals_1 ()
        {
            Tdate td1 = new Tdate(2010,5,13);

            Tdate td2 = new Tdate();
            td2.AddState(Time.DawnOf, Date(2011,01,01));
            td2.AddState(Date(2000,1,1), Date(2010,5,13));

            Tbool result = td1 == td2;
            Assert.AreEqual("Time.DawnOf False 1/1/2000 12:00:00 AM True ", result.TestOutput);        
        }
        
        [Test]
        public void Tdate_Equals_2 ()
        {
            Tdate td1 = new Tdate(2010,5,13);
            Tdate td2 = new Tdate();
            td2.AddState(Time.DawnOf, Date(2011,01,01));
            td2.AddState(Date(2000,1,1), Date(2010,5,13));
            Tbool result = td1 != td2;
            Assert.AreEqual("Time.DawnOf True 1/1/2000 12:00:00 AM False ", result.TestOutput);        
        }
        
        // IsBefore / IsAfter
        
        [Test]
        public void Tdate_IsAfter_1 ()
        {
            Tdate td1 = new Tdate(2010,1,1);
            Tdate td2 = new Tdate();
            td2.AddState(Time.DawnOf, Date(2009,1,1));
            td2.AddState(Date(2000,1,1), Date(2011,1,1));
            Tbool result = td1 > td2;
            Assert.AreEqual("Time.DawnOf True 1/1/2000 12:00:00 AM False ", result.TestOutput);        
        }
        
        [Test]
        public void Tdate_IsBefore_1 ()
        {
            Tdate td1 = new Tdate(2010,1,1);
            Tdate td2 = new Tdate();
            td2.AddState(Time.DawnOf, Date(2009,1,1));
            td2.AddState(Date(2000,1,1), Date(2011,1,1));
            Tbool result = td1 < td2;
            Assert.AreEqual("Time.DawnOf False 1/1/2000 12:00:00 AM True ", result.TestOutput);        
        }
        
        [Test]
        public void Tdate_IsAtOrAfter_1 ()
        {
            Tdate td1 = new Tdate(2008,1,1);
            Tdate td2 = new Tdate();
            td2.AddState(Time.DawnOf, Date(2009,1,1));
            td2.AddState(Date(2000,1,1), Date(2008,1,1));
            Tbool result = td1 >= td2;
            Assert.AreEqual("Time.DawnOf False 1/1/2000 12:00:00 AM True ", result.TestOutput);        
        }
        
        [Test]
        public void Tdate_IsAtOrBefore_1 ()
        {
            Tdate td1 = new Tdate(2000,1,1);
            Tdate td2 = new Tdate();
            td2.AddState(Time.DawnOf, Date(1999,1,1));
            td2.AddState(Date(2000,1,1), Date(2000,1,1));
            td2.AddState(Date(2001,1,1), Date(2008,1,1));
            Tbool result = td2 <= td1;
            Assert.AreEqual("Time.DawnOf True 1/1/2001 12:00:00 AM False ", result.TestOutput);        
        }
        
        // .AddDays
        
        [Test]
        public void Tdate_AddDays_1 ()
        {
            Tdate td = new Tdate(2000,1,1);
            Tdate result = td.AddDays(3);
            Assert.AreEqual("Time.DawnOf 1/4/2000 12:00:00 AM ", result.TestOutput);        
        }
        
        [Test]
        public void Tdate_AddDays_2 ()
        {
            Tdate td = new Tdate(2000,1,1);
            Tdate result = td.AddDays(-3);
            Assert.AreEqual("Time.DawnOf 12/29/1999 12:00:00 AM ", result.TestOutput);        
        }
        
        // .AddMonths
        
        [Test]
        public void Tdate_AddMonths_1 ()
        {
            Tdate td = new Tdate(2000,1,1);
            Tdate result = td.AddMonths(3);
            Assert.AreEqual("Time.DawnOf 4/1/2000 12:00:00 AM ", result.TestOutput);        
        }
        
        [Test]
        public void Tdate_AddMonths_2 ()
        {
            Tdate td = new Tdate(2000,1,1);
            Tdate result = td.AddMonths(-3);
            Assert.AreEqual("Time.DawnOf 10/1/1999 12:00:00 AM ", result.TestOutput);        
        }
        
        // .AddYears
        
        [Test]
        public void Tdate_AddYears_1 ()
        {
            Tdate td = new Tdate(2000,1,1);
            Tdate result = td.AddYears(3);
            Assert.AreEqual("Time.DawnOf 1/1/2003 12:00:00 AM ", result.TestOutput);        
        }
        
        [Test]
        public void Tdate_AddYears_2 ()
        {
            Tdate td = new Tdate(2000,1,1);
            Tdate result = td.AddYears(-3);
            Assert.AreEqual("Time.DawnOf 1/1/1997 12:00:00 AM ", result.TestOutput);        
        }

        // DayDiff
        // When the earlier date is put after the prior date, DayDiff returns a negative number.
        
        [Test]
        public void Tdate_DayDiff_2 ()
        {
            Tdate td1 = new Tdate(2010,1,1);
            Tdate td2 = new Tdate(2000,1,1);
            Tnum result = Tdate.DayDiff(td1,td2);
            Assert.AreEqual("Time.DawnOf -3653 ", result.TestOutput);        
        }

        // MakeTnum
        
        [Test]
        public void Tvar_load_1()
        {
            Tnum t = Tnum.MakeTnum(Time.DawnOf, null,
                              Date(1961,01,01), 5.14,
                              Date(2001,03,04), 7.00);
            
            decimal? result = t.AsOf(Date(1971,1,1)).ToDecimal;
            Assert.AreEqual(5.14, result);        
        } 
        
        [Test]
        public void Tvar_load_2()
        {
            Tnum t = Tnum.MakeTnum(
            Time.DawnOf,    null,
            Date(1978,01,01), 2.65,
            Date(1979,01,01), 2.90,
            Date(1980,01,01), 3.10,
            Date(1981,01,01), 3.35,
            Date(1990,04,01), 3.80,
            Date(1991,04,01), 4.25,
            Date(1996,10,01), 4.75,
            Date(1997,09,01), 5.15,
            Date(2007,07,24), 5.85,
            Date(2008,07,24), 6.55,
            Date(2009,07,24), 7.25,
            null);

            decimal? result = t.AsOf(Date(1991,1,1)).ToDecimal;
            Assert.AreEqual(3.80, result);        
        } 

        [Test]
        public void Tvar_load_3()
        {
            Tnum t = Tnum.MakeTnum(Time.DawnOf, 200);
            Assert.AreEqual(200, t.AsOf(Date(2001,03,03)).Out);        
        }
    }
}   