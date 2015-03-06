using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WcfHost
{
	class TestService
	{

		private readonly ServiceHost _host;
		public TestService()
		{
			var pipePath = "net.pipe://localhost/Math";
            NetNamedPipeBinding binding = new NetNamedPipeBinding();
			_host = new ServiceHost(typeof(MathService), new Uri(pipePath));

			ServiceMetadataBehavior smb = _host.Description.Behaviors.Find<ServiceMetadataBehavior>();
			
			smb = new ServiceMetadataBehavior();

			_host.Description.Behaviors.Add(smb);
			_host.AddServiceEndpoint(typeof(IMath), binding, pipePath);
			_host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexNamedPipeBinding(), pipePath + "/mex");
		}

		public async Task<string> StartAsync()
		{
			await Task.Factory.FromAsync(_host.BeginOpen(null, null), _host.EndOpen);
			return _host.BaseAddresses.First().AbsoluteUri.ToString();
		}

	}

	[ServiceContract]
	public interface IMath
	{
		[OperationContract]
		int Add(int x, int y);
	}

	public class MathService : IMath
	{
		public int Add(int x, int y)
		{
			unchecked
			{
				return x + y;
			}
		}
	}

}
