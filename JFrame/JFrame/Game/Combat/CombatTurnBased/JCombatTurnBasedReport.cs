using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedReportData
    {
        public string winnerTeamUid { get; set; }
        public List<JCombatTurnBasedEvent> events { get; set; }
    }


    public class JCombatTurnBasedReport : IJCombatTurnBasedReport
    {
        List<JCombatTurnBasedEvent> events;

        IJCombatTeam winner;

        public void SetCombatEvents(List<JCombatTurnBasedEvent> events) => this.events = events;

        public void SetCombatWinner(IJCombatTeam team) => this.winner = team;

        public JCombatTurnBasedReportData GetCombatReportData()
        {
            var data = new JCombatTurnBasedReportData();

            data.winnerTeamUid = winner?.Uid ?? null;
            data.events = events;

            return data;
        }
    }
}
