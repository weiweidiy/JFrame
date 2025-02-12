using System;
using System.Collections.Generic;

namespace JFrame
{
    public class CombatBuffer : /*ICombatBuffer, */ICombatUpdatable, IUnique, IActionContent, ICombatAttachable<CombatUnit>
    {
        public string Uid { get; protected set; }

        public CombatExtraData ExtraData { get; set; }

        public CombatUnit Owner { get; private set; }

        public void Initialize(BufferInfo bufferInfo, List<CombatAction> combatActions, CombatUnit caster)
        {
            throw new NotImplementedException();
        }

        public void Update(BattleFrame frame)
        {
            throw new System.NotImplementedException();
        }

        public void OnAttach(CombatUnit target)
        {
            Owner = target;
        }

        public void OnDetach()
        {
            Owner = null;
        }
    }

}