using System;
using System.Windows;
using System.Collections.Specialized;
using Prism.Regions;
using System.Windows.Input;

namespace Tecmosa
{
    public class WindowRegionAdapter : RegionAdapterBase<Window>
    {


        public WindowRegionAdapter(IRegionBehaviorFactory behaviorFactory) : base(behaviorFactory) { }

        protected override void Adapt(IRegion region, Window regionTarget)
        {
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }

        protected override void AttachBehaviors(IRegion region, Window regionTarget)
        {
            base.AttachBehaviors(region, regionTarget);

            WindowRegionBehavior behavior = new WindowRegionBehavior(regionTarget, region, WindowStyle);
            behavior.Attach();

        }

        public Style WindowStyle { get; set; }

        private class WindowRegionBehavior
        {

            private readonly WeakReference _ownerWeakReference;
            private readonly WeakReference _regionWeakReference;
            private readonly Style _windowStyle;

            internal WindowRegionBehavior(Window owner, IRegion region, Style windowStyle)
            {
                _ownerWeakReference = new WeakReference(owner);
                _regionWeakReference = new WeakReference(region);
                _windowStyle = windowStyle;
            }

            internal void Attach()
            {
                IRegion region = _regionWeakReference.Target as IRegion;

                if (region != null)
                {
                    region.Views.CollectionChanged += new NotifyCollectionChangedEventHandler(Views_CollectionChanged);
                    region.ActiveViews.CollectionChanged += new NotifyCollectionChangedEventHandler(ActiveViews_CollectionChanged);
                }
            }

            internal void Detach()
            {
                IRegion region = _regionWeakReference.Target as IRegion;

                if (region != null)
                {
                    region.Views.CollectionChanged -= Views_CollectionChanged;
                    region.ActiveViews.CollectionChanged -= ActiveViews_CollectionChanged;
                }
            }

            private void window_Activated(object sender, EventArgs e)
            {
                IRegion region = _regionWeakReference.Target as IRegion;
                Window window = sender as Window;
                window.SizeToContent = SizeToContent.WidthAndHeight;
                window.Width = SystemParameters.PrimaryScreenWidth / 3;
                window.Height = SystemParameters.PrimaryScreenHeight / 3;
                window.Left = Mouse.GetPosition(Application.Current.MainWindow).X;
                window.Top = Mouse.GetPosition(Application.Current.MainWindow).Y;
                window.ResizeMode = ResizeMode.NoResize;
                window.WindowStyle = System.Windows.WindowStyle.ToolWindow;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                if (window != null && !region.ActiveViews.Contains(window.Content))
                    region.Activate(window.Content);
            }

            private void window_Deactivated(object sender, EventArgs e)
            {
                IRegion region = _regionWeakReference.Target as IRegion;
                Window window = sender as Window;

                if (window != null)
                    region.Deactivate(window.Content);
            }

            private void window_Closed(object sender, EventArgs e)
            {
                Window window = sender as Window;
                IRegion region = _regionWeakReference.Target as IRegion;

                if (window != null && region != null)
                    if (region.Views.Contains(window.Content))
                        region.Remove(window.Content);
            }


            private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                Window owner = _ownerWeakReference.Target as Window;

                if (owner == null)
                {
                    Detach();
                    return;
                }

                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (object view in e.NewItems)
                    {
                        Window window = GetContainerWindow(owner, view);

                        if (window != null && !window.IsFocused)
                        {
                            window.WindowState = WindowState.Normal;
                            window.Activate();
                        }
                    }
                }
            }

            private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                Window owner = _ownerWeakReference.Target as Window;

                if (owner == null)
                {
                    Detach();
                    return;
                }

                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (object view in e.NewItems)
                    {
                        Window window = new Window();
                        window.Activated += new EventHandler(window_Activated);
                        window.Deactivated += new EventHandler(window_Deactivated);
                        window.Style = _windowStyle;
                        window.Content = view;
                        window.Closed += new EventHandler(window_Closed);
                        window.Owner = owner;
                        window.Show();
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (object view in e.OldItems)
                    {
                        Window window = GetContainerWindow(owner, view);

                        if (window != null)
                            window.Close();
                    }
                }
            }

            private Window GetContainerWindow(Window owner, object view)
            {
                foreach (Window window in owner.OwnedWindows)
                {
                    if (window.Content == view)
                        return window;
                }

                return null;
            }
        }
    }
}
