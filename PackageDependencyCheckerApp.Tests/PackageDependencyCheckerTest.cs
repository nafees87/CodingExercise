using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PackageDependencyCheckerApp.Tests
{
    [TestClass]
    public class PackageDependencyCheckerTest
    {

        [TestMethod]
        public void SampleInputOutputOne()
        {
            var input = "[ \"KittenService: CamelCaser\", \"CamelCaser:\" ]";

            var output = "CamelCaser, KittenService";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result,output);
        }

        [TestMethod]
        public void SampleInputOutputTwo()
        {
            var input = "[\"KittenService: \",\"Leetmeme: Cyberportal\",\"Cyberportal: Ice\",\"CamelCaser: KittenService\",\"Fraudstream: Leetmeme\",\"Ice: \"]";

            var output = "KittenService, CamelCaser, Ice, Cyberportal, Leetmeme, Fraudstream";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }
        [TestMethod]
        public void SampleInvalidInputOutputThree()
        {
            var input = "[\"KittenService: \",\"Leetmeme: Cyberportal\",\"Cyberportal: Ice\",\"CamelCaser: KittenService\",\"Fraudstream: \",\"Ice: Leetmeme\"]";

            var output = "Possible Looping of Dependencies (or) Dirty Input. Please check input";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }

        [TestMethod]
        public void InvalidEmptyInput()
        {
            var input = "";

            var output = "Please enter a input";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }

        [TestMethod]
        public void InvalidNoSquareBrackets()
        {
            var input = "sample: 1,sample: 2";

            var output = "Square Brackets format missing";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }

        [TestMethod]
        public void InvalidSquareBracketsNotInProperLocation()
        {
            var input = "[sample: 1,]sample: 2";

            var output = "Input should start with [ and end with ]";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }
        [TestMethod]
        public void InvalidMoreThanOneSetOfSquareBrackets()
        {
            var input = "[sample: 1,]sample: 2]";

            var output = "Input cannot have multiple [ and ] ";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }
        [TestMethod]
        public void ValidNoCommaSeperatedParams()
        {
            var input = "[sample: ]";

            var output = "sample";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }
        [TestMethod]
        public void InValidNoCommaSeperatedParams()
        {
            var input = "[sample: 1 sample: 2]";

            var output = "Parameters not seperated by comma";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }
        [TestMethod]
        public void InValidEmptyPackageNames()
        {
            var input = "[sample: 1,,,sample: 2]";

            var output = "Empty Package Names. Please check input";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }
        [TestMethod]
        public void InValidEmptyPackageNamesLeft()
        {
            var input = "[: 1,sample: 2]";

            var output = "Package Name missing on left of :";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }
        [TestMethod]
        public void InValidEmptyPackageNamesRight()
        {
            var input = "[sample:,sample: 2]";

            var output = "Package Name missing on right of : (or) replace a black space on right of : ";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.AreEqual(result, output);
        }

        [TestMethod]
        public void InValidPackageNamesMultipleColons()
        {
            var input = "[sample:1,sample:: 2]";

            var output = "Package name pair can have only one colon.";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.IsTrue(result.StartsWith(output));
        }

        [TestMethod]
        public void InValidPackageNamesMandatoryOneColons()
        {
            var input = "[sample 1,sample:2]";

            var output = "Packages must be seperated by a colon";

            var myclass = new PackageDependencyChecker();

            var result = myclass.Processor(input);

            Assert.IsTrue(result.StartsWith(output));
        }
    }
}
