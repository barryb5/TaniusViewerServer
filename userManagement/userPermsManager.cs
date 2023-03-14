using Microsoft.Data.Sqlite;
using System.Text.Json;

class UserPermsManager {
    static SqliteConnection connection = new SqliteConnection("Data Source=userManagement/users.db");

    public static void resetTable() {
        connection.Open();

        var command = connection.CreateCommand();
        
        /**
          * Emails have to be unique so that's gonna be the primary key ig
          * Name is name (self explanatory)
          * Accounts are gonna be stored as a json string of a list
          */

        command.CommandText =
        @"
            DROP TABLE IF EXISTS 'users';
            CREATE TABLE users (
                email PRIMARY KEY,
                name TEXT ONLY,
                password TEXT ONLY,
                accounts TEXT ONLY
            );
        ";
        command.ExecuteNonQuery();

        // command.ExecuteNonQuery();

        // Hardcode myself in every time just in case (and for testing)
        command.CommandText = 
        $@"
            INSERT INTO users (email, name, password, accounts) VALUES ('bbalasingham@gmail.com', 'Barry Balasingham', 'password', '{JsonSerializer.Serialize(new AccountList(new List<string>{ "mercury", "mike", "john" }))}')
        ";
        
        command.ExecuteNonQuery();
    }

    public static String getUserAccounts(String email, String password) {
        var command = connection.CreateCommand();

         command.CommandText = 
         $@"
            SELECT accounts FROM user WHERE email = {email} AND password = {password}
         ";

        var reader = command.ExecuteReader();

        if (reader.Read()) {
            return reader.GetString(0);
        } else {
            return "";
        }
    }
}
