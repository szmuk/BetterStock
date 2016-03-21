using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BetterStock
{
    class StockManager
    {
        private CancellationTokenSource CancellationToken;

        private Logger Log = new Logger();

        private const string StooqAddr = @"http://stooq.pl/";

        private const string WigId = "aq_wig_c2";

        private const string Wig20Id = "aq_wig20_c2";

        private const string Wig20FutId = "aq_fw20_c0";

        private const string Wig20UsdId = "aq_wig20usd_c2";

        private const string MWig40Id = "aq_mwig40_c2";

        private const string SWig80Id = "aq_swig80_c2";

        private string htmlOutput;

        public void StartRefreshingStockValues(int interval, string filePath)
        {
            CancellationToken = new CancellationTokenSource();

            new Task(() =>
            {
                while (true)
                {
                    if (!CancellationToken.Token.IsCancellationRequested)
                    {
                        var values = LoadStockValues();

                        Log.LogValues(values, filePath);
                        
                        Thread.Sleep(interval);
                    }
                    else
                    {
                        break;
                    }
                }
            }, CancellationToken.Token).Start();
        }

        public void StopRefreshingStockValues()
        {
            CancellationToken.Cancel();
        }

        private StockValues LoadStockValues()
        {
            var values = new StockValues();

            var wc = new WebClient();

            try
            {
                htmlOutput = wc.DownloadString(StooqAddr);

                values.Wig = GetValue(WigId);

                values.Wig20 = GetValue(Wig20Id);

                values.Wig20Fut = GetValue(Wig20FutId);

                values.Wig20Usd = GetValue(Wig20UsdId);

                values.MWig40 = GetValue(MWig40Id);

                values.SWig80 = GetValue(SWig80Id);
            }
            catch (WebException ex)
            {

                string errorMessage = string.Format("Error occurred: {0}", ex.Message);
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

            return values;
        }

        private float GetValue(string stockIndexId)
        {
            var match = Regex.Match(htmlOutput, @"<span id=" + stockIndexId + @">(?<val>\d*\.{0,1}\d*)<\/span>", RegexOptions.Singleline);

            return float.Parse(match.Groups["val"].ToString().Replace('.', ','));
        }
    }
}
