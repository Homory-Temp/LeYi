using System.ServiceProcess;

namespace LY.Service.Sms
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[] 
            { 
                new HomorySmsService() 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
