using System.Diagnostics.CodeAnalysis;
using Project.DAL;

namespace Project.ConsoleApp.ApplicationModes
{
    public record ApplicationContext
    {
        public required ApplicationDbContext DbContext { get; init; }

        public ApplicationContext() { }

        [SetsRequiredMembers]
        public ApplicationContext(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
    }
}