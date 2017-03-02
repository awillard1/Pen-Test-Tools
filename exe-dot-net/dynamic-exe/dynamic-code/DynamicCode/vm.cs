using System.Management;

namespace DynamicCode
{
    class vm
    {
        const string m = "microsoft corporation";
        const string v = "vmware";
        const string _model = "Model";
        const string _manu = "Manufacturer";

        public static bool isVirtualMachine()
        {
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                string manufacturer = item[_manu].ToString();
                if (manufacturer.Contains(m, true) || manufacturer.Contains(v,true))
                    return true;

                if (item[_model] != null)
                {
                    string model = item[_model].ToString().ToLower();
                    if (model.Contains(m, true) || model.Contains(v, true))
                        return true;
                }
            }
            return false;
        }
    }
}