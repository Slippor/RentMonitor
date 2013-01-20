using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slippor.Rent.Common
{
    public abstract class AbstractFilterBuilder : IFilterBuilder
    {
        /// <summary>
        /// 查询基准Url
        /// </summary>
        protected abstract String QueryBaseUrl { get; }

        public String BuildFilterUrl(FilterConfig filterConfig)
        {
            return String.Format("{0}{1}", QueryBaseUrl, BuildQueryString(filterConfig));
        }

        /// <summary>
        /// 通过过滤配置来构造QueryString
        /// </summary>
        /// <param name="filterConfig"></param>
        /// <returns></returns>
        protected abstract String BuildQueryString(FilterConfig filterConfig);
    }
}
