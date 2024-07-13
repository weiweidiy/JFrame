using System;
using System.Collections.Generic;

namespace JFrame
{

    public class BattleTeam
    {
        Dictionary<BattlePoint, IBattleUnit> units = new Dictionary<BattlePoint, IBattleUnit>();

        public BattleTeam(Dictionary<BattlePoint, IBattleUnit> units)
        {
            this.units = units;
        }

        /// <summary>
        /// 获取指定位置战斗单位
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IBattleUnit GetUnit(BattlePoint point)
        {
            if(units.ContainsKey(point)) 
                return units[point];

            throw new Exception("没有找到对应的点位 " + point.Point);
        }

        /// <summary>
        /// 获取指定位置单位
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IBattleUnit GetUnit(int point)
        {
            foreach(var btPoint in units.Keys)
            {
                if(btPoint.Point == point) 
                    return units[btPoint];
            }

            throw new Exception("没有找到对应的点位 " + point);
        }

        public void Update(BattleFrame frame)
        {
            foreach (var unit in units.Values)
            {
                (unit as BattleUnit).Update(frame);
            }
        }

        public int GetUnitCount()
        {
            return units.Count;
        }
    }
}