using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosotthonKozpont
    {
        private readonly List<OkosEszkoz> eszkozok;

        public OkosotthonKozpont()
        {
            this.eszkozok = new List<OkosEszkoz>();
        }

        public void EszkozHozzaadasa(OkosEszkoz eszkoz)
        {
            this.eszkozok.Add(eszkoz);
        }

        public void OsszesCsatlakoztatasa()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                eszkoz.Csatlakozas();
            }
        }

        public int RendszerDiagnosztikaFuttatasa()
        {
            int sikeresTeszt = 0;

            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz.DiagnosztikaFuttatasa())
                {
                    sikeresTeszt++;
                }
            }

            return sikeresTeszt;
        }

        // Start emergency alarms on all devices that implement IHangjelzo
        public void InditsOsszesVeszjelzest()
        {
            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IHangjelzo h)
                {
                    h.VeszjelzesInditasa();
                }
            }
        }

        // Collect statistics from all devices that implement IStatisztika
        public List<string> OsszesStatisztikaLekerdezese()
        {
            var eredmenyek = new List<string>();

            foreach (OkosEszkoz eszkoz in this.eszkozok)
            {
                if (eszkoz is IStatisztika s)
                {
                    eredmenyek.Add(s.StatisztikatKeszit());
                }
            }

            return eredmenyek;
        }
    }
}
