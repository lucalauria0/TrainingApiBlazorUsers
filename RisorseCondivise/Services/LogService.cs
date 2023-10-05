using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Resources
{
    public class LogService
    {
        Logger logs;
        public LogService(LoggerConfiguration config)
        {
            logs = config.CreateLogger();

            logs.Information("LogUtils Istanza Creata");
        }

        public LogService(IConfiguration config)
        {
            logs = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();

            logs.Information("LogUtils Istanza Creata");
        }



        /// <summary>
        /// SCRIVO SUL LOG CON IL DEBUGGER
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="str"></param>
        public void Debug(string caller, string str)
        {
            logs.Debug($"{caller}: {str}");
        }
        /// <summary>
        /// SCRIVO SUL LOG UN INFORMAZIONE SULL'ANDAMENTO DEL CALLER
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="str"></param>
        public void Info(string caller, string str)
        {
            logs.Information($"{caller}: {str}");
        }
        /// <summary>
        /// SCRIVO UN ERRORE SUL LOG, SPECIFICATO PRECEDENTEMENTE IN UNA TRY CATCH
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="er"></param>
        public void Error(string caller, Errore er)
        {
            logs.Error($"{caller}: {er.Dettagli}");
        }
        /// <summary>
        /// SCRIVO UN ERRORE SUL LOG TRASMETTENDO ANCHE UNA STRINGA CHE DESCRIVE L'ERRORE
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="str"></param>
        public void Error(string caller, string str)
        {
            logs.Error($"{caller}: {str}");
        }
        /// <summary>
        /// SCRIVO UN ERRORE SUL LOG TRASMETTENDO ANCHE UNA STRINGA CHE DESCRIVE L'ERRORE
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="str"></param>
        public void Error(string caller, Exception ex)
        {
            logs.Error($"{caller}: {ex.Message}     -     {ex.StackTrace}");
        }
    }
}
