using System;
using System.Collections;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 根據距離觸發
    /// </summary>
    public abstract class TriggerRange : BaseTrigger
    {
        /// <summary>
        /// 獲取距離最近的n個單位()
        /// </summary>
        /// <returns></returns>
        protected List<ICombatUnit> GetOppoUnitInRange(CombatUnit sourceUnit, int count)
        {
            var reslut = new List<ICombatUnit>();
            var oppoTeamId = context.CombatManager.GetOppoTeamId(sourceUnit);
            //獲取所有攻擊範圍内的單位
            var oppoUnits = context.CombatManager.GetUnits(sourceUnit, oppoTeamId, GetAtkRange());
          
            //獲取距離最近的
            var myX = sourceUnit.GetPosition().x;
            var arr = oppoUnits.ToArray();
            //utility.BinarySort<ICombatUnit>(arr, new Compare(myX));
            SortList(arr, myX);

            //保證不能超過數組長度
            var finalCount = Math.Min(arr.Length, count);

            for (int i = 0; i < finalCount; i++)
            {
                reslut.Add(arr[i]);
            }

            return reslut;
        }

        protected abstract void SortList(ICombatUnit[] arr, float myXPosition);

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


        public override void Update(BattleFrame frame)
        {
            var targets = GetOppoUnitInRange(extraData.SourceUnit as CombatUnit, GetFindAmount());

            if (targets != null && targets.Count > 0)
            {
                extraData.Targets = targets;
                extraData.Action = Owner;
                SetOn(true);
            }
        }



    }
}