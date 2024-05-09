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
        //creo e istanzio stringa connessione con le informazioni
        private const string STRINGA_DI_CONNESSIONE = "Data Source=localhost;Initial Catalog=db_videogame2;Integrated Security=True;Trust";


        //INSRIMENTO NUOVO VIDEOGIOCO
        public void InserisciVideogame(Videogame gioco)
        {

            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                //apro connessione db
                connessioneSql.Open();

                //creo la query per l'inserimento dei dati
                string query = @"INSERT INTO videogames (name, overview, release_date, created_at, updated_at, software_house_id)
                VALUES (@name, @overview, @release_date, @created_at, @updated_at, @sh_id)";

                // Creo un oggetto SqlCommand con la query e la connessione
                using SqlCommand cmd = new SqlCommand(query, connessioneSql);

                //chiamo funzione privata InsertInternal
                //InsertInternal(cmd, gioco.Name, gioco.Overview, gioco.Release, gioco.Id_software);
            }
            catch (Exception ex)
            { }

        }

        //inserimento effettivo dati in db
        private static int InsertInternal(SqlCommand cmd, string nome, string descrizione, DateTime rilasciato, int id_sw)
        {
            DateTime date = DateTime.Now;

            cmd.Parameters.Add(new SqlParameter("@name", nome));
            cmd.Parameters.Add(new SqlParameter("@overview", descrizione));
            cmd.Parameters.Add(new SqlParameter("@release_date", rilasciato));
            cmd.Parameters.Add(new SqlParameter("@created_at", date));
            cmd.Parameters.Add(new SqlParameter("@updated_at", date));
            cmd.Parameters.Add(new SqlParameter("@sh_id", id_sw));

            int affectedRows = cmd.ExecuteNonQuery();
            return affectedRows;
        }

        //RICERCA GIOCO PER ID
        public void GetVideogameById(int id)
        {
            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                //apro connessione db
                connessioneSql.Open();

                //creo la query per l'inserimento dei dati
                string query = @"select name as Nome_Gioco, overview as descrizione, release_date as Periodo_rilascio from videogames where id=@id";

                // Creo un oggetto SqlCommand con la query e la connessione
                using SqlCommand cmd = new SqlCommand(query, connessioneSql);

                // aggiungo il valore passato alla mia query
                cmd.Parameters.Add(new SqlParameter("@id", id));

                // Eseguo il comando e ottengo un SqlDataReader per leggere i risultati
                using SqlDataReader reader = cmd.ExecuteReader();


                // Controllo se ci sono righe restituite
                if (reader.HasRows)
                {
                    // Leggo i risultati e li stampo a console
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nNome: {reader["Nome_Gioco"]},\nDescrizione: {reader["descrizione"]},\nData di rilascio: {reader["Periodo_rilascio"]}");
                    }
                }

                else
                {
                    // Se non ci sono risultati, informo l'utente che l'ID specificato non esiste
                    Console.WriteLine("Nessun videogioco trovato con l'ID specificato.");
                }

            }
            catch (Exception ex)
            { }

        }

        public void GetVideogamesByString(string word)
        {
            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                //apro connessione db
                connessioneSql.Open();

                //creo la query per l'inserimento dei dati
                string query = @"select name as Nome_Gioco, overview as descrizione, release_date as Periodo_rilascio from videogames where name LIKE '%' + @word + '%'";

                // Creo un oggetto SqlCommand con la query e la connessione
                using SqlCommand cmd = new SqlCommand(query, connessioneSql);

                // aggiungo il valore passato alla mia query
                cmd.Parameters.Add(new SqlParameter("@word", word));

                // Eseguo il comando e ottengo un SqlDataReader per leggere i risultati
                using SqlDataReader reader = cmd.ExecuteReader();


                // Controllo se ci sono righe restituite
                if (reader.HasRows)
                {
                    // Leggo i risultati e li stampo a console
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nNome: {reader["Nome_Gioco"]},\nDescrizione: {reader["descrizione"]},\nData di rilascio: {reader["Periodo_rilascio"]}");
                    }
                }

                else
                {
                    // Se non ci sono risultati, informo l'utente che l'ID specificato non esiste
                    Console.WriteLine("Nessun videogioco trovato con l'ID specificato.");
                }

            }
            catch (Exception ex)
            { }
        }

        //CANCELLAZIONE VIDEOGAME
        public void DelVideogame(int id)
        {
            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                connessioneSql.Open();
                string query = @"DELETE FROM videogames WHERE id=@id;";

                using SqlCommand cmd = new SqlCommand(query, connessioneSql);

                cmd.Parameters.Add(new SqlParameter("@id", id));

                int affectedRows = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            { }
        }

        //VISUALIZZAZIONE LISTA SOFTWARE HOUSE
        public void ViewSoftwareHouse()
        {
            using SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE);

            try
            {
                //apro connessione db
                connessioneSql.Open();

                //creo la query per l'inserimento dei dati
                string query = @"select name as SoftwareHouse, id as ID from software_houses";

                // Creo un oggetto SqlCommand con la query e la connessione
                using SqlCommand cmd = new SqlCommand(query, connessioneSql);

                // Eseguo il comando e ottengo un oggetto SqlDataReader per leggere i risultati
                using SqlDataReader reader = cmd.ExecuteReader();

                // Leggo i risultati e li stampo a console
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}, Nome: {reader["SoftwareHouse"]}");
                }


            }
            catch (Exception ex)
            { }
        }
    }
}
