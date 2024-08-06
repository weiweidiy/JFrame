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
                case 101: //增加攻速
                    return new BufferAttackSpeedUp(Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId));
                case 103: //增加状态抵抗
                    return new BufferDebuffAntiUpgrade(Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId));
                case 104:
                    return new BufferSkillDmgUp(Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId));
                case 201:
                    return new BufferShield(Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId));
                case 202:
                    return new DeBufferStunning(Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId));
                case 998:
                    return new DeBufferAttackDown(Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId));
                case 999:
                    return new DeBufferAttackDown(Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId));
                default:
                    throw new Exception("没有实现指定的技能buff " + buffId);
            }
        }
        
    }
}


//var type = Type.GetType("JFrame.BufferAttackDown");
//object[] args = new object[4] { Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId) };
//var buff = (Buffer) Activator.CreateInstance(type, args);
//return buff;
