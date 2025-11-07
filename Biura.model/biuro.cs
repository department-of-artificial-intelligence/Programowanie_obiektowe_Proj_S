using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biura.model
{

    public class Biuro
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public Person Owner { get; set; }
        List<Client> Clients {  get; set; }

        public Biuro()
        {
            Name = string.Empty;
            Location = string.Empty;
            Owner = new Person();
            Clients = new List<Client>();
        }

        public Biuro(string name, string location, Person owner)
        {
            Name = name;
            Location = location;
            Owner = owner;
            Clients = new List<Client>();
        }

        public void AddCLient(Client p)
        {
            Clients.Add(p);
        }
        public void AddClient(Person p)
        {
            Client tmp = new Client(p,DateTime.Now);
            Clients.Add(tmp);
        }

        public void ListClients()
        {
            Console.WriteLine($"Travel agency has {Clients.Count} clients");
            foreach (Client client in Clients)
            {
                Console.WriteLine(client);
            }
        }

        public override string ToString()
        {
            return $"biuro wycieczkowe {Name} ul {Location} wlasciciel {Owner.ToString()}";
        }
    }
}
