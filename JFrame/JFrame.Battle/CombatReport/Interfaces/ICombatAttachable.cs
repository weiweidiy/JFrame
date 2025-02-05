namespace JFrame
{
    public interface ICombatAttachable<TOwner>
    {
        TOwner Owner { get; }

        void OnAttach(TOwner target);

        void OnDetach();
    }




}