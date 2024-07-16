namespace JFrame
{
    /// <summary>
    /// 伤害效果
    /// </summary>
    public class BattleDamage : IBattleExcutor 
    {
        float arg;
        public BattleDamage(float arg)
        {
            this.arg = arg;
        }
        public void Cast(IBattleUnit caster, IBattleUnit unit, BattleReporter reporter, string reportUID)
        {
            var dmg = caster.Atk;
            //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
            unit.HP -= dmg;

            reporter.AddReportResultData(reportUID, unit.UID, dmg, unit.HP, -1);
        }
    }
}