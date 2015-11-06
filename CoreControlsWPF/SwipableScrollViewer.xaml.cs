using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoreControlsWPF
{
    /// <summary>
    /// Interaction logic for SwipableScrollViewer.xaml
    /// </summary>
    public partial class SwipableScrollViewer : ContentControl
    {
        double _yMousePos;
        double _yScrollOffset;
        Point _mouseSpeed;

        Point _MousePrevPos;
        DoubleAnimation ScrollOffsetAnimation = new DoubleAnimation();
        PowerEase ScrollOffsetAnimationPowerEase = new PowerEase();

        DoubleAnimation StopAnimation = new DoubleAnimation();

        public SwipableScrollViewer()
        {
            InitializeComponent();


            Mouse.AddPreviewMouseMoveHandler(this, PreviewMouseMoveHandler);

            Mouse.AddPreviewMouseDownHandler(this, PreviewMouseDownHandler);
            Mouse.AddPreviewMouseUpHandler(this, PreviewMouseUpHandler);

            ScrollOffsetAnimationPowerEase.Power = 6;
            ScrollOffsetAnimationPowerEase.EasingMode = EasingMode.EaseOut;

            ScrollOffsetAnimation.EasingFunction = ScrollOffsetAnimationPowerEase;


        }

        private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                _yMousePos = e.GetPosition(this).Y;
                _yScrollOffset = ContentScrollViewer.VerticalOffset;

                StopAnimation.BeginTime = null;
                this.BeginAnimation(SwipableScrollViewer.VerticalOffsetProperty, StopAnimation);
            }
        }

        private void PreviewMouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Released)
            {
                if (double.IsInfinity(ScrollSpeed * _mouseSpeed.X) || double.IsNaN(ScrollSpeed * _mouseSpeed.X)) return;

                ScrollOffsetAnimation.From = ContentScrollViewer.VerticalOffset;
                ScrollOffsetAnimation.To = ContentScrollViewer.VerticalOffset + _mouseSpeed.Y * 10000 * (ReverseDragDirection ? -1:1);
                ScrollOffsetAnimation.Duration = TimeSpan.FromSeconds(Math.Abs(_mouseSpeed.Y * 25));

                
                this.BeginAnimation(SwipableScrollViewer.VerticalOffsetProperty, ScrollOffsetAnimation,HandoffBehavior.SnapshotAndReplace);

            }
        }
        

        private void PreviewMouseMoveHandler(object sender, MouseEventArgs e)
        {
            _mouseSpeed.X = ((e.GetPosition(this).X / _MousePrevPos.X) - 1);
            _mouseSpeed.Y = ((e.GetPosition(this).Y / _MousePrevPos.Y) - 1);

            if (e.LeftButton == MouseButtonState.Pressed)
                ContentScrollViewer.ScrollToVerticalOffset(_yScrollOffset + ScrollSpeed*(e.GetPosition(this).Y - _yMousePos) * (ReverseDragDirection ? -1 : 1));

            _MousePrevPos = e.GetPosition(this);
        }






        public new object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly new DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(SwipableScrollViewer), new UIPropertyMetadata(new PropertyChangedCallback(OnContentChanged)));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SwipableScrollViewer).ContentScrollViewer.Content = e.NewValue;
        }


        public double ScrollSpeed
        {
            get { return (double)GetValue(ScrollSpeedProperty); }
            set { SetValue(ScrollSpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScrollSpeed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollSpeedProperty =
            DependencyProperty.Register("ScrollSpeed", typeof(double), typeof(SwipableScrollViewer), new UIPropertyMetadata(null));



        public bool ReverseDragDirection
        {
            get { return (bool)GetValue(ReverseDragDirectionProperty); }
            set { SetValue(ReverseDragDirectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReverseDragDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReverseDragDirectionProperty =
            DependencyProperty.Register("ReverseDragDirection", typeof(bool), typeof(SwipableScrollViewer), new UIPropertyMetadata(false));






        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(SwipableScrollViewer), new UIPropertyMetadata(new PropertyChangedCallback(OnVerticalOffsetChanged)));

        private static void OnVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {           
            (d as SwipableScrollViewer).ContentScrollViewer.ScrollToVerticalOffset((double)e.NewValue);
        }
    }
}
