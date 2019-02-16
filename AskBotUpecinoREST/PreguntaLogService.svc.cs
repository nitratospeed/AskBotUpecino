using AskBotUpecinoREST.Dominio;
using AskBotUpecinoREST.Errores;
using AskBotUpecinoREST.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Web.Script.Serialization;

namespace AskBotUpecinoREST
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PreguntaLogService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PreguntaLogService.svc or PreguntaLogService.svc.cs at the Solution Explorer and start debugging.
    public class PreguntaLogService : IPreguntaLogService
    {
        public void InsertaPreguntaLog(Pregunta pregunta)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string postdata = js.Serialize(pregunta);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            CloudQueueMessage message = new CloudQueueMessage(postdata);
            queue.AddMessage(message);
        }

        public void InsertaPreguntasEnDB()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            queue.FetchAttributes();

            int? cachedMessageCount = queue.ApproximateMessageCount;

            for (int i = 0; i < cachedMessageCount; i++)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                CloudQueueMessage message = queue.GetMessage();
                string tramaJson = message.AsString;
                Pregunta preguntacreada = js.Deserialize<Pregunta>(tramaJson);
                PreguntaDAO preguntaDAO = new PreguntaDAO();
                preguntaDAO.CrearLog(preguntacreada);
            }
        }
    }
}
