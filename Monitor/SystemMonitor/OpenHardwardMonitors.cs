using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitor
{
    public class OpenHardwardMonitors
    {
        static OpenHardwareMonitor.Hardware.Computer computerHardware;
        public static SysInfo GetSystemInfo()
        {
            computerHardware = new Computer();
            var info = new SysInfo();
            computerHardware.MainboardEnabled = true;
            computerHardware.FanControllerEnabled = true;
            computerHardware.CPUEnabled = true;
            computerHardware.GPUEnabled = true;
            computerHardware.RAMEnabled = true;
            computerHardware.HDDEnabled = true;
            computerHardware.Open();
            //Get CPU Info
            info.CPU.AddRange(GetCpuInfo());
            //Get HD physic Info
            info.HD_Physic.AddRange(GetHdInfo());
            //Get HD Logical Info
            info.HD_Logical.AddRange(GetLogicalHdInfo());
            return info;
            
         
        
        }
        private static List<CPUInfo> GetCpuInfo()
        {
            //var cpus = computerHardware.Hardware.FirstOrDefault((x) => x.HardwareType == HardwareType.CPU);
            List<CPUInfo> info = new List<CPUInfo>();
            //可能有多顆CPU
            foreach (var cpu in computerHardware.Hardware.Where((x) => x.HardwareType == HardwareType.CPU))
            {
                CPUInfo cpuInfo = new CPUInfo() { Name = cpu.Name };
                //取得每個core資訊
                Dictionary<string, CPUCoreInfo> cores = new Dictionary<string, CPUCoreInfo>();
                foreach (var sensor in cpu.Sensors)
                {
                    if (!cores.ContainsKey(sensor.Name)) cores.Add(sensor.Name, new CPUCoreInfo() { Name = sensor.Name });
                    CPUCoreInfo item = cores[sensor.Name];
                    switch (sensor.SensorType)
                    {
                        case SensorType.Load:
                            item.UsageOfCPU = sensor.Value;
                            break;
                        case SensorType.Temperature:
                            item.Temperature = sensor.Value;
                            break;
                        case SensorType.Fan:
                            cpuInfo.Fan = sensor.Value;
                            break;
                    }
                }
                //因為有些資訊是不需要的，把不需要的CPU資訊去除。EX:CPU Package
                cpuInfo.Cores.AddRange(cores.Values.Where((x) => x.UsageOfCPU != null).ToList());
                info.Add(cpuInfo);
            }


            return info;
        }

        private static List<HdInfo> GetHdInfo()
        {
            List<HdInfo> info = new List<HdInfo>();
            foreach (var hdd in computerHardware.Hardware.Where((x) => x.HardwareType == HardwareType.HDD))
            {
                HdInfo hdInfo = new HdInfo() { Name = hdd.Name };               
                foreach (var sensor in hdd.Sensors)
                {                   
                    switch (sensor.SensorType)
                    {
                        case SensorType.Load:
                            hdInfo.UsageOfSpace = sensor.Value;
                            break;
                        case SensorType.Temperature:
                            hdInfo.Temperature = sensor.Value;
                            break;
                        case SensorType.Fan:
                            hdInfo.Fan = sensor.Value;
                            break;
                    }
                }                
                info.Add(hdInfo);
            }
            return info;
        }

        static private List<LogicalHDInfo> GetLogicalHdInfo()
        {
            List<LogicalHDInfo> info = new List<LogicalHDInfo>();
            foreach (var hd in DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed))
            {
                info.Add(new LogicalHDInfo() { Name = hd.Name, MaxSpace = hd.TotalSize, UsageOfSpace = hd.TotalSize - hd.AvailableFreeSpace });
            }
            return info;
        }
    }
}
