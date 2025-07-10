using JFramework;
using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatTeam : IUnique
    {
        List<IJCombatOperatable> GetAllUnits();

        IJCombatOperatable GetUnit(string uid);

        bool IsAllDead();
    }

}
