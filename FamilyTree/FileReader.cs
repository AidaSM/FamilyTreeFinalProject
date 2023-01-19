using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree
{
    //Reads data from file and make a list with Person objects
    public class FileReader
    {
        public static List<string> ReadFromFile(string fileName)
        {
            return File.ReadAllLines(fileName).ToList();
        }
        //Transforms data in Person object
        public static Person Parse(string line)
        {
            Person member = null;

            try
            {
                var values = line.Split(',');

                var name = values[0];
                var date = DateTime.Parse(values[1]);
                Gender gender;
                if (values[2] == "female")
                    gender = Gender.Female;
                else
                    gender = Gender.Male;
                var ocupation = values[3];
                var children = int.Parse(values[4]);

                member = new Person(name, date, gender, ocupation, children);

            }

            catch (ArgumentException ex)
            {
                Console.WriteLine("Could not parse enum!");
            }

            return member;
        }
    }
}
