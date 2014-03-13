using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GongSolutions.Wpf.DragDrop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvMSample.Models;
using MvvMSample.ViewModels;
using System.Linq;
using System.Linq.Expressions;

namespace MvvMSampleTests
{
    [TestClass]
    public class ChampionshipBetViewModel_DragAndDrop_Tests
    {
        private readonly ChampionshipBetViewModel _championshipBetViewModel;
        private readonly Mock<IChampionship> _championShip;

        private readonly IFootballClub _club1;
        private readonly IFootballClub _club2;
        private readonly IFootballClub _club3;
        private readonly IFootballClub _club4;

        private IFootballClub GetMockClub(int i)
        {
            var club1 = new Mock<IFootballClub>();
            club1.Setup(m => m.FullName).Returns("Club"+i);
            return club1.Object;
        }

        public ChampionshipBetViewModel_DragAndDrop_Tests()
        {
            _club1 = GetMockClub(1);
            _club2 = GetMockClub(2);
            _club3 = GetMockClub(3);
            _club4 = GetMockClub(4);

            _championShip = new Mock<IChampionship>();
            _championShip.SetupGet(c => c.UserBet).Returns(new List<IFootballClub>(new[] { _club1, _club2, _club3, _club4}));
            _championshipBetViewModel = new ChampionshipBetViewModel(_championShip.Object);
        }

        [TestMethod]
        public void DragAndDrop_With_A_Non_Block_Source_Then_Nothing_ShouldHappen_In_DragOver()
        {
            var dropInfo = new Mock<IDropInfo>(MockBehavior.Strict); //nazi mock
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club2, _club4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club2, _club4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(0);
            _championshipBetViewModel.Drop(dropInfo.Object);
        }

        [TestMethod]
        public void Simple_Block_Source_Below_The_Target_InsertIndex_Then_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(3);

            _championshipBetViewModel.Drop(dropInfo.Object);

            var resultClubs = _championshipBetViewModel.FootballClubs.Select(x => x.FullName).ToArray();
            CollectionAssert.AreEqual(new[] { "Club1", "Club2", "Club3", "Club4" }, resultClubs);
        }

        [TestMethod]
        public void When_Source_Is_FirstRow_And_Target_Second_Then_Drop_Should_Reorder()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club1 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(2);

            _championshipBetViewModel.Drop(dropInfo.Object);

            var resultClubs = _championshipBetViewModel.FootballClubs.Select(x=>x.FullName).ToArray();
            CollectionAssert.AreEqual(new[] { "Club2", "Club1", "Club3", "Club4" }, resultClubs);
        }

        [TestMethod]
        public void When_Source_Is_Block_LastTwoRows_And_Target_The_Second_Row_Then_Drop_ShouldNot_Modify_Order()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club3, _club4 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(2);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[3].FullName);
        }

        [TestMethod]
        public void Simple_Block_Source_Below_The_Target3_Then_Drop_The_Columns_Should_Be_Reorderer()
        {
            var dropInfo = new Mock<IDropInfo>();
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club1 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club3, _club4 });
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
            dropInfo.SetupGet(m => m.Data).Returns(new[] { _club1, _club2 });
            dropInfo.SetupGet(m => m.InsertIndex).Returns(4);

            _championshipBetViewModel.Drop(dropInfo.Object);

            Assert.AreEqual("Club3", _championshipBetViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Club4", _championshipBetViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Club1", _championshipBetViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Club2", _championshipBetViewModel.FootballClubs[3].FullName);
        }
    }
}
