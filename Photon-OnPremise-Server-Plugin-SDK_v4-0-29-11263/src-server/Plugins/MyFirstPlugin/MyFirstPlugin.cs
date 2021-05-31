using Photon.Hive.Plugin;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
//??

namespace MyFirstPlugin {
    public class MyFirstPlugin: PluginBase {
		Database db = new Database();

		public MyFirstPlugin() {
			db.Connect("localhost", 3306, "test_db", "root", "password");
		}

		~MyFirstPlugin() {
			db.Disconnect();
		}

		public override string Name => "MyFirstPlugin"; //The reserved plugin names are "Default" and "ErrorPlugin"

		//private IPluginLogger pluginLogger;

		//public override bool SetupInstance(IPluginHost host, Dictionary<string, string> config, out string errorMsg)
		//{
		//	this.pluginLogger = host.CreateLogger(this.Name);
		//	return base.SetupInstance(host, config, out errorMsg);
		//}

		public override void OnCreateGame(ICreateGameCallInfo info) {
            PluginHost.LogInfo(string.Format("OnCreateGame {0} by user {1}", info.Request.GameId, info.UserId));
            info.Continue(); // same as base.OnCreateGame(info);
        }

		public override void OnRaiseEvent(IRaiseEventCallInfo info) {
			base.OnRaiseEvent(info);
			if(info.Request.EvCode == 1) {
				string request = Encoding.Default.GetString((byte[])info.Request.Data);
				string response = "Message Received: " + request;
				PluginHost.BroadcastEvent(
				target: ReciverGroup.All,
				senderActor: 0,
				targetGroup: 0,
				data: new Dictionary<byte, object>() { { 245, response } },
				evCode: info.Request.EvCode,
				cacheOp: 0
				);
			}
		}
	}
}