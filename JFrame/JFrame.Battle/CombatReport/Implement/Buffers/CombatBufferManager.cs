using System.Collections.Generic;

namespace JFrame
{

    public class BufferInfo
    {
        public string uid;
        public int id;
        public Dictionary<int, ActionInfo> actionsData;

    }

    public class CombatBufferManager : UpdateableContainer<CombatBuffer>, ICombatUpdatable
    {

        public CombatBuffer CreateBuffer(BufferInfo bufferInfo, CombatUnit caster, CombatUnit owner,  CombatContext context)
        {
            var buffer = new CombatBuffer();
            var actionFactory = new CombatActionFactory();
            buffer.OnAttach(owner);
            buffer.Initialize(bufferInfo, actionFactory.CreateActions(bufferInfo.actionsData, buffer, context), caster);
            AddItem(buffer);
            return buffer;
        }


        public void Update(BattleFrame frame)
        {
            foreach(var buffer in GetAll())
            {
                buffer.Update(frame);
            }
        }
    }

}