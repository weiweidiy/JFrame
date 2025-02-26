using JFrame.Common;
using System;

namespace JFrame
{
    /// <summary>
    /// type = 2 普通傷害公式
    /// </summary>
    public class FormulaDamage : CombatBaseFormula
    {
        Utility utility = new Utility();
        public override double GetHitValue(CombatExtraData extraData)
        {
            var caster = extraData.Caseter;
            var target = extraData.Target;

            var atk = (double)extraData.Caseter.GetAttributeCurValue(CombatAttribute.ATK);//攻擊屬性
            var actiongRate = extraData.Value; //技能參數
            var cri = (double)extraData.Caseter.GetAttributeCurValue(CombatAttribute.Critical);
            var criAnti = (double)extraData.Target.GetAttributeCurValue(CombatAttribute.CriticalAnti);
            var criDamage = (double)extraData.Caseter.GetAttributeCurValue(CombatAttribute.CriticalDamage);
            var dmgAdvance = (double)extraData.Caseter.GetAttributeCurValue(CombatAttribute.DamageAdvance);
            var dmgAnti = (double)extraData.Target.GetAttributeCurValue(CombatAttribute.DamageAnti);

            //暴擊判定
            bool isCri = true;
            if (cri - criAnti <= 0)
                isCri = false;
            else
                isCri = utility.RandomHit((float)(cri - criAnti) * 100);

            extraData.IsCri = isCri;

            long damage = (long)(atk * actiongRate);

            if (isCri)
                damage = (long)(damage * (float)(1 + criDamage));

            var dmgRate = (dmgAdvance - dmgAnti);
            dmgRate = Math.Max(-1, dmgRate); //不能小于-1，否则伤害小于0了

            return damage * (1 + dmgRate);
        }

        public override int GetValidArgsCount()
        {
            return 0;
        }
    }

}