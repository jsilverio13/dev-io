using System;

namespace DesignPatterns.FactoryMethod
{
    public class ExecucaoFactoryMethod
    {
        public static void Executar()
        {
            var sqlCn = DbFactory.Database(DataBase.SqlServer)
                                 .CreateConnector(@"minhaCS")
                                 .Connect();
            Connection.ExecuteCommand(@"select * from tabelaSql");
            Connection.Close();

            Console.WriteLine(@"");
            Console.WriteLine(@"--------------------------------");
            Console.WriteLine(@"");

            var oracleCn = DbFactory.Database(DataBase.Oracle)
                                    .CreateConnector(@"minhaCS")
                                    .Connect();
            Connection.ExecuteCommand(@"select * from tabelaOracle");
            Connection.Close();
        }
    }
}