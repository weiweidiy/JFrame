namespace JFrame
{
    /// <summary>
    /// 构建combatunit builder
    /// </summary>
    public class CombatUnitInfoBuilder : CombatActionInfoBuilder<CombatUnitInfo>
    {

        public CombatUnitDataSource DataSource { get; set; }

        public CombatUnitInfoBuilder(CombatActionArgSourceBuilder actionArgBuilder) : base(actionArgBuilder)
        {
        }

        /// <summary>
        /// 构建gjj战斗数据
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="maxHp"></param>
        /// <param name="atk"></param>
        /// <param name="atkSpeed"></param>
        /// <param name="position"></param>
        /// <param name="actionsData"></param>
        /// <returns></returns>
        public override CombatUnitInfo Build()
        {
            var unitInfo = new CombatUnitInfo();
            unitInfo.uid = DataSource.GetUid();
            unitInfo.id = DataSource.GetId();
            unitInfo.hp = DataSource.GetHp();
            unitInfo.atk = DataSource.GetAtk();
            unitInfo.maxHp = DataSource.GetMaxHp();
            unitInfo.position = DataSource.GetPosition();
            unitInfo.moveSpeed = DataSource.GetVelocity();
            unitInfo.targetPosition = DataSource.GetTargetPosition();
            var actionIds = DataSource.GetActions();
            var actionsData = CreateActions(actionIds);
            unitInfo.actionsData = actionsData;
            return unitInfo;
        }
    }
}