namespace JFrame
{
    /// <summary>
    /// type = 1 单一属性公式： 参数0：阵营（0=释放者，1=目标）， 参数1：属性ID
    /// </summary>
    public class SingleAttrFormula :  CombatBaseFormula
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

        public override double GetBaseValue(CombatExtraData extraData)
        {
            var teamArg = GetTeamArg();
            CombatUnit unit = null;
            switch (teamArg)
            {
                case 0:
                    unit = extraData.Caster;
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
            var type = (PVPAttribute)attrId;
            return (double)unit.GetAttributeCurValue(type);
        }

        
    }

}