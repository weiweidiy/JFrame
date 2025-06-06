namespace JFramework.Common
{
    public abstract class JFrameworkNetMessage : IUnique
    {
        public abstract string Uid { get; }
        public string MessageType => GetType().Name;
    }
}
