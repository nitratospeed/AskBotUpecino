using AskBotUpecinoREST.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AskBotUpecinoREST.Persistencia
{
    public class PreguntaDAO
    {
        private string connectionString = "Server=tcp:u201817688.database.windows.net,1433;Initial Catalog=AskBotUpecino;Persist Security Info=False;User ID=U201817688;Password=Picard4!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public Pregunta CrearLog(Pregunta pregunta)
        {
            Pregunta preguntaNueva = null;
            var query = "insert into Pregunta values (@idPregunta, @PreguntaTxt, @idCurso, @idUsuario)";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@idPregunta", pregunta.idPregunta));
                    command.Parameters.Add(new SqlParameter("@PreguntaTxt", pregunta.PreguntaTxt));
                    command.Parameters.Add(new SqlParameter("@idCurso", pregunta.idCurso));
                    command.Parameters.Add(new SqlParameter("@idUsuario", pregunta.idUsuario));

                    command.ExecuteNonQuery();
                }
            }
            preguntaNueva = Obtener(pregunta.idPregunta);
            return preguntaNueva;
        }

        public Pregunta Obtener(string idPregunta)
        {
            var query = "select * from Pregunta where idPregunta = @idPregunta";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@idPregunta", idPregunta));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Pregunta()
                            {
                                idPregunta = reader["idPregunta"].ToString(),
                                PreguntaTxt = reader["PreguntaTxt"].ToString(),
                                idCurso = reader["idCurso"].ToString(),
                                idUsuario = reader["idUsuario"].ToString()
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