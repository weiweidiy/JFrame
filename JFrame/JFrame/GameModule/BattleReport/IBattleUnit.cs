using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 战斗单元接口
    /// </summary>
    public interface IBattleUnit
    {
        /// <summary>
        /// 行动时主动事件
        /// </summary>
        //event Action<IBattleUnit, IBattleAction, List<IBattleUnit>> onActionTriggerOn;
        event Action<IBattleUnit, IBattleAction, List<IBattleUnit>, float> onActionCast; //执行效果之前，只有首目标
        event Action<IBattleUnit, IBattleAction, float> onActionStartCD;
        //event Action<IBattleUnit, IBattleAction, IBattleUnit> onActionHitTarget; //执行效果之后消息，每个命中目标调用1次

        /// <summary>
        /// 被动事件
        /// </summary>
        event Action<IBattleUnit, IBattleAction, IBattleUnit, ExecuteInfo> onDamaged; //受到伤害之后
        event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onHealed;        //回血
        event Action<IBattleUnit, IBattleAction, IBattleUnit> onDead;        //死亡
        event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onRebord;        //复活
        event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onMaxHpUp;
        event Action<IBattleUnit, IBattleAction, IBattleUnit, int> onDebuffAnti;    //状态抵抗

        event Action<IBattleUnit, IBuffer> onBufferAdded;
        event Action<IBattleUnit, IBuffer> onBufferRemoved;
        event Action<IBattleUnit, IBuffer> onBufferCast;

        void Update(BattleFrame frame);

        string UID { get; }

        /// <summary>
        /// 名字
        /// </summary>
        string Name { get; }

        #region 属性
        /// <summary>
        /// 当前攻击力
        /// </summary>
        int Atk { get;  }

        /// <summary>
        /// 攻击力提升，返回实际提升的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int AtkUpgrade(int value);
        /// <summary>
        /// 攻击力降低
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int AtkReduce(int value);

        /// <summary>
        /// 攻击速度
        /// </summary>
        float AtkSpeed { get; set; } 

        /// <summary>
        /// 当前生命值
        /// </summary>
        int HP { get; }

        /// <summary>
        /// 最大生命值
        /// </summary>
        int MaxHP { get; }

        /// <summary>
        /// 最大生命提升
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int MaxHPUpgrade(int value);

        /// <summary>
        /// 暴击率 0~1的值 百分比
        /// </summary>
        float Cri { get; }

        /// <summary>
        /// //暴击伤害加成百分比
        /// </summary>
        float CriDmgRate { get; }
        /// <summary>
        /// //暴击伤害抵抗百分比
        /// </summary>
        float CriDmgAnti { get; }
        /// <summary>
        /// //技能伤害加成百分比
        /// </summary>
        float SkillDmgRate { get; }
        /// <summary>
        /// //技能伤害抵抗百分比
        /// </summary>
        float SkillDmgAnti { get; }
        /// <summary>
        /// //伤害加成百分比
        /// </summary>
        float DmgRate { get; }
        /// <summary>
        /// //伤害抵抗百分比
        /// </summary>
        float DmgAnti { get; } 
        /// <summary>
        /// //0~1异常状态命中百分比
        /// </summary>
        float DebuffHit { get; }
        /// <summary>
        /// //0~1异常状态抵抗百分比
        /// </summary>
        float DebuffAnti { get; }
        /// <summary>
        /// //穿透 0~1 百分比
        /// </summary>
        float Penetrate { get; }
        /// <summary>
        ///  //格挡 0~1 百分比
        /// </summary>
        float Block { get; }    



        #endregion
        /// <summary>
        /// 受到伤害了
        /// </summary>
        /// <param name="damage"></param>
        void OnDamage(IBattleUnit caster, IBattleAction action, ExecuteInfo damage);

        /// <summary>
        /// 受到治疗了
        /// </summary>
        /// <param name="heal"></param>
        void OnHeal(IBattleUnit caster, IBattleAction action, ExecuteInfo heal);

        /// <summary>
        /// 复活了
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="heal"></param>
        void OnReborn(IBattleUnit caster, IBattleAction action, ExecuteInfo heal);

        /// <summary>
        /// 生命上限增加
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="value"></param>
        void OnMaxHpUp(IBattleUnit caster, IBattleAction action, ExecuteInfo hp);

        /// <summary>
        /// 抵抗控制
        /// </summary>
        void OnDebuffAnti(IBattleUnit caster, IBattleAction action, int debuffId);

        /// <summary>
        /// 是否活着
        /// </summary>
        /// <returns></returns>
        bool IsAlive();

        /// <summary>
        /// 是否满血
        /// </summary>
        /// <returns></returns>
        bool IsHpFull();

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

