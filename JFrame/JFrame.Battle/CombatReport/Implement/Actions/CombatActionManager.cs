namespace JFrame
{
    public class CombatActionManager : BaseContainer<CombatAction>, ICombatUpdatable
    {
        public void Update(BattleFrame frame)
        {
            foreach (var action in GetAll())
            {
                action.Update(frame);
            }
        }
    }

}