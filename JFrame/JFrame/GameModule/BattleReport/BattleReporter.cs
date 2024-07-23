
using System;
using System.Collections.Generic;


namespace JFrame
{
    public enum ReportType
    {
        Action, //英雄动作
        Damage, //受伤
        Heal,   //治疗回血
        Dead,   //死亡
        AddBuffer,
        RemoveBuffer,
        CastBuffer,
    }

    /// <summary>
    /// pvp战报对象
    /// </summary>
    public class BattleReporter : IBattleReporter
    {
        public class Comp : IComparer<IBattleReportData>
        {
            public int Compare(IBattleReportData x, IBattleReportData y)
            {
                if (x.EscapeTime == y.EscapeTime) return 0;
                if (x.EscapeTime < y.EscapeTime) return -1;
                return 1;
            }
        }

        //Utility utility = new Utility();

        List<IBattleReportData> reports = new List<IBattleReportData>();

        BattleFrame frame;

        Dictionary<PVPBattleManager.Team, BattleTeam> teams;

        public BattleReporter(BattleFrame frame, Dictionary<PVPBattleManager.Team, BattleTeam> teams) {
            this.frame = frame;
            this.teams = teams;
            foreach(var team in this.teams.Values)
            {
                team.onActionCast += Team_onActionCast;
                team.onDamage += Team_onDamage;
                team.onHeal += Team_onHeal;
                team.onDead += Team_onDead;
                team.onBufferAdded += Team_onBufferAdded;
                team.onBufferRemoved += Team_onBufferRemoved;
                team.onBufferCast += Team_onBufferCast;
            }

        }


        private void Team_onActionCast(PVPBattleManager.Team team, IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            AddReportData(caster.UID, ReportType.Action, target.UID, new object[] { action.Id });
        }

        private void Team_onDamage(PVPBattleManager.Team team, IBattleUnit caster, IBattleAction action, IBattleUnit target, int value)
        {
            AddReportData(caster.UID, ReportType.Damage, target.UID, new object[] { value, target.HP, target.MaxHP });
        }

        private void Team_onHeal(PVPBattleManager.Team team, IBattleUnit caster, IBattleAction action, IBattleUnit target, int value)
        {
            AddReportData(caster.UID, ReportType.Heal, target.UID, new object[] { value, target.HP, target.MaxHP });
        }


        private void Team_onDead(PVPBattleManager.Team team, IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            AddReportData(caster.UID, ReportType.Dead, target.UID, new object[] {0});
        }

        private void Team_onBufferAdded(PVPBattleManager.Team team, IBattleUnit target, IBuffer buffer)
        {
            AddReportData(target.UID, ReportType.AddBuffer, target.UID, new object[] {buffer.UID,  buffer.Id });
        }

        private void Team_onBufferCast(PVPBattleManager.Team team, IBattleUnit target, IBuffer buffer)
        {
            AddReportData(target.UID, ReportType.CastBuffer, target.UID, new object[] { buffer.UID, buffer.Id });
        }

        private void Team_onBufferRemoved(PVPBattleManager.Team team, IBattleUnit target, IBuffer buffer)
        {
            AddReportData(target.UID, ReportType.RemoveBuffer, target.UID, new object[] { buffer.UID, buffer.Id });
        }




        public List<IBattleReportData> GetAllReportData()
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
        public string AddReportData(string casterUID, ReportType reportType, string targetUID, object[] arg , float timeOffset = 0f)
        {
            //to do: 增加一个流逝时间偏移量，可以延迟播放
            var data = new BattleReportData(frame.CurFrame,frame.GetDeltaTime(frame.CurFrame) + timeOffset, casterUID, reportType, targetUID, arg);

            if (ContainsReport(data))
                throw new Exception("已经存在战报" + frame + " " + casterUID + " " + reportType);

            reports.Add(data);
            //utility.BinarySearchInsert(reports, data, new Comp());
            return data.UID;
        }

        /// <summary>
        /// 是否已经存在
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        bool ContainsReport(IBattleReportData report)
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
        public IBattleReportData GetReportData(string uid)
        {
            foreach (var report in reports)
            {
                if (report.UID.Equals(uid))
                    return report;
            }
            return null;
        }

    }
}