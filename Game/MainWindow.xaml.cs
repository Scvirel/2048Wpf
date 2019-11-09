
using System.Windows;
using System.Windows.Input;
using Wpf2048.Moduls.Create;
using Wpf2048.Moduls.Move;

namespace Wpf2048
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Begin();
        }
        private void Begin()
        {
            BeginEndResetGame.StartGame(grid, (Style)FindResource("CustomLabel"), (Style)FindResource("RoundButtonTemplate"),scoreLabel,bestScoreLabel);
            MoveFields.SetBest( int.Parse((string)bestScoreLabel.Content));
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (BeginEndResetGame.EndGame) return;
            MoveFields.NextTurn = false;
            switch (e.Key)
            {
                case Key.Left:
                    {
                        MoveFields.SetPath((int)Key.Left);
                    }
                    break;
                case Key.Up:
                    {
                        MoveFields.SetPath((int)Key.Up);
                    }
                    break;
                case Key.Right:
                    {
                        MoveFields.SetPath((int)Key.Right);
                    }
                    break;
                case Key.Down:
                    {
                        MoveFields.SetPath((int)Key.Down);
                    }
                    break;

                default: return;
            }
            if (MoveFields.NextTurn && grid.Children.Count < 37)
            {
                Creator.CreateTwoFourField();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            BeginEndResetGame.Reset();
        }
    }
   
}