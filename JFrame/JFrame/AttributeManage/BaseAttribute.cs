namespace JFrame.AttributeManage
{
    public abstract class BaseAttribute
    {
        public string Uid { get; private set; }
        public int Id { get; private set; }

        public AttributeType Type { get; private set; }

        public BaseAttribute(string uid, int id, AttributeType type)
        {
            this.Uid = uid;
            this.Id = id;
            this.Type = type;
        }
    }
}
