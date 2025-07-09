using JFramework;
using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatTeam : IUnique
    {
        List<IJCombatOperatable> GetAllUnit();

        IJCombatOperatable GetUnit(string uid);

        bool IsAllDead();
    }

}
