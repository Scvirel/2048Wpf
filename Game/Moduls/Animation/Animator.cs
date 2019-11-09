

using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Wpf2048.Moduls.Animation
{
    class Animator
    {
        public static void BeginAnimation(Label label)
        {
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
