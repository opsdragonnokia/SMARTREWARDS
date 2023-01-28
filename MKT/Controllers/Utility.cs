using System;
using Microsoft.Data.Sqlite;

namespace test.Controllers
{
	public class Utility
	{
		public Utility()
		{
		}
		public static  SqliteCommand CreateDBConnection()
		{
			string fileName = "./DB/DB1.db";

			var connection = new SqliteConnection($"Data Source='{fileName}'");
			
				connection.Open();
				var command = connection.CreateCommand();

			return command;
			


		}
	}
}

