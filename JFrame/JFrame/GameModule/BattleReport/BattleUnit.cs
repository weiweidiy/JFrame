using System.Collections.Generic;

namespace JFrame
{

    public class BattleUnit
    {
        List<IBattleAction> actions = new List<IBattleAction>();

        public BattleUnit(BattleUnitInfo info)
        {

        }

        public void Update(BattleFrame frame)
        {
            foreach (var action in actions)
            {
                action.Update(frame);
            }
        }
    }
}