using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Flappy_Bird;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    DispatcherTimer gameTimer = new DispatcherTimer();
    private double score;
    private int gravity = 0;
    bool gameOver = false;
    Rect flappyBirdHitBox;
    public MainWindow()
    {
        InitializeComponent();

        gameTimer.Tick += MainEventTimer;
        gameTimer.Interval = TimeSpan.FromMilliseconds(20);
        StartGame();
    }

    private void MainEventTimer(object? sender, EventArgs e)
    {
        
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
    }

    private void StartGame()
    {
        Canvas.Focus();

        int temp = 300;
        score = 0;
        gameOver = false;
        Canvas.SetTop(flappyBird, 261);

        foreach (var x in Canvas.Children.OfType<Image>())
        {
            if ((string)x.Tag == "pipe1")
            {
                Canvas.SetLeft(x, 500);
            }
            if ((string)x.Tag == "pipe2")
            {
                Canvas.SetLeft(x, 800);
            }
            if ((string)x.Tag == "pipe3")
            {
                Canvas.SetLeft(x, 1100);
            }
            
            if ((string)x.Tag == "cloud")
            {
                Canvas.SetLeft(x, 300 + temp);
                temp = 800;
            }
        }
        
        gameTimer.Start();
        
    }

    private void EndGame()
    {
        
        gameTimer.Stop();
        gameOver = true;
        Score.Content += "\nGame Over. \nPress R to restart";
    }
}