namespace Project.Model
{
    public abstract class Actuating_Device : Iot_Device
    {
        public bool IsOn { get; private set; }
        public virtual void TurnOn() => IsOn = true;
        public virtual void TurnOff() => IsOn = false;
        public bool GetStatus() => IsOn;
    }
}