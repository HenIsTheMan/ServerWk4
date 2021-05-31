using System;
using System.Data;

namespace ServerWk3 {
	class Program {
		static void Main(string[] args) {
			Database db = new Database();

			db.Connect("localhost", 3306, "test_db", "root", "password");

			DataTable results = db.Query("SELECT * FROM students");
			Console.Write('\n');
			Console.Write(results.Rows.Count);
			Console.Write("\n\n");
			foreach(DataRow row in results.Rows) {
				Console.WriteLine("{0}, {1}, {2}", row["id"], row["firstName"], row["lastName"]);
			}
			Console.Write('\n');

			goto endingPart;

			//just "class" can here

			//db.Query("INSERT INTO test_db.class (id, name) VALUES (7, \"Yes\");");
			//INSERT INTO `test_db`.`students` (`id`, `firstName`, `lastName`) VALUES ('3', 'ho', 'ho');

			db.Query("UPDATE test_db.class SET name = 'Yup'");

			db.Query("UPDATE test_db.students SET firstName = 'Ok', lastName = 'Can' WHERE id = 1;");

			//db.Query("DELETE FROM `test_db`.`class` WHERE (`id` = '4')");

			//diff kinds of ids??
			//insert multiple??
			//delete multiple??
			//delete all??

			//db.Query("SELECT id FROM test_db.class;");

			string[] texts = new string[]{
				"class",
				"enrolment",
				"students"
			};
			int textsLen = texts.Length;

			for(int i = 0; i < textsLen; ++i) {
				Console.WriteLine("> SELECT * FROM " + texts[i]);
				db.Query("SELECT * FROM " + texts[i]);
				Console.WriteLine("\n\n");
			}

			endingPart:

			//Console.WriteLine("\n\nPress the Enter key to continue...");
			//Console.ReadLine();

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();

			//System.Threading.Thread.Sleep(1000);

			db.Disconnect();
		}
	}
}
