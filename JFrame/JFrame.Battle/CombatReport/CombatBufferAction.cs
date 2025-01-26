namespace JFrame
{
    public class CombatBufferAction : CombatAction, IAttachable<ICombatBuffer>
    {
        public ICombatBuffer Owner => throw new System.NotImplementedException();

        public void OnAttach(ICombatBuffer target)
        {
            throw new System.NotImplementedException();
        }

        public void OnDetach()
        {
            throw new System.NotImplementedException();
        }
    }




}