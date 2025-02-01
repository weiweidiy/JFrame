using JFrame.Common;
using System;
using System.Collections.Generic;

namespace JFrame
{
    public abstract class FinderBase : BaseActionComponent, ICombatFinder
    {
        /// <summary>
        /// 幫助類
        /// </summary>
        Utility utility = new Utility();

        public abstract List<ICombatUnit> FindTargets(CombatExtraData extraData);

        /// <summary>
        /// 獲取距離最近的n個單位
        /// </summary>
        /// <returns></returns>
        public List<ICombatUnit> GetNearestOppoUnit(CombatExtraData extraData, int count)
        {
            var reslut = new List<ICombatUnit>();
            var oppoTeamId = context.CombatManager.GetOppoTeamId(extraData.SourceUnit);
            var oppoUnits = context.CombatManager.GetUnits(extraData.SourceUnit, oppoTeamId, GetAtkRange());

            CombatUnit selfUnit = null;
            if (Owner is CombatUnitAction)
            {
                var action = Owner as CombatUnitAction;
                selfUnit = action.Owner;
            }
            else //是一個buffaction
            {
                var action = Owner as CombatBufferAction;
                var buffer = action.Owner;
                selfUnit = buffer.SourceUnit;

            }
            //獲取距離最近的
            var myX = selfUnit.GetPosition().x;
            var arr = oppoUnits.ToArray();
            utility.BinarySort<ICombatUnit>(arr, new Compare(myX));

            //保證不能超過數組長度
            var finalCount = Math.Min(arr.Length, count);

            for (int i = 0; i < finalCount; i++)
            {
                reslut.Add(arr[i]);
            }

            return reslut;
        }

        /// <summary>
        /// 獲取攻擊距離              
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected float GetAtkRange()
        {
            if (curArgs.Length < 1)
                throw new Exception(GetType().ToString() + " curArgs 數量小於1個");

            return curArgs[0];
        }

        /// <summary>
        /// 獲取查找數量
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected int GetFindAmount()
        {
            if (curArgs.Length < 2)
                throw new Exception(GetType().ToString() + " curArgs 數量小於2個");

            return (int)curArgs[1];
        }

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
    /// <summary>
    /// 找对手1个单位
    /// </summary>
    public class FinderOneOppo : FinderBase
    {
        public override List<ICombatUnit> FindTargets(CombatExtraData extraData)
        {
            var lst = GetNearestOppoUnit(extraData, GetFindAmount());
            return lst;
        }

        
    }
}