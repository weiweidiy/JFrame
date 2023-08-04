using JFrame.Advertisement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NSubstitute;



namespace JFrameTest
{
    [TestClass]
    public class TestAdManager
    {
        

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        /// <summary>
        /// 测试注册一个广告对象，
        /// </summary>
        [TestMethod]
        
        public void TestRegistAdItem()
        {
            //Arrange
            var manager = new AdsManager();
            var adItem = Substitute.For<IAdItem>();

            //Act
            manager.RegisterAdItem(adItem);


            //Assert
            Assert.AreEqual(1, manager.GetAdItemsCount());
        }
    }
}
