using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree
{
    //Class that keeps name,bith date, gender, ocupation and the number of children for a family member
    public class Person
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Occupation { get; set; }
        public int NumberOfChildren { get; set; }
        //Constructor without parameters
        public Person()
        {
            Name = "none";
            DateOfBirth = new DateTime();
            Gender = new Gender();
            Occupation = "none";
            NumberOfChildren = 0;
        }
        //Constructor with all parameters
        public Person(string name, DateTime dateOfBirth, Gender gender, string ocupation, int numberOfChildren)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Occupation = ocupation;
            NumberOfChildren = numberOfChildren;
        }
    }


}
