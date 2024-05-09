using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //relazione molti a 1 (chiave esterna)
        public long Software_house_id { get; set; }
        public Software_house Software_house { get; set; }
        public Videogame() { }
       
        public Videogame(string name, string overview, DateTime release, long id)
        {
            Overview = overview;
            Name = name;
            Release = release;
            Software_house_id = id;
            
        }

        //override del metodo ToString
        public override string ToString()
        {
            return $"\nNome: {Name} - Descrizione: {Overview} - Data di rilascio: {Release}";
        }


    }

}
