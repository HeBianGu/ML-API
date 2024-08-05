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
using HeBianGu.Models.Classifications.Emotion;
using HeBianGu.Models.Detections.Eye;
using System.Collections.ObjectModel;

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
            string fileName = OpenFileDialog();
            if (fileName == null)
                return;
            Button button = sender as Button;
            button.Background = new ImageBrush(new BitmapImage(new Uri(fileName))) { Stretch = Stretch.UniformToFill };
            var result = await Task.Run(() =>
            {
                var imageBytes = System.IO.File.ReadAllBytes(fileName);
                return SexClassification.PredictLabel(imageBytes);
            });
            MessageBox.Show(result);
        }

        public string OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
            openFileDialog.Filter = "PNG文件(*.png)|*.png|JPG文件(*.jpg)|*.jpg|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true)
                return null;
            return openFileDialog.FileName;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string fileName = OpenFileDialog();
            if (fileName == null)
                return;
            Button button = sender as Button;
            //var image = new BitmapImage(new Uri(fileName));
            //button.Background = new ImageBrush(image) { Stretch = Stretch.UniformToFill };
            //image.Clone();
            var result = await Task.Run(() =>
            {
                //var imageBytes = System.IO.File.ReadAllBytes(fileName);
                return EmotionClassification.PredictLabel(fileName);
            });
            MessageBox.Show(result);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string fileName = OpenFileDialog();
            if (fileName == null)
                return;
            var result = await Task.Run(() =>
            {
                return EyeDetection.PredictObjects(fileName);
            });
            DetectView detectView = new DetectView() { Width = 600, Height = 600 };
            detectView.ImageSource = new BitmapImage(new Uri(fileName));
            detectView.BoundingBoxs = new ObservableCollection<BoundingBox>(result);
            Window window = new Window();
            window.Content = detectView;
            window.ShowDialog();
        }
    }

    public class DetectView : FrameworkElement
    {

        public ObservableCollection<BoundingBox> BoundingBoxs
        {
            get { return (ObservableCollection<BoundingBox>)GetValue(BoundingBoxsProperty); }
            set { SetValue(BoundingBoxsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundingBoxsProperty =
            DependencyProperty.Register("BoundingBoxs", typeof(ObservableCollection<BoundingBox>), typeof(DetectView), new FrameworkPropertyMetadata(default(ObservableCollection<BoundingBox>), (d, e) =>
            {
                DetectView control = d as DetectView;

                if (control == null) return;

                if (e.OldValue is ObservableCollection<BoundingBox> o)
                {

                }

                if (e.NewValue is ObservableCollection<BoundingBox> n)
                {

                }

            }));




        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(DetectView), new FrameworkPropertyMetadata(default(ImageSource), (d, e) =>
            {
                DetectView control = d as DetectView;

                if (control == null) return;

                if (e.OldValue is ImageSource o)
                {

                }

                if (e.NewValue is ImageSource n)
                {

                }

            }));


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.ImageSource is BitmapImage bitmap)
            {
                double w = (bitmap.PixelWidth * 1.0 / bitmap.PixelHeight * 1.0) * 600;

                double span = (600 - w) / 2;
                drawingContext.DrawImage(this.ImageSource, new Rect(span, 0, w, 600));
                drawingContext.DrawRectangle(null, new Pen(Brushes.Blue, 2), new Rect(0, 0, 600, 600));

                foreach (var item in this.BoundingBoxs)
                {
                    drawingContext.DrawRectangle(null, new Pen(Brushes.Red, 2), new Rect(item.X - span * 2, item.Y, item.Width, item.Height));
                }
            }


        }
    }
}