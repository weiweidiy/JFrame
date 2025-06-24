using JFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFrameTest
{
    public class JsonNetSerializer : IJsonSerializer
    {
        public string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T ToObject<T>(string str)
        {
            throw new NotImplementedException();
        }

        public object ToObject(string json, Type type)
        {
            throw new NotImplementedException();
        }

        public object ToObject(byte[] bytes, Type type)
        {
            var str = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject(str, type);
        }
    }
}
