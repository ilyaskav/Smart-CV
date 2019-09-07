using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class LanguageModel
    {
        public int? Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Level { get; set; }

    }
}
