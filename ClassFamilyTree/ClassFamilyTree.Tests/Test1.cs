using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassFamilyTree;
using System;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

namespace ClassFamilyTree.Tests
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void DefaultConstructor_ShouldInitializeWithDefaults()
        {
            var person = new Person();

            Assert.AreEqual("none", person.Name);
            Assert.AreEqual(default(DateTime), person.DateOfBirth);
            Assert.AreEqual(Gender.Male, person.Gender);
            Assert.AreEqual("none", person.Occupation);
            Assert.AreEqual(0, person.NumberOfChildren);
        }

        [TestMethod]
        public void ParameterizedConstructor_ShouldAssignAllPropertiesCorrectly()
        {
            var expectedName = "Alice";
            var expectedDob = new DateTime(1990, 1, 1);
            var expectedGender = Gender.Female;
            var expectedOccupation = "Engineer";
            var expectedChildren = 2;

            var person = new Person(expectedName, expectedDob, expectedGender, expectedOccupation, expectedChildren);

            Assert.AreEqual(expectedName, person.Name);
            Assert.AreEqual(expectedDob, person.DateOfBirth);
            Assert.AreEqual(expectedGender, person.Gender);
            Assert.AreEqual(expectedOccupation, person.Occupation);
            Assert.AreEqual(expectedChildren, person.NumberOfChildren);
        }
    }
}
