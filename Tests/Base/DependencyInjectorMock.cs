using System;
using PX.Data;
using PX.Data.DependencyInjection;

namespace Tests.Base
{
	public class DependencyInjectorMock : IDependencyInjector
	{
		public void InjectDependencies(PXGraph graph, Type graphType, string prefix)
		{
		}
	}
}