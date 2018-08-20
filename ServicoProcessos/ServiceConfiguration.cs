using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ServicoProcessos
{
    class ServiceConfiguration
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<Service>(service =>
                {
                    service.ConstructUsing(s => new Service());

                    service.WhenStarted(s => s.Start());

                    service.WhenStopped(s => s.Stop());
                });

                configure.RunAsLocalSystem();

                configure.SetServiceName("FortesListagemProcessos");

                configure.SetDisplayName("Fortes - Listagem de Processos");

                configure.SetDescription("Remetente de processos para uso exclusivo da Fortes");

                configure.StartAutomatically();
            });
        }
    }
}
