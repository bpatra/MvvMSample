using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvMSample;
using MvvMSample.Models;
using MvvMSample.ViewModels;

namespace MvvMSampleTests
{
    [TestClass]
    public class PlayerSearchViewModelTests
    {
        [TestMethod]
        public void TestingTheSearchingCapabilitiesWithBedoya()
        {
            var viewModel = new PlayerSearchViewModel(new PlayerProvider());
            viewModel.SearchPlayerText = "Bedoya";
            var searchResult = viewModel.DisplayedPlayers.ToArray();
            Assert.AreEqual(1, searchResult.Length);
            IPlayer bedoya = searchResult[0];
            Assert.AreEqual("Nantes",bedoya.Club);
        }
    }
}
