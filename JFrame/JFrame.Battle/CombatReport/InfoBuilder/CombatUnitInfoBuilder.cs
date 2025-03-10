using JFrame.Common.Interface;

namespace JFrame
{
    /// <summary>
    /// 构建combatunit builder
    /// </summary>
    public class CombatUnitInfoBuilder : CombatActionInfoBuilder<CombatUnitInfo>
    {

        public CombatUnitDataSource DataSource { get; set; }

        public CombatUnitInfoBuilder(CombatActionArgSourceBuilder actionArgBuilder, ILogger logger) : base(actionArgBuilder, logger)
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
            unitInfo.mainType = DataSource.GetUnitMainType();
            unitInfo.unitSubType = DataSource.GetUnitSubType();
            unitInfo.hp = DataSource.GetHp();
            unitInfo.atk = DataSource.GetAtk();
            unitInfo.maxHp = DataSource.GetMaxHp();
            unitInfo.bpDamage = DataSource.GetBpDamage();
            unitInfo.bpDamageAnti = DataSource.GetBpDamageAnti();
            unitInfo.cri = DataSource.GetCri();
            unitInfo.criAnti = DataSource.GetCriAnti();
            unitInfo.criDamage = DataSource.GetCriDamage();
            unitInfo.controlHit = DataSource.GetControlHit();
            unitInfo.controlAnti = DataSource.GetControlAnti();
            unitInfo.damageAdvance = DataSource.GetDamageAdvance();
            unitInfo.damageAnti = DataSource.GetDamageAnti();
            unitInfo.hit = DataSource.GetHit();
            unitInfo.dodge = DataSource.GetDodge();
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