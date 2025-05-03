using Log;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace TetrisWpf
{
    class ConnectDB
    {
        private readonly string path;

        public List<Scores> collections;

        public ConnectDB(string path)
        {
            this.path = path;
        }

        public void ReadDB()
        {
            try
            {
                if (File.Exists(path))
                {
                    var db = new SqliteConnection($"Data Source = {path}");
                    db.Open();

                    var sql = $"SELECT id, player, scope FROM table_scope ORDER BY scope DESC";

                    var command = new SqliteCommand(sql, db);

                    var reader = command.ExecuteReader();

                    if (!reader.HasRows) throw new Exception("Таблица пуста");

                    collections = new List<Scores>();

                    while (reader.Read())
                    {
                        var scope = new Scores()
                        {
                            Id = reader.GetInt32(0),
                            Player = reader.GetString(1),
                            Score = reader.GetInt32(2)
                        };
                        collections.Add(scope);
                    }

                    db.Close();
                    //int it = 0;
                    //foreach (var item in collections)
                    //{
                    //    it++;
                    //    Console.WriteLine($"{it}: {item.Player} {item.Score}");
                    //}
                }
                else
                    throw new Exception("Файл не найден!");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }

        public void WriteDB(string Player, int Scope)
        {
            try
            {
                SqliteConnection db = new SqliteConnection($"Data Source = {path}");
                SqliteCommand command;
                string sql;
                int reader;
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    db.Open();
                    sql = $"CREATE TABLE table_scope(id INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT, player TEXT NOT NULL, scope INTEGER NOT NULL)";
                    
                    command = new SqliteCommand(sql, db);

                    reader = command.ExecuteNonQuery();

                    db.Close();
                }

                db.Open();

                sql = $"INSERT INTO table_scope(player, scope) VALUES ('{Player}', '{Scope}')";

                command = new SqliteCommand(sql, db);

                reader = command.ExecuteNonQuery();

                db.Close();

            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }
    }
}
