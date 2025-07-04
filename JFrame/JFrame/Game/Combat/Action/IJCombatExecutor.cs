using JFramework;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatExecutor : IJCombatLifeCycle
    {
        void Execute( List<IJCombatUnit> targets);
    }
}
