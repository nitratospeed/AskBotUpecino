using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AskBotUpecino.Dominio;
using AskBotUpecino.Errores;
using AskBotUpecino.Persistencia;

namespace AskBotUpecino
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AskBotUpecinoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AskBotUpecinoService.svc or AskBotUpecinoService.svc.cs at the Solution Explorer and start debugging.
    public class AskBotUpecinoService : IAskBotUpecinoService
    {
        private PreguntaDAO dao = new PreguntaDAO();

        public Pregunta ObtenerPreguntaMasPopularPorCurso(string idCurso)
        {
            if (string.IsNullOrWhiteSpace(idCurso))
            {
                throw new FaultException<PreguntaException>(
                    new PreguntaException()
                    {
                        Codigo = "101",
                        Descripcion = "El curso no es válido."
                    }, new FaultReason("Error al obtener pregunta."));
            }

            return dao.ObtenerPorCurso(idCurso);
        }
    }
}
