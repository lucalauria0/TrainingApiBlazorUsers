using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RisorseCondivise;
using System.Net.Http.Json;

namespace TaskFormUtenteGenericoBlazorApp.Data
{
    public class Global
    {
        private LogService log;
        public Global(LogService service)
        {
            log = service;
        }
        public async Task<(Errore?, string)> HandleCLient(WebBox box, string destinationUrl, string nomeController, string nomeMetodo)
        {
            string risultato = string.Empty;
            Errore? er = null;
            try
            {


                HttpClientHandler handler = new HttpClientHandler();
                handler.UseDefaultCredentials = true;

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(destinationUrl);


                    HttpResponseMessage response = await client.PostAsJsonAsync($"{nomeController}/{nomeMetodo}", JsonConvert.SerializeObject(box));
                    response.EnsureSuccessStatusCode();

                    risultato = await response.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {
                er = new Errore(ex);
                log.Error("HandleClient", er);
            }

            return (er, risultato);
        }
        public async Task<(Errore, string)> HandleCLient(StringContent content, string destinationUrl, string nomeController, string nomeMetodo)
        {
            Errore er = null;
            string risultato = string.Empty;
            try
            {


                HttpClientHandler handler = new HttpClientHandler();
                handler.UseDefaultCredentials = true;

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(destinationUrl);

                    HttpResponseMessage response = await client.PostAsync($"{nomeController}/{nomeMetodo}", content);
                    response.EnsureSuccessStatusCode();

                    risultato = await response.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            {
                log.Error("HandleClient", new Errore(ex));
                er = new Errore(ex);
            }

            return (er, risultato);
        }
    }
}
