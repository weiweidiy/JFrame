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

        public void SetAllActive(bool active)
        {
            if (active)
            {
                foreach (var action in GetAll())
                {
                    action.SwitchToDisable();
                }
            }
            else
            {
                foreach (var action in GetAll())
                {
                    action.SwitchToCd();
                }
            }
        }
    }

}