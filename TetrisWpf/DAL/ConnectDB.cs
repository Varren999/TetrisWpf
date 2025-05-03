using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections.Generic;
using System.IO;
using Log;
using System;

namespace TetrisWpf
{
    class ConnectDB
    {

        private readonly SqliteConnection _connection;

        public ConnectDB(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    using (var connection = new SqliteConnection($"Data Source = {path}"))
                    {
                        connection.Open();
                        var sql = "CREATE TABLE table_score(id INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT, player TEXT NOT NULL, score INTEGER NOT NULL)";
                        if (connection.Execute(sql) > 0)
                            Logger.Info($"{path} создана");
                        else
                            throw new Exception("Ошибка при создании базы данных");
                    }
                }

                _connection = new SqliteConnection($"Data Source = {path}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.TargetSite + ex.Message);
            }
        }

        public IEnumerable<Scores> GetAll()
        {
            const string sql = "SELECT id, player, score FROM table_score ORDER BY score DESC";
            return _connection.Query<Scores>(sql);
        }

        public bool Add(Scores score)
        {
            var sql = "INSERT INTO table_score(player, score) VALUES (@Player, @Score)";
            var result = _connection.Execute(sql, new { score.Player, score.Score });
            return result > 0;
        }
    }
}
