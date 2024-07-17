using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JFrame
{
    /// <summary>
    /// pvp战报对象
    /// </summary>
    public class BattleReporter /*: IBattleReporter*/
    {
        List<IBattleReportData> reports = new List<IBattleReportData>();

        BattleFrame frame;

        public BattleReporter(BattleFrame frame) {
            this.frame = frame;
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
        /// <param name="actionName"></param>
        /// <param name="targetUID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string AddReportActionData(string casterUID, string actionName, string targetUID)
        {
            var data = new BattleReportActionData(frame.CurFrame,frame.GetDeltaTime(frame.CurFrame), casterUID, actionName, targetUID);

            if (ContainsReport(data))
                throw new Exception("已经存在战报" + frame + " " + casterUID + " " + actionName);

            reports.Add(data);

            return data.UID;
        }

        /// <summary>
        /// 添加一个效果类型战报
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="escapeTime"></param>
        /// <param name="casterUID"></param>
        /// <param name="actionName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string AddReportResultData(string casterUID, string actionName, int value)
        {
            var data = new BattleReportResultData(frame.CurFrame, frame.GetDeltaTime(frame.CurFrame),  casterUID, actionName, value);

            if (ContainsReport(data))
                throw new Exception("已经存在战报" + frame + " " + casterUID + " " + actionName);

            reports.Add(data);
            return data.UID;
        }

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

    /// <summary>
    /// 掉血，添加了buff, 复活等结果数据
    /// </summary>
    public class BattleReportResultData : IBattleReportData
    {
        /// <summary>
        /// 战报数据类型
        /// </summary>
        public string DataType => nameof(BattleReportResultData); 
        public string UID { get; private set; }
        public int Frame { get; private set; }
        public float EscapeTime { get; private set; } //从战斗开始到现在流逝的时间
        public string CasterUID { get; private set; } //效果触发者
        public string ActionName { get; private set; } // 1, 受伤， 2， 加血， 3，加buff， 4，移除buff,
        public int Value { get; private set; } //根据id代表不同含义

        public BattleReportResultData(int frame, float escapeTime, string casterUID, string actionName, int value)
        {
            UID = Guid.NewGuid().ToString();
            Frame = frame;
            EscapeTime = escapeTime;
            CasterUID = casterUID;
            ActionName = actionName;
            Value = value;
        }
    }

    /// <summary>
    /// 动作战报数据，谁什么时间向谁使用了什么动作
    /// </summary>
    public class BattleReportActionData : IBattleReportData
    {
        public string DataType => nameof(BattleReportActionData);
        public string UID { get; private set; }
        public int Frame { get; private set; }
        public float EscapeTime { get; private set; } //从战斗开始到现在流逝的时间
        public string CasterUID { get; private set; } //行动者UID
        public string ActionName { get; private set; } //SkillId
        public string TargetUID { get; private set; } //目标UID

        public BattleReportActionData(int frame, float escapeTime, string casterUID, string actionName, string targetUID)
        {
            UID = Guid.NewGuid().ToString();
            Frame = frame;
            EscapeTime = escapeTime;
            CasterUID = casterUID;
            ActionName = actionName;
            TargetUID = targetUID;
        }
    }
}