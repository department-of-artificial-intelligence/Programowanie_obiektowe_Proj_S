using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Lab2_2_.Project_lab2
{
    class Towar
    {
        public string _name_towar { get; set; }
        public int _id_towar { get; set; }
        public int _number_towar { get; set; } 
        public DateTime _godnosc_towaru { get; set; }

        public Towar(int id_towar, string name_towar,  int number_towar, DateTime godnosc_towaru)
        {
            _name_towar = name_towar;
            _id_towar = id_towar;
            _number_towar = number_towar;
            _godnosc_towaru = godnosc_towaru;
        }

        public override string ToString()
        {
            return $"id{_id_towar} {_name_towar}        ilosc:{_number_towar}      godnosc towaru: {_godnosc_towaru}";
        } 
    }
}
