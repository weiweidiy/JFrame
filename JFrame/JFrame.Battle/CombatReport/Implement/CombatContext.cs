using JFrame.Common.Interface;

namespace JFrame
{
    public class CombatContext
    {
        public virtual CombatManager CombatManager { get; set; }
        
        public virtual CombatBufferFactory CombatBufferFactory { get; set; }

        public ILogger Logger { get; set; }


    }

}