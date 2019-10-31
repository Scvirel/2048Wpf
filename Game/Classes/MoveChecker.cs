using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace _2048Game
{
    public class MoveChecker
    {
        public MoveChecker()
        {
            best = Convert.ToInt32(Creator.bestRes.Content);
        }
        private Label curentScore { get; set; }
        private Label bestScore { get; set; }
        private int best { get; set; }
        public enum movePath : int { Left = 23, Up = 24, Right = 25, Down = 26 }
        public bool NextTurn { get; set; }
        private int Path { get; set; }

        public void SetPath(int _path)
        {
            Path = _path;
            switch (Path)
            {
                case (int)movePath.Left:
                    {
                        Move(0, 4, 1, false);
                        Add(0, 4, 1, false);
                        Move(0, 4, 1, false);
                    }
                    break;
                case (int)movePath.Right:
                    {
                        Move(3, -1, -1, false);
                        Add(3, -1, -1, false);
                        Move(3, -1, -1, false);
                    }
                    break;
                case (int)movePath.Up:
                    {
                        Move(0, 4, 1, true);
                        Add(0, 4, 1, true);
                        Move(0, 4, 1, true);
                    }
                    break;
                case (int)movePath.Down:
                    {
                        Move(3, -1, -1, true);
                        Add(3, -1, -1, true);
                        Move(3, -1, -1, true);
                    }
                    break;
                default:
                    break;
            }
        }
        private void Add(int _min, int _val, int _coef, bool rows)
        {
            Tuple<Label, int, int> tuple1;
            Tuple<Label, int, int> tuple2;

            for (int row_col = 0; row_col < 4; row_col++)
            {
                tuple1 = new Tuple<Label, int, int>(null, 0, 0);
                tuple2 = new Tuple<Label, int, int>(null, 0, 0);
                for (int col_row = _min; col_row != _val; col_row += _coef)
                {
                    if(rows)
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
                            int curentNumber = Int32.Parse(Creator.curentRes.Content.ToString());
                            int content = Int32.Parse(tuple2.Item1.Content.ToString()) + Int32.Parse(tuple1.Item1.Content.ToString());
                            int result = curentNumber + content;
                            if(result>best)
                            {
                                best = result;
                                Creator.bestRes.Content = best.ToString();
                            }
                            Creator.curentRes.Content = (curentNumber + content).ToString();
                            
                            Label label = Creator.CreateField(content);
                            Creator.grid.Children.Add(label);
                            Grid.SetRow(label, tuple1.Item2);
                            Grid.SetColumn(label, tuple1.Item3);
                            Creator.valueFields[tuple1.Item2, tuple1.Item3] = label;
                            Creator.valueFields[tuple2.Item2, tuple2.Item3] = null;
                            Creator.grid.Children.Remove(tuple2.Item1);
                            Creator.grid.Children.Remove(tuple1.Item1);
                            NextTurn = true;
                            
                            tuple1 = new Tuple<Label, int, int>(null, 0, 0);

                            DoubleAnimation widthAnimation = new DoubleAnimation();
                            widthAnimation.From = label.Width / 2;
                            widthAnimation.To = label.Width;
                            widthAnimation.Duration = TimeSpan.FromSeconds(0.2);


                            DoubleAnimation heightAnimation = new DoubleAnimation();
                            heightAnimation.From = label.Height / 2;
                            heightAnimation.To = label.Height;
                            heightAnimation.Duration = TimeSpan.FromSeconds(0.2);


                            label.BeginAnimation(Label.WidthProperty, widthAnimation);
                            label.BeginAnimation(Label.HeightProperty, heightAnimation);
                        }
                    }
                }
            }
        }
        private void Move(int _min, int _val, int _coef, bool rows)
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
