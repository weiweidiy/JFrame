using System.Collections.Generic;

namespace JFrame
{
    public class CombatReport
    {

        public KeyValuePair<CombatTeamType, List<CombatUnitInfo>> attacker;
        public KeyValuePair<CombatTeamType, List<CombatUnitInfo>> defence;
        public List<ICombatReportData> report;
        public int winner;
    }
}