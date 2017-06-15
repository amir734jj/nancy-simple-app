using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Mono.Data.Sqlite;
using ViewModels;

namespace DataBaseManager {
    public class DBSqlite {

        private static String _CONNECTION_STRING_ = "URI=file:db.sqlite";

        public static User AddUser(User user) {
            String sql = String.Format(
                             @"INSERT INTO Users (Firstname, Lastname, Email, Username, Password)
                               VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');",
                             user.Firstname, user.Lastname, user.Email, user.Username, user.Password
                         );

            return ExecuteSQL(sql, true, user);
        }

        public static User GetUser(User user) {
            String sql = String.Format(
                             @"SELECT Firstname, Lastname, Email, Username, Password FROM Users
                               WHERE Username='{0}' AND Password='{1}';",
                             user.Username, user.Password
                         );
                
            return ExecuteSQL(sql, false, user);
        }

        public static User ExecuteSQL(String sql, Boolean flag, User userViewModelObject) {
            User user = null;
            IDbConnection dbcon = new SqliteConnection(_CONNECTION_STRING_);

            dbcon.Open();
            IDbCommand dbcmd = dbcon.CreateCommand();

            dbcmd.CommandText = sql;

            if (flag) {
                dbcmd.ExecuteNonQuery();
                user = userViewModelObject;
            } else {
                IDataReader reader = dbcmd.ExecuteReader();
                if (reader.Read()) {
                    user = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                }
            }

            // clean up
            dbcmd.Dispose();
            dbcon.Close();

            return user;
        }
    }
}

