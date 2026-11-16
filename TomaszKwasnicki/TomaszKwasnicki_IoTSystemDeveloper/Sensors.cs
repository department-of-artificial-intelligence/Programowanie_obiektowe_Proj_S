namespace MyHome.Model
{
    public abstract class Sensors : Iot_Device
    {
        public float CurrentValue { get; protected set; }
        public abstract float ReadValue();
    }
}