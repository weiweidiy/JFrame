using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 獲取所有單位時，只會返回特殊的單位
    /// </summary>
    public class SpecialCombatTeam : CommonCombatTeam
    {
        public override List<CombatUnit> GetUnits(bool mainTarget = false)
        {
            if (!mainTarget)
                return GetAll();

            var units = GetAll();
            if (units.Count > 0)
                return new List<CombatUnit>() { units[0] };

            return units;
        }

        /// <summary>
        /// 属性全部用主单位的
        /// </summary>
        /// <param name="context"></param>
        /// <param name="teamData"></param>
        protected override void CreateUnits(CombatContext context, List<CombatUnitInfo> teamData)
        {
            if (teamData == null || teamData.Count == 0)
                return;

            var actionFactory = new CombatActionFactory();
            var attrFactory = new CombatAttributeFactory();
            var buffFactory = new CombatBufferFactory();

            //主单位
            var mainUnit = teamData[0];
            var attributes = attrFactory.CreateAllAttributes(mainUnit);
            var attrManager = new CombatAttributeManger();
            attrManager.AddRange(attributes);

            //創建並初始化隊伍
            for (int i = 0; i < teamData.Count; i++)
            {
                var unitInfo = teamData[i];
                //創建並初始化戰鬥單位
                var unit = new CombatUnit();
                unit.Initialize(unitInfo.uid, context, actionFactory.CreateUnitActions(unitInfo.actionsData, unit, context), buffFactory.CreateBuffers(unitInfo.buffersData), attrManager);
                unit.SetPosition(unitInfo.position);
                unit.SetSpeed(unitInfo.moveSpeed);
                unit.SetTargetPosition(unitInfo.targetPosition);
                Add(unit);
            }
        }
    }
}