using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 普通的一次行动逻辑 (瞬时动作类型)
    /// </summary>
    public class NormalAction : IBattleAction
    {
        /// <summary>
        /// 准备好了，也找到目标了，可以释放
        /// </summary>
        public event Action<IBattleAction, List<IBattleUnit>> onTriggerOn;
        /// <summary>
        /// 开始释放了
        /// </summary>
        public event Action<IBattleAction, IBattleUnit> onStartCast;
        /// <summary>
        /// 对目标起效了（多个目标时，每个目标调用一次)
        /// </summary>
        public event Action<IBattleAction, IBattleUnit> onHitTarget;

        /// <summary>
        /// 动作名称
        /// </summary>
        public string Name => nameof(NormalAction);

        /// <summary>
        /// 动作ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 触发器
        /// </summary>
        BattleTrigger trigger;

        /// <summary>
        /// 目标搜索器
        /// </summary>
        IBattleTargetFinder finder;

        /// <summary>
        /// 效果执行器
        /// </summary>
        List<IBattleExecutor> exeutors;

        /// <summary>
        /// 常规动作逻辑，触发器触发->搜索敌人->执行效果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trigger"></param>
        /// <param name="finder"></param>
        /// <param name="exutor"></param>
        public NormalAction(int id, BattleTrigger trigger, IBattleTargetFinder finder, List<IBattleExecutor> exutors)
        {
            this.Id = id;
            this.trigger = trigger;
            this.trigger.onTrigger += Trigger_onTrigger;
            this.finder = finder;
            this.exeutors = exutors;

            foreach(var e  in exutors)
            {
                e.onExecute += E_onExecute;
            }
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


        /// <summary>
        /// 更新帧
        /// </summary>
        /// <param name="frame"></param>
        public void Update(BattleFrame frame)
        {
            trigger.Update(frame);
            //exutor.Update(frame);
            foreach (var e in exeutors)
            {
                e.Update(frame);
            }
        }

        /// <summary>
        /// 向指定目标释放
        /// </summary>
        /// <param name="units"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Cast(IBattleUnit caster,  List<IBattleUnit> units, BattleReporter reporter)
        {
            //添加动作战报,第一个目标作为对象
            //reporter.AddReportData(caster.UID, ReportType.Action, units[0].UID, new float[] { Id });
            onStartCast?.Invoke(this, units[0]);

            foreach (var unit in units)
            {
                foreach(var e in exeutors)
                {
                    e.ReadyToExecute(caster,this, unit);
                }
                onHitTarget?.Invoke(this, unit);
            }
        }

        /// <summary>
        /// 执行效果触发
        /// </summary>
        private void E_onExecute() { }

        /// <summary>
        /// 设置动作是否可用
        /// </summary>
        /// <param name="enable"></param>
        public void SetEnable(bool enable)
        {
            trigger.SetEnable(enable);
        }
    }
}