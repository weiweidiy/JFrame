namespace JFramework.Game
{
    public class JCombatExecutorAttributeDataChange : JCombatExecutorBase
    {
        public JCombatExecutorAttributeDataChange(IJCombatFilter filter, IJCombatTargetsFinder finder, IJCombatFormula formulua, float[] args) : base(filter, finder, formulua, args)
        {
        }
        protected override int GetValidArgsCount()
        {
            return 0; // 只需要一个参数，通常是属性值或相关系数
        }
        protected override IJCobmatExecuteArgsHistroy DoExecute(IJCombatExecutorExecuteArgs executeArgs, IJCombatCasterTargetableUnit target)
        {
            var attributeData = executeArgs.Attribute;
            if (attributeData == null)
            {
                return new JCombatExecutorExecuteArgsHistroy();
            }
            var value = (float)attributeData.CurValue;
            formulua.CalcHitValue(null, ref value); // 假设属性提升20%
            attributeData.CurValue = (int)value;
            return new JCombatExecutorExecuteArgsHistroy() {  };
        }
    }
}
