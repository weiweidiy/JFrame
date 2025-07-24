namespace JFramework.Game
{
    public class JCombatFormationInfo<T> where T: JCombatUnitInfo
    {
        public int Point { get; set; }
        public T UnitInfo { get; set; }
    }
}
