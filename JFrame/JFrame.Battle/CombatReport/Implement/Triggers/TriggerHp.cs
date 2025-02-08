using System.Collections.Generic;
using System;
using JFrame.Common;

namespace JFrame
{
    /// <summary>
    /// 查找hp百分比小的 type=4 参数 0 ： 查找数量  1： 队伍id
    /// </summary>
    public class TriggerHp : TriggerFindUnits
    {
        protected Utility utility = new Utility();

        protected override List<CombatUnit> GetUnitsByTargetTeam(int teamId)
        {
            return context.CombatManager.GetUnits(teamId, true);
        }

        protected override void SortList(List<CombatUnit> lst)
        {
            utility.BinarySort<CombatUnit>(lst, new Compare()); //按血量高低排序
            for(int i = lst.Count - 1; i >=0; i --)
            {
                var unit = lst[i];
                if(unit.IsHpFull())
                    lst.RemoveAt(i);
            }
        }

        class Compare : IComparer<CombatUnit>
        {
            int IComparer<CombatUnit>.Compare(CombatUnit x, CombatUnit y)
            {
                var unit1 = x;
                var unit2 = y;

                var unit1Hp = (long)unit1.GetAttributeCurValue(PVPAttribute.HP);
                var unit2Hp = (long)unit2.GetAttributeCurValue(PVPAttribute.HP);

                var unit1HpMax = (long)unit1.GetAttributeMaxValue(PVPAttribute.HP);
                var unit2HpMax = (long)unit2.GetAttributeMaxValue(PVPAttribute.HP);

                if ((double)unit1Hp / unit1HpMax < (double)unit2Hp / unit2HpMax)
                    return -1;

                if ((double)unit1Hp / unit1HpMax > (double)unit2Hp / unit2HpMax)
                    return 1;

                return 0;
            }
        }

        //protected Utility utility = new Utility();



        //protected override void SortList(CombatUnit[] arr, float myXPosition)
        //{
        //    //按血量排序
        //    utility.BinarySort<CombatUnit>(arr, new Compare(myXPosition));

        //}

        //class Compare : IComparer<CombatUnit>
        //{
        //    float myX;
        //    public Compare(float myX)
        //    {
        //        this.myX = myX;
        //    }

        //    int IComparer<CombatUnit>.Compare(CombatUnit x, CombatUnit y)
        //    {
        //        var unit1 = x;
        //        var unit2 = y;

        //        var unit1Hp = (long)unit1.GetAttributeCurValue(PVPAttribute.HP);
        //        var unit2Hp = (long)unit2.GetAttributeCurValue(PVPAttribute.HP);

        //        if (unit1Hp > unit2Hp)
        //            return -1;

        //        if (unit1Hp < unit2Hp)
        //            return 1;

        //        return 0;
        //    }
        //}
    }
}