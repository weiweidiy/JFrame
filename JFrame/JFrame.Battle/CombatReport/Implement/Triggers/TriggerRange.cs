using System;
using System.Collections;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 根據距離觸發 参数：0 攻击距离 1 查找数量
    /// </summary>
    public abstract class TriggerRange : CombatBaseTrigger
    {
        /// <summary>
        /// 獲取距離最近的n個單位(存活的)
        /// </summary>
        /// <returns></returns>
        protected List<CombatUnit> GetOppoUnitInRange(CombatUnit sourceUnit, int count)
        {
            var reslut = new List<CombatUnit>();
            var oppoTeamId = context.CombatManager.GetOppoTeamId(sourceUnit);
            //獲取所有攻擊範圍内的單位
            var oppoUnits = context.CombatManager.GetUnits(sourceUnit, oppoTeamId, GetAtkRange(), true, true);
          
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

        protected abstract void SortList(CombatUnit[] arr, float myXPosition);

        /// <summary>
        /// 獲取攻擊距離              
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected float GetAtkRange()
        {
            return GetCurArg(0);
        }

        /// <summary>
        /// 獲取查找數量
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected int GetFindAmount()
        {
            return (int)GetCurArg(1);
        }


        public override void Update(BattleFrame frame)
        {
            var targets = GetOppoUnitInRange(_extraData.SourceUnit as CombatUnit, GetFindAmount());

            if (targets != null && targets.Count > 0)
            {
                _extraData.Targets = targets;
                SetOn(true);
            }
        }



    }
}