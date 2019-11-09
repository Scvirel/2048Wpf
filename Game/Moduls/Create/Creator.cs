using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Wpf2048.Moduls.Create
{
    internal  class Creator
    {
        
        internal static Grid grid { get; set; }
        internal static Style styleL { get; set; }
        internal static Style styleB { get; set; }


        private static Random randomRow = new Random();
        private static Random randomCol = new Random();
        internal static Label[,] valueFields = new Label[4, 4];

        static public Random TwoOrFour = new Random();

        private static List<int> FindFreeRows()
        {
            List<int> freeRows = new List<int>();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (valueFields[row, col] == null)
                    {
                        freeRows.Add(row++);
                        if (row == 4) break;
                        col = -1;
                    }
                }
            }
            return freeRows;
        }
        private static List<int> FindFreeColumsInRow(int row)
        {
            List<int> freeColums = new List<int>();

            for (int col = 0; col < 4; col++)
            {
                if (valueFields[row, col] == null)
                {
                    freeColums.Add(col);
                }
            }

            return freeColums;
        }
        private static int GetTwoOrFour()
        {
            int number = TwoOrFour.Next(1, 11);
            return (number == 1) ? 4 : 2;
        }
        internal static Label CreateField(int value)
        {
            Label labelToReturn = new Label { };
            labelToReturn.Style = styleL;
            labelToReturn.Content = value.ToString();
            labelToReturn.Background = new SolidColorBrush(GetColor(value));
            labelToReturn.Width =90;
            labelToReturn.Height = 90;
            labelToReturn.FontSize = 30;
            labelToReturn.FontFamily = new FontFamily("Consolas");

            return labelToReturn;
        }
        private static  void CreateFieldInRowCol(int Row, int Column, int value)
        {
            Label label = CreateField(value);
            valueFields[Row, Column] = label;
            grid.Children.Add(label);
            Grid.SetColumn(label, Column);
            Grid.SetRow(label, Row);

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

            if (grid.Children.Count >= 37)
            {
                if (BeginEndResetGame.CheckToEndGame())
                {
                    BeginEndResetGame.EndGame = true;
                    BeginEndResetGame.CreateEndGameMenu();
                }
            }
        }
        public static void CreateTwoFourField()
        {
            List<int> rows = Creator.FindFreeRows();
            int row = rows[Creator.randomRow.Next(0, rows.Count)];
            List<int> colums = FindFreeColumsInRow(row);
            int column = colums[randomCol.Next(0, colums.Count)];
            int value = GetTwoOrFour();
            CreateFieldInRowCol(row, column, value);
        }
        private static Color GetColor(int value)
        {
            switch (value)
            {
                case 2: return Color.FromRgb(204, 255, 255);
                case 4: return Color.FromRgb(128, 255, 255);
                case 8: return Color.FromRgb(27, 118, 245);
                case 16: return Color.FromRgb(34, 27, 245);
                case 32: return Color.FromRgb(137, 3, 255);
                case 64: return Color.FromRgb(213, 3, 255);
                case 128: return Color.FromRgb(255, 3, 209);
                case 256: return Color.FromRgb(255, 3, 91);
                case 512: return Color.FromRgb(255, 3, 3);
                case 1024: return Color.FromRgb(255, 146, 3);
                case 2048: return Color.FromRgb(255, 226, 3);
                default: return Color.FromRgb(146, 255, 3);
            }
        }

    }
}
