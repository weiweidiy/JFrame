//using JFrame.UI;
//using NUnit.Framework;
//using NSubstitute;

//namespace JFrameTest
//{
//    public interface UnityGameObject
//    {
//        void AddChild(UnityGameObject go);
//    }

//    public class UnityUIManager : UIManager<UnityGameObject>
//    {
//        public UnityUIManager(IInstantiator<UnityGameObject> instantiator, IViewBinder<UnityGameObject> viewBinder) : base(instantiator, viewBinder) { }
//        protected override void SetRelationship(UnityGameObject parent, IUIView child)
//        {
//            //parent.AddChild(child);
//        }
//    }

//    public class TestUIManager
//    {


//        /// <summary>
//        /// 打开ui，并进行正确的调用
//        /// </summary>
//        [Test]
//        public void TestOpenUI()
//        {

//            //Arrange
//            string prefabName = "login";
//            var parent = Substitute.For<UnityGameObject>();
//            var view = Substitute.For<IUIView>();
//            var go = Substitute.For<UnityGameObject>();
//            //模拟实例化器
//            var instantiator = Substitute.For<IInstantiator<UnityGameObject>>();
//            instantiator.Instantiate(prefabName, parent).Returns(go);
//            //模拟绑定器
//            var viewBinder = Substitute.For<IViewBinder<UnityGameObject>>();
//            viewBinder.BindView<IUIView>(go).Returns(view);
//            //模拟ui管理器
//            var uiManager = Substitute.For<UIManager<UnityGameObject>>(instantiator, viewBinder);

//            //Act
//            uiManager.Open<IUIView>(prefabName, parent);

//            //Assert
//            instantiator.Received().Instantiate(prefabName, parent);
//            viewBinder.Received().BindView<IUIView>(go);
//        }

//        /// <summary>
//        /// 测试正确打开，并正确的父节点和子节点
//        /// </summary>
//        [Test]
//        public void TestOpenUIAndCorrectRecorgenize()
//        {
//            //Arrange
//            string prefabName = "login";
//            var view = Substitute.For<IUIView>();
//            var go = Substitute.For<UnityGameObject>();
//            var parent = Substitute.For<UnityGameObject>();   
            
            
//            //模拟实例化器
//            var instantiator = Substitute.For<IInstantiator<UnityGameObject>>();
//            instantiator.Instantiate(prefabName, parent).Returns(go);
//            //模拟绑定器
//            var viewBinder = Substitute.For<IViewBinder<UnityGameObject>>();
//            viewBinder.BindView<IUIView>(go).Returns(view);
//            //模拟ui管理器
//            var uiManager = Substitute.For<UIManager<UnityGameObject>>(instantiator, viewBinder);


//            //Act
//            var ui = uiManager.Open<IUIView>(prefabName, parent);

//            //Assert
//            //view.Received().Parent = parent;
//            //parent.Received().AddChild(view);
//        }
//    }
//}
