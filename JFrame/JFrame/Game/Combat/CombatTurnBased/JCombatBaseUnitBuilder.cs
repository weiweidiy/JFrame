using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatBaseUnitBuilder : IJCombatUnitBuilder
    {
        Dictionary<int, JCombatUnitInfo> _unitCache = new Dictionary<int, JCombatUnitInfo>();

        bool useCache = false;

        IJCombatAttrBuilder attrBuilder;
        IJCombatActionBuilder actionBuilder;
        public JCombatBaseUnitBuilder(IJCombatAttrBuilder attrBuilder, IJCombatActionBuilder actionBuilder)
        {
            this.actionBuilder = actionBuilder;
            this.attrBuilder = attrBuilder;
        }

        public bool UseCache { get => useCache; set => useCache = value; }
        

        public JCombatUnitInfo Build(int key)
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

        protected virtual JCombatUnitInfo GetFromCache(int key)
        {
            return _unitCache[key];
        }

        protected virtual JCombatUnitInfo Create(int key)
        {
            var info = new JCombatUnitInfo();
            info.Uid = Guid.NewGuid().ToString();
            info.AttrList = attrBuilder.Create(key);
            info.Actions = actionBuilder.Create(key);
            return info;
        }
    }
}
