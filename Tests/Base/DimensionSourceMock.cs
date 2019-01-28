using PX.Data;

namespace Tests.Base
{
	public class DimensionSourceMock : PXDimensionAttribute.IDimensionSource
	{
		public string[] Dimensions()
		{
			return new string[]
			{
				"ACCGROUP",
				"ACCOUNT",
				"BIZACCT",
				"CASHACCOUNT",
				"COMPANY",
				"CONTRACT",
				"CONTRACTITEM",
				"COSTCODE",
				"CUSTOMER",
				"EMPLOYEE",
				"INITEMCLASS",
				"INLOCATION",
				"INSITE",
				"INSUBITEM",
				"INVENTORY",
				"LOCATION",
				"MLISTCD",
				"PROJECT",
				"PROTASK",
				"SALESPER",
				"SUBACCOUNT",
				"TMCONTRACT",
				"TMPROJECT",
				"VENDOR"
			};
		}
	}
}