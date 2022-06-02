using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Threading;

namespace AutoCopy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            string sourcePath = @"F:\FinallExam";
            string targetPath = @"S:\碩士論文";
            string type = "*."+ txtinput.Text;

            string[] txtList = Directory.GetFiles(sourcePath, type);

            //複製檔案
            foreach (string f in txtList)
            {

                // Remove path from the file name.
                string fName = f.Substring(sourcePath.Length + 1);

                try
                {
                    // 如果目標文件已經存在，則覆蓋。
                    File.Copy(Path.Combine(sourcePath, fName), Path.Combine(targetPath, fName), true);
                }

                //如果文件已被複製，則捕獲異常。
                catch (IOException copyError)
                {
                    MessageBox.Show(copyError.ToString());
                }

                pgbar.Maximum = txtList.Length;
                int length = txtList.Length;
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    for (int i = 0; i <= length; i++)
                    {
                        this.pgbar.Dispatcher.BeginInvoke((ThreadStart)delegate { this.pgbar.Value = i; });
                        Thread.Sleep(100);
                    }
                }));
                thread.Start();
            }
        }
        private void pgbar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void txtinput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
