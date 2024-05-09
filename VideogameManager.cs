using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace net_ef_videogame
{
    internal class VideogameManager
    {
    
        //INSRIMENTO NUOVO VIDEOGIOCO
        public void InserisciVideogame(Videogame gioco)
        {
            
            using (GameDbContext db = new GameDbContext())
            {
                

               Software_house videoGamesFromSoftwareHouse = db.Software_house.FirstOrDefault(v => v.id == gioco.Software_house.id);

                Videogame newVideoGame = new Videogame(gioco.Name, gioco.Overview, gioco.Release, videoGamesFromSoftwareHouse);
                db.Add(newVideoGame);
                db.SaveChanges();
                
            }
        }

        //RICERCA GIOCO PER ID
        public Videogame GetVideogameById(int id)
        {
            using (GameDbContext db = new GameDbContext())
            {

                Videogame videoGames = db.Videogame.FirstOrDefault(v => v.id == id);

                return videoGames;
            }

        }

        public List<Videogame> GetVideogamesByString(string word)
        {
            using (GameDbContext db = new GameDbContext())
            {

                var videoGames = db.Videogame.Where(v => v.Name.Contains(word)).ToList();

                return videoGames;
            }
        }

        //CANCELLAZIONE VIDEOGAME
        public void DelVideogame(int id)
        {

            using (GameDbContext db = new GameDbContext())
            {

                Videogame videoGames = db.Videogame.FirstOrDefault(v => v.id == id);

                db.Remove(videoGames);

                db.SaveChanges();

            }
         
        }

        //VISUALIZZAZIONE LISTA SOFTWARE HOUSE
        public void ViewSoftwareHouse()
        {

            using (GameDbContext db = new GameDbContext())
            {
                
                var SoftwareHouse = db.Software_house.ToList();

                //controllo che ci siano software house
                if (SoftwareHouse.Count == 0)
                {
                    Console.WriteLine("Nessuna software house disponibile nel database.");
                }

                else
                {

                    foreach (var SW in SoftwareHouse)
                    {
                        Console.WriteLine($"\nNomeSW: {SW.Name} Sede: {SW.City} Nazione: {SW.Country} ID: [{SW.id}]\n");
                    }

                }

            }
        }
    }
}
