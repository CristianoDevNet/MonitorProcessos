using IniParser;
using IniParser.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instalador
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string installationPath;

        public MainWindow()
        {
            InitializeComponent();

            installationPath = $@"{AppDomain.CurrentDomain.BaseDirectory}ServicoProcessos\ServicoProcessos.exe";

            txtCMD.Clear();
        }

        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmpresa.Text) || string.IsNullOrWhiteSpace(txtCNPJ.Text))
            {
                MessageBox.Show("Por favor preencha corretamente os campos ao lado antes de continuar", "Preenchimento Obrigatório");

                return;
            }

            txtCMD.Clear();

            runCMD($@"{installationPath} install");
        }

        private void btnDesinstalar_Click(object sender, RoutedEventArgs e)
        {
            txtCMD.Clear();

            runCMD($@"{installationPath} uninstall");
        }

        private void runCMD(string cmd)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd");

            psi.UseShellExecute = false;

            psi.RedirectStandardOutput = true;

            psi.RedirectStandardInput = true;

            psi.CreateNoWindow = true;

            psi.StandardOutputEncoding = Encoding.GetEncoding(1252);

            var proc = Process.Start(psi);

            proc.StandardInput.WriteLine(cmd);

            proc.StandardInput.WriteLine("exit");

            txtCMD.Text = proc.StandardOutput.ReadToEnd();

            if (txtCMD.Text.Contains("transacionada c"))
            {
                string stationId = registerStation();

                if (string.IsNullOrEmpty(stationId)) return;

                if (createINI(stationId))
                {
                    ServiceController fortesService = new ServiceController("FortesListagemProcessos");

                    if (fortesService.Status == ServiceControllerStatus.Stopped) fortesService.Start();

                    MessageBox.Show("O serviço foi instalado com sucesso e está rodando no sistema!", "Êxito na Instalação");
                }
            }
        }

        private string registerStation()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:53468/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            dynamic newStation = new ExpandoObject();

            newStation.cnpj = txtCNPJ.Text.Trim();

            newStation.company_name = txtEmpresa.Text.Trim();

            var jsonObject = JsonConvert.SerializeObject(newStation);

            var strJson = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync($"api/station", strJson).Result;

            string resultId = "";

            if (response.IsSuccessStatusCode) resultId = response.Content.ReadAsStringAsync().Result;

            return resultId;
        }

        private bool createINI(string id)
        {
            try
            {
                var parser = new FileIniDataParser();

                IniData data = new IniData();

                data.Sections.Add(new SectionData("IDENTIFICACAO"));

                data.Sections["IDENTIFICACAO"].AddKey("id", id);

                parser.WriteFile(@"ServicoProcessos\station.ini", data);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
