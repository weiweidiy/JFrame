namespace JFrame
{
    public interface IAttachable<TOwner>
    {
        TOwner Owner { get; }

        void OnAttach(TOwner target);

        void OnDetach();
    }




}