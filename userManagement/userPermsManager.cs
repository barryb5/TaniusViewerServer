using Microsoft.Data.Sqlite;

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
                accounts TEXT ONLY
            );
        ";
        command.ExecuteNonQuery();

        // command.ExecuteNonQuery();

        // Hardcode myself in every time just in case (and for testing)
        command.CommandText = 
        @"
            INSERT INTO users (email, name, accounts) VALUES ('bbalasingham@gmail.com', 'Barry Balasingham', 'all')
        ";

        command.ExecuteNonQuery();
    }
}
