using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AskBotUpecinoRESTTEST
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Pregunta pregunta = new Pregunta()
            {
                idPregunta = "8",
                PreguntaTxt = "Que son los Microservicios?",
                idCurso = "2",
                idUsuario = "1"
            };
            string postdata = js.Serialize(pregunta);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:54365/PreguntaLogService.svc/PreguntaLogService");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            Pregunta preguntacreada = js.Deserialize<Pregunta>(tramaJson);
            Assert.AreEqual("8", pregunta.idPregunta);
            Assert.AreEqual("Que son los Microservicios?", pregunta.PreguntaTxt);
            Assert.AreEqual("2", pregunta.idCurso);
            Assert.AreEqual("1", pregunta.idUsuario);
        }
    }
}
