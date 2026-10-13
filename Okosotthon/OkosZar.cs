using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosZar : OkosEszkoz, IHangjelzo, IStatisztika
    {
        public bool ZartE { get; private set; }

        private string pinKod;
        private int sikeresKinyitasok;
        private int sikertelenProbalkozasok;
        private bool veszjelzesAktiv;
        private int utolsoCsipogHossz;

        public OkosZar(string azonosito, string nev, string pinKod)
            : base(azonosito, nev)
        {
            this.pinKod = pinKod;
            this.ZartE = true;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            if (parancs.StartsWith("NYITAS:"))
            {
                string megadottPin = parancs.Substring(7);

                if (megadottPin == this.pinKod)
                {
                    this.ZartE = false;
                    this.sikeresKinyitasok++;
                    // reset failed attempts after success
                    this.sikertelenProbalkozasok = 0;
                    this.veszjelzesAktiv = false;
                }
                else
                {
                    // wrong PIN: short beep, increment failed attempts
                    this.sikertelenProbalkozasok++;
                    this.Csipog(500);

                    // if too many failed attempts, escalate
                    if (this.sikertelenProbalkozasok >= 3)
                    {
                        this.VeszjelzesInditasa();
                    }
                }
            }
            else if (parancs == "ZARAS")
            {
                this.ZartE = true;
            }
        }

        public override string AllapotJelentes()
        {
            if (this.ZartE)
            {
                return "ZARVA";
            }

            return "NYITVA";
        }

        protected override bool OnTesztFuttatasa()
        {
            return true;
        }

        public override void GyariBeallitasokVisszaallitasa()
        {
            base.GyariBeallitasokVisszaallitasa();

            this.pinKod = "0000";
            this.ZartE = true;
            this.sikeresKinyitasok = 0;
            this.sikertelenProbalkozasok = 0;
            this.veszjelzesAktiv = false;
        }

        // IHangjelzo implementation
        public void Csipog(int hosszu)
        {
            if (hosszu < 0) hosszu = 0;
            this.utolsoCsipogHossz = hosszu;
            // simple simulation: do nothing else
        }

        public void VeszjelzesInditasa()
        {
            this.veszjelzesAktiv = true;
        }

        // IStatisztika implementation
        public string StatisztikatKeszit()
        {
            return $"OkosZar {this.Nev}: sikeres_kinyitasok={this.sikeresKinyitasok}, sikertelen_probalkozasok={this.sikertelenProbalkozasok}, veszjelzes={this.veszjelzesAktiv}";
        }
    }
}
