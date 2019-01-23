using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AskBotUpecino.Dominio
{
    [DataContract]
    public class Pregunta
    {
        [DataMember]
        public string idPregunta { get; set; }
        [DataMember]
        public string PreguntaTxt { get; set; }
        [DataMember]
        public string idCurso { get; set; }
    }
}