namespace JFrame.UI
{
    /// <summary>
    /// 可分层 设置父子层级的接口
    /// </summary>
    public interface ILayerable
    {
        ILayerable GetParent();

        void AddParent(ILayerable parent);

        void RemoveParent();

        ILayerable[] GetChildren();

        ILayerable GetChild(string uid);

        void AddChild(ILayerable child);

        void RemoveChild(string uid);
    }
}