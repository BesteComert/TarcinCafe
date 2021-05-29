using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarcinCafe.UI
{
    public class TasindiEventArgs:EventArgs
    {
        public int YeniMasa { get;}
        public int EskiMasa { get; }

        public TasindiEventArgs(int eski, int yeni)
        {
            EskiMasa = eski;
            YeniMasa = yeni;
        }
    }
}
