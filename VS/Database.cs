using MySql.Data.MySqlClient;
using System;

namespace ServerWk3 {
	class Database {
		MySqlConnection conn = new MySqlConnection();

		public void Connect(string host, int port, string db, string user, string password) {
			conn.ConnectionString = $"server={host};user={user};database={db};port={port};password={password}";

			conn.Open();
		}

		public void Disconnect() {
			conn.Close();
		}

		public void Query(string query) {
			try {
				using(MySqlCommand cmd = new MySqlCommand(query, conn)) {
					using(MySqlDataReader reader = cmd.ExecuteReader()) {
						if(!reader.HasRows) {
							return;
						}

						int i;
						int fieldCount = reader.FieldCount;
						string text;

						while(reader.Read()) {
							text = string.Empty;

							for(i = 0; i < fieldCount; ++i) {
								text += reader[i] + (i == fieldCount - 1 ? "" : ", ");
							}

							Console.WriteLine(text);
						}
					}
				}
			} catch(Exception e) {
				Console.WriteLine(e.ToString());
			}
		}
	}
}
