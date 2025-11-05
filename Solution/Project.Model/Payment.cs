using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Payment
    {
        public required int PaymentId {  get; set; }
        public required int StudentId {  get; set; }
        public required decimal Amount {  get; set; }
        public required DateTime PaymentDate {  get; set; }
        public required PaymentMethod PaymentMethod {  get; set; }
        public required string Description {  get; set; }

        public required Student Student { get; set; }
    }
}
