using JFrame.Common;
using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// finder基類 0：攻擊距離 1: 查找個數
    /// </summary>
    public abstract class CombatBaseFinder : BaseActionComponent, ICombatFinder
    {
        /// <summary>
        /// 幫助類
        /// </summary>
        Utility utility = new Utility();

        public abstract List<CombatUnit> FindTargets(CombatExtraData extraData);

        /// <summary>
        /// 獲取距離最近的n個單位()
        /// </summary>
        /// <returns></returns>
        public List<CombatUnit> GetNearestOppoUnitInRange(CombatExtraData extraData, int count)
        {
            var reslut = new List<CombatUnit>();
            var oppoTeamId = context.CombatManager.GetOppoTeamId(extraData.SourceUnit);
            //獲取所有攻擊範圍内的單位
            var oppoUnits = context.CombatManager.GetUnitsInRange(extraData.SourceUnit, oppoTeamId, GetAtkRange(), true, true);

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
            utility.BinarySort<CombatUnit>(arr, new Compare(myX));

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

            return GetCurArg(0);
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

            return (int)GetCurArg(1);
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
                    return 1;

                if (Math.Abs(myX - unit1.GetPosition().x) < Math.Abs(myX - unit2.GetPosition().x))
                    return -1;

                return 0;
            }
        }
    }
}