namespace AutomatedCar.Views
{
    using AutomatedCar.Models;
    using AutomatedCar.ViewModels;
    using Avalonia.Controls;
    using Avalonia.Input;
    using Avalonia.Markup.Xaml;
    using System;
    using System.Threading;
    using Avalonia.Threading;

    public class MainWindow : Window
    {
        ScrollViewer CarScrollViewer { get; set; }
        private readonly Timer CarFocusTimer;
        public MainWindow()
        {
            this.InitializeComponent();
            CarFocusTimer = new Timer(FocusingOnCar, null, TimeSpan.Zero, TimeSpan.FromSeconds(0.03));
        }
        public bool FocusCar=false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            
            Keyboard.Keys.Add(e.Key);
            base.OnKeyDown(e);

            MainWindowViewModel viewModel = (MainWindowViewModel)this.DataContext;

            if (Keyboard.IsKeyDown(Key.Up))
            {
                viewModel.CourseDisplay.KeyUp();
                //if (!FocusCar)
                //{ 
                //FocusOnCarWhileMoving();
                //}
            }

            if (Keyboard.IsKeyDown(Key.Down))
            {
                viewModel.CourseDisplay.KeyDown();
                viewModel.Dashboard.TurnOffTempomat();
            }

            if (Keyboard.IsKeyDown(Key.Left))
            {
                viewModel.CourseDisplay.KeyLeft();
            }

            if (Keyboard.IsKeyDown(Key.Right))
            {
                viewModel.CourseDisplay.KeyRight();
            }

            if (Keyboard.IsKeyDown(Key.PageUp))
            {
                viewModel.CourseDisplay.PageUp();
            }

            if (Keyboard.IsKeyDown(Key.PageDown))
            {
                viewModel.CourseDisplay.PageDown();
            }
            if (Keyboard.IsKeyDown(Key.Space))
            {
                viewModel.CourseDisplay.Space();
                viewModel.Dashboard.TurnOffTempomat();
            }
            if (Keyboard.IsKeyDown(Key.D1))
            {
                viewModel.CourseDisplay.ToggleDebug();
            }

            if (Keyboard.IsKeyDown(Key.D2))
            {
                viewModel.CourseDisplay.ToggleCamera();
            }

            if (Keyboard.IsKeyDown(Key.D3))
            {
                viewModel.CourseDisplay.ToggleRadar();
            }

            if (Keyboard.IsKeyDown(Key.D4))
            {
                viewModel.CourseDisplay.ToggleUltrasonic();
            }

            if (Keyboard.IsKeyDown(Key.D5))
            {
                viewModel.CourseDisplay.ToggleRotation();
            }

            if (Keyboard.IsKeyDown(Key.F1))
            {
                new HelpWindow().Show();
                Keyboard.Keys.Remove(Key.F1);
            }

            if (Keyboard.IsKeyDown(Key.F5))
            {
                viewModel.NextControlledCar();
                Keyboard.Keys.Remove(Key.F5);
            }

            if (Keyboard.IsKeyDown(Key.F6))
            {
                viewModel.PrevControlledCar();
                Keyboard.Keys.Remove(Key.F5);
            }
            //Transmission controllers
            if (Keyboard.IsKeyDown(Key.Q))
            {
                viewModel.CourseDisplay.TransmissionUp();
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                viewModel.CourseDisplay.TransmissionDown();
            }


            if (Keyboard.IsKeyDown(Key.M))
            {
                viewModel.Dashboard.OnOffTempomat();
            }
            if (Keyboard.IsKeyDown(Key.T))
            {
                viewModel.Dashboard.NextWantedDistance();
            }
            if (Keyboard.IsKeyDown(Key.Add))
            {
                viewModel.Dashboard.AddWantedSpeed();
            }
            if (Keyboard.IsKeyDown(Key.Subtract))
            {
                viewModel.Dashboard.SubtractWantedSpeed();
            }

            if (Keyboard.IsKeyDown(Key.F))
            {
                FocusCar = !FocusCar;
            }

            //var scrollViewer = this.Get<CourseDisplayView>("courseDisplay").Get<ScrollViewer>("scrollViewer");

            //viewModel.CourseDisplay.FocusCar(scrollViewer);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)this.DataContext;
            Keyboard.Keys.Remove(e.Key);
            base.OnKeyUp(e);
            viewModel.CourseDisplay.KeyUpToFalse();
            viewModel.CourseDisplay.KeyDownToFalse();
            viewModel.CourseDisplay.KeyLeftToFalse();
            viewModel.CourseDisplay.KeyRightToFalse();
            //viewModel.CourseDisplay.EmegencyBreakToFalse();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            CarScrollViewer = this.Get<CourseDisplayView>("courseDisplay").Get<ScrollViewer>("scrollViewer");
        }
        
        private void FocusingOnCar(object state)
        {
            if (FocusCar)
            {
                Dispatcher.UIThread.Invoke(() =>(DataContext as MainWindowViewModel).CourseDisplay.FocusCar(CarScrollViewer));
            }
        }
    }
}