using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Wpf2048.Moduls.Animation;
using Wpf2048.Moduls.Create;

namespace Wpf2048.Moduls.Move
{
    public class MoveFields
    {
        public static  int Best { get; set; }
        public enum movePath : int { Left = 23, Up = 24, Right = 25, Down = 26 }
        public static bool NextTurn { get; set; }
        private static int Path { get; set; }
        public static void SetBest(int best)
        {
            Best = best;
        }
        public static void SetPath(int path)
        {
            Path = path;
            switch (Path)
            {
                case (int)movePath.Left:
                    {
                        AddFields.Add(0, 4, 1, false);
                        Move(0, 4, 1, false);
                    }
                    break;
                case (int)movePath.Right:
                    {
                        AddFields.Add(3, -1, -1, false);
                        Move(3, -1, -1, false);
                    }
                    break;
                case (int)movePath.Up:
                    {
                        AddFields.Add(0, 4, 1, true);
                        Move(0, 4, 1, true);
                    }
                    break;
                case (int)movePath.Down:
                    {
                        AddFields.Add(3, -1, -1, true);
                        Move(3, -1, -1, true);
                    }
                    break;
                default:
                    break;
            }
        }
        private static void Move(int _min, int _val, int _coef, bool rows)
        {
            int min = _min;

            for (int row_col = 0; row_col < 4; row_col++)
            {
                min = _min;
                for (int col_row = _min; col_row != _val; col_row += _coef)
                {
                    if (rows)
                    {
                        if (Creator.valueFields[col_row, row_col] != null && min != col_row)
                        {
                            Creator.valueFields[min, row_col] = Creator.valueFields[col_row, row_col];
                            Grid.SetRow(Creator.valueFields[col_row, row_col], min );
                            Creator.valueFields[col_row, row_col] = null;
                            NextTurn = true;
                            min += _coef;
                        }
                        else if (Creator.valueFields[col_row, row_col] != null && min == col_row)
                        {
                            min += _coef;
                        }
                    }
                    else
                    {
                        if (Creator.valueFields[row_col, col_row] != null && min != col_row)
                        {
                            Creator.valueFields[row_col, min] = Creator.valueFields[row_col, col_row];
                            Grid.SetColumn(Creator.valueFields[row_col, col_row], min);
                            Creator.valueFields[row_col, col_row] = null;
                            NextTurn = true;
                            min += _coef;
                        }
                        else if (Creator.valueFields[row_col, col_row] != null && min == col_row)
                        {
                            min += _coef;
                        }
                    }

                }
            }
        }
    }
}
