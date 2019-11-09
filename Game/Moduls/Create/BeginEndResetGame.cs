using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf2048.Moduls.Create
{
    class BeginEndResetGame
    {
        
        private static Canvas tpage { get; set; }
        private static int LastTempRow { get; set; }
        private static int LastTempColumn { get; set; }
        public static Label curentRes { get; set; }
        public static Label bestRes { get; set; }
        internal static bool EndGame { get; set; }

        internal static void CreateEndGameMenu()
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
            resetButton.Style = Creator.styleB;

            var endGameCanvas = new Canvas();
            endGameCanvas.Margin = new Thickness(-92, -128, -83, -22);
            endGameCanvas.Children.Add(resetButton);

            tpage = endGameCanvas;
            Canvas.SetLeft(resetButton, 100);
            Canvas.SetTop(resetButton, 100);
            Creator.grid.Children.Add(endGameCanvas);
            Grid.SetColumnSpan(endGameCanvas, 4);
            Grid.SetRowSpan(endGameCanvas, 4);
            endGameCanvas.Background = new SolidColorBrush(Color.FromArgb(150, 0, 0, 0));

        }
        private static bool CheckIfFree(int Row, int Column)
        {
            if (Creator.valueFields[Row, Column] == null) return true;
            else return false;
        }
        
        private static bool CheckExistingOfField(int row, int col)
        {
            try
            {
                if (Creator.valueFields[row, col].Content.ToString() == Creator.valueFields[LastTempRow, LastTempColumn].Content.ToString())
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
        
        public static void Reset()
        {
            curentRes.Content = "0";
            for (int inx = Creator.grid.Children.Count - 1; inx >= 0; inx--)
            {
                UIElement item = Creator.grid.Children[inx];
                if (item is Label && (item as Label).Name == String.Empty)
                {

                    Creator.grid.Children.Remove(item);
                }
            }
            Array.Clear(Creator.valueFields, 0, Creator.valueFields.Length);

            StartGame();
        }


        public static void StartGame(Grid g, Style l, Style b, Label curentResult, Label bestResult)
        {
            Creator.styleL = l;
            Creator.styleB = b;
            Creator.grid = g;
            curentRes = curentResult;
            bestRes = bestResult;
            Creator.CreateTwoFourField();
            Creator.CreateTwoFourField();
        }
        private static void StartGame()
        {
            Creator.CreateTwoFourField();
            Creator.CreateTwoFourField();
        }

        internal static bool CheckToEndGame()
        {
            bool gameEnd = true;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
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
        private static void Reset_Click(object sender, RoutedEventArgs e)
        {
            EndGame = false;
            Creator.grid.Children.Remove(tpage);
            Reset();
        }
    }
}
