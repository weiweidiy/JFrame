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
        /// 释放完成
        /// </summary>
        event Action<IBattleAction, IBattleUnit> onDone; //动作对象， 目标对象，值，buff

        string Name { get; }

        int Id { get; }

        void Update(BattleFrame frame);

        void Cast(IBattleUnit caster, List<IBattleUnit> units, BattleReporter reporter, string reportUID);
    }
}