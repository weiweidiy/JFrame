using System;
using System.Diagnostics;

namespace JFrame
{
    public class CombatBullet : IUnique, IUpdateable, ICombatUpdatable
    {
        public string Uid {  get; private set; }

        CombatBaseExecutor executor;

        float delay = 0f;

        float delta = 0f;

        CombatUnit target;

        CombatExtraData data;

        public CombatBullet(CombatBaseExecutor executor, CombatUnit target, CombatExtraData data, float delay) { 
            this.executor = executor;
            Uid = Guid.NewGuid().ToString();
            this.delay = delay;
            this.target = target;
            this.data = data;
        }
        public void Update(CombatFrame frame)
        {
            delta += frame.DeltaTime;
            if(delta >= delay)
            {
                target.OnDamage(data);

                //生效
                if (executor.context.Logger != null)
                    executor.context.Logger.Log("bullet update");

                executor.Owner.RemvoeBullet(this);
            }

        }

        public void Update(IUpdateable value)
        {
            
        }


    }

}