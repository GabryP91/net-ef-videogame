using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    //nome tabella in DB
    [Table ("Videogame")]
    [Index(nameof(Name), IsUnique = true)]
    public class Videogame
    {
        
        public int id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime Release { get; set; }
        public int Id_software { get; set; }

        //relazione molti a 1 (chiave esterna)
        public Software_house Software_house { get; set; }
        public Videogame() { }
        public Videogame(string name, string overview, DateTime release, int id)
        {
            Overview = overview;
            Name = name;
            Id_software = id;
            Release = release;
        }

        public Videogame(string name, string overview, DateTime release, Software_house id)
        {
            Overview = overview;
            Name = name;
            Software_house = id;
            Release = release;
        }
    }

}
