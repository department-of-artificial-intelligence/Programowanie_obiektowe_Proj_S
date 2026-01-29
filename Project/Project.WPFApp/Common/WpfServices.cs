using Microsoft.EntityFrameworkCore;
using Project.Configuration;
using Project.DAL;
using Project.Services;

namespace Project.WPFApp.Common
{
    public class WpfServices
    {
        public bool IsReady => this.DbContext != null;
        
        public ApplicationDbContext? DbContext { get; set; }
        
        public HotelService? HotelService => this.DbContext != null ? new HotelService(this.DbContext) : null;
        
        public PersonService? PersonService => this.DbContext != null ? new PersonService(this.DbContext) : null;
        
        public ManagerService? ManagerService => this.DbContext != null ? new ManagerService(this.DbContext, this.PersonService!) : null;
        
        public ResidentService? ResidentService => this.DbContext != null ? new ResidentService(this.DbContext, this.PersonService!) : null;
        
        public RoomService? RoomService => this.DbContext != null ? new RoomService(this.DbContext, this.ResidentService!) : null;
        
        public async Task CloseDatabaseConnectionAsync()
        {
            if (this.DbContext is null)
                return;
            
            await this.DbContext.DisposeAsync();
            this.DbContext = null;
        }
        
        public async Task OpenDatabaseConnectionAsync(string connectionString)
        {
            await this.CloseDatabaseConnectionAsync();

            this.DbContext = new ApplicationDbContextFactory((builder) =>
            {
                builder
                    .UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine);
            }).CreateDbContext([]);
            
            await this.DbContext.PrepareAsync();
        }
    }
}