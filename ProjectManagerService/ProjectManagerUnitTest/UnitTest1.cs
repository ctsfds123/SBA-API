using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;

namespace ProjectManagerUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private DAO dao = new DAO();

        [TestMethod]
        public void TestMethod1()
        {
            dao.TestData("Raj");
        }
    }
}
