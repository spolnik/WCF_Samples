using System.ServiceModel;
using System.ServiceProcess;

namespace WcfSample.Hosting.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private ServiceHost _serviceHost = new ServiceHost(typeof(CalculatorService.CalculatorService));

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (_serviceHost.State == CommunicationState.Opened)
                _serviceHost.Close();
        }
    }
}
