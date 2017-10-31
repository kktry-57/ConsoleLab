using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlTest
{


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class SysAlertValue
    {

        private SysAlertValueCPU cPUField;

        private object hdField;

        /// <remarks/>
        public SysAlertValueCPU CPU
        {
            get
            {
                return this.cPUField;
            }
            set
            {
                this.cPUField = value;
            }
        }

        /// <remarks/>
        public object HD
        {
            get
            {
                return this.hdField;
            }
            set
            {
                this.hdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SysAlertValueCPU
    {

        private byte usageField;

        private byte temperatureField;

        /// <remarks/>
        public byte Usage
        {
            get
            {
                return this.usageField;
            }
            set
            {
                this.usageField = value;
            }
        }

        /// <remarks/>
        public byte Temperature
        {
            get
            {
                return this.temperatureField;
            }
            set
            {
                this.temperatureField = value;
            }
        }
    }



}
