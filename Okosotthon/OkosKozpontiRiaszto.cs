using System;
using System.Collections.Generic;
using System.Text;

namespace Okosotthon
{
    public class OkosKozpontiRiaszto : OkosEszkoz, IIdozitheto, IHangjelzo, IStatisztika
    {
        private int idozitoPerc;
        private bool veszjelzesAktiv;
        private int utolsoCsipogHossz;
        private int riasztasSzamlalo;

        public OkosKozpontiRiaszto(string azonosito, string nev)
            : base(azonosito, nev)
        {
            this.idozitoPerc = 0;
            this.veszjelzesAktiv = false;
            this.utolsoCsipogHossz = 0;
            this.riasztasSzamlalo = 0;
        }

        public override void ParancsVegrehajtasa(string parancs)
        {
            // simple command handling: "RIASZTAS" starts alarm, "LEALLIT" stops it
            if (parancs == "RIASZTAS")
            {
                this.VeszjelzesInditasa();
            }
            else if (parancs == "LEALLIT")
            {
                this.veszjelzesAktiv = false;
            }
        }

        public override string AllapotJelentes()
        {
            return $"KozpontiRiaszto: veszjelzes={this.veszjelzesAktiv}, idozito={this.idozitoPerc}min";
        }

        protected override bool OnTesztFuttatasa()
        {
            // simple self-test: always ok if online
            return true;
        }

        // IIdozitheto
        public void IdozitestBeallit(int perc)
        {
            if (perc < 0) perc = 0;
            this.idozitoPerc = perc;
        }

        public int GetMaradtIdo()
        {
            return this.idozitoPerc;
        }

        // IHangjelzo
        public void Csipog(int hosszu)
        {
            if (hosszu < 0) hosszu = 0;
            this.utolsoCsipogHossz = hosszu;
        }

        public void VeszjelzesInditasa()
        {
            this.veszjelzesAktiv = true;
            this.riasztasSzamlalo++;
        }

        // IStatisztika
        public string StatisztikatKeszit()
        {
            return $"KozpontiRiaszto {this.Nev}: riasztasok={this.riasztasSzamlalo}, idozito={this.idozitoPerc}min";
        }
    }
}
