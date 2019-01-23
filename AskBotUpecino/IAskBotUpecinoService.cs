using AskBotUpecino.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using AskBotUpecino.Errores;
using System.Text;

namespace AskBotUpecino
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAskBotUpecinoService" in both code and config file together.
    [ServiceContract]
    public interface IAskBotUpecinoService
    {
        [OperationContract]
        [FaultContract(typeof(PreguntaException))]
        Pregunta ObtenerPreguntaMasPopularPorCurso(string idCurso);
    }
}
