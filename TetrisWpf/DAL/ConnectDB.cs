using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System;
using System.Data.SqlClient;

namespace TetrisWpf
{
    class ConnectDB
    {
        public static ConnectDB Instance { get; private set; }

        private readonly SqliteConnection _connection;

        public ConnectDB( string path )
        {
            Instance = this;

            if( !File.Exists( path ) )
            {
                File.Create( path ).Close();
                using( var connection = new SqliteConnection( $"Data Source = {path}" ) )
                {
                    connection.Open();
                    var sql = "CREATE TABLE table_score(id INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT, player TEXT NOT NULL, score INTEGER NOT NULL)";
                    connection.Execute( sql );
                }
            }
            _connection = new SqliteConnection( $"Data Source = {path}" );
        }

        public IEnumerable<Scores> GetAll()
        {
            using( _connection )
            {
                _connection.Open();
                const string sql = "SELECT id, player, score FROM table_score ORDER BY score DESC LIMIT 15";
                return _connection.Query<Scores>( sql );
            }
        }

        public bool Add(Scores score)
        {
            if( score == null )
                throw new ArgumentNullException( nameof( score ) );

            try
            {
                using( _connection )
                {
                    _connection.Open();

                    var sql = "INSERT INTO table_score(player, score) VALUES (@Player, @Score)";
                    var result = _connection.Execute( sql, new { Player = score.Player, Score = score.Score } );

                    return result > 0;
                }
            }
            catch( Exception ex )
            {
                Trace.TraceError( ex.Message );
            }
            return false;
        }
    }
}
