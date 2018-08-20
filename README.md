# MonitorProcessos
Solução completa de monitoramento de processos composta de:
- API
- Instalador do serviço
- Serviço do Windows
- Banco de dados
- Site

### API
WebApi ASP.NET, sem MVC.

### Instalador do serviço
Programa feito em WPF que instala e executa um serviço do windows, sem necessidade de reiniciar o computador.

### Serviço do windows
WindowsService feito em C# responsável por capturar e enviar os processos para a API.

### Banco de dados
Apenas 4 tabelas

### Site Reactjs
Interface web para o monitoramento dos processos/máquina.

# Funcionamento
O instalador do serviço recuperar informações(nome da empresa e cnpj), ao clicar em Instalar, então:
* Acessa a api para cadastrar a station, recebendo o cód. de cadastro (id)
* Acessa a subpasta "ServicoProcessos"(este é o projeto do serviço) e grava um pequeno arquivo .ini com o id de cadastro retornado no passo anterior
* Verifica se a instalação do serviço foi realizada e o executa.

Após isso, o serviço passa a enviar periodicamente a api a listagem dos processos correntes na máquina.
Esta periodicidade é configurada no momento em que o serviço é iniciado e faz uma requisição a api, tabela configurações, campo interval (esta informação é um integer e significa o tempo em segundos do evento "tick".
No evento tick, o serviço faz uma solicitação a api para saber se a máquina está com a configuração "ativa"; caso negativo não envia os dados.
A single-page em ReactJs acessa a api para pegar informações das máquinas e processos em andamento.
