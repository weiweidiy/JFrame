namespace JFramework.Game
{
    public abstract class JCombatPlayer : IJCombatPlayer
    {
        IJCombatReport report;

        float scale = 1f;

        public void Play(IJCombatReport report)
        {
            this.report = report;
        }

        public void RePlay()
        {
            throw new System.NotImplementedException();
        }

        public void SetScale(float scale)=> this.scale = scale;
        public float GetScale() => scale;
        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}
