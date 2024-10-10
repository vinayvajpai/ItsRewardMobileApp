using System;
namespace itsRewards.LocalDataBase
{
	public class WalletDatabaseTable
	{
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Type { get; set; }
        public string data { get; set; }
    }
}

