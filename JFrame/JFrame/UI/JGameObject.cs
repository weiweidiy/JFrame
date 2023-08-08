//using System.Collections.Generic;

//namespace JFrame.UI
//{
//    public class JGameObject<T> : IGameObject
//    {
//        public string Uid => throw new System.NotImplementedException();

//        List<IGameObject> _children = new List<IGameObject>();


//        IGameObject _parent;
//        public IGameObject Parent
//        {
//            get
//            {
//                return _parent;
//            }
//            set
//            {
//                if(value == null && _parent != null)
//                    _parent.RemoveChild(this);

//                _parent = value;

//                if(_parent != null)
//                    _parent.AddChild(this);
//            }

//        }

//        public List<IGameObject> Children => _children;

//        public void AddChild(IGameObject child)
//        {
//            _children.Add(child);
//        }

//        public IGameObject FindChild(string uid)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void RemoveChild(IGameObject child)
//        {
//            _children.Remove(child);
//        }
//    }
//}