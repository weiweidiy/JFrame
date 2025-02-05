using System.Collections.Generic;

namespace JFrame
{
    public class CombatReport
    {

        public CommonCombatTeam attacker;
        public CommonCombatTeam defence;
        public List<ICombatReportData> report;
        public int winner;
    }
}