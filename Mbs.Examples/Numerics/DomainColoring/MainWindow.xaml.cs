// #define TRACE_EVENTS
#if TRACE_EVENTS
#define TRACE_MOUSE_EVENTS
#define TRACE_MANIPULATION_EVENTS
#endif
using System;
#if TRACE_MOUSE_EVENTS || TRACE_MANIPULATION_EVENTS
using System.Diagnostics;
#endif
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
using DomainColoring.ComplexFunctions.Predefined;
using Mbs.Numerics;

// ReSharper disable once RedundantExtendsListEntry
namespace DomainColoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Horizontal scroll step as a fraction of the current image width.
        /// </summary>
        private const double HorizontalScrollStepFraction = 0.1;

        /// <summary>
        /// Vertical scroll step as a fraction of the current image height.
        /// </summary>
        private const double VerticalScrollStepFraction = 0.1;

        private const double ZoomFactor = 1.25;
        private const int InitialMapIndex = 0;
        private const int InitialCategoryIndex = 0;
        private const int InitialFunctionIndex = 0;

        private static readonly ColorMap[] Maps = PredefinedColorMaps.Get().ToArray();
        private static readonly Category[] Categories = Functions.GetCategories().ToArray();

        private readonly object imageLock = new object();

        private ComplexFunction[] functions;
        private ColorMap currentMap;
        private Category currentCategory;
        private ComplexFunction currentFunction;
        private Point ptOrigin;
        private Point ptScale;
        private Point ptMouseDragStart;
        private Cursor cursorCaptured;
        private bool isMouseCaptured;
        private CancellationTokenSource cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();

            foreach (ColorMap c in Maps)
            {
                ColorMapComboBox.Items.Add(c.Label);
            }

            foreach (Category c in Categories)
            {
                CategoryComboBox.Items.Add(c.Label);
            }

            CategoryComboBox.SelectedIndex = InitialCategoryIndex;
            ColorMapComboBox.SelectedIndex = InitialMapIndex;
        }

        private void CategoryChanged(object unused, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            Category c = Categories[CategoryComboBox.SelectedIndex];
            if (c != currentCategory)
            {
                currentCategory = c;
                functions = c.Functions.ToArray();
                FunctionComboBox.Items.Clear();
                foreach (ComplexFunction f in functions)
                {
                    if (f.Function == null)
                    {
                        FunctionComboBox.Items.Add(new Separator());
                    }
                    else
                    {
                        var equation = f.TexKey != null
                            ? (Viewbox)Resources[f.TexKey]
                            : null;
                        FunctionComboBox.Items.Add(equation != null ? (object)equation : f.Label);
                    }
                }

                FunctionComboBox.SelectedIndex = InitialFunctionIndex;
            }
        }

        private void FunctionChanged(object unused, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            int i = FunctionComboBox.SelectedIndex < InitialFunctionIndex ? 0 : FunctionComboBox.SelectedIndex;
            ComplexFunction f = functions[i];
            if (f != currentFunction)
            {
                f.Reset();
                currentFunction = f;
                if (currentMap != null)
                {
                    Render(Rect.ActualWidth, Rect.ActualHeight);
                }
            }
        }

        private void ColorMapChanged(object unused, SelectionChangedEventArgs e)
        {
            e.Handled = true;
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
        private void Render(double width, double height)
        {
            if (currentFunction == null || currentMap == null || width == 0 || height == 0)
            {
                return;
            }

            Func<Complex, Complex> f = currentFunction.Function;
            Func<Complex, int> m = currentMap.Function;
            lock (imageLock)
            {
                int w = (int)width, h = (int)height;
                double xMin, yMax, scaleX, scaleY;
                if (width > height)
                {
                    yMax = currentFunction.CurrentImMax;
                    scaleY = (yMax - currentFunction.CurrentImMin) / height;
                    scaleX = (currentFunction.CurrentReMax - currentFunction.CurrentReMin) / height;
                    xMin = currentFunction.CurrentReMin - scaleX * (width - height) / 2;
                }
                else
                {
                    xMin = currentFunction.CurrentReMin;
                    scaleX = (currentFunction.CurrentReMax - xMin) / width;
                    scaleY = (currentFunction.CurrentImMax - currentFunction.CurrentImMin) / width;
                    yMax = currentFunction.CurrentImMax + scaleY * (height - width) / 2;
                }

                if (cancellationTokenSource != null)
                {
                    if (!cancellationTokenSource.IsCancellationRequested)
                    {
                        cancellationTokenSource.Cancel();
                    }

                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                }

                cancellationTokenSource = new CancellationTokenSource();
                Task.Factory.StartNew(() =>
                {
                    var cancellationToken = cancellationTokenSource.Token;
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    var bitmap = new WriteableBitmap(w, h, 96, 96, PixelFormats.Bgra32, null);
                    var pixels = new int[h * w];

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    Parallel.For(0, h, y =>
                    {
                        double im = yMax - scaleY * y;
                        int i = y * w;
                        for (int x = 0; x < w; x++)
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                return;
                            }

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

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    bitmap.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 4, 0, 0);
                    bitmap.Freeze();

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    Dispatcher.Invoke((ThreadStart)(() =>
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }

                        lock (imageLock)
                        {
                            Image.Source = bitmap;
                            ptOrigin.X = xMin;
                            ptOrigin.Y = yMax;
                            ptScale.X = scaleX;
                            ptScale.Y = scaleY;

                            if (cancellationTokenSource != null)
                            {
                                cancellationTokenSource.Dispose();
                                cancellationTokenSource = null;
                            }
                        }
                    }));
                });
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.Handled = true;
#if TRACE_MANIPULATION_EVENTS
            string pivot = e.Pivot == null
                ? "null"
                : $"center {e.Pivot.Center} radius {e.Pivot.Radius}";

            Trace.WriteLine($"Manipulation Starting: mode {e.Mode}, pivot {pivot}, singleTouch {e.IsSingleTouchEnabled}");
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            e.Handled = true;
#if TRACE_MANIPULATION_EVENTS
            static string PrintVec(Vector v)
            {
                return $"{{translation L {v.Length}, L2 {v.LengthSquared}, X {v.X}, Y {v.Y}}}";
            }

            string delta = e.DeltaManipulation == null
                ? "null"
                : $"[translation {PrintVec(e.DeltaManipulation.Translation)}, scale {PrintVec(e.DeltaManipulation.Scale)}, expansion {PrintVec(e.DeltaManipulation.Expansion)}, rotation {e.DeltaManipulation.Rotation}]";

            string cumulative = e.CumulativeManipulation == null
                ? "null"
                : $"[translation {PrintVec(e.CumulativeManipulation.Translation)}, scale {PrintVec(e.CumulativeManipulation.Scale)}, expansion {PrintVec(e.CumulativeManipulation.Expansion)}, rotation {e.CumulativeManipulation.Rotation}]";

            string origin = $"[X {e.ManipulationOrigin.X}, Y {e.ManipulationOrigin.Y}]";

            string velocities = e.Velocities == null
                ? "null"
                : $"[linear {e.Velocities.LinearVelocity}, expansion {e.Velocities.ExpansionVelocity}, angular {e.Velocities.AngularVelocity}]";

            Trace.WriteLine($"Manipulation Delta: origin {origin}, cumulative {cumulative}, delta {delta}, velocities {velocities}, isInertial {e.IsInertial}");
#endif
            if (currentFunction == null)
            {
                return;
            }

            double dx = e.DeltaManipulation?.Translation.X ?? default;
            double dy = e.DeltaManipulation?.Translation.Y ?? default;
            double ex = e.DeltaManipulation?.Expansion.X ?? default;
            double ey = e.DeltaManipulation?.Expansion.Y ?? default;

            if (Math.Abs(dx) < double.Epsilon && Math.Abs(dy) < double.Epsilon &&
                Math.Abs(ex) < double.Epsilon && Math.Abs(ey) < double.Epsilon)
            {
                return;
            }

            lock (imageLock)
            {
                dx *= ptScale.X;
                ex *= ptScale.X;
                dy *= ptScale.Y;
                ey *= ptScale.Y;
                ex /= 2;
                ey /= 2;
                currentFunction.CurrentReMin -= dx - ex;
                currentFunction.CurrentReMax -= dx + ex;
                currentFunction.CurrentImMin += dy + ey;
                currentFunction.CurrentImMax += dy - ey;
            }

            Render(Rect.ActualWidth, Rect.ActualHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.Handled = true;
#if TRACE_MANIPULATION_EVENTS
            static string PrintVec(Vector v)
            {
                return $"{{translation L {v.Length}, L2 {v.LengthSquared}, X {v.X}, Y {v.Y}}}";
            }

            string origin = $"[X {e.ManipulationOrigin.X}, Y {e.ManipulationOrigin.Y}]";

            string velocities = e.InitialVelocities == null
                ? "null"
                : $"[linear {e.InitialVelocities.LinearVelocity}, expansion {e.InitialVelocities.ExpansionVelocity}, angular {e.InitialVelocities.AngularVelocity}]";

            string translation = e.TranslationBehavior == null
                ? "null"
                : $"[deceleration {e.TranslationBehavior.DesiredDeceleration}, displacement {e.TranslationBehavior.DesiredDisplacement}, velocities {PrintVec(e.TranslationBehavior.InitialVelocity)}]";

            string expansion = e.ExpansionBehavior == null
                ? "null"
                : $"[deceleration {e.ExpansionBehavior.DesiredDeceleration}, radius {e.ExpansionBehavior.InitialRadius}, velocities {PrintVec(e.ExpansionBehavior.InitialVelocity)}, expansions {PrintVec(e.ExpansionBehavior.DesiredExpansion)}]";

            string rotation = e.RotationBehavior == null
                ? "null"
                : $"[deceleration {e.RotationBehavior.DesiredDeceleration}, velocity {e.RotationBehavior.InitialVelocity}, rotation {e.RotationBehavior.DesiredRotation}]";

            Trace.WriteLine($"Inertia Starting: origin {origin}, velocities {velocities}, translation {translation}, expansion {expansion}, rotation {rotation}");
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageMouseDown(object unused, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var pt = e.GetPosition(Image);
#if TRACE_MOUSE_EVENTS
            Trace.WriteLine($"Mouse Down: {e.ChangedButton}, clicks {e.ClickCount}, point [X {pt.X}, Y {pt.Y}]");
#endif
            if (e.ClickCount < 2)
            {
                if (!isMouseCaptured && Image.CaptureMouse())
                {
                    isMouseCaptured = true;
                    cursorCaptured = Image.Cursor;
                    ptMouseDragStart = pt;
                    Image.Cursor = Cursors.ScrollAll;
                }
            }
            else
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    Zoom(1 / ZoomFactor);
                }
                else if (e.ChangedButton == MouseButton.Right)
                {
                    Zoom(ZoomFactor);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageMouseUp(object unused, MouseButtonEventArgs e)
        {
            e.Handled = true;
#if TRACE_MOUSE_EVENTS
            var pt = e.GetPosition(Image);
            Trace.WriteLine($"Mouse Up: {e.ChangedButton}, clicks {e.ClickCount}, point [X {pt.X}, Y {pt.Y}]");
#endif
            if (isMouseCaptured)
            {
                isMouseCaptured = false;
                Image.Cursor = cursorCaptured;
                Image.ReleaseMouseCapture();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageMouseMove(object unused, MouseEventArgs e)
        {
            e.Handled = true;
            Point pt = e.GetPosition(Image);
#if TRACE_MOUSE_EVENTS
            Trace.WriteLine($"Mouse Move: buttons [left {e.LeftButton}, right {e.LeftButton}], point [X {pt.X}, Y {pt.Y}]");
#endif

            if (currentFunction == null)
            {
                return;
            }

            if (isMouseCaptured)
            {
                double dx = pt.X - ptMouseDragStart.X;
                double dy = pt.Y - ptMouseDragStart.Y;
                if (Math.Abs(dx) < double.Epsilon && Math.Abs(dy) < double.Epsilon)
                {
                    return;
                }

                ptMouseDragStart = pt;
                lock (imageLock)
                {
                    dx *= ptScale.X;
                    dy *= ptScale.Y;
                    currentFunction.CurrentReMin -= dx;
                    currentFunction.CurrentReMax -= dx;
                    currentFunction.CurrentImMin += dy;
                    currentFunction.CurrentImMax += dy;
                }

                Render(Rect.ActualWidth, Rect.ActualHeight);
            }

            double real, imag;
            lock (imageLock)
            {
                real = ptOrigin.X + ptScale.X * pt.X;
                imag = ptOrigin.Y - ptScale.Y * pt.Y;
            }

            var z = new Complex(real, imag);
            try
            {
                const string fmt = "g4";
                Complex fz = currentFunction.Function(z);
                CoordinateTextBlock.Text = $"f({z.ToString(fmt)}) = {fz.ToString(fmt)}";
            }
            catch
            {
                CoordinateTextBlock.Text = string.Empty;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageMouseLeave(object unused, MouseEventArgs e)
        {
            e.Handled = true;
            CoordinateTextBlock.Text = string.Empty;
#if TRACE_MOUSE_EVENTS
            var pt = e.GetPosition(Image);
            Trace.WriteLine($"Mouse Leave: buttons [left {e.LeftButton}, right {e.RightButton}], point [X {pt.X}, Y {pt.Y}]");
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ImageMouseWheel(object unused, MouseWheelEventArgs e)
        {
            e.Handled = true;
#if TRACE_MOUSE_EVENTS
            var pt = e.GetPosition(Image);
            Trace.WriteLine($"Mouse Wheel: left {e.LeftButton}, right {e.RightButton}, delta {e.Delta}, point [X {pt.X}, Y {pt.Y}]");
#endif

            double factor = e.Delta > 0 ? 1 / ZoomFactor : ZoomFactor;
            Zoom(factor);
        }

        private void ImageReset(object unused1, RoutedEventArgs unused2)
        {
            if (currentFunction == null)
            {
                return;
            }

            lock (imageLock)
            {
                currentFunction.Reset();
            }

            Render(Rect.ActualWidth, Rect.ActualHeight);
        }

        private void ImageLeft(object unused1, RoutedEventArgs unused2)
        {
            ScrollHorizontal(-HorizontalScrollStepFraction);
        }

        private void ImageUp(object unused1, RoutedEventArgs unused2)
        {
            ScrollVertical(-VerticalScrollStepFraction);
        }

        private void ImageDown(object unused1, RoutedEventArgs unused2)
        {
            ScrollVertical(VerticalScrollStepFraction);
        }

        private void ImageRight(object unused1, RoutedEventArgs unused2)
        {
            ScrollHorizontal(HorizontalScrollStepFraction);
        }

        private void ImageZoomOut(object unused1, RoutedEventArgs unused2)
        {
            Zoom(ZoomFactor);
        }

        private void ImageZoomIn(object unused1, RoutedEventArgs unused2)
        {
            Zoom(1 / ZoomFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ScrollVertical(double ratio)
        {
            if (currentFunction == null)
            {
                return;
            }

            ratio *= Rect.ActualHeight;
            lock (imageLock)
            {
                double dy = ptScale.Y * ratio;
                currentFunction.CurrentImMin += dy;
                currentFunction.CurrentImMax += dy;
            }

            Render(Rect.ActualWidth, Rect.ActualHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ScrollHorizontal(double ratio)
        {
            if (currentFunction == null)
            {
                return;
            }

            ratio *= Rect.ActualWidth;
            lock (imageLock)
            {
                double dy = ptScale.Y * ratio;
                currentFunction.CurrentReMin -= dy;
                currentFunction.CurrentReMax -= dy;
            }

            Render(Rect.ActualWidth, Rect.ActualHeight);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Zoom(double factor)
        {
            if (currentFunction == null)
            {
                return;
            }

            lock (imageLock)
            {
                currentFunction.CurrentReMin *= factor;
                currentFunction.CurrentReMax *= factor;
                currentFunction.CurrentImMin *= factor;
                currentFunction.CurrentImMax *= factor;
            }

            Render(Rect.ActualWidth, Rect.ActualHeight);
        }
    }
}
