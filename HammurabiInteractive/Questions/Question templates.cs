//// Copyright (c) 2012 Hammura.bi LLC
//// 
//// Permission is hereby granted, free of charge, to any person obtaining a copy
//// of this software and associated documentation files (the "Software"), to deal
//// in the Software without restriction, including without limitation the rights
//// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//// copies of the Software, and to permit persons to whom the Software is
//// furnished to do so, subject to the following conditions:
//// 
//// The above copyright notice and this permission notice shall be included in
//// all copies or substantial portions of the Software.
//// 
//// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//// THE SOFTWARE.
//
//using System;
//using System.Collections.Generic;
//
//namespace Interactive
//{
//	public class Templates
//	{
//		/// <summary>
//		/// Returns a question, given a question relationship.  Something of a proto-database.
//		/// </summary>
//		public static Question GetQ(string rel)
//		{
//			switch(rel)       
//			{
//                case "BranchOfArmedForces":
//                    return new Question(rel, "string", "Does {subj} serve in a branch of the U.S. Armed Forces?", "",
//                                new List<string>() { "Army", "Navy", "Air Force", "Marine Corps", "Coast Guard", "---",
//                                                     "Army National Guard", "Army Reserve", "Air National Guard", "Navy Reserve", "Air Force Reserve", 
//                                                     "Marine Corps Reserve", "Coast Guard Reserve", "---", "None of the above" });
//                
//                case "CanBeClaimedAsQCByTwoTaxpayers":
//                    return new Question(rel, "bool", "Can {subj} be claimed as a qualifying child by more than one taxpayer?", "");
//                
//                case "DateFamilyLeaveBegins":
//                    return new Question(rel, "date", "When does {subj} want to go on leave?", "", DateTime.Now.AddYears(-3), DateTime.Now.AddYears(3));
//
//                case "DateOfBirth":
//                    return new Question(rel, "date", "What is {subj}'s date of birth?", "", new DateTime(1900, 1, 1), DateTime.Now);
//
//                case "DateStartedWorkingAt":
//                    return new Question(rel, "date", "When did {subj} start working at {obj}?", "", new DateTime(1950, 1, 1), DateTime.Now);
//
//                case "EmploymentRelationship":
//                    return new Question(rel, "string", "What is {subj}'s employment relationship at {obj}?", "",
//                                new List<string>() { "Employee", "Independent contractor", "Other", "None" });
//                
//                case "Gender":
//                    return new Question(rel, "string", "What is {subj}'s gender?", "",
//                                new List<string>() { "Female", "Male", "Transgender" });
//
//                case "HasDayToDayResponsibilityFor":
//                    return new Question(rel, "bool", "Does {subj} have day-to-day responsibility for {obj}?", "");
//
//                case "HasSeriousHealthCondition":
//                    return new Question(rel, "bool", "Does {subj} have a serious health condition?",
//                                        "A <b>serious health condition</b> involves inpatient care or continuing treatment by a health care provider.");
//
//                case "HoursWorkedInLast12Months":
//                    return new Question(rel, "numvar", "In the last twelve months, how many hours has {subj} worked at {obj}?", "", 0, 4000);
//
//                case "HoursWorkedPerWeek":
//                    return new Question(rel, "numvar", "On average, how many hours does {subj} work per week at {obj}?", "", 0, 168);
//
//                case "IsAirlineFlightCrew":
//                    return new Question(rel, "bool", "Does {subj} work on an airline flight crew at {obj}?", "");
//
//                case "IsDisabled":
//                    return new Question(rel, "bool", "Is {subj} disabled?", "");
//
//                case "IsDeployedToCombatZone":
//                    return new Question(rel, "bool", "Is {subj} deployed to a combat zone?", "");
//
//                case "IsEmployedBy":
//                    return new Question(rel, "bool", "Does {subj} work at {obj}?", "");
//
//                case "IsEmployeeUnder5USC6301":
//                    return new Question(rel, "bool", "Is {subj} a federal employee subject to 5 U.S.C. § 6301?", "");
//
//                case "IsGovtAgency":
//                    return new Question(rel, "bool", "Is {subj} a government agency?", "");
//
//                case "IsIncapableOfSelfCare":
//                    return new Question(rel, "bool", "Is {subj} incapable of self-care?", "");
//
//                case "IsLegalGuardianOf":
//                    return new Question(rel, "bool", "Is {subj} the legal guardian of {obj}?", "");
//
//                case "IsMarried": // slated for deletion
//                    return new Question(rel, "bool", "Is {subj} married?", "");
//
//                case "IsMarriedTo":
//                    return new Question(rel, "person", "What's the name of {subj}'s spouse?", "");
//
//                case "IsNextOfKinOf":
//                    return new Question(rel, "bool", "Is {subj} {obj}'s next of kin?", "");
//                                
//                case "IsPermanentlyAndTotallyDisabled":
//                    return new Question(rel, "bool", "Is {subj} permanently and totally disabled?", "");
//                
//                case "IsStudent":
//                    return new Question(rel, "bool", "Is {subj} a student?", "");
//                
//                case "FamilyRelationship":
//                    return new Question(rel, "string", "What is {subj}'s relation to {obj}?", "",
//                                new List<string>() 
//                                {
//                                "Spouse", "Partner by civil union", "Domestic partner", "Former spouse", "-----",
//                                "Biological parent", "Stepparent", "Adoptive parent", "Foster parent", "-----",
//                                "Biological child", "Stepchild", "Adopted child", "Foster child", "-----",
//                                "Sibling", "Half sibling", "Stepsibling", "-----", 
//                                "Grandparent", "Great-grandparent", "Great-great-grandparent", "-----",
//                                "Grandchild", "Great-grandchild", "Great-great-grandchild", "-----",
//                                "Aunt or uncle", "Great aunt or uncle", "Great-great aunt or uncle", "-----",
//                                "Niece or nephew", "Grand niece or nephew", "Great-grand niece or nephew", "-----",
//                                "First cousin", "Other cousin", "-----",
//                                "Other family relation", "No family relation"
//                                 });
//    
//                case "FedTaxFilingStatus":
//                    return new Question(rel, "string", "What is {subj}'s federal tax filing status?", "",
//                                new List<string>() 
//                                { "Single", "Married filing jointly", "Married filing separately", "Qualifying widow(er)", 
//                                "Head of household" });
//                
//				case "LengthOfInitialProbationaryPeriodAtEmployerInMonths":
//                    return new Question(rel, "numvar", "How long was {subj}'s initial probationary period at {obj} (in months)?",
//                                 "If there was no probationary period, enter <b>0</b>.", 0, 36);
//
//                case "LessThan50EmployeesAtWorksite":
//                    return new Question(rel, "bool", "Are there fewer than 50 employees at {subj}'s worksite?", "");
//
//                case "LessThan50EmployeesWithin75MilesOfWorksite":
//                    return new Question(rel, "bool", "Does the employer have fewer than 50 employees within 75 miles of {subj}'s worksite?", "");
//
//                case "LivesWith":
//                    return new Question(rel, "bool", "Does {subj} live with {obj}?", "");
//                
//                case "MaritalStatus":
//                    return new Question(rel, "string", "What is {subj}'s marital status?", "",
//                                new List<string>() { "Married", "In a civil union", "In a domestic partnership", "Single/unmarried", "Legally separated", "Widowed" });
//                
//                case "MonthTaxYearBegins":
//                    return new Question(rel, "numvar", "In what calendar month does {subj}'s federal tax year begin?", "", 1, 12);
//                
//                case "NameOfEmployee":
//                    return new Question(rel, "person", "What is the employee's name?", "");
//
//                case "NatureOfEmploymentRelationship":
//                    return new Question(rel, "string", "What's the nature of {subj}'s employment relationship with {obj}?", "",
//                                new List<string>() { "Employee", "Independent contractor", "Other" });
//
//                case "NeedsLeaveToProvideCareFor":
//                    return new Question(rel, "person", "What's the name of the person that {subj} needs to provide care for?", "");
//
//                case "NumberOfEmployees":
//                    return new Question(rel, "numvar", "How many employees does {subj} have?", "", 0, 5000000);
//    
//                case "PercentSelfSupport":
//                    return new Question(rel, "numvar", "What percentage of {subj}'s living costs does {subj} pay for (in percentage points)?", "", 0, 100);
//                
//                case "ProvidesSupportFor":
//                    return new Question(rel, "bool", "Does {subj} provide financial support for {obj}?", "");
//
//                case "ReasonForRequestingLeaveFrom":
//                    return new Question(rel, "string", "Why does {subj} want to take leave?", "",
//                                new List<string>() 
//                                {
//                                "Maternity or paternity",
//                                "To adopt a child",
//                                "To become a foster parent",
//                                "To care for family member with a health condition",
//                                "Employee cannot work due to health condition",
//                                "To care for a family member in the Armed Forces",
//                                "Other need arising due to family member serving in Armed Forces",
//                                "Other"
//                                 });
//    
//                case "SharesPrincipalAbodeWith":
//                    return new Question(rel, "bool", "Does {subj} share a principal abode with {obj}?", "");
//                
//                case "StateJurisdiction":
//                    return new Question(rel, "string", "Please select the applicable state.", "", Lists.States);
//
//                case "WantsToTakeLeaveFrom":
//                    return new Question(rel, "bool", "Does {subj} want to take leave from {obj}?", "");
//
//			default: return DefaultQuestion(rel);
//			}
//		}
//
//		/// <summary>
//		/// Creates a default question to return if the relationship doesn't 
//		/// have a defined question template.
//		/// </summary>
//		private static Question DefaultQuestion(string rel)
//		{
//			// Generate primitive question text
//			string qText = "{subj} " + rel + " {obj}?";
//			qText = qText.Replace(" {obj}","");
//
//			// Return the question object (assumed to be Boolean!)
//			return new Question("", "bool", qText, ""); 
//		}
//	}
//}
//
