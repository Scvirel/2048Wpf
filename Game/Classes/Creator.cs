using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace _2048Game
{
    internal static class Creator
    {
        internal static bool EndGame { get; set;}
        internal static Grid grid { get; set; }
        private static Style styleL { get; set; }
        private static Style styleB { get; set; }
        private static Canvas tpage { get; set; }
        public static Label curentRes { get; set; }
        public static Label bestRes { get; set; }
        private static int LastTempRow { get; set; }
        private static int LastTempColumn { get; set; }
        private static Random randomRow = new Random();
        private static Random randomCol = new Random();
        internal static Label[,] valueFields = new Label[4, 4];

        static public Random TwoOrFour = new Random();

        private static List<int> FreeRows()
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
        private static List<int> FreeColumsInRow(int row)
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
        private static void CreateFieldInRowCol(int Row, int Column, int value)
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
                if (CheckToEndGame())
                {
                    EndGame = true;
                    NewGame();
                }
            }
        }

        private static void NewGame()
        {
            var resetButton = new Button();
            resetButton.Content = "Reset";
            resetButton.FontFamily = new FontFamily("Consolas");
            resetButton.Width = 350;
            resetButton.Height = 60;
            resetButton.FontSize = 25;
            resetButton.HorizontalAlignment = HorizontalAlignment.Center;
            resetButton.VerticalAlignment = VerticalAlignment.Center;
            resetButton.Click += Reset_Click;
            resetButton.Style = styleB;

            var endGameCanvas = new Canvas();
            endGameCanvas.Margin = new Thickness(-92, -128, -83, -22);
            endGameCanvas.Children.Add(resetButton);

            tpage = endGameCanvas;
            Canvas.SetLeft(resetButton, 100);
            Canvas.SetTop(resetButton, 100);
            grid.Children.Add(endGameCanvas);
            Grid.SetColumnSpan(endGameCanvas, 4);
            Grid.SetRowSpan(endGameCanvas, 4);
            endGameCanvas.Background = new SolidColorBrush(Color.FromArgb(150,0,0,0));
            

           
        }

        private static bool CheckIfFree(int Row, int Column)
        {
            if (valueFields[Row, Column] == null) return true;
            else return false;
        }
        public static void CreateTwoFourField()
        {
            List<int> rows = FreeRows();
            int row = rows[randomRow.Next(0, rows.Count)];
            List<int> colums = FreeColumsInRow(row);
            int column = colums[randomCol.Next(0, colums.Count)];
            int value = GetTwoOrFour();
            CreateFieldInRowCol(row, column, value);
        }

        public static void StartGame(Grid g, Style l,Style b,Label curentResult,Label bestResult)
        {
            styleL = l;
            styleB = b;
            grid = g;
            curentRes = curentResult;
            bestRes = bestResult;
            CreateTwoFourField();
            CreateTwoFourField();
        }
        private static void StartGame()
        {
            CreateTwoFourField();
            CreateTwoFourField();
        }

        private static bool CheckToEndGame()
        {
            bool gameEnd = true;
            for(int row =0;row<4;row++)
            {
                for(int col = 0; col < 4; col++)
                {
                    LastTempRow = row;
                    LastTempColumn = col;
                    if (
                CheckExistingOfField(row - 1, col) ||
                CheckExistingOfField(row + 1, col) ||
                CheckExistingOfField(row, col + 1) ||
                CheckExistingOfField(row, col - 1)
                )
                        return false;
                }
            }
            return gameEnd;
        }
        private static bool CheckExistingOfField(int row, int col)
        {
            try
            {
                if (valueFields[row, col].Content.ToString() == valueFields[LastTempRow, LastTempColumn].Content.ToString())
                {
                    return true;
                }
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        private static void Reset_Click(object sender, RoutedEventArgs e)
        {
            EndGame = false;
            grid.Children.Remove(tpage);
            Reset();
        }
        public static void Reset()
        {
            curentRes.Content = "0"; 
            for (int inx = grid.Children.Count - 1; inx >= 0; inx--)
            {
                UIElement item = grid.Children[inx];
                if (item is Label && (item as Label).Name ==String.Empty)
                {

                    grid.Children.Remove(item);
                }
            }
            Array.Clear(valueFields, 0, valueFields.Length);

            StartGame();
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
