namespace JFrame
{
    //public abstract class BaseBattleExecutor : IBattleExecutor
    //{
    //    /// <summary>
    //    /// 首次延迟
    //    /// </summary>
    //    float delay;

    //    /// <summary>
    //    /// 是否已经延迟过了
    //    /// </summary>
    //    bool delayed;

    //    /// <summary>
    //    /// 临时变量
    //    /// </summary>
    //    float delta;

    //    /// <summary>
    //    /// 是否正在释放
    //    /// </summary>
    //    bool isCasting = false;

    //    /// <summary>
    //    /// 战报记录器
    //    /// </summary>
    //    BattleReporter reporter;

    //    IBattleUnit caster;

    //    IBattleUnit target;

    //    /// <summary>
    //    /// 开始释放
    //    /// </summary>
    //    /// <param name="caster"></param>
    //    /// <param name="unit"></param>
    //    /// <param name="reporter"></param>
    //    public void Execute(IBattleUnit caster, IBattleUnit target, BattleReporter reporter/*, string reportUID*/)
    //    {
    //        isCasting = true;
    //        this.reporter = reporter;
    //    }

    //    public void Update(BattleFrame frame) 
    //    {
    //        if (!isCasting) return;

    //        delta += frame.DeltaTime;

    //        //延迟指定时间
    //        if (!delayed) 
    //        {
    //            if (delta - delay > 0f)
    //            {
    //                delta -= delay;
    //                delayed = true;
    //            }
    //            return;
    //        }

    //        //延迟结束，可以执行效果
    //        DoEffect();

    //        isCasting = false;
    //    }

    //    public abstract void DoEffect();
    //    //{
    //    //    var dmg = caster.Atk;
    //    //    //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
    //    //    target.HP -= dmg;

    //    //    reporter.AddReportResultData(caster.UID,)
    //    //}
    //}

    /// <summary>
    /// 多段伤害效果
    /// </summary>
    public class BattleDamage : IBattleExecutor
    {
        float count; //攻击次数
        float dmgRate; //伤害倍率

        public BattleDamage(float[] args)
        {
            if (args != null && args.Length >= 2)
            {

                count = args[0];
                dmgRate = args[1];
            }

        }
        public void Execute(IBattleUnit caster, IBattleUnit target, BattleReporter reporter)
        {

            var dmg = caster.Atk;
            //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
            target.HP -= dmg;

            reporter.AddReportResultData(target.UID, nameof(BattleDamage),dmg);
        }

        public void Update(BattleFrame frame)
        {
            
        }
    }
}