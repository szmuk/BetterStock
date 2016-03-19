using System;
using System.Text;

namespace BetterStock
{
    class StockValues
    {
        public StockValues()
        {
        }

        public StockValues(StockValues objectToCopy)
        {
            Wig = objectToCopy.Wig;
            Wig20 = objectToCopy.Wig20;
            Wig20Fut = objectToCopy.Wig20Fut;
            Wig20Usd = objectToCopy.Wig20Usd;
            MWig40 = objectToCopy.MWig40;
            SWig80 = objectToCopy.SWig80;
        }

        public float Wig { get; set; }

        public float Wig20 { get; set; }

        public float Wig20Fut { get; set; }

        public float Wig20Usd { get; set; }

        public float MWig40 { get; set; }

        public float SWig80 { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("WIG: " + Wig + Environment.NewLine);
            sb.Append("WIG20: " + Wig20 + Environment.NewLine);
            sb.Append("WIG20FUT: " + Wig20Fut + Environment.NewLine);
            sb.Append("WIG20USD: " + Wig20Usd + Environment.NewLine);
            sb.Append("mWIG40: " + MWig40 + Environment.NewLine);
            sb.Append("sWIG80: " + SWig80 + Environment.NewLine);

            return sb.ToString();
        }
    }
}
