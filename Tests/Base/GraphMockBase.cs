using PX.Data;
using PX.Objects.ExtensionTests;

namespace Tests.Base
{
	public class GraphMockBase<TMock> : PXGraph<TMock>
		where TMock : PXGraph
	{
		public GraphMockBase()
		{
			foreach (PXCache cache in Caches.Values)
			{
				cache.Interceptor = new PXUIEmulatorAttribute();
			}
		}
	}
}