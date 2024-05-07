using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Reversi_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Reversi reversi = new Reversi();

        public const int HEIGHT = 8;
        public const int WIDTH = 8;


        public UCcell[,] UCcells = new UCcell[WIDTH, HEIGHT];


        public MainWindow()
        {
            InitializeComponent();
            SetupGrid();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            int possibleMove = CountPossibleMove();

            if (possibleMove != 0)
            {
                for (int y = 0; y < WIDTH; y++)
                {
                    for (int x = 0; x < HEIGHT; x++)
                    {
                        UCcells[x, y].Update(reversi.grid[x, y]);
                    }
                }
                DisplayPossibleMove();
            }
            else
            {
                reversi.ChangePlayer();
            }

            
        }

        public void SetupGrid()
        {
            int light = 0; // Light Green or Dark Green

            grdMain.Children.Clear();
            grdMain.RowDefinitions.Clear();
            grdMain.ColumnDefinitions.Clear();

            for (int i = 0; i < HEIGHT; i++)
            {
                grdMain.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < WIDTH; i++)
            {
                grdMain.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    UCcells[x, y] = new UCcell();

                    if (light % 2 == 0)
                    {
                        UCcells[x, y] = new UCcell(true);
                        light++;
                    }
                    else
                    {
                        UCcells[x, y] = new UCcell(false);
                        light++;
                    }

                    UCcells[x, y].MouseLeftButtonDown += HandleClick;
                    
                    Grid.SetColumn(UCcells[x, y], x);
                    Grid.SetRow(UCcells[x, y], y);

                    grdMain.Children.Add(UCcells[x, y]);
                }
                light++;

            }
    }

        public void HandleClick(object sender, RoutedEventArgs e)
        {
            UCcell cell = (UCcell)sender;
            int x = Grid.GetColumn(cell);
            int y = Grid.GetRow(cell);

            if (!reversi.IsFinished())
            {
                if (reversi.PutPawn(x, y))
                {

                    if (reversi.activePlayer == -1)
                    {
                        currentPlayerLabel.Content = "White's Turn";
                    }
                    else
                    {
                        currentPlayerLabel.Content = "Black's Turn";
                    }
                    
                    reversi.ChangePlayer();

                    UpdateGrid();
                }
            }
            else
            {

                int Winner = reversi.GetWinner();

                if (Winner == -1)
                {
                    MessageBox.Show($"Game Finished, Black won {reversi.GetScore(-1)} to {reversi.GetScore(1)} ");
                }
                else
                {
                    MessageBox.Show($"Game Finished, White won {reversi.GetScore(1)} to {reversi.GetScore(-1)} ");
                }
            }
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            SetupGrid();
            reversi = new Reversi();
            UpdateGrid();
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void DisplayPossibleMove()
        {
            for (int y = 0; y < WIDTH; y++)
            {
                for (int x = 0; x < HEIGHT; x++)
                {
                    UCcells[x, y].TogglePossibleMove(false);
                    if (reversi.CanPutPawn(x, y))
                    {
                        UCcells[x, y].TogglePossibleMove(true);
                    }
                }
            }
        }

        public int CountPossibleMove()
        {
            int count = 0;
            for (int y = 0; y < WIDTH; y++)
            {
                for (int x = 0; x < HEIGHT; x++)
                {
                    if (reversi.CanPutPawn(x, y))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}