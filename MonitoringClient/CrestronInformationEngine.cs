using System;
using proAV.Core.CustomEventArgs;
using proAV.Core.Interfaces.Communication;

namespace MonitoringClient {
	public class CrestronInformationEngine {
		

		public CrestronInformationEngine(ITcpServer server_){
			server_.ClientDataReceived += ServerOnClientDataReceived;
		}

		private void ServerOnClientDataReceived(object sender_, TcpServerDataReceivedEventArgs tcpServerDataReceivedEventArgs_){
			throw new NotImplementedException();
		}
	}
}