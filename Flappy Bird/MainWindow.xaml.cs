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
        Score.Content = $"Score: {score}";
        flappyBirdHitBox = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width, flappyBird.Height);
        Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);
        
        if(Canvas.GetTop(flappyBird) < -10 || Canvas.GetTop(flappyBird) > 458)
        {
            EndGame();
        }
        foreach (var x in Canvas.Children.OfType<Image>())
        {
            if ((string)x.Tag == "pipe1" || (string)x.Tag == "pipe2" || (string)x.Tag == "pipe3")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);

                if (Canvas.GetLeft(x) < -100)
                {
                    Canvas.SetLeft(x, 800);
                    score += 0.5;
                    
                }
                
                Rect pipeHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                if (flappyBirdHitBox.IntersectsWith(pipeHitBox))
                {
                    EndGame();
                }
            }
            if ((string)x.Tag == "cloud")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - 2);

                if (Canvas.GetLeft(x) < -250)
                {
                    Canvas.SetLeft(x, 550);
                }
            }
        }
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
            gravity = -8;
        }
        
        if (e.Key == Key.R && gameOver == true)
        {
            StartGame();
        }
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
        gravity = 8;
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