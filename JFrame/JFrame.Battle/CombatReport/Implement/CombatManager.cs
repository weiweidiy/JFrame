using System;
using System.Collections.Generic;
using System.Linq;
using static JFrame.PVPBattleManager;

namespace JFrame
{

    public enum ActionComponentType
    {
        ConditionTrigger,
        Finder,
        DelayTrigger,
        Executor,
        CdTrigger
    }

    /// <summary>
    /// action組件：4種類型
    /// </summary>
    public class ActionComponentInfo
    {
        public int id;
        public float[] args;
    }


    public class ActionInfo
    {
        public string uid;
        public ActionType type;
        public ActionMode mode;
        public Dictionary<ActionComponentType, List<ActionComponentInfo>> componentInfo;
    }

    public class CombatUnitInfo
    {
        public string uid;
        public int id;
        public Dictionary<int, ActionInfo> actionsData;
        public Dictionary<int, Dictionary<ActionComponentType, List<ActionComponentInfo>>> buffersData;
        public int hp;
        public int maxHp;
        public int atk;
        public float atkSpeed;
        public float cri; //暴击率 0~1的值 百分比
        public float criDmgRate; //暴击伤害加成百分比
        public float criDmgAnti; //暴击伤害抵抗百分比
        public float skillDmgRate; //技能伤害加成百分比
        public float skillDmgAnti; //技能伤害抵抗百分比
        public float dmgRate; //伤害加成百分比
        public float dmgAnti; //伤害抵抗百分比
        public float debuffHit; //0~1异常状态命中百分比
        public float debuffAnti; //0~1异常状态抵抗百分比
        public float penetrate; //穿透 0~1 百分比
        public float block;     //格挡 0~1 百分比
        public CombatVector position; //初始坐標點
        public CombatVector moveSpeed; //移動速度，向左就是負數，向右是正數

    }

    public class CombatManager : ICombatManager<CombatReport, CommonCombatTeam, CombatUnit>
    {
        Dictionary<int, CommonCombatTeam> teams;

        BattleFrame frame = new BattleFrame();
        public BattleFrame Frame { get => frame; }

        CombatJudge combatJudge;

        CombatReport report;

        public CombatReporter Reporter;


        public void Initialize(List<CombatUnitInfo> team1Data, List<CombatUnitInfo> team2Data, float timeLimit, CombatUnitInfo god = null)
        {
            teams = new Dictionary<int, CommonCombatTeam>();
            var context = new CombatContext();
            context.CombatManager = this;

            if (team1Data == null || team2Data == null)
                throw new ArgumentNullException("teamdata 不能為null");

            CommonCombatTeam team1 = CreateTeam(team1Data, context, new SpecialCombatTeam());
            if (team1Data.Count > 0)
                AddTeam(0, team1); //1 = 隊伍id

            CommonCombatTeam team2 = CreateTeam(team2Data, context, new CommonCombatTeam());
            if (team2Data.Count > 0)
                AddTeam(1, team2);


            combatJudge = new CombatJudge(team1, team2);

            Reporter = new CombatReporter(frame, GetTeams());

            report = new CombatReport();
            report.attacker = team1 as SpecialCombatTeam;
            report.defence = team2;
        }

        #region 創建對象
        /// <summary>
        /// 創建一個隊伍對象
        /// </summary>
        /// <param name="teamData"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        CommonCombatTeam CreateTeam(List<CombatUnitInfo> teamData, CombatContext context, CommonCombatTeam team)
        {
            //var team = new CommonCombatTeam();
            if (teamData != null)
            {
                var actionFactory = new CombatActionFactory();
                var attrFactory = new CombatAttributeFactory();
                //創建並初始化隊伍
                foreach (var unitInfo in teamData)
                {
                    //創建並初始化戰鬥單位
                    var unit = new CombatUnit();
                    unit.Initialize(unitInfo.uid, context, actionFactory.CreateUnitActions(unitInfo.actionsData, unit, context), CreateBuffers(unitInfo.buffersData), attrFactory.CreateAllAttributes(unitInfo));
                    unit.SetPosition(unitInfo.position);
                    unit.SetSpeed(unitInfo.moveSpeed);
                    team.Add(unit);
                }
            }
            team.Initialize(context);

            return team;
        }

        /// <summary>
        /// 創建所有默認帶有的buffers
        /// </summary>
        /// <param name="buffersData"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<CombatBuffer> CreateBuffers(Dictionary<int, Dictionary<ActionComponentType, List<ActionComponentInfo>>> buffersData)
        {
            return null;
        }



        #endregion

        #region 添加獲取方法
        /// <summary>
        /// 添加隊伍
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="teamObj"></param>
        /// <exception cref="Exception"></exception>
        public void AddTeam(int teamId, CommonCombatTeam teamObj)
        {
            if (teams == null)
                throw new Exception("team list is not init , please call the Initialize method ");

            teams.Add(teamId, teamObj);
        }

        /// <summary>
        /// 獲取隊伍對象
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public CommonCombatTeam GetTeam(int teamId)
        {
            if (teams.ContainsKey(teamId))
                return teams[teamId];
            return null;
        }

        /// <summary>
        /// 獲取所有隊伍
        /// </summary>
        /// <returns></returns>
        public List<CommonCombatTeam> GetTeams()
        {
            return teams.Values.ToList();
        }

        /// <summary>
        /// 獲取友方隊伍id
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int GetFriendTeamId(CombatUnit unit)
        {
            var teams = GetTeams();
            foreach (var team in teams)
            {
                var units = team.GetUnits();
                foreach (var item in units)
                {
                    if (item.Uid == unit.Uid)
                        return team.TeamId;
                }
            }

            throw new Exception("沒有找對對方的隊伍id");
        }

        /// <summary>
        /// 獲取敵對隊伍id
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public int GetOppoTeamId(int teamId)
        {
            return teamId == 0 ? 1 : 0;
        }

        /// <summary>
        /// 獲取敵對隊伍id
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual int GetOppoTeamId(CombatUnit unit)
        {
            var team0 = GetTeam(0);
            foreach (var item in team0.GetUnits())
            {
                if (item.Uid == unit.Uid)
                    return 1;
            }

            return 0;

            //throw new Exception("沒有找對對方的隊伍id");
        }


        public void AddUnit(int teamId, CombatUnit unit)
        {
            throw new System.NotImplementedException();
        }
        public void RemoveUnit(int teamId, CombatUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public CombatUnit GetUnit(string uid)
        {
            foreach(var team in teams.Values)
            {
                var unit = team.GetUnit(uid);
                if(unit != null)
                    return unit;
            }
            return null;
        }

        /// <summary>
        /// 獲取指定隊伍所有單位
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public virtual List<CombatUnit> GetUnits(int teamId)
        {
            var team = GetTeam(teamId);
            return team.GetUnits();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="teamId"></param>
        /// <param name="range">-1:無視距離</param>
        /// <returns></returns>
        public virtual List<CombatUnit> GetUnits(CombatUnit unit, int teamId, float range = -1f, bool alive = true)
        {
            var units = GetUnits(teamId);
            if (range == -1)
                return units;

            var result = new List<CombatUnit>();

            foreach (var item in units)
            {
                if (item.IsAlive() != alive)
                    continue;

                var myX = (unit as ICombatMovable).GetPosition().x;
                var x = (item as ICombatMovable).GetPosition().x;
                if (Math.Abs(myX - x) <= range)
                    result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 獲取指定隊伍戰鬥單位數量
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public int GetUnitCount(int teamId)
        {
            return GetTeam(teamId).Count();
        }

        public int GetAllUnitCount()
        {
            throw new NotImplementedException();
        }


        public float GetCombatTimeLimit()
        {
            throw new System.NotImplementedException();
        }

        public object GetExtraData()
        {
            throw new System.NotImplementedException();
        }



        #endregion



        //public void StartUpdate()
        //{
        //    isStartUpdate = true;
        //}

        //public void StopUpdate()
        //{
        //    isStartUpdate = false;
        //}

        //public void Update()
        //{
        //    if (!isStartUpdate)
        //        return;

        //    foreach (var team in teams.Values)
        //    {
        //        team.Update(frame);
        //    }

        //    frame.NextFrame();
        //}
        public CombatReport GetResult()
        {
            //如果战斗没有决出胜负，则继续战斗
            while (!combatJudge.IsOver() && !frame.IsMaxFrame())
            {
                Update(frame);

                frame.NextFrame();
            }

            var winner = combatJudge.GetWinner().TeamId == 0 ? 1 : 0; //1:挑战成功 0：挑战失败

            report.report = Reporter.GetAllReportData();
            report.winner = winner;
            //Debug.Log("战斗结束 " + frame.FrameCount);
            return report;
        }

        public void Update(BattleFrame frame)
        {
            foreach(var team in teams.Values)
            {
                team.UpdatePosition(frame);
            }

            foreach (var team in teams.Values)
            {
                team.Update(frame);
            }
        }

        public void ClearResult()
        {

        }

        public bool IsBuffer(int buffId)
        {
            throw new System.NotImplementedException();
        }

    }
}
