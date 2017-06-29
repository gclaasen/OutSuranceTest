using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class DataSortingTest
    {
        [TestMethod]
        public void SortSurnameByFrequency()
        {
            OutSuranceBComp.Worker.CreateSortedDataFilesFromCSVFile client = new OutSuranceBComp.Worker.CreateSortedDataFilesFromCSVFile();
            client.CreateSortedDataFiles();
        }
    }
}
