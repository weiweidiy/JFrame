

using System;
using System.Collections.Generic;


namespace JFrame
{

    public abstract class CombatPlayer
    {
        /// <summary>
        /// 开始播放
        /// </summary>
        public event Action onPlayerStart;


        /// <summary>
        /// 退出播放
        /// </summary>
        public event Action<bool> onPlayerExit;
        public void NotifyExit(bool win) => onPlayerExit?.Invoke(win);


        /// <summary>
        /// 战报结果
        /// </summary>
        protected CombatReport report;

        /// <summary>
        /// 战报解析器
        /// </summary>
        protected CombatReprotParser parser;

        /// <summary>
        /// 加载战报
        /// </summary>
        /// <param name="report"></param>
        public virtual void LoadReport(CombatReport report)
        {
            this.report = report;
            parser = new CombatReprotParser(report.report);
        }

        public virtual void Release() { }


        bool isPlaying;
        /// <summary>
        /// 流逝的总时间
        /// </summary>
        float escapeTime = 0f;
        

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            escapeTime = 0f;
            isPlaying = true;

            onPlayerStart?.Invoke();
        }

        public void Stop()
        {
            escapeTime = 0f;
            isPlaying = false;
        }

        /// <summary>
        /// 跳过
        /// </summary>
        public void Skip()
        {
            escapeTime = 100;
        }

        /// <summary>
        /// 重播
        /// </summary>
        public abstract void Replay();

        public abstract float GetDeltaTime();



        public void Update()
        {
            if (!isPlaying)
                return;

            escapeTime += GetDeltaTime();

            //获取需要播放的数据
            var lstData = parser.GetData(escapeTime);
            if (lstData.Count > 0)
            {
                //Debug.LogError("播放");
                foreach (var data in lstData)
                {
                    //Debug.Log("play " + data.ReportType + " frame " + escapeTime);
                    PlayData(data);
                }
            }

            if (parser.Count() == 0)
            {
                //Debug.LogError("播放完毕");
                Stop();

                //战报结束了
                OnReportEnd(report.winner == 1);
            }
        }

        protected virtual void OnReportEnd(bool result)
        {
            
        }

        void PlayData(ICombatReportData data)
        {
            var actionName = data.ReportType;
            switch (actionName)
            {
                case ReportType.StartMove:
                    {
                        PlayStartMove(data);
                    }
                    break;
                case ReportType.EndMove:
                    {
                        PlayEndMove(data);
                    }
                    break;
                case ReportType.ActionCast:
                    {
                        PlayAction(data);
                    }
                    break;
                case ReportType.Damage:
                    {
                        PlayDamage(data);
                    }
                    break;
                case ReportType.Dead:
                    {
                        PlayDead(data);
                    }
                    break;
                case ReportType.AddBuffer:
                    {
                        PlayAddBuffer(data);
                        // Debug.LogError("AddBuffer");
                    }
                    break;
                case ReportType.Heal:
                    {
                        PlayHeal(data);
                    }
                    break;
                case ReportType.RemoveBuffer:
                    {
                        PlayRemoveBuffer(data);
                    }
                    break;
                case ReportType.Reborn:
                    {
                        PlayReborn(data);
                    }
                    break;
                case ReportType.MaxHpUp:
                    {
                        //Debug.LogError("MaxHpUp" + (int)data.Arg[2]);
                        //PlayHeal(data);
                    }
                    break;
                case ReportType.ActionCD:
                    {
                        PlayActionCD(data);
                    }
                    break;
                case ReportType.DebuffAnti:
                    {
                        //PlayDebuffAnti(data);
                    }
                    break;
                case ReportType.UpdateBuffer:
                    {

                    }
                    break;
                default:
                    throw new System.Exception("没有实现的动作类型" + actionName);
            }
        }

        protected abstract void PlayActionCD(ICombatReportData data);
        protected abstract void PlayReborn(ICombatReportData data);
        protected abstract void PlayRemoveBuffer(ICombatReportData data);
        protected abstract void PlayAddBuffer(ICombatReportData data);

        protected abstract void PlayStartMove(ICombatReportData data);

        protected abstract void PlayEndMove(ICombatReportData data);

        /// <summary>
        /// 播放攻击动作
        /// </summary>
        /// <param name="data"></param>
        protected abstract void PlayAction(ICombatReportData data);

        /// <summary>
        /// 播放各种掉血，加血，添加BUFF，移除BUFF，复活等
        /// </summary>
        /// <param name="data"></param>
        protected abstract void PlayDamage(ICombatReportData data);

        /// <summary>
        /// 播放死亡
        /// </summary>
        /// <param name="data"></param>
        protected abstract void PlayDead(ICombatReportData data);


        /// <summary>
        /// 播放加血
        /// </summary>
        /// <param name="data"></param>
        protected abstract void PlayHeal(ICombatReportData data);

        /// <summary>
        /// 播放结果
        /// </summary>
        /// <param name="win"></param>
        protected abstract void PlayResult(int win);
    }
}
