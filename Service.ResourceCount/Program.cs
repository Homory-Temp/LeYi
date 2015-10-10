using System.ServiceProcess;

namespace LY.Service.ResourceCount
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[] 
            { 
                new HomoryResourceCountService() 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
