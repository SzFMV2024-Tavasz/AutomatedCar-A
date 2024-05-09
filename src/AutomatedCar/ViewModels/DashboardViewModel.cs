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
            if (!World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn) 
            {
                if (World.Instance.ControlledCar.Velocity < 30)
                {
                    World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed = 30;
                }
                else
                {
                    World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed = (int)World.Instance.ControlledCar.Velocity;
                }
            }
            World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn = !World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn;
        }
        public void TurnOffTempomat()
        {
            World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn = false;
        }
        public void NextWantedDistance()
        {
            if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn)
            {
                if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedDistance == 0 || World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedDistance == 1.4)
                {
                    World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedDistance = 0.8;
                }
                else
                {
                    World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedDistance += 0.2;
                }
            }
            

        }
        public void AddWantedSpeed()
        {
            if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn)
            {
                if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed <= 160)
                {
                    if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed % 10 != 0)
                    {
                        World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed += 10 - World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed % 10;
                    }
                    else if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed <= 150)
                    {
                        World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed += 10;
                    }
                }
            }
        }
        public void SubtractWantedSpeed()
        {
            if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.IsItOn)
            {
                if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed >= 30)
                {
                    if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed % 10 != 0)
                    {
                        World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed -= World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed % 10;
                    }
                    else if (World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed >= 40)
                    {
                        World.Instance.ControlledCar.VirtualFunctionBus.TempomatPacket.WantedSpeed -= 10;
                    }
                }
            }
        }
    }
    
}