using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project.Entities;

namespace Project.Models
{
    public class CinemaNetwork : BaseEntity
    {
        public string CompanyName { get; private set; }
        public string ManagerName { get; private set; }

        public CinemaNetwork()
        {
            throw new NotImplementedException("Cannot create an epty Cinema Network obj");
        }

        public CinemaNetwork(string companyName, string managerName)
        {
            this.CompanyName = companyName;
            this.ManagerName = managerName;
        }

        public CinemaNetwork(string companyName, string managerName, DateTime updatedAt, DateTime createdAt)
        {
            this.CompanyName = companyName;
            this.ManagerName = managerName;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;
        }

        public override string ToString()
        {
            return $"Company name: {this.CompanyName} \n" +
                   $"Manager name: {this.ManagerName} \n" +
                   $"updated_at: {this.UpdatedAt} \n" +
                   $"created_at: {this.CreatedAt} \n";
        }

        public void updateGlobalInfo(string companyName, string managerName)
        {
            this.CompanyName = companyName;
            this.ManagerName = managerName;

            this.MarkAsUpdated();
        }
    }
}
