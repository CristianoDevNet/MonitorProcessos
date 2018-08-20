using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServicoProcessos
{
    public class ClientHTTP
    {
        HttpClient client;

        public ClientHTTP()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:53468/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient Client()
        {
            return client;
        }
    }
}
