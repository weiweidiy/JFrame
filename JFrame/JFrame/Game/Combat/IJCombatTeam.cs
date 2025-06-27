using JFramework;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatTeam
    {
        List<IJCombatUnit> GetAllUnit();

        bool IsAllDead();
    }

    public class JCombatTeam : DictionaryContainer<IJCombatUnit>, IJCombatTeam
    {
        public List<IJCombatUnit> GetAllUnit()
        {
            throw new System.NotImplementedException();
        }

        public bool IsAllDead()
        {
            throw new System.NotImplementedException();
        }
    }
}
