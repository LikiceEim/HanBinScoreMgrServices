
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Tool.IoC;

namespace iCMS.Common.Component.Tool.IoC
{
    public class UnityServiceHostCollection : Collection<UnityServiceHost>, IDisposable
    {
        public UnityServiceHostCollection(params Type[] serviceTypes)
        {
            BatchingHostingSettings settings = BatchingHostingSettings.GetSection();
            foreach (ServiceTypeElement element in settings.ServiceTypes)
            {
                this.Add(element.ServiceType);
            }

            if (null != serviceTypes)
            {
                Array.ForEach<Type>(serviceTypes, serviceType => this.Add(new UnityServiceHost(serviceType, "defaultContainer")));
            }
        }
        public void Add(params Type[] serviceTypes)
        {
            if (null != serviceTypes)
            {
                Array.ForEach<Type>(serviceTypes, serviceType => this.Add(new UnityServiceHost(serviceType, "defaultContainer")));
            }
        }
        public void Open()
        {
            foreach (UnityServiceHost host in this)
            {
                host.Open();
            }
        }
        public void Dispose()
        {
            foreach (IDisposable host in this)
            {
                host.Dispose();
            }
        }
    }
}
