using System.ServiceProcess;

namespace LY.Service.ResourceConvert
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[] 
            { 
                new HomoryResourceConvertService() 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
