using System.Text;
using Slippor.Common.WebWorker;

namespace Slippor.Rent.Common
{
    public class GanjiWebWorker : RentWebWorker
    {
        public GanjiWebWorker()
        {
            Encoding = Encoding.UTF8;
        }
    }
}