namespace JFrame
{
    public class CombatBufferManager : BaseContainer<CombatBuffer>, ICombatUpdatable
    {
        public void Update(BattleFrame frame)
        {
            foreach(var buffer in GetAll())
            {
                buffer.Update(frame);
            }
        }
    }

}