using System.Collections.Generic;

namespace JFrame
{
    public interface ICombatManager<TResult, TTeam, TUnit>
    {
        void Update();

        void Start();

        TResult GetResult();

        void ClearResult();

        void AddUnit(TUnit unit);

        void RemoveUnit(TUnit unit);

        TUnit GetUnit(string uid);

        List<TUnit> GetUnits(int teamId);

        void AddTeam(int teamId, TTeam teamObj);

        int GetOppoTeam(int teamId);

        int GetOppoTeam(TUnit unit);

        int GetFriendTeam(TUnit unit);

        float GetCombatTimeLimit();

        bool IsBuffer(int buffId);

        object GetExtraData();
    }
}