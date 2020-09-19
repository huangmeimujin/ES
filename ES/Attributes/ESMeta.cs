using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ES.Attributes
{
    public class ESMeta
    {
        public Servers Server { get; private set; }

        public string Index { get; private set; }

        public bool AutoMap { get; private set; } = true;

        private static readonly ConcurrentDictionary<Type, ESMeta> Cache = new ConcurrentDictionary<Type, ESMeta>();

        public ESMeta()
        {

        }
        public static ESMeta Get<T>()
        {
            if (!Cache.TryGetValue(typeof(T), out ESMeta esmeta))
            {
                var attr=(ESMetaAttribute)typeof(T).GetCustomAttributes(typeof(ESMetaAttribute),true)?.FirstOrDefault();
                if (attr == null)
                {
                    throw new Exception($"{typeof(T).FullName}没有设置ESMetaAttribute");
                }
                esmeta = new ESMeta()
                {
                    Server = attr.Server,
                    Index = attr.Index,
                    AutoMap = attr.AutoMap
                };
                Cache.TryAdd(typeof(T),esmeta);
            }
            return esmeta;
        }


    }

    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =true)]
    public class ESMetaAttribute : Attribute
    {
        public string Server { get;}

        public string Index { get;}

        public bool AutoMap { get; } = true;

        public ESMetaAttribute(string server,string index,bool automap=true)
        {
            this.AutoMap = automap;
            if (!string.IsNullOrEmpty(server))
                this.Server = server;
            else
                throw new ArgumentException(server);
            if (!string.IsNullOrEmpty(index))
                this.Index = index;
            else
                throw new ArgumentException(index);
        }

    }
}
