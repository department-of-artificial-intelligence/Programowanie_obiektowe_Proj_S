namespace BikeRental.Models;

public class RentalPeriod
{
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; private set; }

    public RentalPeriod()
    {
        StartDate = DateTime.Now;
    }

    public void Finish()
    {
        EndDate = DateTime.Now;
    }

    public TimeSpan GetDuration()
    {
        DateTime end = EndDate ?? DateTime.Now;
        return end - StartDate;
    }
}
