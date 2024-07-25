using System;

namespace JFrame
{
    public class BufferFactory
    {
        BufferDataSource dataSource;// = new BufferDataSource();
        public BufferFactory(BufferDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public virtual Buffer Create(int buffId, int foldCount = 1)
        {
            switch(buffId)
            {
                case 999:
                    return new BufferAttackDown(Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId));
                default:
                    throw new Exception("没有实现指定的技能buff " + buffId);
            }
        }
        
    }
}