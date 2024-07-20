using System;

namespace JFrame
{
    /// <summary>
    /// 伤害效果
    /// </summary>
    public class BattleDamage : IBattleExecutor
    {

        public event Action onExecute;

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 攻击次数
        /// </summary>
        float count = 1;

        /// <summary>
        /// 攻击间隔
        /// </summary>
        float interval = 0.25f;

        /// <summary>
        /// 伤害倍率
        /// </summary>
        float dmgRate = 1f;

        /// <summary>
        /// 延迟命中
        /// </summary>
        float delay = 0f;

        /// <summary>
        /// 是否已经延迟过了
        /// </summary>
        bool delayed;
        /// <summary>
        /// 临时变量
        /// </summary>
        float delta;

        /// <summary>
        /// 临时计数
        /// </summary>
        int tempCount;

        /// <summary>
        /// 执行对象相关缓存
        /// </summary>
        IBattleUnit caster;
        IBattleAction action;
        IBattleUnit target;

        public BattleDamage(float[] args)
        {
            if (args != null && args.Length >= 4)
            {
                count = args[0];
                dmgRate = args[1];
                delay = args[2];
                interval = args[3];
            }
        }

        /// <summary>
        /// 执行命中
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="target"></param>
        /// <param name="reporter"></param>
        public void Hit(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            var dmg = GetValue(caster,action,target);
            //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
            target.OnDamage(caster, action, (int)dmg);
        }

        /// <summary>
        /// 获取执行效果的值
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            return caster.Atk * dmgRate;
        }

        /// <summary>
        /// 更新帧
        /// </summary>
        /// <param name="frame"></param>
        public void Update(BattleFrame frame)
        {
            if (!Active)
                return;

            delta += frame.DeltaTime;

            if (!delayed)
            {
                if (delta - delay > 0f)
                {
                    delta -= delay;
                    delayed = true;
                }
                return;
            }

            if (delta < interval)
                return;

            delta = 0f;
            //延迟完成了
            Hit(caster, action, target);

            tempCount++;

            if (tempCount >= count)
            {
                Active = false;
                delayed = false;
                tempCount = 0;
            }
        }


        /// <summary>
        /// 准备释放
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <exception cref="Exception"></exception>
        public void ReadyToExecute(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            if (Active)
                throw new Exception("执行器正在执行中，无法再次执行");

            //激活
            Active = true;

            this.caster = caster;
            this.action = action;
            this.target = target;
        }
    }
}