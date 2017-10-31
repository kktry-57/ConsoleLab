using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitor
{
    public class SysInfo
    {
        public String IP { get; set; }
        public Boolean IsConnected { get; set; }
        public Int64 MaxMemory { get; set; }
        public Int64 UsageOfMemroy { get; set; }
        public DateTime DateTime { get; set; }
        public List<CPUInfo> CPU = new List<CPUInfo>();
        public List<HdInfo> HD_Physic = new List<HdInfo>();
        public List<LogicalHDInfo> HD_Logical = new List<LogicalHDInfo>();
        public List<NetWorkInfo> NetWork = new List<NetWorkInfo>();
    }

    public class HdInfo
    {
        public string Name { get; set; }
        public Int64 MaxSpace { get; set; }
        public float? UsageOfSpace { get; set; }
        public float? Fan { get; set; }
        public float? Temperature { get; set; }

    }

    public class LogicalHDInfo
    {
        public string Name { get; set; }
        public Int64 MaxSpace { get; set; }
        public Int64 UsageOfSpace { get; set; }
    }

    public class CPUInfo
    {
        public CPUInfo()
        {
            Fan = -1;
        }
        public string Name { get; set; }
        public List<CPUCoreInfo> Cores = new List<CPUCoreInfo>();
        public float?  Fan { get; set; }
    }

    public class CPUCoreInfo
    {
        public string Name { get; set; }
        public float? Temperature { get; set; }
        public float? UsageOfCPU { get; set; }
    }

    public class NetWorkInfo
    {
        public string Name { get; set; }
        public Int64 CurrentRecive { get; set; }
        public Int64 CurrentSend { get; set; }
        public Int64 TotalRecive { get; set; }
        public Int64 TotalSend { get; set; }
        public double Speed { get; set; }

    }
}
