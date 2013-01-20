using System;
using System.Collections.Generic;
using Slippor.Common.WebWorker;

namespace Slippor.Rent.Common.Spider
{
    public abstract class AbstractSpider : ISpider
    {
        protected abstract IWebWorker WebWorker { get; }
        protected virtual IAgentInfo AgentInfo { get { return null; } }

        public String GetContent(String url)
        {
            if (WebWorker != null)
            {
                if (AgentInfo != null)
                {
                    WebWorker.SetAgentInfo(AgentInfo);
                }
                return WebWorker.AchieveResponse(url, null, null, true, RequestMethod.Get);
            }
            throw new InvalidOperationException("WebWorker不应为空。");
        }

        public String PostContent(String url, Dictionary<String, String> postData)
        {
            if (WebWorker != null)
            {
                if (AgentInfo != null)
                {
                    WebWorker.SetAgentInfo(AgentInfo);
                }
                return WebWorker.AchieveResponseWithPostData(url, postData, null, true, RequestMethod.Post);
            }
            throw new InvalidOperationException("WebWorker不应为空。");
        }
    }
}
