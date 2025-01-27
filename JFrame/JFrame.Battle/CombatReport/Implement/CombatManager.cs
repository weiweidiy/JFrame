using System.Collections.Generic;

namespace JFrame
{
    public class CombatUnitInfo 
    {
        public string uid;
        public int id;
        public Dictionary<int, List<float[]>> actionsId;//float[]是參數列表,condition, finder, executor, cd 4組參數
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
    }

    public class CombatManager : ICombatManager<Report, CommonCombatTeam, ICombatUnit>
    {
        public void Initialize(List<CombatUnitInfo> team1Data, List<CombatUnitInfo> team2Data, float timeLimit, CombatUnitInfo god = null)
        {
            var combatTeam = new CommonCombatTeam();
            foreach (var unitInfo in team1Data)
            {
                var unit = new CombatUnit();
                combatTeam.Add(unit);
            }
        }

        public void AddTeam(int teamId, CommonCombatTeam teamObj)
        {
            throw new System.NotImplementedException();
        }

        public void AddUnit(int teamId, ICombatUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public void ClearResult()
        {
            throw new System.NotImplementedException();
        }

        public float GetCombatTimeLimit()
        {
            throw new System.NotImplementedException();
        }

        public object GetExtraData()
        {
            throw new System.NotImplementedException();
        }

        public int GetFriendTeam(ICombatUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public int GetOppoTeam(int teamId)
        {
            throw new System.NotImplementedException();
        }

        public int GetOppoTeam(ICombatUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public Report GetResult()
        {
            throw new System.NotImplementedException();
        }

        public ICombatUnit GetUnit(string uid)
        {
            throw new System.NotImplementedException();
        }

        public List<ICombatUnit> GetUnits(int teamId)
        {
            throw new System.NotImplementedException();
        }

        public bool IsBuffer(int buffId)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveUnit(int teamId, ICombatUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}