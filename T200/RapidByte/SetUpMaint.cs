using PX.Data;

namespace RB.RapidByte
{
    public class SetupMaint : PXGraph<SetupMaint>
    {
        public PXSave<Setup> Save;
        public PXCancel<Setup> Cancel;

        public PXSelect<Setup> LastNumbers;
    }
}