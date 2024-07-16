using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 普通的一次行动逻辑
    /// </summary>
    public class NormalAction : IBattleAction
    {
        /// <summary>
        /// 准备好了，也找到目标了，可以释放
        /// </summary>
        public event Action<IBattleAction, List<IBattleUnit>> onTriggerOn;
        public event Action<IBattleAction, IBattleUnit> onDone;

        /// <summary>
        /// 动作名称
        /// </summary>
        public string Name => nameof(NormalAction);

        /// <summary>
        /// 动作ID
        /// </summary>
        public int Id { get; private set; }

        BattleTrigger trigger;
        IBattleTargetFinder finder;
        IBattleExecutor exutor;

        /// <summary>
        /// 常规动作逻辑，触发器触发->搜索敌人->执行效果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trigger"></param>
        /// <param name="finder"></param>
        /// <param name="exutor"></param>
        public NormalAction(int id, BattleTrigger trigger, IBattleTargetFinder finder, IBattleExecutor exutor)
        {
            this.Id = id;
            this.trigger = trigger;
            this.trigger.onTrigger += Trigger_onTrigger;
            this.finder = finder;
            this.exutor = exutor;
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
            //exutor.Update(frame);
        }

        /// <summary>
        /// 向指定目标释放
        /// </summary>
        /// <param name="units"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Cast(IBattleUnit caster,  List<IBattleUnit> units, BattleReporter reporter)
        {
            //添加动作战报
            reporter.AddReportActionData(caster.UID, nameof(NormalAction), units[0].UID);

            foreach(var unit in units)
            {
                exutor.Execute(caster, unit, reporter);
                onDone?.Invoke(this, unit);
            }
        }
    }
}