using JFrame.UI;
using NUnit.Framework;
using NSubstitute;

namespace JFrameTest
{
    public class TestUIManager
    {
        [Test]
        public void TestOpenUI()
        {
            //Arrange
            string prefabName = "login";
            int parent = 1;

            var instantiator = Substitute.For<IInstantiator<int>>();
            instantiator.Instantiate(prefabName, parent).Returns(2);
            var viewBinder = Substitute.For<IViewBinder>();
            var uiManager = Substitute.For<UIManager<int>>(instantiator, viewBinder);

            //Act
            var uiView = uiManager.Open<IUIView>(prefabName, parent);

            //Assert
            instantiator.Received().Instantiate(prefabName, parent);
            viewBinder.Received().BindView<IUIView, int>(2);
        }

        /// <summary>
        /// 测试正确打开，并正确的层级
        /// </summary>
        [Test]
        public void TestOpenUIAndCorrectRecorgenize()
        {
            //Arrange
            string prefabName = "login";
            int parent = 1;

            var instantiator = Substitute.For<IInstantiator<int>>();
            instantiator.Instantiate(prefabName, parent).Returns(2);
            var viewBinder = Substitute.For<IViewBinder>();
            var uiManager = Substitute.For<UIManager<int>>(instantiator, viewBinder);

            //Act
            var uiView = uiManager.Open<IUIView>(prefabName, parent);

            //Assert
            instantiator.Received().Instantiate(prefabName, parent);
            viewBinder.Received().BindView<IUIView, int>(2);
        }
    }
}
