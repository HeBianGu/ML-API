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
using HeBianGu.Models.Data;
using System.Globalization;

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
            DetectView detectView = new DetectView();
            detectView.ImageSource = new BitmapImage(new Uri(fileName));
            detectView.BoundingBoxs = new ObservableCollection<IBoundingBox>(result);
            detectView.ImageWidth = 600;
            detectView.ImageHeight = 600;
            Window window = new Window();
            window.Content = detectView;
            window.ShowDialog();
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string fileName = OpenFileDialog();
            if (fileName == null)
                return;
            var result = await Task.Run(() =>
            {
                return TinyYolov2Detection.PredictObjects(fileName);
            });

            var boxes = result.ToList();

            DetectView detectView = new DetectView();
            detectView.ImageSource = new BitmapImage(new Uri(fileName));
            detectView.BoundingBoxs = new ObservableCollection<IBoundingBox>(result.SelectMany(x => x));
            detectView.ImageWidth = 416;
            detectView.ImageHeight = 416;
            Window window = new Window();
            window.Content = detectView;
            window.ShowDialog();
        }
    }

    public class DetectView : FrameworkElement
    {

        public ObservableCollection<IBoundingBox> BoundingBoxs
        {
            get { return (ObservableCollection<IBoundingBox>)GetValue(BoundingBoxsProperty); }
            set { SetValue(BoundingBoxsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundingBoxsProperty =
            DependencyProperty.Register("BoundingBoxs", typeof(ObservableCollection<IBoundingBox>), typeof(DetectView), new FrameworkPropertyMetadata(default(ObservableCollection<IBoundingBox>), (d, e) =>
            {
                DetectView control = d as DetectView;

                if (control == null) return;

                if (e.OldValue is ObservableCollection<IBoundingBox> o)
                {

                }

                if (e.NewValue is ObservableCollection<IBoundingBox> n)
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


        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(DetectView), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                DetectView control = d as DetectView;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));


        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(DetectView), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                DetectView control = d as DetectView;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));



        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (this.ImageSource is BitmapImage bitmap)
            {
                double w = (bitmap.PixelWidth * 1.0 / bitmap.PixelHeight * 1.0) * this.ImageWidth;

                double span = (this.ImageWidth - w) / 2;
                drawingContext.DrawImage(this.ImageSource, new Rect(span, 0, w, this.ImageHeight));
                drawingContext.DrawRectangle(null, new Pen(Brushes.Blue, 2), new Rect(0, 0, this.ImageWidth, this.ImageHeight));

                foreach (var item in this.BoundingBoxs)
                {
                    drawingContext.DrawRectangle(null, new Pen(Brushes.Red, 2), new Rect(item.X, item.Y, item.Width, item.Height));
                    //-span * 2
                    if (item is ILabelBoundingBox label)
                    {
                        var format = new FormattedText(label.Label, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(new FontFamily("微软雅黑"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), 15.0, Brushes.Red, 96);
                        drawingContext.DrawText(format, new Point(label.X, label.Y));
                    }
                }
            }
        }
    }
}