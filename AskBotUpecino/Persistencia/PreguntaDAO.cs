using AskBotUpecino.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AskBotUpecino.Persistencia
{
    public class PreguntaDAO
    {
        private string connectionString = "Server=tcp:u201817688.database.windows.net,1433;Initial Catalog=AskBotUpecino;Persist Security Info=False;User ID=U201817688;Password=Picard4!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public Pregunta ObtenerPorCurso(string idCurso)
        {
            var query = "select * from Pregunta where idCurso = @idCurso";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@idCurso", idCurso));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Pregunta()
                            {
                                idPregunta = reader["idPregunta"].ToString(),
                                PreguntaTxt = reader["PreguntaTxt"].ToString(),
                                idCurso = reader["idCurso"].ToString()
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
        
        }
    }
}