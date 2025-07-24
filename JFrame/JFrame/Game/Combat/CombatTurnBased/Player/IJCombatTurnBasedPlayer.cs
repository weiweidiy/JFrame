namespace JFramework.Game
{
    public interface IJCombatTurnBasedPlayer<T> : IJCombatPlayer where T : JCombatUnitData, new()
    {
        void Play(JCombatTurnBasedReportData<T> reportData);
    }
}
