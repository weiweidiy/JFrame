﻿using System;

namespace JFrame
{
    public enum BattleTriggerType
    {
        All,
        Normal,    
        AfterDead, //死亡后触发
    }

    public interface IBattleTrigger : IAttachable
    {

        event Action<IBattleTrigger, object[]> onTriggerOn;

        BattleTriggerType TriggerType { get; }

        void SetEnable(bool isOn);

        bool GetEnable();

        void Update(BattleFrame frame);

        //void OnAttach(IBattleAction action);

        void Restart();

        bool IsOn();

        /// <summary>
        /// 获取CD
        /// </summary>
        /// <returns></returns>
        float[] GetArgs();
        float[] GetOriginalArgs();
        /// <summary>
        /// 设置cd
        /// </summary>
        /// <param name="cd"></param>
        /// <exception cref="NotImplementedException"></exception>
        void SetArgs(float[] args);

        /// <summary>
        /// 设置无效
        /// </summary>
        void SetOn(bool isOn);

        /// <summary>
        /// 获取透传参数，比如触发的目标敌人，比如受到伤害时触发的伤害对象
        /// </summary>
        /// <returns></returns>
        object GetExtraArg();
    }

    //public interface IBattleTrigger
    //{
    //    BattleTriggerType TriggerType { get; }

    //    IBattleAction Owner { get; }

    //    void SetEnable(bool isOn);

    //    bool GetEnable();

    //    void Update(BattleFrame frame);

    //    void OnAttach(IBattleAction action);

    //    void Restart();

    //    bool IsOn();

    //    /// <summary>
    //    /// 获取CD
    //    /// </summary>
    //    /// <returns></returns>
    //    float[] GetArgs();

    //    /// <summary>
    //    /// 设置cd
    //    /// </summary>
    //    /// <param name="cd"></param>
    //    /// <exception cref="NotImplementedException"></exception>
    //    void SetArgs(float[] args);

    //    /// <summary>
    //    /// 设置无效
    //    /// </summary>
    //    void SetOn(bool isOn);
    //}
}