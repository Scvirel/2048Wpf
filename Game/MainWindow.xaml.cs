using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _2048Game;


namespace Wpf2048
{
    public partial class MainWindow : Window
    {

        MoveChecker _moveChecker;


        public MainWindow()
        {
            InitializeComponent();
            
            Begin();
        }
        private void Begin()
        {  
            Creator.StartGame(grid, (Style)FindResource("CustomLabel"), (Style)FindResource("RoundButtonTemplate"),scoreLabel,bestScoreLabel);
            _moveChecker = new MoveChecker();
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Creator.EndGame == true) return;
            _moveChecker.NextTurn = false;
            switch (e.Key)
            {
                case Key.Left:
                    {
                        _moveChecker.SetPath((int)Key.Left);
                    }
                    break;
                case Key.Up:
                    {
                        _moveChecker.SetPath((int)Key.Up);
                    }
                    break;
                case Key.Right:
                    {
                        _moveChecker.SetPath((int)Key.Right);
                    }
                    break;
                case Key.Down:
                    {
                        _moveChecker.SetPath((int)Key.Down);
                    }
                    break;

                default: return;
            }
            if (_moveChecker.NextTurn && Creator.grid.Children.Count < 37)
            {
                Creator.CreateTwoFourField();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Creator.Reset();
            
        }
    }
   
}