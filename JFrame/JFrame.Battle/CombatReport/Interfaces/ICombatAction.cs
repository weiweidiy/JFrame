using System.Collections.Generic;
using System;

namespace JFrame
{
    public interface ICombatAction 
    {
        #region 委托事件
        /// <summary>
        /// 通知actionmanager 可以释放了
        /// </summary>
        event Action<ICombatAction> onCanCast;
        /// <summary>
        /// 通知条件满足，可以释放
        /// </summary>
        void NotifyCanCast();

        /// <summary>
        /// 触发了，群体也只会返回首目标
        /// </summary>
        event Action<ICombatAction, List<ICombatUnit>, float> onStartCast;

        /// <summary>
        /// 通知开始释放
        /// </summary>
        void NotifyStartCast(List<ICombatUnit> targets, float duration);

        /// <summary>
        /// 开始进入冷却
        /// </summary>
        event Action<ICombatAction, float> onStartCD;

        /// <summary>
        /// 通知进入冷却
        /// </summary>
        /// <param name="cd"></param>
        void NotifyStartCD(float cd);

        /// <summary>
        /// 即将命中目标
        /// </summary>
        event Action<ICombatAction, ICombatUnit, ExecuteInfo> onHittingTarget;

        /// <summary>
        /// 已经命中
        /// </summary>
        event Action<ICombatAction, ICombatUnit, ExecuteInfo, ICombatUnit> onHittedComplete;

        #endregion

        #region 生命周期
        void OnStart(); //开始更新前只调用1次

        void OnEnable(); //触发时调用

        void Update(BattleFrame frame);

        void OnStop(); //停止更新

        #endregion

        //#region 属性
        ///// <summary>
        ///// 类型 区分普通动作和技能动作
        ///// </summary>
        //ActionType Type { get; }

        ///// <summary>
        ///// 动作模式：主动，被动
        ///// </summary>
        //ActionMode Mode { get; }

        //#endregion

        #region 状态切换
        /// <summary>
        /// 待机状态
        /// </summary>
        void Standby();

        /// <summary>
        /// 释放技能，返回释放周期
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="units"></param>
        /// <returns>施放持续时间</returns>
        float Cast();

        /// <summary>
        /// 进入CD
        /// </summary>
        float EnterCD();

        /// <summary>
        /// 设置这个动作是否可触发
        /// </summary>
        /// <param name="active"></param>
        void SetValid(bool active);

        #endregion

        #region 查询
        /// <summary>
        /// 是否冷却完成了
        /// </summary>
        /// <returns></returns>
        bool IsCDComplete();

        /// <summary>
        /// 是否正在执行
        /// </summary>
        bool IsExecuting();

        /// <summary>
        /// 是否满足了触发条件
        /// </summary>
        /// <returns></returns>
        bool CanCast();


        /// <summary>
        /// 获取执行周期
        /// </summary>
        /// <returns></returns>
        float GetCastDuration();

        /// <summary>
        /// 获取当前状态
        /// </summary>
        /// <returns></returns>
        string GetCurState();

        /// <summary>
        /// 获取冷却触发器
        /// </summary>
        /// <returns></returns>
        IBattleTrigger GetCDTrigger();
        #endregion

        #region 功能接口
        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <returns></returns>
        List<ICombatUnit> FindTargets(object[] args);

        /// <summary>
        /// 准备执行效果
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="targets"></param>
        void ReadyToExecute(ICombatUnit caster, ICombatAction action, List<ICombatUnit> targets);

        /// <summary>
        /// 打断
        /// </summary>
        void Interrupt();

        /// <summary>
        /// 设置是否可用
        /// </summary>
        void SetEnable(bool enable);

        #endregion

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="args"></param>
        void SetConditionTriggerArgs(float[] args);
        void SetFinderArgs(float[] args);
        void SetExecutorArgs(float[] args);
        void SetCdArgs(float[] args);
        /// <summary>
        /// 重置属性
        /// </summary>
        /// <param name="args"></param>
        void ResetConditionTriggerArgs(float[] args);
        void ResetFinderArgs(float[] args);
        void ResetExecutorArgs(float[] args);
        void ResetCdArgs(float[] args);

    }




}