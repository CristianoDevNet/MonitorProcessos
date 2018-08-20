using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace ServicoProcessos
{
    class Service
    {
        const string IDENT = "IDENTIFICACAO";

        int stationId;

        Timer timer;

        HttpResponseMessage response;

        public void Start()
        {
            int interval = 0;

            IniData data = new FileIniDataParser().ReadFile("station.ini");

            response = new ClientHTTP().Client().GetAsync($"api/station/{data[IDENT]["id"]}").Result;

            if (response.IsSuccessStatusCode)
            {
                stationId = ((dynamic)JObject.Parse(response.Content.ReadAsStringAsync().Result)).id;
            }

            response = new ClientHTTP().Client().GetAsync("api/config").Result;

            if (response.IsSuccessStatusCode) interval = ((dynamic)JObject.Parse(response.Content.ReadAsStringAsync().Result)).interval;

            timer = new Timer();

            timer.Interval = interval == 0 ? 60000 : (interval * 1000);

            timer.Elapsed += Timer_Ticket;

            timer.Enabled = true;

            gravarLog($"{DateTime.Now} : Serviço de log de processos da Fortes iniciado");
        }

        private async void gravarLog(string mensagem)
        {
            using (StreamWriter sw = File.AppendText("Log.txt"))
            {
                await sw.WriteLineAsync(mensagem);
            }
        }

        public void Stop()
        {
            gravarLog($"{DateTime.Now} : Serviço parou!");
        }

        private void Timer_Ticket(object sender, ElapsedEventArgs e)
        {
            response = new ClientHTTP().Client().GetAsync($"api/station/{stationId}").Result;

            if (response.IsSuccessStatusCode)
            {
                bool isActive = ((dynamic)JObject.Parse(response.Content.ReadAsStringAsync().Result)).is_active;

                if (!isActive) return;
            }

            int resultId = 0;

            dynamic occurrence = new ExpandoObject();

            occurrence.station_id = stationId;

            occurrence.occurred_when = DateTime.Now;

            List<string> procList = new List<string>();

            Process.GetProcesses()
                   .OrderBy(p => p.ProcessName)
                   .Select(s => s.ProcessName)
                   .Distinct()
                   .ToList()
                   .ForEach(p =>
                   {
                       procList.Add(p);
                   });

            occurrence.proc = procList;

            var jsonObject = JsonConvert.SerializeObject(occurrence);

            var strJson = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            response = new ClientHTTP().Client().PostAsync($"api/occurrence", strJson).Result;

            if (response.IsSuccessStatusCode) resultId = Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
            
            gravarLog($"{DateTime.Now} : Ticket Fortes");
        }
    }
}