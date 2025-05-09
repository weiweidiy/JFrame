namespace JFrame
{
    /// <summary>
    /// 獨立綫程執行器，執行后會立即進入冷卻 只能打1次 type = 12 參數：0：执行時間 1：傷害加成 2: 类型（0：无  1：普通伤害， 100：灼烧伤害 ）3:延遲
    /// </summary>
    public class ExecutorCombatSingleThreadDamage : ExecutorCombatDamage
    {
        public ExecutorCombatSingleThreadDamage(CombatBaseFinder combinFinder, CombatBaseFormula formula) : base(combinFinder, formula)
        {

        }

        public override float GetDuration()
        {
            return 0f;
        }
        public override int GetValidArgsCount()
        {
            return 4;
        }

        float GetDelay()
        {
            return Owner.GetReadyCdTriggerDuration();
        }


        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {

            var bullet = new CombatBullet(this, target, data, GetDelay());
            Owner.AddBullet(bullet);

            //target.OnDamage(data);
            StealHp(data);
        }

    }
}