
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 发动者自身百分x的生命为数值，添加给目标， 参数  1：执行段数，2：延迟执行 3: 段数间隔  4 ：自身HP比率  type = 5
    /// </summary>
    public class ExecutorMaxHpUp : ExecutorDamage
    {
        int hpValue;

        public ExecutorMaxHpUp(float[] args) : base(args)
        {
        }

        public override void OnAttach(IBattleAction action)
        {
            base.OnAttach(action);

            hpValue = (int)(action.Owner.MaxHP * arg);
        }

        /// <summary>
        /// 获取需要增加的生命上限
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public override float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            return hpValue;
        }


        public override void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets)
        {
            foreach(var target in targets)
            {
                target.MaxHPUpgrade((int)GetValue(caster, action , target));
            }
            
        }


    }
}