﻿using System;
using System.Collections.Generic;

namespace JFramework
{
    /// <summary>
    /// 周期buffer 参数 1：持续时间
    /// </summary>
    public class DurationBuffer : Buffer
    {
        /// <summary>
        /// 临时变量，记录流逝时间
        /// </summary>
        float deltaTime;

        /// <summary>
        /// buffer是否有效
        /// </summary>
        //bool isValid;

        /// <summary>
        /// 周期buffer , 第一个参数是周期时间
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="id"></param>
        /// <param name="foldCount"></param>
        /// <param name="args"></param>
        /// <exception cref="ArgumentException"></exception>
        public DurationBuffer(IBattleUnit caster, bool isBuff, int buffType, string UID, int id, int foldCount, float[] args, IBattleTrigger trigger, IBattleTargetFinder finder, List<IBattleExecutor> exutors) : base(caster, isBuff,buffType, UID, id, foldCount, args, trigger, finder, exutors)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentException("durationbuffer 参数不能为空 ，需要有个持续时间参数" + id);

        }


        ///// <summary>
        ///// 是否有效
        ///// </summary>
        ///// <returns></returns>
        //public override bool IsValid()
        //{
        //    return isValid;
        //}

        /// <summary>
        /// 获取周期
        /// </summary>
        /// <returns></returns>
        public override float GetDuration()
        {
            return Args[0];
        }

        /// <summary>
        /// 更新帧
        /// </summary>
        /// <param name="frame"></param>
        public override void Update(CombatFrame frame)
        {
            base.Update(frame);

            if (!isValid)
                return;

            deltaTime += frame.DeltaTime;

            if(deltaTime >= GetDuration())
            {
                isValid = false;
            }
        }

        /// <summary>
        /// 添加到unit上时
        /// </summary>
        /// <param name="unit"></param>
        public override void OnAttach(IBattleUnit unit)
        {
            base.OnAttach(unit);

            isValid = true;
        }

    }
}




///// <summary>
///// Id
///// </summary>
//public int Id { get; private set; }

///// <summary>
///// 唯一id
///// </summary>
//public string Uid { get; private set; }

///// <summary>
///// buff类型
///// </summary>
//public BufferTriggerType BufferType { get; private set; }

///// <summary>
///// 是否是BUFF
///// </summary>
//public bool IsBuff { get; protected set; }

///// <summary>
///// buff参数
///// </summary>
//public float Arg { get; set; }

///// <summary>
///// 层数
///// </summary>
//public int FoldCount { get; set; }

///// <summary>
///// 剩余周期
///// </summary>
//public float RestDuration { get; protected set; }

///// <summary>
///// buffer是否生效
///// </summary>
//bool isValid;

///// <summary>
///// 生命周期
///// </summary>
//protected float duration;

///// <summary>
///// 拥有者
///// </summary>
//protected IBattleUnit owner;

///// <summary>
///// 最大值
///// </summary>
//protected int maxValue;

//public Buffer(int id, string Uid, BufferTriggerType bufferType, float arg, float duration, int fold, int maxValue, IBattleUnit owner = null)
//{
//    Id = id;
//    Uid = Uid;
//    BufferType = bufferType;
//    Arg = arg;
//    FoldCount = fold;
//    this.owner = owner;
//    this.duration = duration;
//    this.maxValue = maxValue;

//    OnValid();

//    if (duration != -1)
//    {
//        //tweenerDuration = DotweenManager.Ins.DOTweenDelay(duration, 1, () =>
//        //{
//        //    if (isValid)
//        //        OnInValid();

//        //    onBuffInValid?.Invoke(this);
//        //});
//    }
//}

///// <summary>
///// 重置周期
///// </summary>
//public void Reset(float duration)
//{
//    this.duration = duration;
//    //tweenerDuration?.Kill();
//    if (duration != -1)
//    {
//        isValid = true;
//        //tweenerDuration = DotweenManager.Ins.DOTweenDelay(duration, 1, () =>
//        //{
//        //    if (isValid)
//        //        OnInValid();

//        //    onBuffInValid?.Invoke(this);
//        //});
//    }

//}


///// <summary>
///// buff生效（周期开始）
///// </summary>
//protected virtual void OnValid()
//{
//    isValid = true;
//}

///// <summary>
///// buff失效（周期到了）
///// </summary>
//protected virtual void OnInValid()
//{
//    isValid = false;
//}

///// <summary>
///// 值加成
///// </summary>
///// <param name="value"></param>
///// <returns></returns>
//public abstract int Buff(int value);

///// <summary>
///// 值加成
///// </summary>
///// <param name="value"></param>
///// <returns></returns>
//public abstract float Buff(float value);

//public virtual void Release()
//{
//    //tweenerDuration?.Kill();

//    if (isValid)
//        OnInValid();
//}