using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatBaseUnitBuilder<T> : IJCombatUnitBuilder<T> where T : JCombatUnitInfo, new()
    {
        Dictionary<int, T> _unitCache = new Dictionary<int, T>();

        bool useCache = false;

        IJCombatAttrBuilder attrBuilder;
        IJCombatActionBuilder actionBuilder;
        public JCombatBaseUnitBuilder(IJCombatAttrBuilder attrBuilder, IJCombatActionBuilder actionBuilder)
        {
            this.actionBuilder = actionBuilder;
            this.attrBuilder = attrBuilder;
        }

        public bool UseCache { get => useCache; set => useCache = value; }
        

        public T Build(int key)
        {
            if(useCache)
            {
                if (_unitCache.ContainsKey(key))
                    return GetFromCache(key);
            }

            var info = Create(key);
            _unitCache[key] = info;
            return info;
        }

        protected virtual T GetFromCache(int key)
        {
            return _unitCache[key];
        }

        protected virtual T Create(int key)
        {
            var info = new T();
            info.Uid = Guid.NewGuid().ToString();
            info.AttrList = attrBuilder.Create(key);
            info.Actions = actionBuilder.Create(key);
            return info;
        }
    }
}
