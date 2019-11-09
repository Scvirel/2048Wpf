using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf2048.Moduls.Animation;
using Wpf2048.Moduls.Create;

namespace Wpf2048.Moduls.Move
{
    class AddFields
    {
        public static void Add(int _min, int _val, int _coef, bool rows)
        {
            Tuple<Label, int, int> tuple1;
            Tuple<Label, int, int> tuple2;
            MoveFields moveFields = new MoveFields();
            for (int row_col = 0; row_col < 4; row_col++)
            {
                tuple1 = new Tuple<Label, int, int>(null, 0, 0);
                tuple2 = new Tuple<Label, int, int>(null, 0, 0);
                for (int col_row = _min; col_row != _val; col_row += _coef)
                {
                    if (rows)
                    {
                        tuple2 = Tuple.Create(Creator.valueFields[col_row, row_col], col_row, row_col);
                    }
                    else
                    {
                        tuple2 = Tuple.Create(Creator.valueFields[row_col, col_row], row_col, col_row);
                    }


                    if (tuple2.Item1 != null && tuple1.Item1 == null)
                    {
                        tuple1 = tuple2;
                        tuple2 = new Tuple<Label, int, int>(null, 0, 0);
                    }


                    if (tuple2.Item1 != null && tuple1.Item1 != null)
                    {
                        if (tuple1.Item1.Content.ToString() != tuple2.Item1.Content.ToString())
                        {
                            tuple1 = tuple2;
                        }
                        else if (tuple1.Item1.Content.ToString() == tuple2.Item1.Content.ToString())
                        {
                            int curentNumber = Int32.Parse(BeginEndResetGame.curentRes.Content.ToString());
                            int content = Int32.Parse(tuple2.Item1.Content.ToString()) + Int32.Parse(tuple1.Item1.Content.ToString());
                            int result = curentNumber + content;
                            if (result > MoveFields.Best)
                            {
                                MoveFields.Best = result;
                                BeginEndResetGame.bestRes.Content = MoveFields.Best.ToString();
                            }
                            BeginEndResetGame.curentRes.Content = (curentNumber + content).ToString();

                            Label label = Creator.CreateField(content);
                            Creator.grid.Children.Add(label);
                            Grid.SetRow(label, tuple1.Item2);
                            Grid.SetColumn(label, tuple1.Item3);
                            Creator.valueFields[tuple1.Item2, tuple1.Item3] = label;
                            Creator.valueFields[tuple2.Item2, tuple2.Item3] = null;
                            Creator.grid.Children.Remove(tuple2.Item1);
                            Creator.grid.Children.Remove(tuple1.Item1);

                            tuple1 = new Tuple<Label, int, int>(null, 0, 0);

                            MoveFields.NextTurn = true;
                            Animator.BeginAnimation(label);
                        }
                    }
                }
            }
        }
    }
}
