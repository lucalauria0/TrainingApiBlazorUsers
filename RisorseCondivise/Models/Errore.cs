using System;
using System.Collections.Generic;

namespace Shared_Resources
{
    public class Errore
    {
        public string Messaggio { get; set; }
        public string StackTrace { get; set; }
        public Dictionary<string, string> InfoAggiuntive { get; set; }

        public string Dettagli
        {
            get
            {
                string str = "";

                if (!string.IsNullOrEmpty(Messaggio))
                    str += $"MESSAGGIO: {Messaggio}{Environment.NewLine}";

                if (!string.IsNullOrEmpty(StackTrace))
                    str += $"STACKTRACE: {StackTrace}{Environment.NewLine}";

                if (InfoAggiuntive != null)
                {
                    foreach (var kvp in InfoAggiuntive)
                        str += $"{kvp.Key.ToUpper()}: {kvp.Value}{Environment.NewLine}";
                }

                return str;
            }
        }

        public Errore()
        {
            InfoAggiuntive = new Dictionary<string, string>();
        }

        public Errore(Exception ex, Dictionary<string, string>? dati = null)
        {
            Messaggio = ex.Message;
            StackTrace = ex.StackTrace ?? string.Empty;
            InfoAggiuntive = dati ?? new Dictionary<string, string>();
        }

        public Errore(string messaggio, Dictionary<string, string>? dati = null)
        {
            Messaggio = messaggio;
            StackTrace = "";
            InfoAggiuntive = dati ?? new Dictionary<string, string>();
        }
    }
}