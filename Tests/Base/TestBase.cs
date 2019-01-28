using System;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using PX.Data;
using PX.Data.DependencyInjection;

namespace Tests.Base
{
	public class TestBase : IDisposable
	{
		protected IContainer _Container;

		public TestBase()
		{
			var builder = new ContainerBuilder();
			RegisterServices(builder);
			_Container = builder.Build();
			var serviceLocator = new AutofacServiceLocator(_Container);
			ServiceLocator.SetLocatorProvider(() => serviceLocator);
			PXGraph.OnPrepare += (sender) =>
			{
				sender.Accessinfo = new AccessInfo
				{
					ScreenID = null,
					UserName = "admin",
					DisplayName = "admin",
					UserID = new Guid("B5344897-037E-4D58-B5C3-1BDFD0F47BF9"),
					BranchID = 1,
					CompanyName = "Tenant",
					BusinessDate = DateTime.Today
				};
				sender.Prototype = new PXGraph.PXGraphPrototype();
			};
		}

		public void Dispose()
		{
			if (_Container != null)
			{
				_Container.Dispose();
			}
		}

		//not really needed for RabidByte, but is required when working with Graphs from PX.Objects
		protected virtual void RegisterServices(ContainerBuilder builder)
		{
			builder
				.RegisterType<LicensePolicyMock>()
				.As<IPXLicensePolicy>()
				.SingleInstance();
			builder
				.RegisterType<DependencyInjectorMock>()
				.As<IDependencyInjector>();
			builder
				.RegisterType<DimensionSourceMock>()
				.As<PXDimensionAttribute.IDimensionSource>();
		}
	}
}
