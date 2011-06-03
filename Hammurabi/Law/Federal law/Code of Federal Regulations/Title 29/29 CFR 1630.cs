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
// 
// NO REPRESENTATION OR WARRANTY IS MADE THAT THIS PROGRAM ACCURATELY REFLECTS
// OR EMBODIES ANY APPLICABLE LAWS, REGULATIONS, RULES OR EXECUTIVE ORDERS 
// ("LAWS"). YOU SHOULD RELY ONLY ON OFFICIAL VERSIONS OF LAWS PUBLISHED BY THE 
// RELEVANT GOVERNMENT AUTHORITY, AND YOU ASSUME THE RESPONSIBILITY OF 
// INDEPENDENTLY VERIFYING SUCH LAWS. THE USE OF THIS PROGRAM IS NOT A 
// SUBSTITUTE FOR THE ADVICE OF AN ATTORNEY.

using Hammurabi;

namespace CFR.Tit29
{
    /// <summary>
    /// Regulations to implement the equal employment provisions of the Americans
    /// with Disabilities Act (ADA).
    /// </summary>
    /// <cite>29 C.F.R. Part 1630 (2011)</cite>   
    /// <updated>2011-04-03</updated>
    public class Part1630
    {
        /// <summary>
        /// 1630.2 - Indicates whether a person (p) is "disabled."
        /// NEEDS ELABORATION.
        /// </summary>
        public static Tbool IsDisabled(Person p)
        {
            // A physical or mental impairment that substantially limits one
            // or more of the major life activities of such individual
            return Input.Tbool(p,"IsDisabled");
        }
    }
}