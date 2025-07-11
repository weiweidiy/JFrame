namespace JFramework.Game
{
    public interface IJCombatPlayer
    {
        void Play(IJCombatReport report);

        void RePlay();

        void Stop();

        void SetScale(float scale);

        float GetScale();
    }
}
