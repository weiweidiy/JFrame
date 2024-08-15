using System;

namespace JFrame
{
    public abstract class Buffer : IBuffer
    {

        public event Action<IBuffer> onCast;

        protected void NotifyOnCast(IBuffer buffer)
        {
            onCast?.Invoke(buffer);
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 唯一ID
        /// </summary>
        public virtual string Uid { get; private set; }

        /// <summary>
        /// 叠加层数
        /// </summary>
        public int FoldCount { get; private set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public float[] Args { get; set; }

        /// <summary>
        /// 目标对象
        /// </summary>
        protected IBattleUnit target;

        /// <summary>
        /// 释放者
        /// </summary>
        protected IBattleUnit caster;

        public Buffer(IBattleUnit caster, string UID, int id, int foldCount, float[] args)
        {
            Id = id;
            this.Uid = UID;
            this.Args = args;
            this.FoldCount = foldCount;
            this.caster = caster;
        }



        /// <summary>
        /// buffer是否有效（时间到了，或者数值消耗完了等等）
        /// </summary>
        public abstract bool IsValid();

        /// <summary>
        /// 被添加上时
        /// </summary>
        public virtual void OnAttach(IBattleUnit target)
        {
            this.target = target;
        }

        /// <summary>
        /// 被移除时
        /// </summary>
        public virtual void OnDettach()
        {

        }

        /// <summary>
        /// 更新帧
        /// </summary>
        public virtual void Update(BattleFrame frame)
        {

        }

        /// <summary>
        /// 添加层数
        /// </summary>
        /// <param name="foldCount"></param>
        public void AddFoldCount(int foldCount)
        {
            FoldCount += foldCount;
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

//public Buffer(int id, string uid, BufferTriggerType bufferType, float arg, float duration, int fold, int maxValue, IBattleUnit owner = null)
//{
//    Id = id;
//    Uid = uid;
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