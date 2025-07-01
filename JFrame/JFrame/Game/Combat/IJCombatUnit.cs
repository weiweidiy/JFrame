using JFramework;
using JFramework.Game;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatUnit : IUnique
    {
        /// <summary>
        /// 是否已死亡
        /// </summary>
        /// <returns></returns>
        bool IsDead();

        /// <summary>
        /// 获取属性对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uid"></param>
        /// <returns></returns>
        IUnique GetAttribute(string uid);

    }


    public class JCombatUnit : DictionaryContainer<IUnique>, IJCombatUnit
    {
        public string Uid { get; set; }

        public JCombatUnit(List<IUnique> attrList,  Func<IUnique, string> keySelector) : base(keySelector)
        {
            AddRange(attrList);
        }    

        public IUnique GetAttribute(string uid)
        {
            var attr = Get(uid);
            return attr;
        }

        public bool IsDead()
        {
            var attr = Get("Hp") as GameAttributeInt;

            if (attr == null)
                return true;

            return attr.CurValue <= 0;
        }
    }
}
