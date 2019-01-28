using System.Collections.Generic;
using PX.Data;
using PX.Data.Update.LicensingService;
using PX.SM;

namespace Tests.Base
{
	public class LicensePolicyMock : IPXLicensePolicy
	{
		public void OnDataRowInserted(string tableName)
		{
		}
		public void RegisterErpTransaction(PXGraph pxGraph)
		{
		}
		public bool IsLicenseSuspended(PXLicense pxLicense)
		{
			return false;
		}
		public bool HasConstraintsViolations(PXLicense license)
		{
			return false;
		}
		public InstanceStatistics GetStatistics()
		{
			return null;
		}
		public void RemoveSession(string sessionID)
		{
		}

		public IEnumerable<RowActiveUserInfo> GetCurrentApiUsers()
		{
			return new RowActiveUserInfo[0];
		}

		public void CheckApiUsersLimits()
		{
		}
	}
}