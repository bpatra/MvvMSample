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
    public class ColumnManagement_DragAndDrop_With_A_Non_Block_DataSource_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_With_A_Non_Block_DataSource_Tests() :base()
        {
            _dropInfo = new Mock<IDropInfo>(MockBehavior.Strict); //nazi mock
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col2, Col4 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(0);
        }

        [TestMethod]
        public void Then_Nothing_ShouldHappen_In_DragOver()
        {
            ColumnManagementViewModel.DragOver(_dropInfo.Object);
        }

        [TestMethod]
        public void Then_Nothing_ShouldHappen_In_Drop()
        {
            ColumnManagementViewModel.Drop(_dropInfo.Object);
        }
    }

    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Below_The_Target_InsertIndex_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Below_The_Target_InsertIndex_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col1 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(1);

            ColumnManagementViewModel.Drop(_dropInfo.Object);

        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[3].FullName);
        }

    }

    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Above_The_Target_InsertIndex_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Above_The_Target_InsertIndex_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col1 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(0);
            ColumnManagementViewModel.Drop(_dropInfo.Object);

        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[3].FullName);
        }
    }

    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Last_Target_InsertIndex_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Last_Target_InsertIndex_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col4 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(4);

            ColumnManagementViewModel.Drop(_dropInfo.Object);

        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[3].FullName);
        }

    }

    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Last_Target_InsertIndex2_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Last_Target_InsertIndex2_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col4 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(3);

            ColumnManagementViewModel.Drop(_dropInfo.Object);

          
        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Not_Be_Reorderer()
        {
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[3].FullName);
        }
    }


    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Below_The_Target_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Below_The_Target_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col1 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(2);

            ColumnManagementViewModel.Drop(_dropInfo.Object);
        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Be_Reorderer()
        {
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[3].FullName);
        }

    }

    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Below_The_Target3_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Below_The_Target3_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col1 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(3);

            ColumnManagementViewModel.Drop(_dropInfo.Object);
        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Be_Reorderer()
        {

            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[3].FullName);
        }
    }

    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Below_The_Target2_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Below_The_Target2_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col1 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(4);

            ColumnManagementViewModel.Drop(_dropInfo.Object);
        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Be_Reorderer()
        {
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[3].FullName);
        }

    }


    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Above_The_Target_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Above_The_Target_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col4 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(2);

            ColumnManagementViewModel.Drop(_dropInfo.Object);
        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Be_Reorderer()
        {
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[3].FullName);
        }

    }

    [TestClass]
    public class ColumnManagement_DragAndDrop_Simple_Block_DataSource_Above_The_Target2_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_Simple_Block_DataSource_Above_The_Target2_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col4 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(0);

            ColumnManagementViewModel.Drop(_dropInfo.Object);
        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Be_Reorderer()
        {
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[3].FullName);
        }
    }


    [TestClass]
    public class ColumnManagement_DragAndDrop_With_A_Contiguous_Block_DataSource_Below_The_Target_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_With_A_Contiguous_Block_DataSource_Below_The_Target_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col3, Col4 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(1);

            ColumnManagementViewModel.Drop(_dropInfo.Object);
        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Be_Reorderer()
        {
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[3].FullName);
        }
    }

    [TestClass]
    public class ColumnManagement_DragAndDrop_With_A_Contiguous_Block_DataSource_Above_The_Target_Tests : ColumnManagement_DragAndDrop
    {
        private Mock<IDropInfo> _dropInfo;
        public ColumnManagement_DragAndDrop_With_A_Contiguous_Block_DataSource_Above_The_Target_Tests()
        {
            _dropInfo = new Mock<IDropInfo>();
            _dropInfo.SetupGet(m => m.Data).Returns(new[] { Col1, Col2 });
            _dropInfo.SetupGet(m => m.InsertIndex).Returns(4);

            ColumnManagementViewModel.Drop(_dropInfo.Object);
        }

        [TestMethod]
        public void Then_After_Drop_The_Columns_Should_Be_Reorderer()
        {

            Assert.AreEqual("Col3", ColumnManagementViewModel.FootballClubs[0].FullName);
            Assert.AreEqual("Col4", ColumnManagementViewModel.FootballClubs[1].FullName);
            Assert.AreEqual("Col1", ColumnManagementViewModel.FootballClubs[2].FullName);
            Assert.AreEqual("Col2", ColumnManagementViewModel.FootballClubs[3].FullName);
        }
    }


    public abstract class ColumnManagement_DragAndDrop
    {

        protected ChampionshipBetViewModel ColumnManagementViewModel;

        protected IFootballClub Col1;
        protected IFootballClub Col2;
        protected IFootballClub Col3;
        protected IFootballClub Col4;

        protected ColumnManagement_DragAndDrop()
        {
            var col1 = new Mock<IFootballClub>();
            col1.Setup(m => m.FullName).Returns("Col1");
            Col1 = col1.Object;

            var col2 = new Mock<IFootballClub>();
            col2.Setup(m => m.FullName).Returns("Col2");
            Col2 = col2.Object;

            var col3 = new Mock<IFootballClub>();
            col3.Setup(m => m.FullName).Returns("Col3");
            Col3 = col3.Object;

            var col4 = new Mock<IFootballClub>();
            col4.Setup(m => m.FullName).Returns("Col4");
            Col4 = col4.Object;

            var colection = new ObservableCollection<IFootballClub>(new[] { col1.Object, col2.Object, col3.Object, col4.Object });
            var processor = new Mock<IChampionship>();
            processor.SetupGet(c => c.UserBet).Returns(colection);
           
            ColumnManagementViewModel = new ChampionshipBetViewModel(processor.Object);

        }
    }
}
