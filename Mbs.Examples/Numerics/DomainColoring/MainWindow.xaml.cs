using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DomainColoring.ColorMaps;
using DomainColoring.ComplexFunctions;
using Mbs.Numerics;
#pragma warning disable S125 // Sections of code should not be commented out

// ReSharper disable once RedundantExtendsListEntry
namespace DomainColoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ColorMap[] Maps = PredefinedColorMaps.Get().ToArray();
        private static readonly ComplexFunction[] Functions = PredefinedComplexFunctions.Get().ToArray();

        private readonly object imageLock = new object();

        private ColorMap currentMap;
        private ComplexFunction currentFunction;
        private Point ptOrigin;
        private Point ptScale;
        private bool block;

        public MainWindow()
        {
            InitializeComponent();
            foreach (ColorMap c in Maps)
            {
                ColorMapComboBox.Items.Add(c.Label);
            }

            foreach (ComplexFunction f in Functions)
            {
                if (f.Function == null)
                {
                    FunctionComboBox.Items.Add(new Separator());
                }
                else
                {
                    FunctionComboBox.Items.Add(f.Label);
                }
            }

            const int initialMapIndex = 0;
            const int initialFunctionIndex = 0;

            currentMap = Maps[initialMapIndex];
            currentFunction = Functions[initialFunctionIndex];

            ColorMapComboBox.SelectedIndex = initialMapIndex;
            FunctionComboBox.SelectedIndex = initialFunctionIndex;
        }

        private void FunctionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComplexFunction f = Functions[FunctionComboBox.SelectedIndex];
            if (f != currentFunction)
            {
                currentFunction = f;
                if (currentMap != null)
                {
                    Render(Rect.ActualWidth, Rect.ActualHeight);
                }
            }
        }

        private void ColorMapChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorMap m = Maps[ColorMapComboBox.SelectedIndex];
            if (m != currentMap)
            {
                currentMap = m;
                if (currentFunction != null)
                {
                    Render(Rect.ActualWidth, Rect.ActualHeight);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImagePlaceholderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged || e.WidthChanged)
            {
                Render(e.NewSize.Width, e.NewSize.Height);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            // CoordinateTextBlock.Text = string.Empty;
            CoordinateTextBlock.Text = "Mouse Leave";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageMouseMove(object sender, MouseEventArgs e)
        {
            /*Point pt = e.GetPosition(Image);
                        double xMin, yMax, scaleX, scaleY;
                        lock (imageLock)
                        {
                            xMin = ptOrigin.X;
                            yMax = ptOrigin.Y;
                            scaleX = ptScale.X;
                            scaleY = ptScale.Y;
                        }

                        var z = new Complex(xMin + scaleX * pt.X, yMax - scaleY * pt.Y);
                        const string fmt = "g4";
                        if (currentFunction == null)
                        {
                            CoordinateTextBlock.Text = string.Concat("z = ", z.ToString(fmt, CultureInfo.InvariantCulture.NumberFormat));
                        }
                        else
                        {
                            Complex fz = currentFunction.Function(z);
                            CoordinateTextBlock.Text = string.Concat(
                                "f(",
                                z.ToString(fmt, CultureInfo.InvariantCulture.NumberFormat),
                                ") = ",
                                fz.ToString(fmt, CultureInfo.InvariantCulture.NumberFormat));
                        }*/
            CoordinateTextBlock.Text = $"Mouse Move: left {e.LeftButton}, right {e.LeftButton}, point: {e.MouseDevice.GetPosition(Image)}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Render(double width, double height)
        {
            if (block || currentFunction == null || currentMap == null)
            {
                return;
            }

            Cursor = Cursors.Wait;
            Func<Complex, Complex> f = currentFunction.Function;
            Func<Complex, int> m = currentMap.Function;
            int w = (int)width, h = (int)height;
            double xMin, yMax, scaleX, scaleY;
            if (w > h)
            {
                yMax = currentFunction.ImMax;
                scaleY = (yMax - currentFunction.ImMin) / h;
                scaleX = (currentFunction.ReMax - currentFunction.ReMin) / h;
                xMin = currentFunction.ReMin - scaleX * (w - h) / 2;
            }
            else if (w < h)
            {
                xMin = currentFunction.ReMin;
                scaleX = (currentFunction.ReMax - xMin) / w;
                scaleY = (currentFunction.ImMax - currentFunction.ImMin) / w;
                yMax = currentFunction.ImMax + scaleY * (h - w) / 2;
            }
            else
            {
                xMin = currentFunction.ReMin;
                scaleX = (currentFunction.ReMax - xMin) / w;
                yMax = currentFunction.ImMax;
                scaleY = (yMax - currentFunction.ImMin) / w;
            }

            Task.Factory.StartNew(() =>
            {
                var bitmap = new WriteableBitmap(w, h, 96, 96, PixelFormats.Bgra32, null);
                var pixels = new int[h * w];

                Parallel.For(0, h, y =>
                {
                    double im = yMax - scaleY * y;
                    int i = y * w;
                    for (int x = 0; x < w; x++)
                    {
                        var z = new Complex(xMin + x * scaleX, im);
                        int color;
                        try
                        {
                            Complex fz = f(z);
                            color = m(fz);
                        }
                        catch
                        {
                            color = 0;
                        }

                        pixels[i++] = color;
                    }
                });
                bitmap.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 4, 0, 0);
                bitmap.Freeze();
                Dispatcher.Invoke((ThreadStart)(() =>
                {
                    lock (imageLock)
                    {
                        Image.Source = bitmap;
                        ptOrigin.X = xMin;
                        ptOrigin.Y = yMax;
                        ptScale.X = scaleX;
                        ptScale.Y = scaleY;
                        block = false;
                        Cursor = Cursors.Arrow;
                    }
                }));
            });
        }

        private void ImageManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            CoordinateTextBlock.Text = $"Manipulation Starting: mode {e.Mode}, pivot center {e.Pivot.Center} radius {e.Pivot.Radius}, singleTouch {e.IsSingleTouchEnabled}";
        }

        private void ImageManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            CoordinateTextBlock.Text = "Manipulation Delta";
        }

        private void ImageInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            CoordinateTextBlock.Text = "Inertia Starting";
        }

        private void ImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            CoordinateTextBlock.Text = $"Mouse Down: {e.ChangedButton}, clicks {e.ClickCount}, point {e.MouseDevice.GetPosition(Image)}";
        }

        private void ImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            CoordinateTextBlock.Text = $"Mouse Up: {e.ChangedButton}, clicks {e.ClickCount}, point {e.MouseDevice.GetPosition(Image)}";
        }

        private void Image_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            CoordinateTextBlock.Text = $"Mouse Wheel: left {e.LeftButton}, right {e.RightButton}, delta {e.Delta}, point {e.MouseDevice.GetPosition(Image)}";
        }

        private void ImageReset(object sender, RoutedEventArgs e)
        {
            // Implement.
        }

        private void ImageDown(object sender, RoutedEventArgs e)
        {
            // Implement.
        }

        private void ImageRight(object sender, RoutedEventArgs e)
        {
            // Implement.
        }

        private void ImageLeft(object sender, RoutedEventArgs e)
        {
            // Implement.
        }

        private void ImageUp(object sender, RoutedEventArgs e)
        {
            // Implement.
        }

        private void ImageZoomOut(object sender, RoutedEventArgs e)
        {
            // Implement.
        }

        private void ImageZoomIn(object sender, RoutedEventArgs e)
        {
            // Implement.
        }
    }
}
