﻿using System.ServiceProcess;

namespace LY.Service.QRCode
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
