using System.Windows;

namespace ConsoleApp
{
    internal class Blocks
    {
        private readonly Point[] block;

        public Point[] Block
        {
            get { return  block; }
        }

        public Blocks(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        block = new Point[] { new Point(5, 0), new Point(6, 0),         // [][]
                                              new Point(5, 1), new Point(6, 1) };       // [][]                                              
                    } break;

                case 1:
                    {
                        block = new Point[] { new Point(4, 0), new Point(5, 0), new Point(6, 0), new Point(7, 0) };   // [][][][]                                       
                    } break;

                case 2:
                    {
                        block = new Point[]{ new Point(5, 0), new Point(6, 0),                          // [][]
                                                              new Point(6, 1), new Point(7, 1) };       //   [][]
                    } break;

                case 3:
                    {
                        block = new Point[]{                  new Point(5, 1), new Point(6, 1),         //   [][]
                                             new Point(6, 0), new Point(7, 0) };                        // [][]
                    } break;

                case 4:
                    {
                        block = new Point[] { new Point(5, 0), new Point(6, 0), new Point(7, 0),        // [][][]
                                              new Point(5, 1) };                                        // []
                    } break;

                case 5:
                    {
                        block = new Point[]{ new Point(5, 0), new Point(6, 0), new Point(7, 0),         // [][][]
                                                                               new Point(7, 1) };       //     []
                    } break;

                case 6:
                    {
                        block = new Point[]{                  new Point( 6, 0),                         //   []
                                             new Point(5, 1), new Point(6, 1), new Point(7, 1) };       // [][][]
                    } break;
            }
        }
    }
}
