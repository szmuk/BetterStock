using System;
using System.IO;
using System.Text;
using System.Windows;

namespace BetterStock
{
    class Logger
    {
        private StockValues previous;

        public void LogValues(StockValues data, string filePath)
        {
            var input = PrepareData(data);

            try
            {
                using (var file = new StreamWriter(filePath, true))
                {
                    file.WriteLine(input);

                    file.Close();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Error occurred: {0}", ex.Message);
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private string PrepareData(StockValues data)
        {
            var timeStamp = Environment.NewLine + "< " + DateTime.Now.ToString("yy-MM-dd HH:mm tt") + ">" + Environment.NewLine;

            var sb = new StringBuilder();

            sb.Append(timeStamp);

            if (previous != null)
            {
                if (!previous.Wig.Equals(data.Wig))
                    sb.Append("WIG: " + data.Wig + Environment.NewLine);

                if (!previous.Wig20.Equals(data.Wig20))
                    sb.Append("WIG20: " + data.Wig20 + Environment.NewLine);

                if (!previous.Wig20Fut.Equals(data.Wig20Fut))
                    sb.Append("WIG20FUT: " + data.Wig20Fut + Environment.NewLine);

                if (!previous.Wig20Usd.Equals(data.Wig20Usd))
                    sb.Append("WIG20USD: " + data.Wig20Usd + Environment.NewLine);

                if (!previous.MWig40.Equals(data.MWig40))
                    sb.Append("mWIG40: " + data.MWig40 + Environment.NewLine);

                if (!previous.SWig80.Equals(data.SWig80))
                    sb.Append("sWIG80: " + data.SWig80 + Environment.NewLine);
            }
            else
            {
                sb.Append(data.ToString());
            }
            
            previous = new StockValues(data);

            if (sb.ToString().Equals(timeStamp))
            {
                sb.Append("No changes since last check." + Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
