using System.Collections.Generic;

namespace JFrame
{
    public class CombatManager : ICombatManager<Report, CommonCombatTeam, ICombatUnit>
    {
        public void AddTeam(int teamId, CommonCombatTeam teamObj)
        {
            throw new System.NotImplementedException();
        }

        public void AddUnit(ICombatUnit unit)
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

        public void RemoveUnit(ICombatUnit unit)
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