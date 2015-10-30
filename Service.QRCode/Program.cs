using System.ServiceProcess;

namespace LY.Service.QRCode
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[] 
            { 
                new HomoryQRCodeService() 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
