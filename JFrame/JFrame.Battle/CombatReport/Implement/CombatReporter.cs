using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// pvp战报对象
    /// </summary>
    public class CombatReporter : ICombatReporter
    {
        public class Comp : IComparer<ICombatReportData>
        {
            public int Compare(ICombatReportData x, ICombatReportData y)
            {
                if (x.EscapeTime == y.EscapeTime) return 0;
                if (x.EscapeTime < y.EscapeTime) return -1;
                return 1;
            }
        }

        //Utility utility = new Utility();

        List<ICombatReportData> reports = new List<ICombatReportData>();

        BattleFrame frame;

        List<CommonCombatTeam> teams;

        public CombatReporter(BattleFrame frame, List<CommonCombatTeam> teams)
        {
            this.frame = frame;
            this.teams = teams;
            if (teams != null)
            {
                foreach (var team in this.teams)
                {
                    //team.onActionCast += Team_onActionCast;
                    //team.onActionStartCD += Team_onActionStartCD;
                    //team.onDamage += Team_onDamage;
                    //team.onHeal += Team_onHeal;
                    //team.onReborn += Team_onReborn;
                    //team.onDebuffAnti += Team_onDebuffAnti;
                    //team.onMaxHpUp += Team_onMaxHpUp;
                    //team.onDead += Team_onDead;
                    //team.onBufferAdded += Team_onBufferAdded;
                    //team.onBufferRemoved += Team_onBufferRemoved;
                    //team.onBufferCast += Team_onBufferCast;
                    //team.onBufferUpdate += Team_onBufferUpdate;
                }
            }
        }



        //private void Team_onActionCast(int teamId, ICombatUnit caster, CombatAction action, List<ICombatUnit> targets, float duration)
        //{
        //    List<string> lstUID = new List<string>();
        //    for (int i = 0; i < targets.Count; i++)
        //    {
        //        lstUID.Add(targets[i].Uid);
        //    }
        //    AddReportData(caster.Uid, ReportType.Action, targets[0].Uid, new object[] { action.Id, lstUID, duration });
        //}

        //private void Team_onActionStartCD(PVPBattleManager.Team team, ICombatUnit caster, CombatAction action, float cd)
        //{
        //    AddReportData(caster.Uid, ReportType.ActionCD, action.Uid, new object[] { action.Id, cd });
        //}

        //private void Team_onDamage(PVPBattleManager.Team team, ICombatUnit caster, CombatAction action, ICombatUnit target, ExecuteInfo value)
        //{
        //    AddReportData(caster.Uid, ReportType.Damage, target.Uid, new object[] { value.Value, target.HP, target.MaxHP, value.IsCri, value.IsBlock });
        //}


        //private void Team_onHeal(PVPBattleManager.Team team, ICombatUnit caster, CombatAction action, ICombatUnit target, int value)
        //{
        //    AddReportData(caster.Uid, ReportType.Heal, target.Uid, new object[] { value, target.HP, target.MaxHP });
        //}

        //private void Team_onMaxHpUp(PVPBattleManager.Team team, ICombatUnit caster, CombatAction action, ICombatUnit target, int value)
        //{
        //    AddReportData(caster.Uid, ReportType.MaxHpUp, target.Uid, new object[] { value, target.HP, target.MaxHP });
        //}

        //private void Team_onReborn(PVPBattleManager.Team team, ICombatUnit caster, CombatAction action, ICombatUnit target, int value)
        //{
        //    AddReportData(caster.Uid, ReportType.Reborn, target.Uid, new object[] { value, target.HP, target.MaxHP });
        //}
        //private void Team_onDead(PVPBattleManager.Team team, ICombatUnit caster, CombatAction action, ICombatUnit target)
        //{
        //    AddReportData(caster.Uid, ReportType.Dead, target.Uid, new object[] { 0 });
        //}

        //private void Team_onBufferAdded(PVPBattleManager.Team team, ICombatUnit target, IBuffer buffer)
        //{
        //    AddReportData(target.Uid, ReportType.AddBuffer, target.Uid, new object[] { buffer.Uid, buffer.Id, buffer.FoldCount });
        //}

        //private void Team_onBufferCast(PVPBattleManager.Team team, ICombatUnit target, IBuffer buffer)
        //{
        //    AddReportData(target.Uid, ReportType.CastBuffer, target.Uid, new object[] { buffer.Uid, buffer.Id });
        //}

        //private void Team_onBufferRemoved(PVPBattleManager.Team team, ICombatUnit target, IBuffer buffer)
        //{
        //    AddReportData(target.Uid, ReportType.RemoveBuffer, target.Uid, new object[] { buffer.Uid, buffer.Id });
        //}

        //private void Team_onBufferUpdate(PVPBattleManager.Team arg1, ICombatUnit target, IBuffer buffer, int foldCount, float[] args)
        //{
        //    AddReportData(target.Uid, ReportType.UpdateBuffer, target.Uid, new object[] { buffer.Uid, buffer.Id, foldCount, args });
        //}

        //private void Team_onDebuffAnti(PVPBattleManager.Team team, ICombatUnit caster, CombatAction action, ICombatUnit target, int debuffId)
        //{
        //    AddReportData(target.Uid, ReportType.DebuffAnti, target.Uid, new object[] { debuffId });
        //}


        public List<ICombatReportData> GetAllReportData()
        {
            return reports;
        }

        /// <summary>
        /// 添加一个动作类型战报，并返回唯一id
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="escapeTime"></param>
        /// <param name="casterUID"></param>
        /// <param name="reportType"></param>
        /// <param name="targetUID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string AddReportData(ReportType reportType, ReportData reportData)
        {
            //to do: 增加一个流逝时间偏移量，可以延迟播放
            var data = new CombatReportData(frame.CurFrame, frame.GetDeltaTime(frame.CurFrame), reportType, reportData);

            if (ContainsReport(data))
                throw new Exception("已经存在战报" + frame  + " " + reportType);

            reports.Add(data);
            //utility.BinarySearchInsert(reports, data, new Comp());
            return data.UID;
        }

        /// <summary>
        /// 是否已经存在
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        bool ContainsReport(ICombatReportData report)
        {
            foreach (var data in reports)
            {
                if (data.UID.Equals(report.UID))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取指定战报
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="hostUID"></param>
        /// <returns></returns>
        public ICombatReportData GetReportData(string uid)
        {
            foreach (var report in reports)
            {
                if (report.UID.Equals(uid))
                    return report;
            }
            return null;
        }
    }


    /// <summary>
    /// 动作战报数据，谁什么时间向谁使用了什么动作
    /// </summary>
    public class CombatReportData : ICombatReportData
    {
        public string UID { get; private set; }
        public int Frame { get; private set; }
        public float EscapeTime { get; private set; } //从战斗开始到现在流逝的时间

        public ReportType ReportType { get; private set; }

        public ReportData ReportData { get; private set; }

        public CombatReportData(int frame, float escapeTime, ReportType reportType, ReportData reportData)
        {
            UID = Guid.NewGuid().ToString();
            Frame = frame;
            EscapeTime = escapeTime;
            ReportType = reportType;
            ReportData = reportData;
        }
    }
}