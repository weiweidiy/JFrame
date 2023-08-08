using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using JFrame.UI;
using NUnit.Framework;

namespace JFrameTest
{
    class TestViewBinder
    {


        [Test]
        public void TestBinderGameObject()
        {
            //Arrange
            var go = Substitute.For<IGameObject>();
            var view = Substitute.For<IView>();
            var binder = Substitute.For<ViewBinder>();
            binder.Create<IView>().Returns(view);

            //Act
            view = binder.BindView<IView> (go);

            //Assert
            view.Received().Bind(go);
        }
    }
}
