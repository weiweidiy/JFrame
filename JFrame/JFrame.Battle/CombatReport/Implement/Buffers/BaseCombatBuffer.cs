using System;

namespace JFrame
{

    public abstract class BaseCombatBuffer :  ICombatUpdatable, IUnique, IActionOwner, ICombatAttachable<CombatUnit>
    {
        public virtual string Uid { get; set; }

        public virtual int Id { get; protected set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public virtual bool Expired { get; protected set; }

        public virtual CombatBufferFoldType FoldType { get; protected set; }

        /// <summary>
        /// 最大叠加层数
        /// </summary>
        public int MaxFoldCount { get; protected set; }

        /// <summary>
        /// 透传数据，其中caster
        /// </summary>
        public CombatExtraData ExtraData { get; set; }

        public CombatUnit Owner { get; private set; }


        public abstract void Update(ComabtFrame frame);

        public abstract float GetDuration();

        /// <summary>
        /// 当前层数
        /// </summary>
        public abstract void SetCurFoldCount(int foldCount);
        public abstract int GetCurFoldCount();

        public void OnAttach(CombatUnit target) => Owner = target;

        public void OnDetach() => Owner = null;
    }

}