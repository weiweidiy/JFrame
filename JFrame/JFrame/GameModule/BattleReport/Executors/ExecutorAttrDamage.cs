﻿namespace JFrame
{
    /// <summary>
    /// 根据自身属性给与目标伤害 type 17
    /// </summary>
    public class ExecutorAttrDamage : ExecutorDamage
    {
        PVPAttribute  attrType;
        public ExecutorAttrDamage(FormulaManager formulaManager, float[] args) : base(formulaManager, args)
        {
            if (args != null && args.Length >= 5)
            {
                attrType = (PVPAttribute)args[4];
            }
            else
            {
                throw new System.Exception(this.GetType().ToString() + " 参数数量不对 缺少伤害倍率参数");
            }
        }

        public override float GetValue(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            switch(attrType)
            {
                case PVPAttribute.ATK:
                    return caster.Atk * arg;
                case PVPAttribute.HP:
                    return caster.HP * arg;
                case PVPAttribute.MaxHP:
                    return caster.MaxHP * arg;
                default:
                    throw new System.Exception("ExecutorAttrDamage没有实现pvp属性" + attrType);
            }
        }
    }

}
