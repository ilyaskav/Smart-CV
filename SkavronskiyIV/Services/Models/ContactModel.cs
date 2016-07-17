using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        public ContactTitleModel ContactTitle { get; set; }

        public String Data { get; set; }
    }
}
