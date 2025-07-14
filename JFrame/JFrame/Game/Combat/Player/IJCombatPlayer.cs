namespace JFramework.Game
{
    public interface IJCombatPlayer
    {
        void Play(JCombatTurnBasedReportData reportData);

        void RePlay();

        void Stop();

        void SetScale(float scale);

        float GetScale();
    }
}
