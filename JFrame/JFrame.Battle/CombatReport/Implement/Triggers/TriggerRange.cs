//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;

//namespace JFrame
//{

//    /// <summary>
//    /// 根據距離觸發 参数：0 查找数量 1：队伍参数 0=友军 1=敌军 2 攻击距离
//    /// </summary>
//    public abstract class TriggerRange : TriggerFindUnits
//    {
//        protected TriggerRange(CombatBaseFinder finder) : base(finder)
//        {
//        }

//        public override int GetValidArgsCount()
//        {
//            return 3;
//        }

//        /// <summary>
//        /// 獲取攻擊距離              
//        /// </summary>
//        /// <returns></returns>
//        /// <exception cref="Exception"></exception>
//        protected float GetAtkRange()
//        {
//            return GetCurArg(2);
//        }

//        /// <summary>
//        /// 获取获得坐标
//        /// </summary>
//        /// <returns></returns>
//        float GetMyX()
//        {
//            return _extraData.Caster.GetPosition().x;
//        }



//        /// <summary>
//        /// 获取指定队伍里的单位
//        /// </summary>
//        /// <param name="teamId"></param>
//        /// <returns></returns>
//        protected override List<CombatUnit> GetUnitsByTargetTeam(int teamId)
//        {
//            return context.CombatManager.GetUnitsInRange(_extraData.Caster, teamId, GetAtkRange(), true, true);
//        }


//        /// <summary>
//        /// 按条件排序数组
//        /// </summary>
//        /// <param name="arr"></param>
//        /// <param name="myXPosition"></param>
//        protected abstract void SortList(List<CombatUnit> lst, float myXPosition);

//        /// <summary>
//        /// 排序筛选
//        /// </summary>
//        /// <param name="lst"></param>
//        protected override void Filter(List<CombatUnit> lst)
//        {
//            var myX = GetMyX();

//            SortList(lst, myX);
//        }


//    }
//}