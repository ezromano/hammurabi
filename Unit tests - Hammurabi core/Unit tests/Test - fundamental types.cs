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

namespace Hammurabi.UnitTests.CoreFcns
{
    [TestFixture]
    public class FundamentalTypes : H
    {
        // .Lean
        
        [Test]
        public void FT_Lean_1 ()
        {
            Tbool t = new Tbool(true);
            t.AddState(Time.DawnOf.AddYears(5), true);
            Tbool res = t.Lean;
            Assert.AreEqual(true, res.Out);        
        }
        
        // .AsOf
        
        [Test]
        public void FT_AsOf_1 ()
        {
            Tbool t = new Tbool(true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = t.AsOf(Time.DawnOf.AddYears(2));
            Assert.AreEqual(true, res.Out);        
        }
        
        [Test]
        public void FT_AsOf_2 ()
        {
            Tbool t = new Tbool(true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = t.AsOf(Time.DawnOf.AddYears(12));
            Assert.AreEqual(false, res.Out);        
        }
        
        [Test]
        public void FT_AsOf_3 ()
        {
            Tbool t = new Tbool(true);
            Tbool res = t.AsOf(Time.DawnOf.AddYears(12));
            Assert.AreEqual(true, res.Out);        
        }
        
        [Test]
        public void FT_AsOf_4 ()
        {
            Tnum t = new Tnum(4);
            t.AddState(Time.DawnOf.AddYears(5), 44);
            Tnum res = t.AsOf(Time.DawnOf.AddYears(2));
            Assert.AreEqual(4, res.Out);        
        }
        
        [Test]
        public void FT_AsOf_5 ()
        {
            Tnum t = new Tnum(4);
            t.AddState(Time.DawnOf.AddYears(5), 44);
            Tnum res = t.AsOf(Time.DawnOf.AddYears(12));
            Assert.AreEqual(44, res.Out);        
        }
        
        [Test]
        public void FT_AsOf_6 ()
        {
            Tstr t = new Tstr("ham");
            t.AddState(Time.DawnOf.AddYears(5), "sam");
            Tstr res = t.AsOf(Time.DawnOf.AddYears(12));
            Assert.AreEqual("sam", res.Out);        
        }
        
        [Test]
        public void FT_AsOf_7 ()
        {
            Tstr t = new Tstr("ham");
            t.AddState(Time.DawnOf.AddYears(5), "sam");
            Tstr res = t.AsOf(Time.DawnOf.AddYears(2));
            Assert.AreEqual("ham", res.Out);        
        }
        
        [Test]
        public void FT_AsOf_8 ()
        {
            Tbool res = new Tbool(Hstate.Uncertain).AsOf(Time.DawnOf.AddYears(2));
            Assert.AreEqual("Uncertain", res.Out);       
        }

        [Test]
        public void FT_AsOf_9 ()
        {
            Tbool t = new Tbool(true);
            t.AddState(new DateTime(2000,1,1), false);

            Tdate time = new Tdate(1999,1,1);

            Assert.AreEqual(true, t.AsOf(time).Out);        
        }

        [Test]
        public void FT_AsOf_10 ()
        {
            Tbool t = new Tbool(true);
            t.AddState(Time.DawnOf.AddYears(5), false);

            Tdate time = new Tdate(Hstate.Stub);

            Assert.AreEqual("Stub", t.AsOf(time).Out);        
        }

        [Test]
        public void FT_AsOf_11 ()
        {
            Tbool t = new Tbool(Hstate.Stub);
            Tdate time = new Tdate(Hstate.Unstated);
            Assert.AreEqual("Stub", t.AsOf(time).Out);        
        }

        [Test]
        public void FT_AsOf_12 ()
        {
            // Tdate varies, but base Tvar is eternal, so .AsOf should return that eternal value
            Tbool t = new Tbool(true);
            Tdate time = new Tdate(Date(2000,1,1));
            time.AddState(Date(2010,1,1),Date(2010,1,1));
            Assert.AreEqual(true, t.AsOf(time).Out);        
        }

        [Test]
        public void FT_AsOf_13 ()
        {
            // Tdate unknown, but base Tvar is eternal, so .AsOf should return that eternal value
            Tbool t = new Tbool(true);
            Tdate time = new Tdate(Hstate.Stub);
            Assert.AreEqual(true, t.AsOf(time).Out);        
        }

        [Test]
        public void FT_AsOf_14 ()
        {
            // Both Tdate and Tvar vary
            Tbool t = new Tbool(true);
            t.AddState(Date(2000,1,1),false);

            // When Tdate varies, the FirstValue is used...
            Tdate time = new Tdate(Date(1999,1,1));
            time.AddState(Date(2010,1,1),Date(2010,1,1));

            Assert.AreEqual(true, t.AsOf(time).Out);        
        }

        // .IsAlways
        
        [Test]
        public void FT_IsAlways_1 ()
        {
            Tbool t = new Tbool(true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = t.IsAlwaysTrue();
            Assert.AreEqual(false, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_2 ()
        {
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = (!t).IsAlwaysTrue();
            Assert.AreEqual(false, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_3 ()
        {
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, true);
            Tbool res = t.IsAlwaysTrue();
            Assert.AreEqual(true, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_4 ()
        {
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = t.IsAlwaysTrue(Time.DawnOf.AddYears(3), Time.DawnOf.AddYears(9));
            Assert.AreEqual(false, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_5 ()
        {
            Tbool t = new Tbool(true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = t.IsAlwaysTrue(Time.DawnOf.AddYears(2), Time.DawnOf.AddYears(3));
            Assert.AreEqual(true, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_6 ()
        {
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = t.IsAlwaysTrue(Time.DawnOf.AddYears(7), Time.DawnOf.AddYears(9));
            Assert.AreEqual(false, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_7 ()
        {
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = (!t).IsAlwaysTrue(Time.DawnOf.AddYears(7), Time.DawnOf.AddYears(9));
            Assert.AreEqual(true, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_8 ()
        {
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, true);
            Tbool res = t.IsAlwaysTrue(Time.DawnOf.AddYears(7), Time.DawnOf.AddYears(9));
            Assert.AreEqual(true, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_9 ()
        {
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, true);
            Tbool res = (!t).IsAlwaysTrue(Time.DawnOf.AddYears(7), Time.DawnOf.AddYears(9));
            Assert.AreEqual(false, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_10 ()
        {
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, true);
            Tbool res = (!t).IsAlwaysTrue();
            Assert.AreEqual(false, res.Out);        
        }
        
        [Test]
        public void FT_IsAlways_11 ()
        {
            Tbool t = new Tnum(3.4) == 3.4;
            Tbool res = t.IsAlwaysTrue();
            Assert.AreEqual(true, res.Out);       
        }

        // .IsEver
        
        [Test]
        public void FT_IsEver_1 ()
        {
            Tbool t = new Tbool(true);
            t.AddState(Time.DawnOf.AddYears(5), false);
            Tbool res = t.IsEverTrue();
            Assert.AreEqual(true, res.Out);        
        }
        
        [Test]
        public void FT_IsEver_2 ()
        {
            Tbool t = new Tbool(false);
            Tbool res = t.IsEverTrue();
            Assert.AreEqual(false, res.Out);        
        }

        [Test]
        public void FT_IsEver_8 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2012,11,8), true);
            Tbool res = t.IsEverTrue();
            Assert.AreEqual(true, res.Out);            
        }
        
        [Test]
        public void FT_IsEver_9 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2012,11,8), true);
            Tbool res = t.IsEverTrue(Date(2013,1,1), Date(2014,1,1));
            Assert.AreEqual(true, res.Out);            
        }
        
        [Test]
        public void FT_IsEver_10 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2012,11,8), true);
            Tbool res = t.IsEverTrue(Date(2012,1,1), Date(2013,1,1));
            Assert.AreEqual(true, res.Out);            
        }
        
        [Test]
        public void FT_IsEver_11 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2012,11,8), true);
            Tbool res = t.IsEverTrue(Date(2011,1,1), Date(2012,1,1));
            Assert.AreEqual(false, res.Out);            
        }

        [Test]
        public void FT_IsEver_13 ()
        {
            Tbool t = new Tbool(Hstate.Unstated);
            Tbool res = t.IsEverTrue(Time.DawnOf.AddYears(3), Time.DawnOf.AddYears(9));
            Assert.AreEqual("Unstated", res.Out);        
        }

        [Test]
        public void FT_IsEver_14 ()
        {
            Tbool t = new Tbool(Hstate.Unstated);
            Tbool res = t.IsEverTrue();
            Assert.AreEqual("Unstated", res.Out);        
        }

        [Test]
        public void FT_IsEver_15 ()
        {
            Tbool t = new Tbool(Hstate.Stub);
            Tbool res = t.IsEverTrue();
            Assert.AreEqual("Stub", res.Out);        
        }
        
        // .ToBool
        
        [Test]
        public void FT_ToBool_1 ()
        {
            Tbool t = new Tbool(true);
            Assert.AreEqual(true, t.ToBool);        
        }
        
        [Test]
        public void FT_ToBool_2 ()
        {
            Tbool t = new Tbool(false);
            Assert.AreEqual(false, t.ToBool);        
        }
        
        [Test]
        public void FT_ToBool_3 ()
        {
            // If value is not eternal, .ToBool should return null
            Tbool t = new Tbool(false);
            t.AddState(Time.DawnOf.AddYears(5), true);
            Assert.AreEqual(null, t.ToBool);        
        }
        
        [Test]
        public void FT_ToBool_4 ()
        {
            // If value is unknown, .ToBool should return null
            Tbool t = new Tbool(Hstate.Uncertain);
            Assert.AreEqual(null, t.ToBool);        
        }
        
        // .EverPerInterval
        
        [Test]
        public void FT_EverPerInterval_1 ()
        {
            // This will break annually b/c Year is determined by the system clock
            Tnum theYear = TheTime.Year(5);
            
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, false);
            t.AddState(Date(2012,11,8), true);
            
            Tbool result = t.EverPer(theYear).Lean;
            Assert.AreEqual("{Dawn: false; 1/1/2012: true}", result.Out);        
        }
        
        [Test]
        public void FT_EverPerInterval_2 ()
        {
            Tnum theYear = TheTime.Year(5);
            Tbool t = new Tbool(Hstate.Unstated);
            Tbool result = t.EverPer(theYear);
            Assert.AreEqual("Unstated", result.Lean.Out);        
        }

        [Test]
        public void FT_EverPerInterval_3 ()
        {
            Tnum theYear = new Tnum(Hstate.Stub);
            Tbool t = new Tbool(Hstate.Unstated);
            Tbool result = t.EverPer(theYear);
            Assert.AreEqual("Stub", result.Lean.Out);        
        }

        
        // .AlwaysPerInterval
        
        [Test]
        public void FT_AlwaysPerInterval_1 ()
        {
            // This will break annually b/c Year is determined by the system clock
            Tnum theYear = TheTime.Year(5);
            
            Tbool t = new Tbool();
            t.AddState(Time.DawnOf, false);
            t.AddState(Date(2012,11,8), true);
            
            Tbool result = t.AlwaysPer(theYear).Lean;
            Assert.AreEqual("{Dawn: false; 1/1/2013: true}", result.Out);        
        }
        
        [Test]
        public void FT_AlwaysPerInterval_2 ()
        {
            Tnum theYear = TheTime.Year(5);
            Tbool result = new Tbool(Hstate.Stub).AlwaysPer(theYear);
            Assert.AreEqual("Stub", result.Lean.Out);        
        }
        
        // .ToInt
        
        [Test]
        public void FT_ToInt_1 () 
        {
            Tnum t = new Tnum(42);
            Assert.AreEqual(42, t.ToInt);        
        }
        
        [Test]
        public void FT_ToInt_2 ()
        {
            Tnum t = new Tnum(Hstate.Uncertain);
            Assert.AreEqual(null, t.ToInt);        
        }
        
        [Test]
        public void FT_ToInt_3 ()
        {
            Tnum t = new Tnum(42);
            t.AddState(Date(1912,5,3), 43);
            Assert.AreEqual(null, t.ToInt);        
        }
        
        // Tnum.ToDecimal
        
        [Test]
        public void FT_ToDecimal_1 ()
        {
            Tnum t = new Tnum(42.1);
            Assert.AreEqual(42.1, t.ToDecimal);        
        }
        
        [Test]
        public void FT_ToDecimal_2 ()
        {
            Tnum t = new Tnum(Hstate.Uncertain);
            Assert.AreEqual(null, t.ToDecimal);        
        }
        
        [Test]
        public void FT_ToDecimal_3 ()
        {
            Tnum t = new Tnum(42.13);
            t.AddState(Date(1912,5,3), 43.13);
            Assert.AreEqual(null, t.ToDecimal);        
        }
        
        // Tstr.ToString
        
        [Test]
        public void FT_ToString_1 ()
        {
            Tstr t = new Tstr("42");
            Assert.AreEqual("42", t.ToString);        
        }
        
        [Test]
        public void FT_ToString_2 ()
        {
            Tstr t = new Tstr(Hstate.Stub);
            Assert.AreEqual(null, t.ToString);        
        }
        
        [Test]
        public void FT_ToString_3 ()
        {
            Tstr t = new Tstr("42");
            t.AddState(Date(2004,2,21), "43");
            Assert.AreEqual(null, t.ToString);        
        }
        
        // Tdate.ToDateTime
        
        [Test]
        public void FT_ToDateTime_1 ()
        {
            Tdate t = new Tdate(2000,1,1);
            Assert.AreEqual(Date(2000,1,1), t.ToNullDateTime);        
        }
        
        [Test]
        public void FT_ToDateTime_2 ()
        {
            Tdate t = new Tdate(Hstate.Uncertain);
            Assert.AreEqual(null, t.ToNullDateTime);        
        }
        
        [Test]
        public void FT_ToDateTime_3 ()
        {
            Tdate t = new Tdate(Date(1000,1,1));
            t.AddState(Date(1000,1,1), Date(2000,1,1));
            Assert.AreEqual(null, t.ToNullDateTime);        
        }
        
        // Tvar.ElapsedTime
        
        [Test]
        public void FT_ElapsedTime_1 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            t.AddState(Date(2000,1,2), false);
            t.AddState(Date(2000,1,3), true);
            t.AddState(Date(2000,1,4), false);      
            Tnum result = t.TotalElapsedDays(Date(1999,1,1), Date(2000,1,6));
            Assert.AreEqual(2, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_2 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            t.AddState(Date(2000,1,2), false);
            t.AddState(Date(2000,1,3), true);
            t.AddState(Date(2000,1,4), false);
            Tnum result = t.TotalElapsedDays(Date(2000,1,2), Date(2000,1,6));
            Assert.AreEqual(1, result.Out);      
        }
        
        [Test]
        public void FT_ElapsedTime_3 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            t.AddState(Date(2000,1,2), false);
            t.AddState(Date(2000,1,3), true);
            t.AddState(Date(2000,1,4), false);
            Tnum result = t.TotalElapsedDays(Date(2010,1,2), Date(2010,1,6));
            Assert.AreEqual(0, result.Out);      
        }
        
        [Test]
        public void FT_ElapsedTime_4 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            t.AddState(Date(2000,1,2), false);
            Tnum result = t.TotalElapsedDays(Date(1999,1,2), Date(1999,1,6));
            Assert.AreEqual(0, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_5 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            t.AddState(Date(2000,2,1), false);
            Tnum result = t.TotalElapsedDays(Date(2000,1,15), Date(2000,1,20));
            Assert.AreEqual(5, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_6 ()
        {
            Tbool t = new Tbool(true);
            Tnum result = t.TotalElapsedDays(Date(2000,1,15), Date(2000,1,20));
            Assert.AreEqual(5, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_7 ()
        {
            Tbool t = new Tbool(true);
            Tnum result = (!t).TotalElapsedDays(Date(2000,1,15), Date(2000,1,20));
            Assert.AreEqual(0, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_8 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            Tnum result = t.TotalElapsedDays(Date(2000,1,15), Date(2000,1,20));
            Assert.AreEqual(5, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_9 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            t.AddState(Date(2000,1,5), false);
            Tnum result = t.TotalElapsedDays(Date(2000,1,2), Date(2000,1,6));
            Assert.AreEqual(3, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_10 ()
        {
            Tbool t = new Tbool(false);
            Tnum result = t.TotalElapsedDays(Date(2000,1,1), Date(2010,1,1));
            Assert.AreEqual(0, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_11 ()
        {
            Tbool t = new Tbool(false);
            Tnum result = t.TotalElapsedDays(Time.DawnOf, Time.EndOf);
            Assert.AreEqual(0, result.Out);
        }
        
        [Test]
        public void FT_ElapsedTime_12 ()
        {
            Tbool t = new Tbool(Hstate.Uncertain);
            Tnum result = t.TotalElapsedDays(Time.DawnOf, Time.EndOf);
            Assert.AreEqual("Uncertain", result.Out);
        }

        // Tvar.ElapsedDaysPerInterval
        
        [Test]
        public void FT_ElapsedDaysPerInterval_1 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            t.AddState(Date(2001,1,1), false);
            t.AddState(Date(2002,1,1), true);
            t.AddState(Date(2003,1,1), false);
            Tnum result = t.TotalElapsedDaysPer(TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2000: 366; 1/1/2001: 0; 1/1/2002: 365; 1/1/2003: 0}", result.Out);      
        }
        
        [Test]
        public void FT_ElapsedDaysPerInterval_2 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,6,1), true);
            t.AddState(Date(2001,1,1), false);
            Tnum result = t.TotalElapsedDaysPer(TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2000: 214; 1/1/2001: 0}", result.Out);      
        }
        
        [Test]
        public void FT_ElapsedDaysPerInterval_3 ()
        {
            Tbool t = new Tbool(false);
            Tnum result = t.TotalElapsedDaysPer(TheYear);
            Assert.AreEqual(0, result.Out);      
        }

        [Test]
        public void FT_ElapsedDaysPerInterval_4 ()
        {
            Tbool t = new Tbool(Hstate.Unstated);
            Tnum result = t.TotalElapsedDaysPer(TheYear);
            Assert.AreEqual("Unstated", result.Out);      
        }

        [Test]
        public void FT_ElapsedDaysPerInterval_5 ()
        {
            Tbool t = new Tbool(false);
            Tnum n = new Tnum(Hstate.Unstated);
            Tnum result = t.TotalElapsedDaysPer(n);
            Assert.AreEqual("Unstated", result.Out);      
        }

        // Tbool.CountPer
        
        [Test]
        public void FT_CountPer_1 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2010,1,1), true);
            t.AddState(Date(2010,2,1), true);
            t.AddState(Date(2010,3,1), true);
            t.AddState(Date(2010,4,1), false);
            Tnum actual = t.CountPer(TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2010: 3; 1/1/2011: 0}", actual.Out);      
        }
        
        [Test]
        public void FT_CountPer_2 ()
        {
            Tbool t = new Tbool(false);
            Tnum actual = t.CountPer(TheYear);
            Assert.AreEqual(0, actual.Out);      
        }
        
        [Test]
        public void FT_CountPer_3 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2010,1,1), true);
            t.AddState(Date(2010,2,1), false);
            t.AddState(Date(2010,3,1), true);
            t.AddState(Date(2010,4,1), false);
            Tnum actual = t.CountPer(TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2010: 2; 1/1/2011: 0}", actual.Out);      
        }
        
        [Test]
        public void FT_CountPer_4 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2010,11,1), true);
            t.AddState(Date(2010,12,1), true);
            t.AddState(Date(2011,1,1), true);
            t.AddState(Date(2011,2,1), false);
            Tnum actual = t.CountPer(TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2010: 2; 1/1/2011: 1; 1/1/2012: 0}", actual.Out);      
        }
        
        [Test]
        public void FT_CountPer_5 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2009,12,15), true);
            t.AddState(Date(2010,2,1), false);
            t.AddState(Date(2010,3,1), true);
            t.AddState(Date(2010,4,1), false);
            Tnum actual = t.CountPer(TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2010: 1; 1/1/2011: 0}", actual.Out);      
        }
        
        // Tbool.RunningCountPer
        
        [Test]
        public void FT_RunningCountPer_1 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2010,1,1), true);
            t.AddState(Date(2010,2,1), true);
            t.AddState(Date(2010,3,1), true);
            t.AddState(Date(2010,4,1), false);
            Tnum actual = t.RunningCountPer(TheYear);
            Assert.AreEqual("{Dawn: 0; 2/1/2010: 1; 3/1/2010: 2; 4/1/2010: 3}", actual.Out);      
        }
        
        [Test]
        public void FT_RunningCountPer_2 ()
        {
            Tbool t = new Tbool(false);
            Tnum actual = t.RunningCountPer(TheYear);
            Assert.AreEqual(0, actual.Out);      
        }
        
        [Test]
        public void FT_RunningCountPer_3 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2010,1,1), true);
            t.AddState(Date(2010,2,1), false);
            t.AddState(Date(2010,3,1), true);
            t.AddState(Date(2010,4,1), false);
            Tnum actual = t.RunningCountPer(TheYear);
            Assert.AreEqual("{Dawn: 0; 2/1/2010: 1; 4/1/2010: 2}", actual.Out);      
        }
        
        // Tbool.CountPastNIntervals
        
        [Test]
        public void FT_CountPastNIntervals_1 ()
        {
            // This test case will break every year due to the use of TheTime.Year
            Tbool t = new Tbool(true);
            t.AddState(Date(2012,1,1), false);
            Tnum actual = t.CountPastNIntervals(TheTime.Year(2), 2);
            Assert.AreEqual("{Dawn: 0; 1/1/2011: 2; 1/1/2012: 1; 1/1/2013: 0}", actual.Out);      
        }
        
        [Test]
        public void FT_CountPastNIntervals_2 ()
        {
            // This test case will break every year due to the use of TheTime.Year
            Tbool t = new Tbool(true);
            Tnum actual = t.CountPastNIntervals(TheTime.Year(2), 2);
            Assert.AreEqual("{Dawn: 0; 1/1/2011: 2}", actual.Out);      
        }
        
        [Test]
        public void FT_CountPastNIntervals_3 ()
        {
            Tbool t = new Tbool(false);
            Tnum actual = t.CountPastNIntervals(TheTime.Year(2), 2);
            Assert.AreEqual(0, actual.Out);      
        }
        
        // Tbool.ForConsecutiveMonths
        
        [Test]
        public void ForConsecutiveMonths_1 ()
        {
            Tbool t = new Tbool(false);
            Tbool r = t.ConsecutiveMonths(12);
            Assert.AreEqual(false, r.Out);      
        }
        
        [Test]
        public void ForConsecutiveMonths_2 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(new DateTime(2000,1,1), true);

            Tbool r = t.ConsecutiveMonths(12);
            Assert.AreEqual("{Dawn: false; 1/1/2001: true}", r.Out);      
        }
        
        [Test]
        public void ForConsecMonths_3 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1),true);
            t.AddState(Date(2001,1,1),false);
            Tbool r = t.ConsecutiveMonths(6);
            Assert.AreEqual("{Dawn: false; 7/1/2000: true; 1/1/2001: false}", r.Out);      
        }
        
        [Test]
        public void ForConsecutiveMonths_4 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1),true);
            t.AddState(Date(2001,1,1),false);
            Tbool r = t.ConsecutiveMonths(18);
            Assert.AreEqual(false, r.Out);      
        }
        
        [Test]
        public void ForConsecutiveMonths_5 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1),true);
            t.AddState(Date(2001,1,1),false);
            t.AddState(Date(2001,6,1),true);
            Tbool r = t.ConsecutiveMonths(20);
            Assert.AreEqual("{Dawn: false; 2/1/2003: true}", r.Out);      
        }

        [Test]
        public void ForConsecutiveMonths_6 ()
        {
            Tbool t = new Tbool(Hstate.Uncertain);
            Tbool r = t.ConsecutiveMonths(20);
            Assert.AreEqual("Uncertain", r.Out);      
        }

        // Tbool.DateFirstTrue

        [Test]
        public void DateFirst_1 ()
        {
            // Base Tvar never meets the specified condition
            Tbool t = new Tbool(false);
            Assert.AreEqual("Stub", t.DateFirstTrue.Out);      
        }
        
        [Test]
        public void DateFirst_2 ()
        {
            Tbool t = new Tbool(true);
            Assert.AreEqual(Time.DawnOf, t.DateFirstTrue.ToDateTime);   
        }
        
        [Test]
        public void DateFirst_3 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            Assert.AreEqual(Date(2000,1,1), t.DateFirstTrue.ToDateTime);      
        }

        [Test]
        public void DateFirst_4 ()
        {
            // Base Tvar is eternally unknown; that state must percolate up
            Tbool t = new Tbool(Hstate.Unstated);
            Assert.AreEqual("Unstated", t.DateFirstTrue.Out);      
        }

        [Test]
        public void DateFirst_5 ()
        {
            // Base Tvar is unknown, then the required value
            Tbool t = new Tbool(Hstate.Unstated);
            t.AddState(Date(2000,1,1), true);
            Assert.AreEqual(Date(2000,1,1), t.DateFirstTrue.ToDateTime);      
        }

        [Test]
        public void DateFirst_6 ()
        {
            Tbool tb = new Tbool(false);
            tb.AddState(new DateTime(2000,1,1),true);
            tb.AddState(new DateTime(2000,1,3),false);
            tb.AddState(new DateTime(2000,1,10),true);
            tb.AddState(new DateTime(2000,2,18),false);

            Assert.AreEqual(Date(2000,1,1), tb.DateFirstTrue.ToDateTime);      
        }

        // Tbool.DateLastTrue

        [Test]
        public void DateLast_1 ()
        {
            // Base Tvar never meets the specified condition
            Tbool t = new Tbool(false);
            Assert.AreEqual("Stub", t.DateLastTrue.Out);      
        }
        
        [Test]
        public void DateLast_2 ()
        {
            Tbool t = new Tbool(true);
            Assert.AreEqual(Time.EndOf, t.DateLastTrue.ToDateTime);   
        }
        
        [Test]
        public void DateLast_3 ()
        {
            Tbool t = new Tbool(false);
            t.AddState(Date(2000,1,1), true);
            Assert.AreEqual(Time.EndOf, t.DateLastTrue.ToDateTime);      
        }

        [Test]
        public void DateLast_4 ()
        {
            // Base Tvar is eternally unknown; that state must percolate up
            Tbool t = new Tbool(Hstate.Unstated);
            Assert.AreEqual("Unstated", t.DateLastTrue.Out);      
        }

        [Test]
        public void DateLast_5 ()
        {
            // Base Tvar is unknown, then the required value
            Tbool t = new Tbool(Hstate.Unstated);
            t.AddState(Date(2000,1,1), true);
            Assert.AreEqual(Time.EndOf, t.DateLastTrue.ToDateTime);      
        }

        [Test]
        public void DateLast_6 ()
        {
            Tbool tb = new Tbool(false);
            tb.AddState(new DateTime(2000,1,1),true);
            tb.AddState(new DateTime(2000,1,3),false);
            tb.AddState(new DateTime(2000,1,10),true);
            tb.AddState(new DateTime(2000,2,18),false);

            Assert.AreEqual(Date(2000,2,17), tb.DateLastTrue.ToDateTime);      
        }

        // Type identification
        
        [Test]
        public void TnumTypeTest1 ()
        {
            bool isTnum = new Tnum().GetType().ToString() == "Hammurabi.Tnum";
            Assert.AreEqual(true, isTnum);        
        }
        
        [Test]
        public void TnumTypeTest2 ()
        {
            bool isTnum = new Tbool().GetType().ToString() == "Hammurabi.Tnum";
            Assert.AreEqual(false, isTnum);        
        }
        
        [Test]
        public void TvarTypeTest1 ()
        {
            Tnum item = new Tnum(3);
            bool istype = Util.IsType<Tnum>(item);
            Assert.AreEqual(true, istype);        
        }
        
        [Test]
        public void TvarTypeTest2 ()
        {
            Tnum item = new Tnum(3);
            bool istype = Util.IsType<Tbool>(item);
            Assert.AreEqual(false, istype);        
        }

        // ObjectAsOf

        [Test]
        public void ObjectAsOf1 ()
        {
            Tnum item = new Tnum(3);
            Hval h = item.ObjectAsOf(DateTime.Now);
            Assert.AreEqual(3, h.Val);        
        }

        [Test]
        public void ObjectAsOf2 ()
        {
            Tnum item = new Tnum(Hstate.Stub);
            Hval h = item.ObjectAsOf(DateTime.Now);
            Assert.AreEqual("Stub", h.ToString);        
        }

        // Value re-assignment

        [Test]
        public void ValueReassignment1 ()
        {
            Tnum t = new Tnum(3);
            t = new Tnum(4);
            Assert.AreEqual(4, t.Out);        
        }

        // .IsUnstated

        [Test]
        public void IsUnstated1 ()
        {
            Hval unst = new Hval(null,Hstate.Unstated);

            Tbool tb1 = new Tbool(false);
            tb1.AddState(Date(2000,1,1), unst);
            tb1.AddState(Date(2001,1,1), true);

            Assert.AreEqual("{Dawn: false; 1/1/2000: true; 1/1/2001: false}", tb1.IsUnstated.Out);        
        }

        // .Shift

        [Test]
        public void FT_Shift_1 ()
        {
            Tnum t = new Tnum(0);
            t.AddState(Date(2010,1,1), 100);
            t.AddState(Date(2011,1,1), 200);
            Tnum actual = t.Shift(-1, TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2011: 100; 1/1/2012: 200}", actual.Out);      
        }

        [Test]
        public void FT_Shift_2 ()
        {
            Tnum t = new Tnum(0);
            t.AddState(Date(2010,1,1), 100);
            t.AddState(Date(2011,1,1), 200);
            Tnum actual = t.Shift(0, TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2010: 100; 1/1/2011: 200}", actual.Out);      
        }

        [Test]
        public void FT_Shift_3 ()
        {
            Tnum t = new Tnum(0);
            t.AddState(Date(2010,1,1), 100);
            t.AddState(Date(2011,1,1), 200);
            Tnum actual = t.Shift(2, TheYear);
            Assert.AreEqual("{Dawn: 0; 1/1/2008: 100; 1/1/2009: 200}", actual.Out);      
        }

        [Test]
        public void FT_Shift_uncertain1 ()
        {
            Tnum t = new Tnum(Hstate.Stub);
            Assert.AreEqual("Stub", t.Shift(2, TheYear).Out);      
        }

        [Test]
        public void FT_Shift_uncertain2 ()
        {
            Tnum t = new Tnum(Hstate.Uncertain);
            Assert.AreEqual("Uncertain", t.Shift(-2, TheYear).Out);      
        }

        [Test]
        public void FT_Shift_uncertain3 ()
        {
            Tnum t = new Tnum(Hstate.Unstated);
            Assert.AreEqual("Unstated", t.Shift(0, TheYear).Out);      
        }
    }    
}