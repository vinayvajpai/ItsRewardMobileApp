using System;
namespace itsRewards.LocalDataBase
{
	public class InitDataBaseTable : DataBase
    {
        protected override void InitDatabaseTables()
        {
            Connection.CreateTable<WalletDatabaseTable>();
        }
    }
}

