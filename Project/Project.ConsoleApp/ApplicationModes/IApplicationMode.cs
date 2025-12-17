namespace Project.ConsoleApp.ApplicationModes
{
    public interface IApplicationMode
    {
        Task Run(ApplicationContext context);
    }
}