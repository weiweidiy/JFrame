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
        event Action<IBattleUnit, IBattleAction, IBattleUnit> onActionCast; //执行效果之前，只有首目标
        event Action<IBattleUnit, IBattleAction, IBattleUnit> onActionHitTarget; //执行效果之后消息，每个命中目标调用1次

        event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onDamage; //受到伤害之后
        event Action<IBattleUnit, IBattleAction, IBattleUnit> onDead;        //死亡

        event Action<IBattleUnit, IBuffer> onBufferAdded;
        event Action<IBattleUnit, IBuffer> onBufferRemoved;
        event Action<IBattleUnit, IBuffer> onBufferCast;

        void Update(BattleFrame frame);

        string UID { get; }

        string Name { get; }

        /// <summary>
        /// 当前攻击力
        /// </summary>
        int Atk { get; set; }

        /// <summary>
        /// 攻击力提升，返回实际提升的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int AtkUpgrade(int value);

        /// <summary>
        /// 当前生命值
        /// </summary>
        int HP { get; }

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

        /// <summary>
        /// 添加buffer
        /// </summary>
        /// <param name="bufferId"></param>
        /// <param name="foldCout"></param>
        /// <returns></returns>
        IBuffer AddBuffer(int bufferId, int foldCout = 1);

        /// <summary>
        /// 获取所有buffers
        /// </summary>
        /// <returns></returns>
        IBuffer[] GetBuffers();

        /// <summary>
        /// 移除buffer
        /// </summary>
        /// <param name="bufferId"></param>
        void RemoveBuffer(string bufferUID);
    }
}

