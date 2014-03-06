using System;
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

        private readonly IFootballClub _col1;
        private readonly IFootballClub _col2;
        private readonly IFootballClub _col3;
        private readonly IFootballClub _col4;

        public ChampionshipBetViewModel_DragAndDrop_Tests()
        {
            var col1 = new Mock<IFootballClub>();
            col1.Setup(m => m.FullName).Returns("Club1");
            _col1 = col1.Object;

            var col2 = new Mock<IFootballClub>();
            col2.Setup(m => m.FullName).Returns("Club2");
            _col2 = col2.Object;

            var col3 = new Mock<IFootballClub>();
            col3.Setup(m => m.FullName).Returns("Club3");
            _col3 = col3.Object;

            var col4 = new Mock<IFootballClub>();
            col4.Setup(m => m.FullName).Returns("Club4");
            _col4 = col4.Object;

            var colection = new ObservableCollection<IFootballClub>(new[] { col1.Object, col2.Object, col3.Object, col4.Object });
            var championShip = new Mock<IChampionship>();
            championShip.SetupGet(c => c.UserBet).Returns(colection);

            _championshipBetViewModel = new ChampionshipBetViewModel(championShip.Object);
        }

        [TestMethod]
        public void DragAndDrop_With_A_Non_Block_Source_Then_Nothing_ShouldHappen_In_DragOver()
        {
            var dropInfo = new Mock<IDropInfo>(MockBehavior.Strict); //nazi mock
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col2, _col4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(0);
            _championshipBetViewModel.DragOver(dropInfo.Object);
        }

        [TestMethod]
        public void DragAndDrop_With_A_Non_Block_Source_Then_Nothing_ShouldHappen_In_Drop()
        {
            var dropInfo = new Mock<IDropInfo>(MockBehavior.Strict); //nazi mock
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col2, _col4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(0);
            _championshipBetViewModel.Drop(dropInfo.Object);
        }

        [TestMethod]
        public void Simple_Block_Source_Below_The_Target_InsertIndex_Then_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col3, _col4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _col1, _col2 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(4);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[3].FullName);
        }
    }
}
