using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Slippor.Common.WebWorker;

namespace Slippor.Rent.Common.Spider
{
    /// <summary>
    /// 赶集蜘蛛
    /// </summary>
    public class GanjiSpider : AbstractSpider
    {
        private readonly IWebWorker _webWorker = new GanjiWebWorker();

        #region Overrides of AbstractSpider

        protected override IWebWorker WebWorker
        {
            get { return _webWorker; }
        }

        #endregion
    }
}
