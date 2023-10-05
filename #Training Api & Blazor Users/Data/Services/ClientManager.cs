
using System.Net.Http.Json;
using Newtonsoft.Json;
using Shared_Resources;

namespace _Training_Api___Blazor_Users
{
    public class ClientManager
    {
        public string baseUrl = string.Empty;
        public LogService log;
        public Global g;
        static HttpClient client = new HttpClient();

        public ClientManager(string urlWebApi = @"https://localhost:7291", LogService logger = null)
        {
            baseUrl = urlWebApi;
            log = logger;
            g = new Global(logger);
            log.Info("ApiHelper", "Logs ON");
        }

     

        public async Task<(Errore?, List<Persona>)> GetPersone(Dictionary<string, List<object>> attributi)
        {
            
            Errore? er = null;
            List<Persona> retVal = new List<Persona>();
            string caller = "ApiHelper.GetPersone()";
            string result = string.Empty;

            try
            {
                WebBox box = new WebBox { Attributi = attributi };

                (er, result) = await g.HandleCLient(box, baseUrl, "api/User", "GetUsers");
                if (er != null)
                    return (er, retVal);
                
                log.Debug(caller, $"Result = {result}");

                (er, retVal) = JsonConvert.DeserializeObject <(Errore?, List<Persona>)>(result, new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Replace});
            }
            catch (Exception ex)
            {
                er = new Errore(ex);
                return (er, retVal);
            }
            return (er, retVal);
        }

        public async Task<Errore> Delete(Dictionary<string, List<object>> attributi)
        {

            Errore er = null;
            string caller = "ApiHelper.Delete()";
            string result = string.Empty;

            try
            {
                WebBox box = new WebBox { Attributi = attributi };

                (er, result) = await g.HandleCLient(box, baseUrl, "api/User", "Delete");
                if (er != null)
                    return (er);

                log.Debug(caller, $"Result = {result}");
                (er) = JsonConvert.DeserializeObject<Errore>(result, new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Replace });
            }
            catch (Exception ex)
            {
                er = new Errore(ex);
                return (er);
            }
            return (er);
        }

        public async Task<Errore> InsertPersona(Persona p)
        {
            Errore er = null;
            string caller = "ApiHelper.InsertPersona()";
            string result = string.Empty;
           try
            {

                log.Debug(caller, $"Persona = {p.ToString()}");
                WebBox box = new WebBox { Persona = p };

                (er,result) = await g.HandleCLient(box, baseUrl, "api/User", "InsertUser");

                if (er != null)
                    return (er);

                log.Debug(caller, $"Result = {result}");
            }
            catch (Exception e)
            {
                er = new Errore(e);
                log.Error(caller, er);
                return er;
            }
            return er;

        }

        public async Task<Errore> UpdatePersona(Persona p)
        {
            Errore er = null;
            string caller = "ApiHelper.UpdatePersona()";
            string result = string.Empty;
            try
            {

                log.Debug(caller, $"Persona = {p}");
                WebBox box = new WebBox { Persona = p };

                (er, result) = await g.HandleCLient(box, baseUrl, "api/User", "UpdateUser");
                if (er != null)
                    return (er);

                log.Debug(caller, $"Box = {box.Persona.ToString()}");
                log.Debug(caller, $"Result = {result}");
            }
            catch (Exception e)
            {
                er = new Errore(e);
                log.Error(caller, er);
                return er;
            }
            return er;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cf"></param>
        /// <returns></returns>
        public async Task<Errore> CheckUser(string cf)
        {
            Errore er = null;
            bool result = true;
            string caller = "Manager, CheckUser()";
            log.Info(caller, "ON");

            try
            {
                Dictionary<string, List<object>> keyValuePairs = new Dictionary<string, List<object>>();
                keyValuePairs.Add("CodiceFiscale", new List<object> { cf });
                List<Persona> personas;

                (er, personas) = await GetPersone(keyValuePairs);
                log.Debug(caller, $"Persone trovate: {personas.Count}");

                if (personas.Any())
                {
                    er = new Errore("L'Utente è gia registrato.");
                }

            }
            catch (Exception ex)
            {
                er = new Errore("Qualcosa è andato storto... :(");
                log.Error(caller, er);
            }

            log.Info(caller, "OFF");

            return er;
        }

    }
}
