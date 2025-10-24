using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public interface IKontakt
    {
        Adres Adres { get; set; }

        string Email { get; set; }
        string Telefon { get; set; }

        string LoadContactInfo();
    }
}