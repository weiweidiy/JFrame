using JFrame.Common;
using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 距離觸發器，如果距離内有單位則觸發 目標最近的單位 type = 2 参数：0 攻击距离 1 查找数量
    /// </summary>
    public class TriggerRangeFartest : TriggerRange
    {
        protected Utility utility = new Utility();

        protected override void SortList(CombatUnit[] arr, float myXPosition)
        {
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
                var unit1 = x as CombatUnit;
                var unit2 = y as CombatUnit;

                if (Math.Abs(myX - unit1.GetPosition().x) > Math.Abs(myX - unit2.GetPosition().x))
                    return -1;

                if (Math.Abs(myX - unit1.GetPosition().x) < Math.Abs(myX - unit2.GetPosition().x))
                    return 1;

                return 0;
            }
        }
    }
}