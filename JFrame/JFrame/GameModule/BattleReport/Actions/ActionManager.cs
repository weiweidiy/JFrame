using System.Collections.Generic;
using System;
using static System.Collections.Specialized.BitVector32;


namespace JFrame
{
    /// <summary>
    /// 动作管理器
    /// </summary>
    public class ActionManager : BaseContainer<IBattleAction>, IActionManager
    {
        event Action<IBattleAction, List<IBattleUnit>> onStartCast;

        public bool IsBusy { get; private set; }

        float curDuration = 1f;

        float deltaTime = 0f;

        /// <summary>
        /// 添加到管理器
        /// </summary>
        /// <param name="member"></param>
        public override void Add(IBattleAction member)
        {
            base.Add(member);
         
            member.onCanCast += Member_onCanCast;
            member.onStartCast += Member_onStartCast;     
        }

        /// <summary>
        /// 从管理器移除
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public override bool Remove(string uid)
        {
            var member = base.Get(uid);
            if (member != null)
            {
                member.onCanCast -= Member_onCanCast;
                member.onStartCast -= Member_onStartCast;
                //member.onCdComplete -= Member_onCdComplete;
                return base.Remove(uid);
            }
            else
                return false;
        }

        /// <summary>
        /// 更新帧
        /// </summary>
        /// <param name="frame"></param>
        public void Update(BattleFrame frame)
        {
            var actions = GetAll();
            foreach (var action in actions)
            {
                action.Update(frame);
            }

            UpdateDuration(frame);
        }

        /// <summary>
        /// 更新释放时间
        /// </summary>
        /// <param name="frame"></param>
        public void UpdateDuration(BattleFrame frame)
        {
            if(IsBusy)
            {
                deltaTime += frame.DeltaTime;

                if(deltaTime >= curDuration)
                {
                    IsBusy = false;
                    deltaTime = 0f;
                }
            }
        }
        

        /// <summary>
        /// 技能能释放了
        /// </summary>
        /// <param name="action"></param>
        private void Member_onCanCast(IBattleAction action)
        {
            //如果正在释放其他技能，则返回，否则立即释放
            if(!IsBusy)
            {
                curDuration = action.Cast();
                IsBusy = true;
            }
        }

        /// <summary>
        /// 技能已经释放了
        /// </summary>
        /// <param name="action"></param>
        /// <param name="targets"></param>
        private void Member_onStartCast(IBattleAction action, List<IBattleUnit> targets)
        {
            onStartCast?.Invoke(action,targets);
        }




    }
}