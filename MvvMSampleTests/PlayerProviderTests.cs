using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvMSample;
using MvvMSample.Models;

namespace MvvMSampleTests
{
    [TestClass]
    public class PlayerProviderTests
    {
        [TestMethod]
        public void PlayerWellRetrieved()
        {
            var playerProvider = new PlayerProvider();
            Assert.IsTrue(playerProvider.GetAllWorldCupPlayer().Any());
        }

        [TestMethod]
        public void FirtPlayerOk()
        {
            var playerProvider = new PlayerProvider();
            IPlayer firstPlayer = playerProvider.GetAllWorldCupPlayer().First();
            Assert.AreEqual("Jefferson", firstPlayer.Name);
        }
    }
}
