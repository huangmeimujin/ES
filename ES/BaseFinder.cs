using ES.Attributes;
using ES.Conds;
using Nest;
using System;
using System.Threading.Tasks;

namespace ES
{
    public abstract class BaseFinder<T,TCond>
        where T:class
        where TCond:BaseCondition
    {
        public ElasticClient Conn { get; }

        public ESMeta Meta { get; }

        /// <summary>
        /// 
        /// </summary>
        public BaseFinder()
        {
            this.Meta = ESMeta.Get<T>();

            this.Conn = ESConfigHelper.GetElasticClient(this.Meta.Server);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meta"></param>
        public BaseFinder(ESMeta meta)
        {
            this.Meta = meta;

            this.Conn = ESConfigHelper.GetElasticClient(this.Meta.Server);
        }

        /// <summary>
        /// 构建查询语句
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        protected abstract SearchDescriptor<T> GetBuilder(TCond cond);
    }
}
