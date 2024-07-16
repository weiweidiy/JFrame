using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JFrame
{
    /// <summary>
    /// pvp战报对象
    /// </summary>
    public class BattleReporter : IBattleReporter
    {
        List<BattleReportData> reports = new List<BattleReportData>();

        public List<BattleReportData> GetAllReportData()
        {
            return reports;
        }

        /// <summary>
        /// 添加一个战报，并返回唯一id
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="escapeTime"></param>
        /// <param name="casterUID"></param>
        /// <param name="actionId"></param>
        /// <param name="targetsUID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string AddReportMainData(int frame, float escapeTime, string casterUID, int actionId, List<string> targetsUID)
        {
            var data = new BattleReportData(frame, escapeTime,casterUID, actionId, targetsUID);

            if(ContainsReport(data))
                throw new Exception("已经存在战报" + frame + " " + casterUID + " " + actionId);

            reports.Add(data);

            return data.UID;           
        }

        /// <summary>
        /// 添加结果数据段 , 每个目标对象会调用1次
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="value"></param>
        /// <param name="attribute"></param>
        /// <param name="buffs"></param>
        /// <exception cref="Exception"></exception>
        public void AddReportResultData(string uid, string targetUID, int value, int attr, int buff)
        {
            var mainData = GetReportData(uid);
            if (mainData == null)
                throw new Exception("不存在指定的战报对象 " + uid);

            mainData.AddTargetValue(targetUID, value);
            mainData.AddTargetAttribute(targetUID, attr);
            mainData.AddTargetBuff(targetUID, buff);
        }

        /// <summary>
        /// 更新战报结果数据段
        /// </summary>
        /// <param name="uid"></param>
        public void UpdateReportResultData(string uid, Dictionary<string, int> values, Dictionary<string, int> attributes, Dictionary<string, List<int>> buffs)
        {
            var mainData = GetReportData(uid);
            if (mainData == null)
                throw new Exception("不存在指定的战报对象 " + uid);

            mainData.UpdateTargetsValue(values);
            mainData.UpdateTargetsBuff(buffs);
            mainData.UpdateTargetsAttribute(attributes);
        }


        bool ContainsReport(BattleReportData report)
        {
            foreach(var data in reports)
            {
                if(data.UID.Equals(report.UID))
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
        public BattleReportData GetReportData(string uid)
        {
            foreach (var report in reports)
            {
                if (report.UID.Equals(uid))
                    return report;
            }
            return null;
        }

    }

    // xx 普通攻击了 yy 和 zz, 造成了 yy 10点伤害， zz 20点伤害
    // xx 特殊攻击了 yy 和 zz, 造成了 yy 10点伤害，并添加了a buff, 造成了 zz 20点伤害
    // xx 移除a buff

    /// <summary>
    /// 战报数据
    /// </summary>
    public class BattleReportData
    {
        //========================> 行动段 <=========================================
        public string UID { get; private set; }
        public int Frame { get; private set; }
        public float EscapeTime { get; private set; } //从战斗开始到现在流逝的时间
        public string CasterUID { get; private set; } //行动者UID
        public int ActionId { get; private set; } 
        public List<string> TargetsUID { get; private set; } //目标UID

        //=========================> 结果段 <=========================================
        public Dictionary<string, int> TargetsValue { get; private set; }  //动作造成的数值，如果是伤害类的，则是伤害恢复数值，
        public Dictionary<string, List<int>> TargetsBuff { get; private set; } //目标身上所有的buffid

        public Dictionary<string, int> TargetsAttribute; //所有目标的生命属性


        public BattleReportData(int frame, float escapeTime, string casterUID, int actionId, List<string> targetsUID) 
        {
            UID = Guid.NewGuid().ToString();
            Frame = frame;
            EscapeTime = escapeTime;
            CasterUID = casterUID;
            ActionId = actionId;
            TargetsUID = targetsUID;
            TargetsValue = new Dictionary<string, int>();
            TargetsBuff = new Dictionary<string, List<int>>();
            TargetsAttribute = new Dictionary<string, int>();
            //foreach(var  target in targetsUID)
            //{
            //    TargetsAttribute.Add(target, 0);
            //}
        }

        /// <summary>
        /// 向目标造成 hp数值变更
        /// </summary>
        /// <param name="targetUID"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public void AddTargetValue(string targetUID, int value)
        {
            if (TargetsValue.ContainsKey(targetUID))
                throw new Exception("添加目标结果数值时，发现已经存在该单位" + targetUID);

            TargetsValue.Add(targetUID, value);
        }

        /// <summary>
        /// 向目标添加buff
        /// </summary>
        /// <param name="targetUID"></param>
        /// <param name="value"></param>
        public void AddTargetBuff(string targetUID, int value)
        {

            if(TargetsBuff.ContainsKey(targetUID))
            {
                TargetsBuff[targetUID].Add(value);
            }

            else
            {
                TargetsBuff.Add(targetUID, new List<int>());
                TargetsBuff[targetUID].Add(value);
            }
        }

        /// <summary>
        /// 添加当前HP属性值
        /// </summary>
        /// <param name="targetUID"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public void AddTargetAttribute(string targetUID, int value)
        {
            if (TargetsAttribute.ContainsKey(targetUID))
                throw new Exception("添加目标结果数值时，发现已经存在该单位" + targetUID);
            else
                TargetsAttribute.Add(targetUID, value);
        }

        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="targetsValue"></param>
        public void UpdateTargetsValue(Dictionary<string, int> targetsValue)
        {
            TargetsValue = targetsValue;
        }

        /// <summary>
        /// 更新目标的buff
        /// </summary>
        /// <param name="targetsBuff"></param>
        public void UpdateTargetsBuff(Dictionary<string, List<int>> targetsBuff)
        {
            TargetsBuff = targetsBuff;
        }

        /// <summary>
        ///  更新目标最新生命
        /// </summary>
        /// <param name="targetsAttribute"></param>
        public void UpdateTargetsAttribute(Dictionary<string, int> targetsAttribute)
        {
            TargetsAttribute = targetsAttribute;
        }
    }
}