using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Elasticsearch.Net;

namespace ES
{
    public class ESConfigHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly IConfigurationRoot Cfg;
        /// <summary>
        /// 
        /// </summary>
        private static readonly ConcurrentDictionary<Servers, ElasticClient> ClientMaps = new ConcurrentDictionary<Servers, ElasticClient>();

        static ESConfigHelper()
        {
            var cb = new ConfigurationBuilder().AddJsonFile("ES.json",false,true);
            Cfg = cb.Build();
        }

        public static ElasticClient GetElasticClient(Servers servers)
        {
            var value = Cfg.GetSection(servers.Name).Get<ESItem>();
            var uris = value.Address.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(a => Uri.IsWellFormedUriString(a, UriKind.Absolute))
                .Select(a => new Uri(a));
            if (uris.Count()==0)
            {
                throw new Exception($"ES:{servers.Name}没有配置有效的地址");
            }
            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool);
            if (!string.IsNullOrEmpty(value.User))
            {
                settings.BasicAuthentication(value.User,value.Pwd);
            }
            return new ElasticClient(settings);
        }

    }
    public class ESItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string User { get; set; }

        ///
        public string Pwd { get; set; }
    }
}
