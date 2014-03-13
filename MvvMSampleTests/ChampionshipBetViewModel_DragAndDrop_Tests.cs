using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GongSolutions.Wpf.DragDrop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvMSample.Models;
using MvvMSample.ViewModels;

namespace MvvMSampleTests
{
    [TestClass]
    public class ChampionshipBetViewModel_DragAndDrop_Tests
    {
        private readonly ChampionshipBetViewModel _championshipBetViewModel;
        private readonly Mock<IChampionship> _championShip;

        private readonly IFootballClub _row1;
        private readonly IFootballClub _row2;
        private readonly IFootballClub _row3;
        private readonly IFootballClub _row4;

        public ChampionshipBetViewModel_DragAndDrop_Tests()
        {
            var row1 = new Mock<IFootballClub>();
            row1.Setup(m => m.FullName).Returns("Club1");
            _row1 = row1.Object;

            var row2 = new Mock<IFootballClub>();
            row2.Setup(m => m.FullName).Returns("Club2");
            _row2 = row2.Object;

            var row3 = new Mock<IFootballClub>();
            row3.Setup(m => m.FullName).Returns("Club3");
            _row3 = row3.Object;

            var row4 = new Mock<IFootballClub>();
            row4.Setup(m => m.FullName).Returns("Club4");
            _row4 = row4.Object;

            var initialList = new List<IFootballClub>(new[] { row1.Object, row2.Object, row3.Object, row4.Object });
            _championShip = new Mock<IChampionship>();
            _championShip.SetupGet(c => c.UserBet).Returns(initialList);

            _championshipBetViewModel = new ChampionshipBetViewModel(_championShip.Object);
        }

        [TestMethod]
        public void DragAndDrop_With_A_Non_Block_Source_Then_Nothing_ShouldHappen_In_DragOver()
        {
            var dropInfo = new Mock<IDropInfo>(MockBehavior.Strict); //nazi mock
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row2, _row4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(0);
            _championshipBetViewModel.DragOver(dropInfo.Object);
        }

        [TestMethod]
        public void When_Clicking_Save_Then_The_Football_List_Is_Passed_Back_To_The_Model()
        {
            _championshipBetViewModel.FootballClubs.Move(0,3);//the new order should be Club2, Club3, Club4, Club1
            _championshipBetViewModel.ClickSave.Execute(null);
            _championShip.VerifySet(c=>c.UserBet = It.Is<List<IFootballClub>>(x => x.Count==4 &&  x[0].FullName=="Club2" ), Times.Once);
        }

        [TestMethod]
        public void DragAndDrop_With_A_Non_Block_Source_Then_Nothing_ShouldHappen_In_Drop()
        {
            var dropInfo = new Mock<IDropInfo>(MockBehavior.Strict); //nazi mock
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row2, _row4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(0);
            _championshipBetViewModel.Drop(dropInfo.Object);
        }

        [TestMethod]
        public void Simple_Block_Source_Below_The_Target_InsertIndex_Then_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row1 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(1);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Simple_Block_Source_Above_The_Target_InsertIndex_Then_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row1 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(0);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[3].FullName);
        }


        [TestMethod]
        public void Simple_Block_Source_Last_Target_InsertIndex_Then_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(4);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Simple_Block_Source_Last_Target_InsertIndex2_Then_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(3);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Simple_Block_Source_Below_The_Target_Then_Drop_The_Columns_Should_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row1 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(2);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Simple_Block_Source_Below_The_Target3_Then_Drop_The_Columns_Should_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row1 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(3);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Simple_Block_Source_Below_The_Target2_Then_Drop_The_Columns_Should_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row1 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(4);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Simple_Block_Source_Above_The_Target_Then_Drop_The_Columns_Should_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(2);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Simple_Block_Source_Above_The_Target2_Then_Drop_The_Columns_Should_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(0);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Contiguous_Block_Source_Below_The_Target_Then_Drop_The_Columns_Should_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row3, _row4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(1);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Contiguous_Block_Source_Above_The_Target_Then_Drop_The_Columns_Should_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _row1, _row2 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(4);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[3].FullName);
        }
    }
}
