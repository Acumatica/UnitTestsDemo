using PX.Data;

namespace RB.RapidByte
{
    public class CountryMaint : PXGraph<CountryMaint>
    {
        public PXCancel<Country> Cancel;
        public PXSave<Country> Save;

        public PXSelect<Country> Countries;
    }
}