using JFrame.Common;
using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 距離觸發器，如果距離内有單位則觸發 目標最近的單位 type = 1
    /// </summary>
    public class TriggerRangeNearest : TriggerRange
    {
        protected Utility utility = new Utility();

        protected override void SortList(ICombatUnit[] arr, float myXPosition)
        {
            utility.BinarySort<ICombatUnit>(arr, new Compare(myXPosition));
        }

        /// <summary>
        /// 按距離由近到遠
        /// </summary>
        class Compare : IComparer<ICombatUnit>
        {
            float myX;
            public Compare(float myX)
            {
                this.myX = myX;
            }

            int IComparer<ICombatUnit>.Compare(ICombatUnit x, ICombatUnit y)
            {
                var unit1 = x as CombatUnit;
                var unit2 = y as CombatUnit;

                if (Math.Abs(myX - unit1.GetPosition().x) > Math.Abs(myX - unit2.GetPosition().x))
                    return 1;

                if (Math.Abs(myX - unit1.GetPosition().x) < Math.Abs(myX - unit2.GetPosition().x))
                    return -1;

                return 0;
            }
        }
    }
}