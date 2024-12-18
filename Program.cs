﻿/*

Vogliamo modificare l’esercizio di ieri rimuovendo le parti gestite con SqlClient e implementandole con Entity Framework.
Devono essere presenti tutte le funzionalità dell’esercizio originale.

Aggiungiamo anche un’altra voce al menu :
- inserisci una nuova software house
Fatto questo, ogni volta che creiamo un nuovo videogioco dobbiamo abbinargli la software house che l’ha prodotto 
(che dobbiamo aver precedentemente inserito in tabella), chiedendo all’utente l’id della software house e impostandolo nell’entity del videogame.
Realizzare quindi tutte le entity e le migration necessarie per creare il database e implementare tutte le richieste dell’esercizio.

BONUS :
aggiungere un’altra voce di menu
- stampa tutti i videogiochi prodotti da una software house (all’utente verrà chiesto l’id della software house della quale mostrare i videogame)
Avete notato quanto è più veloce e semplice creare le tabelle e interrogarle tramite un ORM? :) 
 
*/
namespace net_ef_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideogameManager manager = new VideogameManager();

            bool running = true;

            while (running)
            {
                Console.WriteLine("Seleziona un'opzione:");
                Console.WriteLine("1. Inserire un nuovo videogioco");
                Console.WriteLine("2. Ricercare un videogioco per ID");
                Console.WriteLine("3. Ricercare tutti i videogioco aventi il nome contenente una determinata stringa");
                Console.WriteLine("4. Cancellare un videogioco");
                Console.WriteLine("5. Inseriemnto automatico SW");
                Console.WriteLine("6. Cancellazione automatica TUTTE SW");
                Console.WriteLine("7. Chiudere il programma");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            using (GameDbContext db = new GameDbContext())
                            {
                                //se la tabella Sw è popolata
                                if (db.Software_house.Any())
                                {
                                    string name = CheckString("\nInserisci il nome del videogioco: ");

                                    string overview = CheckString("\nInserisci una descrizione: ");

                                    DateTime date = CheckDate("\nInserisci data rilascio: ");

                                    int id = CheckIdSoftware("Inserisci id Software di riferimento: ", manager);

                                    Videogame gioco = new Videogame(name, overview, date, id);

                                    try
                                    {
                                        manager.InserisciVideogame(gioco);

                                        Console.WriteLine("Videogioco inserito con successo.");
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine($"Errore: {ex.Message}");
                                    }
                                }

                                else
                                {
                                    
                                    Console.WriteLine("\nNessuna software house presente.Procedere prima all'inserimento\n");
                                }

                            }
                               

                            break;
                        case 2:

                            int idVideogame = CheckIdGame("\nInserisci l'ID del videogioco da cercare: ");

                            Videogame prova = manager.GetVideogameById(idVideogame);

                            Console.WriteLine(prova.ToString());

                            break;
                        case 3:

                            string searchTerm = CheckString("\n Inserisci una parola: ");

                            List<Videogame> listaGiochi = manager.GetVideogamesByString(searchTerm);

                            if (listaGiochi.Count > 0)
                            {
                                foreach (Videogame giocoL in listaGiochi)
                                {
                                    // Stampo tutte le informazioni di ogni gioco trovato
                                    Console.WriteLine(giocoL.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nessun videogioco trovato.");
                            }

                            break;
                        case 4:

                            int idVideogameDel = CheckIdGame("\nInserisci l'ID del videogioco da cancellare: ");

                            manager.DelVideogame(idVideogameDel);

                            Console.WriteLine("Cancellazione avvenuta con successo.");

                            break;
                        case 5:

                            using(GameDbContext db = new GameDbContext())
                            {
                                //se la tabella Sw è popolata
                                if(db.Software_house.Any())
                                {
                                    Console.WriteLine("\nDB non vuoto.....eliminare informazioni da software House");
                                }

                                else
                                {
                                    manager.InsertSoftwareHouse();

                                    Console.WriteLine("\nInserimento software house avvenuto con successo");
                                }
                           
                            }
                    

                            break;
                        case 6:

                            using (GameDbContext db = new GameDbContext())
                            {
                                // Rimuovi tutte le software house
                                db.Software_house.RemoveRange(db.Software_house);
                                db.SaveChanges();

                                Console.WriteLine("\nSvuotamento della tabella Software_house completato.");

                            }

                            /*using (var context = new GameDbContext())
                            {
                                context.Database.EnsureDeleted();
                            }*/

                            break;
                        case 7:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Scelta non valida. Riprova.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Input non valido. Riprova.");
                }

                Console.WriteLine();
            }
        }

        static string CheckString(string message)
        {
            string input;
            Console.WriteLine(message);
            while (true)
            {
                input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("\n La stringa è vuota. Inserisci una stringa valida.");
                    Console.WriteLine(message);
                }
            }
        }

        static DateTime CheckDate(string message)
        {
            DateTime input;
            Console.WriteLine(message);
            while (!DateTime.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("\n Sintassi errata. Inserisci una data corretta");
                Console.WriteLine(message);
            }
            return input;
        }

        static int CheckIdSoftware(string message, VideogameManager manager)
        {
            int num;
            Console.WriteLine();
            Console.WriteLine("Lista Software_House");
            manager.ViewSoftwareHouse();

            Console.WriteLine();

            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Sintassi errata. Inserisci un numero intero");
                Console.WriteLine(message);
            }

            return num;
        }

        static int CheckIdGame(string message)
        {
            int num;

            Console.WriteLine();

            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Sintassi errata. Inserisci un numero intero");
                Console.WriteLine(message);
            }
            return num;

        }
    }
}
