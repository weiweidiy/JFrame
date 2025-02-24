using JFrame.Common;

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
            var criDamage = (double)extraData.Target.GetAttributeCurValue(CombatAttribute.CriticalDamage);

            //暴擊判定
            bool isCri = true;
            if (cri - criAnti <= 0)
                isCri = false;
            else
                isCri = utility.RandomHit((float)(cri - criAnti) * 100);



            extraData.IsCri = isCri;



            long damage = 0;

            if (isCri)
                damage = (long)(damage * (float)(1 + criDamage));

            return damage;
        }

        public override int GetValidArgsCount()
        {

            return 0;
        }
    }

    /// <summary>
    /// type = 1 单一属性公式： 参数0：阵营（0=释放者，1=目标）， 参数1：属性ID
    /// </summary>
    public class FormulaSingleAttr :  CombatBaseFormula
    {
        public override int GetValidArgsCount()
        {
            return 2;
        }

        int GetTeamArg()
        {
            return (int)GetCurArg(0);
        }

        int GetAttrTypeArg()
        {
            return (int)GetCurArg(1);
        }

        public override double GetHitValue(CombatExtraData extraData)
        {
            var teamArg = GetTeamArg();
            CombatUnit unit = null;
            switch (teamArg)
            {
                case 0:
                    unit = extraData.Owner;
                    break;
                case 1:
                    unit = extraData.Target;
                    break;
                default:
                    throw new System.Exception($"{GetType()}没有实现队伍参数 {teamArg}");
            }

            if (unit == null)
                throw new System.Exception($"{GetType()} 没有找到对应的unit目标，无法计算数值");

            var attrId = GetAttrTypeArg();
            var type = (CombatAttribute)attrId;
            return (double)unit.GetAttributeCurValue(type) * extraData.Value; //這個value是執行器參數
        }

        
    }

}