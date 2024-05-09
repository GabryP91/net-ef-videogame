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

                db.Videogame.Add(gioco);

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

                

                foreach (var SW in SoftwareHouse)
                    {
                        Console.WriteLine($"\nNomeSW: {SW.Name} Sede: {SW.City} Nazione: {SW.Country} ID: [{SW.id}]\n");
                    }

                

            }
        }

        public void InsertSoftwareHouse()
        {
            using (GameDbContext db = new GameDbContext())
            {

                //aggiunta nuove software house
                Software_house newSoftwareHouse1 = new Software_house("Nintendo", "ME-697-14-7528-0", "Kyoto", "Japan");
                db.Add(newSoftwareHouse1);
                Software_house newSoftwareHouse2 = new Software_house("Rockstar Games", "GA-160-16-9503-1", "New York City", "United States");
                db.Add(newSoftwareHouse2);
                Software_house newSoftwareHouse3 = new Software_house("Valve Corporation", "UT-277-92-7542-2", "Bellevue", "United States");
                db.Add(newSoftwareHouse3);
                Software_house newSoftwareHouse4 = new Software_house("Electronic Arts", "SD-032-99-9226-3", "Redwood City", "United States");
                db.Add(newSoftwareHouse4);
                Software_house newSoftwareHouse5 = new Software_house("Ubisoft", "NC-134-01-6528-4", "Montreuil", "France");
                db.Add(newSoftwareHouse5);

                db.SaveChanges();

            }
        }
     }
}
