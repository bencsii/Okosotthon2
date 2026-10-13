using System;

namespace Okosotthon
{
    public interface IIdozitheto
    {
        void IdozitestBeallit(int perc);
        int GetMaradtIdo();
    }

    public interface IHangjelzo
    {
        void Csipog(int hosszu);
        void VeszjelzesInditasa();
    }

    public interface IStatisztika
    {
        string StatisztikatKeszit();
    }
}
