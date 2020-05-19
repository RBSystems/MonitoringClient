using System;
using System.Collections.Generic;
using Crestron.SimplSharp.Reflection;
using Crestron.SimplSharpPro.CrestronThread;
using proAV.Core;
using proAV.Core.Net;
using proAV.Core.Utilities;

namespace MonitoringClient {
	public class ControlSystem : ProAvControlSystem {
		public Thread ProgramThread;
		public ControlSystem(): base(Assembly.GetExecutingAssembly(), 300) {}
		public List<Thread> ProgramThreads { get; set; }
		public TcpServer TcpServer { get; private set; }
		public CrestronInformationEngine CrestronInformationEngine { get; private set; }

		public override void Initialise(){
			ProgramUpdateChecker.AutoUpdateProgram = true;
			ProgramThreads = new List<Thread>();
			ProgramThreads.Add(ProgramThread = new Thread(StartProgram, null, Thread.eThreadStartOptions.Running));
			Program.ProgramStopTriggered += ProgramStopTriggered;
			AddProjectConsoleCommands();
		}

		public object StartProgram(object _) {
			var factory = new CommunicationsHandlerFactory();
			TcpServer = new TcpServer(factory, 200, 2);
			CrestronInformationEngine = new CrestronInformationEngine(TcpServer);
			return null;
		}

		private void ProgramStopTriggered(object sender_, EventArgs eventArgs_) {
			if (ProgramThreads.Count <= 0) {
				return;
			}
			ProgramThreads.ForEach(thread_ => thread_.Join());
		}

		private void AddProjectConsoleCommands() { }
	}
}