namespace JFrame
{

    /// <summary>
    /// 属性计算器
    /// </summary>
    public abstract class CombatBaseFormula : BaseActionComponent , ICombatFormula 
    {
        public abstract double GetHitValue(CombatExtraData extraData);

        protected override void OnUpdate(CombatFrame frame)
        {        
        }
    }

}