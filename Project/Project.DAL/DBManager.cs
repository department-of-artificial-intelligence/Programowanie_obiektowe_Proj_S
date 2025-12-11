using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Project.DAL
{
    public static class DBManager
    {
        private static ApplicationDBContext? Context;

        public static ApplicationDBContext BuildDB()
        {
            if (Context == null)
            {
                IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
                {
                    var cns = context.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(cns));
                }).Build();

                Context = _host.Services.GetService<ApplicationDBContext>() ?? throw new Exception("Database couldnt't run properly");
            }

            Context.Database.Migrate();

            return Context;
        }

        public static void ClearDB()
        {
            if (Context is null) return;

            try
            {
                if (Context.Actors.Any()) Context.Actors.RemoveRange(Context.Actors);
                if (Context.Auditoriums.Any()) Context.Auditoriums.RemoveRange(Context.Auditoriums);
                if (Context.Cinemas.Any()) Context.Cinemas.RemoveRange(Context.Cinemas);
                if (Context.CinemaNetworks.Any()) Context.CinemaNetworks.RemoveRange(Context.CinemaNetworks);
                if (Context.Films.Any()) Context.Films.RemoveRange(Context.Films);
                if (Context.Reservations.Any()) Context.Reservations.RemoveRange(Context.Reservations);
                if (Context.Seances.Any()) Context.Seances.RemoveRange(Context.Seances);
                if (Context.Tickets.Any()) Context.Tickets.RemoveRange(Context.Tickets);

                Context.SaveChanges();
            }
            catch (Exception) { }
        }
    }
}
