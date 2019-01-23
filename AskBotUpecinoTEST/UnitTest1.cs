using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AskBotUpecinoTEST
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestObtenerPreguntaMasPopularPorCurso()
        {
            PreguntaWS.AskBotUpecinoServiceClient proxy = new PreguntaWS.AskBotUpecinoServiceClient();
            proxy.ObtenerPreguntaMasPopularPorCurso("1");
        }

        [TestMethod]
        public void TestObtenerPreguntaMasPopularPorCursoError()
        {
            PreguntaWS.AskBotUpecinoServiceClient proxy = new PreguntaWS.AskBotUpecinoServiceClient();
            try
            {
                proxy.ObtenerPreguntaMasPopularPorCurso("");
            }
            catch (FaultException<PreguntaWS.PreguntaException> error)
            {
                Assert.AreEqual("Error al obtener pregunta.", error.Reason.ToString());
                Assert.AreEqual("101", error.Detail.Codigo);
                Assert.AreEqual("El curso no es válido.", error.Detail.Descripcion);
            }           
        }
    }
}
