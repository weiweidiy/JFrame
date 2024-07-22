using System;
using System.Collections.Generic;

namespace JFrame
{
    public interface IBattleAction
    {


        /// <summary>
        /// 可以触发了
        /// </summary>
        event Action<IBattleAction, List<IBattleUnit>> onTriggerOn;

        /// <summary>
        /// 触发了，群体也只会返回首目标
        /// </summary>
        event Action<IBattleAction, IBattleUnit> onStartCast;

        /// <summary>
        /// 释放完成，每一个目标都会触发1次
        /// </summary>
        event Action<IBattleAction, IBattleUnit> onHitTarget; 

        string Name { get; }

        int Id { get; }

        void Update(BattleFrame frame);

        void Cast(IBattleUnit caster, List<IBattleUnit> units, IBattleReporter reporter);

        /// <summary>
        /// 设置这个动作是否可触发
        /// </summary>
        /// <param name="active"></param>
        void SetEnable(bool active);
    }
}