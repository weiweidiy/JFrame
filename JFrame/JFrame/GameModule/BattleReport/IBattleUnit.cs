using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 战斗单元接口
    /// </summary>
    public interface IBattleUnit
    {
        event Action<IBattleUnit, IBattleAction, List<IBattleUnit>> onActionTriggerOn;
        event Action<IBattleUnit, IBattleAction, IBattleUnit> onActionCast;
        event Action<IBattleUnit, IBattleAction, IBattleUnit> onActionDone;

        event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onDamage;
        event Action<IBattleUnit, IBattleAction, IBattleUnit> onDead;

        string UID { get; }

        string Name { get; }

        /// <summary>
        /// 当前攻击力
        /// </summary>
        int Atk { get; }

        /// <summary>
        /// 当前生命值
        /// </summary>
        int HP { get; set; }

        /// <summary>
        /// 最大生命值
        /// </summary>
        int MaxHP { get; }

        /// <summary>
        /// 受到伤害了
        /// </summary>
        /// <param name="damage"></param>
        void OnDamage(IBattleUnit caster, IBattleAction action, int damage);

        /// <summary>
        /// 受到治疗了
        /// </summary>
        /// <param name="heal"></param>
        void OnHeal(int heal);


        /// <summary>
        /// 是否活着
        /// </summary>
        /// <returns></returns>
        bool IsAlive();
    }
}

