namespace JFrame
{

    /// <summary>
    /// 战斗单位数据源
    /// </summary>
    public abstract class CombatUnitDataSource : CombatActionDataSource
    {
        public abstract string GetUid();
        public abstract int GetId();
        public abstract long GetHp();
        public abstract long GetMaxHp();
        public abstract long GetAtk();
        public abstract CombatVector GetPosition();
        public abstract CombatVector GetVelocity();
        public abstract CombatVector GetTargetPosition();

    }
}