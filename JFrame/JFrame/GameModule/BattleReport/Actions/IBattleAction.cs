using System;
using System.Collections.Generic;

namespace JFrame
{
    public interface IBattleAction : IUnique
    {
        /// <summary>
        /// 通知可以释放了
        /// </summary>
        event Action<IBattleAction> onCanCast;

        /// <summary>
        /// 触发了，群体也只会返回首目标
        /// </summary>
        event Action<IBattleAction, List<IBattleUnit>> onStartCast;


        IBattleUnit Owner { get;  }

        string Name { get; }

        int Id { get; }

        void Update(BattleFrame frame);

        /// <summary>
        /// 待机状态
        /// </summary>
        void Standby();

        /// <summary>
        /// 释放技能，返回释放周期
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        float Cast();

        /// <summary>
        /// 进入CD
        /// </summary>
        void EnterCD();

        /// <summary>
        /// 设置这个动作是否可触发
        /// </summary>
        /// <param name="active"></param>
        void SetEnable(bool active);

        void OnAttach(IBattleUnit owner);

        /// <summary>
        /// 是否冷却完成了
        /// </summary>
        /// <returns></returns>
        bool IsCDComplete();

        /// <summary>
        /// 是否满足了触发条件
        /// </summary>
        /// <returns></returns>
        bool CanCast();

        /// <summary>
        /// 通知条件满足，可以释放
        /// </summary>
        void NotifyCanCast();

        ///// <summary>
        ///// 通知cd完成了
        ///// </summary>
        //void NotifyCdComplete();

        /// <summary>
        /// 通知开始释放
        /// </summary>
        void NotifyStartCast(List<IBattleUnit> targets);

        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <returns></returns>
        List<IBattleUnit> FindTargets();

        /// <summary>
        /// 准备执行效果
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="targets"></param>
        void ReadyToExecute(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets);

        /// <summary>
        /// 获取执行周期
        /// </summary>
        /// <returns></returns>
        float GetCastDuration();

        string GetCurState();
    }
}