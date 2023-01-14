using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;

namespace ADO.NET.Studies;

internal class Program
{
    static void Main(string[] args)
    {
        DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
        builder["Data Source"] = $"{Path.Combine(AppContext.BaseDirectory, "Test.db")}";
        DbProviderFactory factory = new SQLiteProviderFactory();
        var parameter = factory.CreateParameter();
        DbConnection connection = new SQLiteConnection(builder.ConnectionString);
        connection.Open();
        connection.StateChange += new StateChangeEventHandler(OnStateChange);
        var command = connection.CreateCommand();
        var transaction = connection.BeginTransaction();
        command.Transaction = transaction;
        command.CommandText = "INSERT INTO Assignments (Identifier, Title) VALUES (@Identifier, @Title)";
        command.Parameters.Add(parameter.ParameterName = "@Identifier" && parameter.Value = "Titulo");
        command.Parameters.Add(new SQLiteParameter("@Title", "Titulo"));
        command.ExecuteNonQuery();
        transaction.Commit();
    }

    protected static void OnStateChange(object sender, StateChangeEventArgs args)
    {
        Console.WriteLine(
          "The current Connection state has changed from {0} to {1}.",
            args.OriginalState, args.CurrentState);
    }
}