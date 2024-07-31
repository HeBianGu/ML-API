using HeBianGu.Models.Classifications.Sex;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace HeBianGu.Tests.Main
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
            openFileDialog.Filter = "PNG文件(*.png)|*.png|JPG文件(*.jpg)|*.jpg|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true)
                return;
            //var imageBytes = System.IO.File.ReadAllBytes(@"C:\Users\LENOVO\Pictures\图像分类\人物\0348134dd5de2a80f148522228b33263.jpg");
            this.btn.Background = new ImageBrush(new BitmapImage(new Uri(openFileDialog.FileName))) { Stretch = Stretch.UniformToFill };
            this.btn.Content = "正在生成结果...";
            var result = await Task.Run(() =>
            {
                var imageBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                return SexClassification.PredictLabel(imageBytes);
            });
            this.btn.Content = $"输出结果:{result}";
        }
    }
}