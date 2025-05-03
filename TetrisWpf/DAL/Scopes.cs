using System.Diagnostics;
using System;

namespace TetrisWpf
{
    class Scores
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public int Score { get; set; }

        public Scores() { }
        public Scores(int id, string player, int score) 
        {
            Id = id;
            Player = player;
            Score = score;
        }

        public Scores(string player, int score)
        {
            Player = player;
            Score = score;
        }

        public override string ToString() => $"{Player}\t\t\t{Score}";

    }
}
