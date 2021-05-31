using Photon.Hive.Plugin;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using System;

namespace MyFirstPlugin {
    public class MyFirstPlugin: PluginBase {
		Database db = new Database();

		public MyFirstPlugin() {
			db.Connect("localhost", 3306, "test_db", "root", "password");
		}

		~MyFirstPlugin() {
			db.Disconnect();
		}

		private static List<T> DataTableToList<T>(DataTable dt) {
			List<T> data = new List<T>();
			foreach(DataRow row in dt.Rows) {
				T item = GetItem<T>(row);
				data.Add(item);
			}
			return data;
		}

		private static T GetItem<T>(DataRow dr) {
			Type temp = typeof(T);
			T obj = Activator.CreateInstance<T>();
			foreach(DataColumn column in dr.Table.Columns) {
				foreach(PropertyInfo pro in temp.GetProperties()) {
					if(pro.Name != column.ColumnName) {
						continue;
					}

					pro.SetValue(obj, dr[column.ColumnName], null);
				}
			}
			return obj;
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

			switch(info.Request.EvCode){
				case 1: {
					/*string request = Encoding.Default.GetString((byte[])info.Request.Data);
					string response = "Message Received: " + request;
					PluginHost.BroadcastEvent(
						target: ReciverGroup.All,
						senderActor: 0,
						targetGroup: 0,
						data: new Dictionary<byte, object>() { { 245, response } },
						evCode: info.Request.EvCode,
						cacheOp: 0
					);*/

					DataTable dt = db.Query("SELECT * FROM students");
					List<Student> students = DataTableToList<Student>(dt);
					string response = string.Format("{0}", JsonConvert.SerializeObject(students));

					PluginHost.BroadcastEvent(
						recieverActors: new List<int>() { info.ActorNr },
						senderActor: 0,
						data: new Dictionary<byte, object>() { { 245, response } },
						evCode: info.Request.EvCode,
						cacheOp: CacheOperations.DoNotCache
					);

					break;
				}
				case 2: {
					string firstNameOfStudent = (string)info.Request.Data;
					DataTable dt = db.Query("SELECT * FROM students");
					List<Student> students = DataTableToList<Student>(dt);
					int studentCount = students.Count;

					bool isStudentPresent = false;
					for(int i = 0; i < studentCount; ++i) {
						if(students[i].firstName == firstNameOfStudent) {
							isStudentPresent = true;
							break;
						}
					}

					PluginHost.BroadcastEvent(
						recieverActors: new List<int>() { info.ActorNr },
						senderActor: 0,
						data: new Dictionary<byte, object>() { { 245, "Student with first name of \"" + firstNameOfStudent + (isStudentPresent ? "\" is enrolled." : "\" is not enrolled.") } },
						evCode: info.Request.EvCode,
						cacheOp: CacheOperations.DoNotCache
					);

					break;
				}
			}
		}
	}
}