using System.Collections.Generic;

namespace JFrame
{

    public class BattleTeam
    {
        Dictionary<int, BattleUnit> units = new Dictionary<int, BattleUnit>();

        public BattleTeam(Dictionary<int, BattleUnit> units)
        {
            this.units = units;
        }

        public void Update(BattleFrame frame)
        {
            foreach (var unit in units.Values)
            {
                unit.Update(frame);
            }
        }

        public int GetUnitCount()
        {
            return units.Count;
        }
    }
}