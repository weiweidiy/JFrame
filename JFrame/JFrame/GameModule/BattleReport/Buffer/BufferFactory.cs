using System;

namespace JFrame
{
    public class BufferFactory
    {
        BufferDataSource dataSource = new BufferDataSource();
        public BufferFactory(BufferDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public virtual Buffer Create(int buffId, int foldCount = 1)
        {
            return new BufferAttackUp(Guid.NewGuid().ToString(), buffId, foldCount, new float[] { });
        }
        
    }
}