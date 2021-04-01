using Microsoft.VisualStudio.TestTools.UnitTesting;
using Routing;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace Routing.Tests
{
    [TestClass()]
    public class RouteAnalyzerTests
    {
        [TestMethod()]
        public void ProcessTestHappyPathScenario1()
        {
            var analyzer = new RouteAnalyzer();
            var result = analyzer.Process(new string[] { "1 -> 2", "2 -> 3", "3 -> 4"}).ToList();
            Assert.IsTrue(result.Count() == 1);
            Assert.AreEqual(result[0], "1 -> 2 -> 3 -> 4");
        }
        [TestMethod()]
        public void ProcessTestHappyPathScenario2()
        {
            var analyzer = new RouteAnalyzer();
            var result = analyzer.Process(new string[] { "1 -> 2", "2 -> 3", "4", "5 -> 6"}).ToList();
            Assert.IsTrue(result.Count() == 3);
            Assert.AreEqual(result[0], "1 -> 2 -> 3");
            Assert.AreEqual(result[1], "4");
            Assert.AreEqual(result[2], "5 -> 6");
        }
        [TestMethod()]
        public void ProcessTestHappyPathScenario3()
        {
            var analyzer = new RouteAnalyzer();
            var result = analyzer.Process(new string[] { "8 -> 9", "1 -> 3", "3 -> 4", "2 -> 3", "6 -> 7" }).ToList();
            Assert.IsTrue(result.Count() == 4);
            Assert.AreEqual(result[0], "8 -> 9");
            Assert.AreEqual(result[1], "1 -> 3 -> 4");
            Assert.AreEqual(result[2], "2 -> 3 -> 4");
            Assert.AreEqual(result[3], "6 -> 7");
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "invalid route.")]
        public void ProcessTestInvalidRouteScenario1()
        {
            var analyzer = new RouteAnalyzer();
            var result = analyzer.Process(new string[] { "1 -> 2", "1 -> 3", "2 -> 3" }).ToList();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "invalid route.")]
        public void ProcessTestInvalidRouteScenario2()
        {
            var analyzer = new RouteAnalyzer();
            var result = analyzer.Process(new string[] { "1 -> 2", "2", "2 -> 3" }).ToList();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Circular dependency detected")]
        public void ProcessTestCircularDependencyCheck()
        {
            var analyzer = new RouteAnalyzer();
            var result = analyzer.Process(new string[] { "1 -> 2", "2 -> 3", "3 -> 1" }).ToList();
        }
    }
}