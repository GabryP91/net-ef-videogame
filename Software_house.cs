using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    [Table("Software_house")]
    //colonna con valori unici
    [Index(nameof(Tax_id), IsUnique = true)]
    public class Software_house
    {
        public long id { get; set; }
        public string Name { get; set; }
        public string Tax_id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //relazione 1 a molti
        public List<Videogame> Videogames { get; set; }
        public Software_house() { }
        public Software_house(string name, string tax_id, string city, string country)
        {
            Tax_id = tax_id;
            Name = name;
            City = city;
            Country = country;
        }

    }
}
