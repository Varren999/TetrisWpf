using System.Diagnostics;
using System;

namespace TetrisWpf
{
    class Scores
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public int Score { get; set; }

        public override string ToString() => $"{Player}\t\t\t{Score}";

    }
}
