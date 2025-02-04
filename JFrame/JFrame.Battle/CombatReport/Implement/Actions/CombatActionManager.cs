namespace JFrame
{
    public class CombatActionManager : BaseContainer<CombatAction>, ICombatUpdatable
    {
        bool isBusy;

        public void Initialize()
        {
            foreach(var action in GetAll())
            {
                action.onTriggerOn += Action_onTriggerOn;
            }
        }

        private void Action_onTriggerOn(CombatExtraData extraData)
        {
            if (!isBusy && extraData.Action.Mode == ActionMode.Active)
            {

            }
        }

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