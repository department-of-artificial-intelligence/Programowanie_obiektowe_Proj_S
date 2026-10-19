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
        public Restaurant(int id, string name, string adress, string phone)
        {
            Id = id;
            Name = name;
            Adress = adress;
            Phone = phone;
            
            Employees = new List<Employee>();
            Menu = new List<Dish>();

        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public bool RemoveEmployee(string firstName, string lastName)
        {
            Employee? employee = Employees.Find(x => x.FirstName == firstName && x.LastName == lastName);
            if (employee != null)
            {
                Employees.Remove(employee);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddDish(Dish dish)
        {
            Menu.Add(dish);
        }

        // TODO!!!
        // CreateOrder(customer, items)
        // GetAvailableDishes()
    }
}