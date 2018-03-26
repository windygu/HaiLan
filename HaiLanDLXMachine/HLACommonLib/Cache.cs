using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLACommonLib
{
    /// <summary>
    /// 全局统一的缓存类
    /// </summary>
    public class Cache
    {
        private SortedDictionary<string, object> dic = new SortedDictionary<string, object>();
        private static volatile Cache instance = null;
        private static object lockHelper = new object();

        private Cache()
        {

        }
        public void Add(string key, object value)
        {
            dic.Add(key, value);
        }
        public void Remove(string key)
        {
            dic.Remove(key);
        }

        public object this[string key]
        {
            get
            {
                if (dic.ContainsKey(key))
                    return dic[key];
                else
                    return null;
            }
            set { dic[key] = value; }
        }

        public static Cache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new Cache();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
