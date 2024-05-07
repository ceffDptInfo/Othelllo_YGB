using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Reversi_WPF
{
    /// <summary>
    /// Interaction logic for UCcell.xaml
    /// </summary>
    public partial class UCcell : UserControl
    {
        private SolidColorBrush BLACK = Brushes.Black;
        private SolidColorBrush WHITE = Brushes.White;
        private SolidColorBrush GRAY = Brushes.Gray;
        private SolidColorBrush TRANSPARENT = Brushes.Transparent;
        private SolidColorBrush GREEN = Brushes.Green;
        private SolidColorBrush LIGHTGREEN = Brushes.LightGreen;

        public UCcell()
        {
            InitializeComponent();
        }

        public UCcell(bool IsClear) 
        { 
            InitializeComponent();
            if (IsClear)
            {
                grd_uc.Background = GREEN; 
            } 
            else
            {
                grd_uc.Background = LIGHTGREEN; 
            }
        }

        public void Update(int cell)
        {
            switch (cell)
            {
                case -1: 
                    ell_uc.Fill = BLACK; 
                    break; 
                case 1:
                    ell_uc.Fill = WHITE; 
                    break;
                case 2:
                    ell_uc.Fill = Brushes.Transparent;
                    ell_uc.Stroke = GRAY;
                    ell_uc.StrokeThickness = 2;
                    break;
                default:
                    ell_uc.Fill = TRANSPARENT;
                    break; 
            }
        }


        public void TogglePossibleMove(bool display)
        {
            // If true, display possible move
            // If false, hide possible move

            if (display && ell_uc.Fill != Brushes.Black)
            {
                ell_uc.Fill = Brushes.Gray;
            }
            else if (!display && ell_uc.Fill == Brushes.Gray)
            {
                ell_uc.Fill = Brushes.Transparent;
            }
        }
    }
}
