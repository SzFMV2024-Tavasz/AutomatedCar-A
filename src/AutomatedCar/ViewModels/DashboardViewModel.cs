namespace AutomatedCar.ViewModels
{
    using AutomatedCar.Models;
    using ReactiveUI;

    public class DashboardViewModel : ViewModelBase
    {
        public CarViewModel ControlledCar { get; set; }
        
        public DashboardViewModel(AutomatedCar controlledCar)
        {
            this.ControlledCar = new CarViewModel(controlledCar);
        }
        public void OnOffTempomat()
        {
            World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn = !World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn;
        }
    }
}