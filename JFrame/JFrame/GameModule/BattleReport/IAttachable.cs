namespace JFrame
{
    public interface IAttachable
    {
        IAttachOwner Owner { get; }

        void OnAttach(IAttachOwner target);

        void OnDetach();  
    }

    public interface IAttachOwner
    {
       
    }

}

