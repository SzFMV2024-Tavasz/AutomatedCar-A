namespace AutomatedCar.ViewModels
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using ReactiveUI;

    public class DashboardViewModel : ViewModelBase
    {
        public CarViewModel ControlledCar { get; set; }
        
        public DashboardViewModel(AutomatedCar controlledCar)
        {
            this.ControlledCar = new CarViewModel(controlledCar);
        
        }
        
    }
}