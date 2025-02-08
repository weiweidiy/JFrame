using System.Collections.Generic;
using System;
using JFrame.Common;

namespace JFrame
{
    /// <summary>
    /// 根据HP的情况来触发 type=3 参数：0 攻击距离 1 查找数量 2：队伍id
    /// </summary>
    public class TriggerHp : TriggerRange
    {
        protected Utility utility = new Utility();

        public override void Update(BattleFrame frame)
        {
            //敌人的
            base.Update(frame);

            //自己的update
        }

        protected override void SortList(CombatUnit[] arr, float myXPosition)
        {
            //arr是敌人数组 按血量排序
            utility.BinarySort<CombatUnit>(arr, new Compare(myXPosition));

        }

        class Compare : IComparer<CombatUnit>
        {
            float myX;
            public Compare(float myX)
            {
                this.myX = myX;
            }

            int IComparer<CombatUnit>.Compare(CombatUnit x, CombatUnit y)
            {
                var unit1 = x;
                var unit2 = y;

                var unit1Hp = (long)unit1.GetAttributeCurValue(PVPAttribute.HP);
                var unit2Hp = (long)unit2.GetAttributeCurValue(PVPAttribute.HP);

                if (unit1Hp > unit2Hp)
                    return -1;

                if (unit1Hp < unit2Hp)
                    return 1;

                return 0;
            }
        }

    }
}