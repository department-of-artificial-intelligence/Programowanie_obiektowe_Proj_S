﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Clinic
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }


        public List<Veterinarian> Veterinarians { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {City} {Address} {PhoneNumber}";
        }

    }   
}
