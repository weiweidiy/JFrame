
using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 改变属性执行器：参数 4：属性id  参数5：改变百分比
    /// </summary>
    public class ExecutorAttrChange : NormalExecutor
    {
        PVPAttribute attrType;
        float arg;

        //真正改变的值
        float valueChanged;

        /// <summary>
        /// 发生属性改变的目标列表
        /// </summary>
        List<IBattleUnit> changedTargets = new List<IBattleUnit>();
        public ExecutorAttrChange(FormulaManager formulaManager, float[] args) : base(formulaManager, args)
        {
            if (args != null && args.Length >= 5)
            {
                attrType = (PVPAttribute)((int)args[3]);
                arg = args[4];
            }
            else
            {
                throw new System.Exception(this.GetType().ToString() + " 参数数量不对");
            }
        }

        public override void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets, object arg = null)
        {
            foreach (var target in targets)
            {
                var value = GetValue(caster, action, target);
                if (value > 0)
                    valueChanged = UpgradeValue(target, Math.Abs(value));
                else
                    valueChanged = ReduceValue(target, Math.Abs(value));

                changedTargets.Add(target);
            }
        }

        public override void OnDetach()
        {
            base.OnDetach();

            foreach (var t in changedTargets)
            {
                var value = GetValue(null, null, t);
                if (value > 0)
                    ReduceValue(t, valueChanged);
                else
                    UpgradeValue(t, valueChanged);
            }
        }

        protected float UpgradeValue(IBattleUnit target, float value)
        {
            switch (attrType)
            {
                case PVPAttribute.ATK:
                    {
                        return target.AtkUpgrade((int)value);
                    }
                case PVPAttribute.HP: return 0;
                case PVPAttribute.AtkSpeed: return 0;
                case PVPAttribute.Critical: return 0;
                case PVPAttribute.CriticalDamage: return 0;
                case PVPAttribute.CriticalDamageResist: return 0;
                case PVPAttribute.ControlHit: return 0;
                case PVPAttribute.ControlResistance: return 0;
                case PVPAttribute.DamageEnhance: return 0;
                case PVPAttribute.DamageReduce: return 0;
                case PVPAttribute.SkillDamageEnhance:
                    {
                        return target.SkillDamageEnhanceUpgrade(value);
                    }
                case PVPAttribute.SkillDamageReduce: return 0;
                case PVPAttribute.Block: return 0;
                case PVPAttribute.Puncture: return 0;
                default:
                    throw new Exception("没有实现pvp属性 " + attrType);
            }
        }

        protected float ReduceValue(IBattleUnit target, float value)
        {
            switch (attrType)
            {
                case PVPAttribute.ATK:
                    {
                        return target.AtkReduce((int)value);
                    }
                case PVPAttribute.HP: return 0;
                case PVPAttribute.AtkSpeed: return 0;
                case PVPAttribute.Critical: return 0;
                case PVPAttribute.CriticalDamage: return 0;
                case PVPAttribute.CriticalDamageResist: return 0;
                case PVPAttribute.ControlHit: return 0;
                case PVPAttribute.ControlResistance: return 0;
                case PVPAttribute.DamageEnhance: return 0;
                case PVPAttribute.DamageReduce: return 0;
                case PVPAttribute.SkillDamageEnhance:
                    {
                        return target.SkillDamageEnhanceReduce(value);
                    }
                case PVPAttribute.SkillDamageReduce: return 0;
                case PVPAttribute.Block: return 0;
                case PVPAttribute.Puncture: return 0;
                default:
                    throw new Exception("没有实现pvp属性 " + attrType);
            }
        }

        protected virtual float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            float attrValue;
            switch (attrType)
            {
                case PVPAttribute.ATK:
                    {
                        attrValue = target.Atk;
                    }
                    break;
                case PVPAttribute.HP:
                    {
                        attrValue = target.HP;
                    }
                    break;
                case PVPAttribute.Critical:
                    {
                        attrValue = target.Critical;
                    }
                    break;
                case PVPAttribute.CriticalDamage:
                    {
                        attrValue = target.CriticalDamage;
                    }
                    break;
                case PVPAttribute.CriticalDamageResist:
                    {
                        attrValue = target.CriticalDamageResist;
                    }
                    break;
                case PVPAttribute.DamageEnhance:
                    {
                        attrValue = target.DamageEnhance;
                    }
                    break;
                case PVPAttribute.DamageReduce:
                    {
                        attrValue = target.DamageReduce;
                    }
                    break;
                case PVPAttribute.SkillDamageEnhance:
                    {
                        attrValue = target.SkillDamageEnhance;
                    }
                    break;
                case PVPAttribute.SkillDamageReduce:
                    {
                        attrValue = target.SkillDamageReduce;
                    }
                    break;
                case PVPAttribute.Block:
                    {
                        attrValue = target.Block;
                    }
                    break;
                case PVPAttribute.Puncture:
                    {
                        attrValue = target.Puncture;
                    }
                    break;
                default:
                    throw new Exception("没有实现pvp属性 " + attrType);

            }

            return attrValue * arg * Owner.GetFoldCount();
        }

        //protected virtual float CalcCD(float originValue)
        //{
        //    return originValue / (1 + arg * Owner.GetFoldCount());
        //}
        //float AtkSpeedUpgrade()
        //{
        //    var result = 0f;
        //    var actions = Owner.Owner.GetActions();
        //    foreach (var action in actions)
        //    {
        //        if (action.Type == ActionType.Normal) //普通攻击
        //        {
        //            var cdTrigger = action.GetCDTrigger();
        //            var cdTimeTrigger = cdTrigger as CDTimeTrigger;
        //            if (cdTimeTrigger != null)
        //            {
        //                var args = cdTimeTrigger.GetArgs();
        //                var originValue = args[0];

        //                var cd = CalcCD(originValue);
        //                result = originValue - cd;
        //                args[0] = cd;
        //                cdTimeTrigger.SetArgs(args);

        //                //Debug.LogError(target.Name + "OnAttach new cd " + cdTimeTrigger.GetArgs()[0]);
        //            }
        //        }
        //    }
        //    return result;
        //}
    }

}


///// <summary>
///// 伤害效果 参数  1：执行段数，2：延迟执行 3: 段数间隔  4 ：伤害倍率  type = 1
///// </summary>
//public class ExecutorDamage : BaseExecutor
//{ 
//    /// <summary>
//    /// 伤害倍率
//    /// </summary>
//    protected float arg = 1f;


//    /// <summary>
//    /// 伤害效果，1：执行段数，2：延迟执行 3: 段数间隔 4：伤害倍率
//    /// </summary>
//    /// <param name="args"></param>
//    public ExecutorDamage(FormulaManager formulaManager, float[] args):base(formulaManager, args)
//    {
//        if (args != null && args.Length >= 4)
//        {
//            arg = args[3];
//        }
//        else
//        {
//            throw new System.Exception( this.GetType().ToString() + " 参数数量不对 缺少伤害倍率参数" );
//        }
//    }

//    /// <summary>
//    /// 执行命中
//    /// </summary>
//    /// <param name="caster"></param>
//    /// <param name="targets"></param>
//    /// <param name="reporter"></param>
//    public override void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets)
//    {
//        foreach(var target in targets) {

//            bool isCri = false;
//            bool isBlock = false;
//            var baseValue = (int)GetValue(caster, action, target);

//            int dmg = 0;
//            var owner = Owner as IBattleAction;

//            if(owner.Type == ActionType.Normal) //普通攻击
//                dmg = formulaManager.GetNormalDamageValue(baseValue, caster, action, target, out isCri, out isBlock);
//            else
//                dmg = formulaManager.GetSkillDamageValue(baseValue, caster, action, target, out isCri);

//            var info = new ExecuteInfo() { Value = (int)dmg, IsCri = isCri, IsBlock = isBlock };

//            //广播，可以改变这个值
//            NotifyHitTarget(target,info);

//            target.OnDamage(caster, action, info);

//            OnTargetHit(caster, action, target, info);
//        }

//    }

//    protected virtual void OnTargetHit(IBattleUnit caster, IBattleAction action, IBattleUnit target, ExecuteInfo info) { }

//    /// <summary>
//    /// 获取执行效果的值
//    /// </summary>
//    /// <param name="caster"></param>
//    /// <param name="action"></param>
//    /// <param name="actionArg">动作加成值</param>
//    /// <param name="target"></param>
//    /// <param name="isCri"></param>
//    /// <param name="isBlock"></param>
//    /// <returns></returns>
//    public virtual float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
//    {
//        return caster.Atk * arg;

//    }
//}
