using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
    public class ServiceHostGroup
    {
        static List<ServiceHost> _hosts = new List<ServiceHost>();

        private static void OpenHost(Type type)
        {
            ServiceHost svt = new ServiceHost(type);
            svt.Open();
            _hosts.Add(svt);
        }

        public static void StartAllConfigureService()
        {
            Configuration conf = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            ServiceModelSectionGroup svcmod = (ServiceModelSectionGroup)conf.GetSectionGroup("system.serviceModel");


            foreach (ServiceElement el in svcmod.Services.Services)
            {
                Type type = Type.GetType(el.Name + "," + "iCMS.Presentation.Server");
                if (type == null)
                {
                    //throw;
                }
                else
                {

                    OpenHost(type);
                }
            }
        }

        public static void CloseAllService()
        {
            foreach (ServiceHost host in _hosts)
            {
                host.Close();
            }
        }
    }
}
