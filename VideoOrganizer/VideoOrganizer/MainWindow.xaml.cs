using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Drawing.Drawing2D;
using System.IO;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using YourNamespace;
using WinForms = System.Windows.Forms;


namespace VideoOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool isPlaying = false;
        private bool sliderMoving = false;

        private readonly DispatcherTimer Timer = new DispatcherTimer() {Interval = TimeSpan.FromSeconds(0.1)};
        private readonly WinForms.FolderBrowserDialog folderSearch = new WinForms.FolderBrowserDialog();

        // Funkcje
        private Model.Functions functions = new Model.Functions();


        public MainWindow()
        {
            InitializeComponent();
            Timer.Tick += Timer_Tick;
            Timer.Start();

            

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if(mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan && !sliderMoving)
            {
                timeSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                timeSlider.Value = mediaPlayer.Position.TotalSeconds;
            }
        }



        private void ListFiles(string directoryPath)
        {
            // Clear previous list
            fileListBox.Items.Clear();

            // Get files in the directory
            var files = functions.GetMediaFiles(directoryPath);

            if (!files.Any())
            {
                isEmptyTextBlock.Visibility = Visibility.Visible;
                isEmptyTextBlock.Text = "No files found";
            }
            else
            {
                isEmptyTextBlock.Visibility = Visibility.Hidden;


                foreach (var text in files)
                {
                    var customControl = new CustomControl();
                    customControl.Text = text; // Set the text for the custom control
                    stackPanel.Children.Add(customControl);
                }
                mediaPlayer.Source = new Uri(files.First());
                mediaPlayer.Play();
                mediaPlayer.Stop();


            }

        }




        private void OpenLocation_Click(object sender, RoutedEventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedDirectory = folderBrowserDialog.SelectedPath;
                    ListFiles(selectedDirectory);
                }
            }
            


        }
        


        #region Button Click Functions

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {


            if (mediaPlayer?.Source != null)
            {

                Console.WriteLine("Play pressed");
                mediaPlayer.Play();
                isPlaying = true;
            }
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e) 
        {
            if (mediaPlayer?.Source != null)
            {
                Console.WriteLine("Pause pressed");
                mediaPlayer.Pause();
                isPlaying = false;
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e) 
        {
            if (mediaPlayer?.Source != null)
            {
                Console.WriteLine("Stop pressed");
                mediaPlayer.Stop();
                isPlaying = false;
            }
        }

        #endregion


        private void TimeSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            mediaPlayer.Stop();
            sliderMoving = true;

        }
        private void TimeSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (isPlaying)
            {
                mediaPlayer.Play();

            }
            sliderMoving = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(timeSlider.Value);
        }
        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timeTextBlock.Text = TimeSpan.FromSeconds(timeSlider.Value).ToString(@"hh\:mm\:ss");
        }
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = volumeSlider.Value;
        }


    }
}