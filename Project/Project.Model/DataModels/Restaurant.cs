using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class Restaurant
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public Employee Manager { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Dish> Menu { get; set; }
        public Storage Storage { get; set; }

        public Restaurant(int id, string name, string adress, string phone, Employee manager, List<Employee> employees, List<Dish> menu, Storage storage)
        {
            Id = id;
            Name = name;
            Adress = adress;
            Phone = phone;
            Manager = manager;
            Employees = employees;
            Menu = menu;
            Storage = storage;
        }

        //test!!!!!!!
        public Restaurant(int id, string name, string adress, string phone, Employee manager, Storage storage)
        {
            Id = id;
            Name = name;
            Adress = adress;
            Phone = phone;
            Manager = manager;
            Storage = storage;
            
            Employees = new List<Employee>();
            Menu = new List<Dish>();

        }

    }
}