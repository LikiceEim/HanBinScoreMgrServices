using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool.IoC
{
    public class BatchingHostingSettings : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ServiceTypeElementCollection ServiceTypes
        {
            get { return (ServiceTypeElementCollection)this[""]; }
        }

        public static BatchingHostingSettings GetSection()
        {
            return ConfigurationManager.GetSection("icms.batchingHosting")
                       as BatchingHostingSettings;
        }
    }
    public class ServiceTypeElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceTypeElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            ServiceTypeElement serviceTypeElement = (ServiceTypeElement)element;
            return serviceTypeElement.ServiceType.MetadataToken;
        }
    }
    public class ServiceTypeElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        [TypeConverter(typeof(AssemblyQualifiedTypeNameConverter))]
        public Type ServiceType
        {
            get { return (Type)this["type"]; }
            set { this["type"] = value; }
        }
    }
}
