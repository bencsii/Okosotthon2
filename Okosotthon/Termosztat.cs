using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Okosotthon
{
    public class Termosztat : OkosEszkoz
    {
        public double JelenlegiHomerseklet { get; private set; }
        public double CelHomerseklet { get; private set; }

        public Termosztat(string azonosito, string nev, double celHomerseklet)
            : base(azonosito, nev)
        {
            this.JelenlegiHomerseklet = 21.0;
            this.CelHomerseklet = celHomerseklet;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("BEALLIT_HOMERSEKLET:"))
            {
                string ertek = parancs.Substring(20);
                double homerseklet;

                // Parse using invariant culture so commands with dot as decimal separator
                // (e.g. "24.5") work consistently regardless of system locale.
                if (double.TryParse(ertek, NumberStyles.Float, CultureInfo.InvariantCulture, out homerseklet))
                {
                    this.CelHomerseklet = homerseklet;
                }
            }
        }

        public override string AllapotJelentes()
        {
            return "Jelenlegi homerseklet: " + this.JelenlegiHomerseklet
                + ", celhomerseklet: " + this.CelHomerseklet;
        }

        protected override bool OnTesztFuttatasa()
        {
            return this.CelHomerseklet >= 5.0
                && this.CelHomerseklet <= 35.0;
        }
    }
}
