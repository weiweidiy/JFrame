using System.Collections.Generic;

namespace JFrame
{
    public interface ICombatManager<TResult, TTeam, TUnit>
    {
        //void Update();

        //void StartUpdate();

        TResult GetResult();

        //void StopUpdate();

        void ClearResult();

        /// <summary>
        /// 添加一個隊伍
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="teamObj"></param>
        void AddTeam(int teamId, TTeam teamObj);

        /// <summary>
        /// 獲取指定隊伍
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        TTeam GetTeam(int teamId);

        /// <summary>
        /// 獲取所有隊伍
        /// </summary>
        /// <returns></returns>
        List<TTeam> GetTeams();
        /// <summary>
        /// 獲取敵對隊伍
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        int GetOppoTeamId(int teamId);
        /// <summary>
        /// 獲取敵對隊伍
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        int GetOppoTeamId(TUnit unit);
        /// <summary>
        /// 獲取友軍隊伍Id
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        int GetFriendTeamId(TUnit unit);

        float GetCombatTimeLimit();

        bool IsBuffer(int buffId);

        /// <summary>
        /// 獲取透傳參數
        /// </summary>
        /// <returns></returns>
        object GetExtraData();

        void AddUnit(int teamId, TUnit unit);

        void RemoveUnit(int teamId, TUnit unit);
        /// <summary>
        /// 獲取單位數量
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        int GetUnitCount(int teamId);
        /// <summary>
        /// 獲取所有單位數量
        /// </summary>
        /// <returns></returns>
        int GetAllUnitCount();
        /// <summary>
        /// 獲取指定單位
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        TUnit GetUnit(string uid);
        /// <summary>
        /// 獲取指定隊伍所有單位
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        List<TUnit> GetUnits(int teamId);

        /// <summary>
        /// 指定距離的單位
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        List<TUnit> GetUnits(TUnit unit, int teamId, float range, bool alive);
    }
}