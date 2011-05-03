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

namespace Hammurabi
{
    // TODO: Add family relation inference rules where appropriate.
    
    /// <summary>
    /// Represent various types of family relationships among people.
    /// </summary>
    public class Fam
    {
        /// <summary>
        /// Returns a string indicating the familial relationship between
        /// two people.
        /// </summary>
        public static Tstr Relationship(Person p1, Person p2)
        {
            return Facts.InputTstr(p1, "FamilyRelationship", p2);
        }
        
        /// <summary>
        /// Returns whether two people are married.
        /// </summary>
        public static Tbool AreMarried(Person p1, Person p2)
        {
            if (Facts.GetUnknowns == true)
            {
                return Facts.Sym(p1, "FamilyRelationship", p2, "Spouse");
            }
            else
            {
                return Facts.Either(Facts.Sym(p1, "IsMarriedTo", p2),
                                    Facts.Sym(p1, "FamilyRelationship", p2, "Spouse"));
            }
        }
        
        /// <summary>
        /// Returns whether two people are legally separated.
        /// </summary>
        public static Tbool AreSeparated(Person p1, Person p2)
        {
            return Facts.Sym(p1, "IsSeparatedFrom", p2);
        }
        
        /// <summary>
        /// Returns whether two people are divorced.
        /// </summary>
        public static Tbool AreDivorced(Person p1, Person p2)
        {
            return Facts.Sym(p1, "IsDivorcedFrom", p2); 
        }
        
        /// <summary>
        /// Returns whether one person is the parent of another.
        /// </summary>
        public static Tbool IsParentOf(Person p1, Person p2)
        {
            Tbool isP = IsBiologicalParentOf(p1, p2) |
                        IsAdoptiveParentOf(p1, p2) |
                        IsFosterParentOf(p1, p2) |
                        IsStepparentOf(p1, p2);  
            
            if (Facts.GetUnknowns == true)
            {
                return isP;
            }
            else
            {
                return Facts.Either(isP, Facts.InputTbool(p1, "IsParentOf", p2));
            }   
        }
        
        /// <summary>
        /// Returns whether one person is the biological parent of another.
        /// </summary>
        public static Tbool IsBiologicalParentOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Biological parent",
                                Relationship(p2, p1) == "Biological child");
        }
        
        /// <summary>
        /// Returns whether one person is the adoptive parent of another.
        /// </summary>
        public static Tbool IsAdoptiveParentOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Adoptive parent",
                                Relationship(p2, p1) == "Adopted child");   
        }
        
        /// <summary>
        /// Returns whether one person is the foster parent of another.
        /// </summary>
        public static Tbool IsFosterParentOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Foster parent",
                                Relationship(p2, p1) == "Foster child");    
        }
        
        /// <summary>
        /// Returns whether one person is the step parent of another.
        /// </summary>
        public static Tbool IsStepparentOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Stepparent",
                                Relationship(p2, p1) == "Stepchild");   
        }
        
        /// <summary>
        /// Returns whether two people are siblings.
        /// </summary>
        public static Tbool AreSiblings(Person p1, Person p2)
        {
            return Facts.Sym(p1, "FamilyRelationship", p2, "Sibling");
            
            // or, share a parent
        }
        
        /// <summary>
        /// Returns whether two people are half-siblings.
        /// </summary>
        public static Tbool AreHalfSiblings(Person p1, Person p2)
        {
            return Facts.Sym(p1, "FamilyRelationship", p2, "Half sibling");
            
            // or, share one parent
        }
        
        /// <summary>
        /// Returns whether two people are step-siblings.
        /// </summary>
        public static Tbool AreStepsiblings(Person p1, Person p2)
        {
            return Facts.Sym(p1, "FamilyRelationship", p2, "Stepsibling");
        }
        
        /// <summary>
        /// Returns whether one person is the grandparent of another.
        /// </summary>
        public static Tbool IsGrandparentOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Grandparent",
                                Relationship(p2, p1) == "Grandchild");  
        }
        
        /// <summary>
        /// Returns whether one person is the great-grandparent of another.
        /// </summary>
        public static Tbool IsGreatGrandparentOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Great-grandparent",
                                Relationship(p2, p1) == "Great-grandchild");    
        }
  
        /// <summary>
        /// Returns whether one person is the great-great-grandparent of another.
        /// </summary>
        public static Tbool IsGreatGreatGrandparentOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Great-great-grandparent",
                                Relationship(p2, p1) == "Great-great-grandchild");    
        }
        
        /// <summary>
        /// Returns whether one person is the aunt or uncle of another.
        /// </summary>
        public static Tbool IsAuntOrUncleOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Aunt or uncle",
                                Relationship(p2, p1) == "Niece or nephew");    
        }
        
        /// <summary>
        /// Returns whether one person is the great aunt or uncle of another.
        /// </summary>
        public static Tbool IsGreatAuntOrUncleOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Great aunt or uncle",
                                Relationship(p2, p1) == "Grand niece or nephew");    
        }
        
        /// <summary>
        /// Returns whether one person is the great-great aunt or uncle of another.
        /// </summary>
        public static Tbool IsGreatGreatAuntOrUncleOf(Person p1, Person p2)
        {
            return Facts.Either(Relationship(p1, p2) == "Great-great aunt or uncle",
                                Relationship(p2, p1) == "Great-grand niece or nephew");    
        }
        
        /// <summary>
        /// Returns whether two people are cousins.
        /// </summary>
        public static Tbool IsCousinOf(Person p1, Person p2)
        {
            return IsFirstCousinOf(p1, p2) | IsNonFirstCousinOf(p1, p2);
        }
        
        /// <summary>
        /// Returns whether two people are first cousins.
        /// </summary>
        public static Tbool IsFirstCousinOf(Person p1, Person p2)
        {
            return Facts.Sym(p1, "FamilyRelationship", p2, "First cousin");    
        }
        
        /// <summary>
        /// Returns whether two people are non-first cousins.
        /// </summary>
        public static Tbool IsNonFirstCousinOf(Person p1, Person p2)
        {
            return Facts.Sym(p1, "FamilyRelationship", p2, "Other cousin");    
        }      
        
        /// <summary>
        /// Returns whether one person is the ancestor of another.
        /// </summary>
        public static Tbool IsAncestorOf(Person p1, Person p2)
        {
            return Facts.InputTbool(p1, "IsAncestorOf", p2);    
        }

        /// <summary>
        /// Returns whether one person has legal custody of another.
        /// </summary>
        public static Tbool HasCustodyOf(Person p1, Person p2)
        {
            return Facts.InputTbool(p1, "HasCustodyOf", p2);    
        }
        
        /// <summary>
        /// Returns whether one person is the legal guardian of another.
        /// </summary>
        public static Tbool IsLegalGuardianOf(Person p1, Person p2)
        {
            return Facts.InputTbool(p1, "IsLegalGuardianOf", p2);   
        }
        
        /// <summary>
        /// Returns whether one person acts in loco parentis of another.
        /// </summary>
        public static Tbool ActsInLocoParentisOf(Person p1, Person p2)
        {
            return Facts.InputTbool(p1, "ActsInLocoParentisOf", p2);   
        }
        
        /// <summary>
        /// Returns whether one person has day-to-day responsibility for
        /// another person.
        /// </summary>
        public static Tbool HasDayToDayResponsibilityFor(Person p1, Person p2)
        {
            return Facts.InputTbool(p1, "HasDayToDayResponsibilityFor", p2);   
        }
        
        /// <summary>
        /// Infers whether one person is the custodial parent of another.
        /// </summary>
        public static Tbool IsCustodialParentOf(Person p1, Person p2)
        {
            return IsParentOf(p1,p2) &
                   HasCustodyOf(p1,p2); 
        }
        
        /// <summary>
        /// Returns whether one person is the next of kin (nearest
        /// blood relative) of another.
        /// </summary>
        public static Tbool IsNextOfKinOf(Person p1, Person p2)
        {
            return Facts.InputTbool(p1, "IsNextOfKinOf", p2);  
        }
        
    }
}
