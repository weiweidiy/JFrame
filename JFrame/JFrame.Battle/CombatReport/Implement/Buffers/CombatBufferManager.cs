using System.Collections.Generic;

namespace JFrame
{

    public class BufferInfo
    {
        public string uid;
        public int id;
        public Dictionary<int, ActionInfo> actionsData;

    }

    public class CombatBufferManager : BaseContainer<CombatBuffer>, ICombatUpdatable
    {

       

        public void Initialize( CombatContext context, List<BufferInfo> lstBuffers)
        {
            
            foreach (var item in lstBuffers)
            {

            }
        }

        public CombatBuffer CreateBuffer(BufferInfo bufferInfo, CombatContext context)
        {
            var buffer = new CombatBuffer();
            var actionFactory = new CombatActionFactory();

            actionFactory.CreateActions(bufferInfo.actionsData, buffer)

            return null;
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