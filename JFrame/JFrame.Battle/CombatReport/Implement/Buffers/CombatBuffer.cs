namespace JFrame
{
    public class CombatBuffer : ICombatBuffer, ICombatUpdatable, IUnique
    {
        public string Uid { get; protected set; }
        public CombatUnit SourceUnit { get ; set ; }

        public void Update(BattleFrame frame)
        {
            throw new System.NotImplementedException();
        }
    }

}