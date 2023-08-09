using System;
using System.Collections.Generic;
using System.Text;
using JFrame.Common.Interface;
using System.Linq;

namespace JFrame.Common
{
    public class ProcesserManager
    {
        /// <summary>
        /// 数据加工处理器列表
        /// </summary>
        protected List<IProcesser> _lstProcessers = new List<IProcesser>();

        public ProcesserManager(List<IProcesser> processers)
        {
            _lstProcessers = processers ?? _lstProcessers;
        }

        public ProcesserManager(params IProcesser[] processers)
        {
            _lstProcessers = processers.ToList();
        }

        /// <summary>
        /// 加添加工处理器
        /// </summary>
        /// <param name="processer"></param>
        /// <returns></returns>
        public ProcesserManager AddProcesser(IProcesser processer)
        {
            _lstProcessers.Add(processer);
            return this;
        }

        /// <summary>
        /// 获取处理结果
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] GetResult(byte[] bytes)
        {
            byte[] result = (byte[])bytes.Clone();
            if (_lstProcessers != null)
            {
                foreach (var processer in _lstProcessers)
                {
                    result = processer.Process(result);
                }
            }
            return result;
        }
    }
}
