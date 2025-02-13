using System;
using System.Collections.Generic;

namespace JFrame
{
    public class CombatBufferFactory : BaseContainer<BaseCombatBuffer>
    {
        public List<BaseCombatBuffer> CreateBuffers(Dictionary<int, ActionInfo> actionsInfo)
        {


            return null;
        }

        public BaseCombatBuffer CreateBuffer(BufferInfo bufferInfo, CombatContext context)
        {
            var buffer = new CombatBuffer();
            var actionFactory = new CombatActionFactory();
            buffer.Initialize(bufferInfo, actionFactory.CreateActions(bufferInfo.actionsData, buffer, context));
            Add(buffer);
            return buffer;
        }
    }
}