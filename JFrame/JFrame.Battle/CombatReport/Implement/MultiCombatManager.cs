using System;
using System.Collections.Generic;
using static JFrame.PVPBattleManager;

namespace JFrame
{
    /// <summary>
    /// 多波战斗
    /// </summary>
    public class MultiCombatManager : CombatManager
    {
        Dictionary<int, List<CommonCombatTeam>> dicTeams = new Dictionary<int, List<CommonCombatTeam>>();

        Dictionary<int, List<KeyValuePair<CombatTeamType, List<CombatUnitInfo>>>> dicTeamsData = new Dictionary<int, List<KeyValuePair<CombatTeamType, List<CombatUnitInfo>>>>();

        int curLeftTeamIndex = 0;
        int curRightTeamIndex = 0;

        public MultiCombatManager(float limitTime, float deltaTime) : base(limitTime, deltaTime)
        {
        }

        public void Initialize(List<KeyValuePair<CombatTeamType, List<CombatUnitInfo>>> dicTeam1Data, List<KeyValuePair<CombatTeamType, List<CombatUnitInfo>>> dicTeam2Data, List<CombatBufferInfo> bufferInfos,  CombatUnitInfo god = null)
        {
            dicTeams = new Dictionary<int, List<CommonCombatTeam>>();
            dicTeamsData = new Dictionary<int, List<KeyValuePair<CombatTeamType, List<CombatUnitInfo>>>>();
            dicTeamsData.Add(0, dicTeam1Data);
            dicTeamsData.Add(1, dicTeam2Data);

            var context = new CombatContext();
            context.CombatManager = this;
            context.CombatBufferFactory = bufferFactory;


            for(int i = 0; i < dicTeam1Data.Count; i++)
            {
                var groupType = dicTeam1Data[i].Key;
                var groupData = dicTeam1Data[i].Value;

                if (groupData == null)
                    throw new ArgumentNullException("teamdata 不能為null");

                CommonCombatTeam group = groupType == CombatTeamType.Combine ? new SpecialCombatTeam() : new CommonCombatTeam();
                group.Initialize(0, context, groupData);
                if (groupData.Count > 0)
                    AddTeam(0, group); //1 = 隊伍id
            }


            for (int i = 0; i < dicTeam2Data.Count; i++)
            {
                var groupType = dicTeam2Data[i].Key;
                var groupData = dicTeam2Data[i].Value;
                if (groupData == null)
                    throw new ArgumentNullException("teamdata 不能為null");

                CommonCombatTeam group = groupType == CombatTeamType.Combine ? new SpecialCombatTeam() : new CommonCombatTeam();
                group.Initialize(1, context, groupData);
                if (groupData.Count > 0)
                    AddTeam(1, group);
            }

            //预加载所有buffers
            bufferFactory.PreloadBuffers(bufferInfos, context);
        }

        /// <summary>
        /// 获取战报
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override CombatReport GetResult()
        {
            var report =  base.GetResult();
            var result = NextGroup(report.winner);
            isCombatOver = !result;
            return report;
        }
        
        /// <summary>
        /// 获取小组数量
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        int GetGroupCount(int teamId)
        {
            return dicTeamsData[teamId].Count;
        }

        /// <summary>
        /// 当前是否最后一组
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        bool IsLastGroup(int teamId)
        {
            var curIndex = GetCurGroupIndex(teamId);
            var groupCount = GetGroupCount(teamId);
            return curIndex == groupCount - 1;
        }

        /// <summary>
        /// 进入下一组
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        bool NextGroup(int teamId)
        {
            var curIndex = GetCurGroupIndex(teamId);
            var groupCount = GetGroupCount(teamId);

            if(curIndex + 1 < groupCount)
            {
                SetCurGroupIndex(teamId, curIndex + 1);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// 获取当前的队伍的group索引
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        int GetCurGroupIndex(int teamId)
        {
            return teamId == 0 ? curLeftTeamIndex : curRightTeamIndex;
        }

        /// <summary>
        /// 设置当前索引
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="curIndex"></param>
        void SetCurGroupIndex(int teamId , int curIndex)
        {
            if (teamId == 0)
            {
                curLeftTeamIndex++;
            }
            else
            {
                curRightTeamIndex++;
            }
        }

        /// <summary>
        /// 获取指定队伍的队伍原始数据
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override KeyValuePair<CombatTeamType, List<CombatUnitInfo>> GetTeamData(int teamId)
        {
            var curIndex = GetCurGroupIndex(teamId);
            var allData = dicTeamsData[teamId];
            return allData[curIndex];
        }

        /// <summary>
        /// 添加到队伍里
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="team"></param>
        public override void AddTeam(int teamId, CommonCombatTeam team)
        {
            if (!dicTeams.ContainsKey(teamId))
                dicTeams.Add(teamId, new List<CommonCombatTeam>());

            var allGroup = dicTeams[teamId];
            allGroup.Add(team);
        }

        /// <summary>
        /// 获取当前的队伍
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public override CommonCombatTeam GetTeam(int teamId)
        {
            var curIndex = GetCurGroupIndex(teamId);
            var allGroup = dicTeams[teamId];
            return allGroup[curIndex];
        }

        /// <summary>
        /// 获取当前波次双方队伍
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<CommonCombatTeam> GetTeams()
        {
            var result = new List<CommonCombatTeam>();
            result.Add(GetTeam(0));
            result.Add(GetTeam(1));
            return result;
        }


        public override void AddUnit(int teamId, CombatUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public override void ClearResult()
        {
            throw new System.NotImplementedException();
        }

        public override int GetAllUnitCount()
        {
            throw new System.NotImplementedException();
        }


        public override object GetExtraData()
        {
            throw new System.NotImplementedException();
        }







        public override bool IsBuffer(int buffId)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUnit(int teamId, CombatUnit unit)
        {
            throw new System.NotImplementedException();
        }


    }
}
