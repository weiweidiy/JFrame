using System.Collections.Generic;

namespace JFrame
{
    public abstract class CombatActionArgSourceBuilder
    {
        public abstract Dictionary<int, CombatActionArgSource> Build();
    }
}