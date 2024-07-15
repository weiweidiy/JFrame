using System;
using System.Collections.Generic;

namespace JFrame
{

    public class NormalAttack : IBattleAction
    {
        /// <summary>
        /// 准备好了，也找到目标了，可以释放
        /// </summary>
        public event Action<IBattleAction, List<IBattleUnit>> onTriggerOn;
        public event Action<IBattleAction, IBattleUnit> onDone;

        /// <summary>
        /// 动作名称
        /// </summary>
        public string Name => nameof(NormalAttack);

        public int Id { get; private set; }

        BattleTrigger trigger;
        IBattleTargetFinder finder;

        public NormalAttack(int id, BattleTrigger trigger, IBattleTargetFinder finder)
        {
            this.Id = id;
            this.trigger = trigger;
            this.trigger.onTrigger += Trigger_onTrigger;
            this.finder = finder;
        }

        /// <summary>
        /// 触发了，可以攻击了
        /// </summary>
        private void Trigger_onTrigger()
        {
            var targets = finder.FindTargets(); //第一个是首要目标
            if(targets == null || targets.Count == 0)
            {
                //没有找到合适的目标:不管，也不进入CD
                return;
            }

            onTriggerOn?.Invoke(this, targets);
        }

        //cd管理
        public void Update(BattleFrame frame)
        {
            trigger.Update(frame);
        }

        /// <summary>
        /// 向指定目标释放
        /// </summary>
        /// <param name="units"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Cast(IBattleUnit caster,  List<IBattleUnit> units, BattleReporter reporter, string reportUID)
        {
            foreach(var unit in units)
            {
                var dmg = caster.Atk;
                //to do: unit.getbuffvalue(bufftype, dmg) 返回最终受伤值
                unit.HP -= dmg;

                reporter.AddReportResultData(reportUID, unit.UID, dmg, unit.HP, -1);

                onDone?.Invoke(this, unit);
            }
        }
    }
}