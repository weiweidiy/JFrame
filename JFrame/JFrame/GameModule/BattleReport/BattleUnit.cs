using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace JFrame
{
    public class BattleUnit : IBattleUnit
    {
        /// <summary>
        /// 有动作准备完毕，可以释放了
        /// </summary>
        public event Action<IBattleUnit, IBattleAction, List<IBattleUnit>> onActionReady;

        /// <summary>
        /// 所有动作列表
        /// </summary>
        List<IBattleAction> actions = null;

        public BattleUnit(BattleUnitInfo info, List<IBattleAction> actions)
        {
            this.actions = actions;      
            foreach(var action in actions) {
                action.onReady += Action_onReady;
            }
        }

        /// <summary>
        /// 动作已经准备好了
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Action_onReady(IBattleAction action, List<IBattleUnit> targets)
        {
            onActionReady?.Invoke(this, action, targets);
        }

        /// <summary>
        /// 更新帧了
        /// </summary>
        /// <param name="frame"></param>
        public void Update(BattleFrame frame)
        {
            foreach (var action in actions)
            {
                action.Update(frame);
            }

            //Console.WriteLine("unit update");
        }

        public bool IsAlive()
        {
            throw new NotImplementedException();
        }
    }
}