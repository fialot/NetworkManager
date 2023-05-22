using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Management;
//using Network;

namespace NetworkManager;


public static class IPv4
{

    public static bool ValidateIP(string ipString)
    {
        if (String.IsNullOrWhiteSpace(ipString))
        {
            return false;
        }

        string[] splitValues = ipString.Split('.');
        if (splitValues.Length != 4)
        {
            return false;
        }

        byte tempForParsing;

        return splitValues.All(r => byte.TryParse(r, out tempForParsing));
    }

    public static void Set(IpSetting setting)
    {
        if (setting.SetIP)
        {
            if (!setting.IsDHCP) SetIP(setting.Interface, setting.IP, setting.NetMask, setting.Gateway);
            else SetDynamicIP(setting.Interface);

        }

        if (setting.SetDNS)
        {
            if (!setting.IsAutoDNS) SetDNS(setting.Interface, setting.DNS1 + "," + setting.DNS2);
            else SetDNS(setting.Interface, "");
        }
    }

    /// <summary>
    /// Set DHCP (dynamic IP) to selected network 
    /// </summary>
    /// <param name="netInterface">Network adapter</param>
    public static void SetDynamicIP(string netInterface)
    {

        if (netInterface == "") throw new Exception("Interface not selected!");

        // ----- Get network collection -----
        var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
        var networkCollection = adapterConfig.GetInstances();

        bool findAdapter = false;
        foreach (ManagementObject adapter in networkCollection)
        {
            // ----- Find network adapter -----
            string description = adapter["Description"] as string;
            //if (string.Compare(description, netInterface, StringComparison.InvariantCultureIgnoreCase) == 0)
            if (description.IndexOf(netInterface, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                findAdapter = true;
                // ----- Setting a dynamic IP address -----
                var enableDhcp = adapter.InvokeMethod("EnableDHCP", null);

                // ----- Throw error if setting DHCP not correct -----
                if ((uint)enableDhcp != 0)
                {
                    throw new Exception("Change to DHCP error. Is cable connected?");
                }
            }
        }
        if (!findAdapter) throw new Exception("Adapter not found!");
    }

    /// <summary>
    /// Set static IP to selected network 
    /// </summary>
    /// <param name="netInterface">Network adapter</param>
    /// <param name="address">IP address</param>
    /// <param name="subnetMask">Subnet mask</param>
    /// <param name="gateway">Default gateway</param>
    public static void SetIP(string netInterface, string address, string subnetMask, string gateway)
    {
        if (netInterface == "") throw new Exception("Interface not selected!");

        // ----- Get network collection -----
        var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
        var networkCollection = adapterConfig.GetInstances();

        bool findAdapter = false;
        foreach (ManagementObject adapter in networkCollection)
        {
            // ----- Find network adapter -----
            string description = adapter["Description"] as string;
            //if (string.Compare(description, netInterface, StringComparison.InvariantCultureIgnoreCase) == 0)
            if (description.IndexOf(netInterface, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                findAdapter = true;
                // ----- Set DefaultGateway -----
                var newGateway = adapter.GetMethodParameters("SetGateways");
                newGateway["DefaultIPGateway"] = new string[] { gateway };
                newGateway["GatewayCostMetric"] = new int[] { 1 };

                // ----- Set IPAddress and Subnet Mask -----
                var newAddress = adapter.GetMethodParameters("EnableStatic");
                newAddress["IPAddress"] = new string[] { address };
                newAddress["SubnetMask"] = new string[] { subnetMask };

                adapter.InvokeMethod("EnableStatic", newAddress, null);
                adapter.InvokeMethod("SetGateways", newGateway, null);
            }
        }
        if (!findAdapter) throw new Exception("Adapter not found!");
    }

    /// <summary>
    /// Set's the DNS Server of the local machine
    /// </summary>
    /// <param name="netInterface">NIC address</param>
    /// <param name="DNS">DNS server address</param>
    /// <remarks>Requires a reference to the System.Management namespace</remarks>
    public static void SetDNS(string netInterface, string DNS)
    {
        if (netInterface == "") throw new Exception("Interface not selected!");

        // ----- Get network collection -----
        var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
        var networkCollection = adapterConfig.GetInstances();

        bool findAdapter = false;
        foreach (ManagementObject adapter in networkCollection)
        {
            // ----- Find network adapter -----
            string description = adapter["Description"] as string;
            //if (string.Compare(description, netInterface, StringComparison.InvariantCultureIgnoreCase) == 0)
            if (description.IndexOf(netInterface, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                findAdapter = true;
                // ----- Setting a DNS -----
                ManagementBaseObject newDNS = adapter.GetMethodParameters("SetDNSServerSearchOrder");
                if (DNS == null || DNS == "")
                    newDNS["DNSServerSearchOrder"] = null;
                else
                    newDNS["DNSServerSearchOrder"] = DNS.Split(',');
                ManagementBaseObject setDNS = adapter.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
            }
        }
        if (!findAdapter) throw new Exception("Adapter not found!");
    }

    /// <summary>
    /// Set's WINS of the local machine
    /// </summary>
    /// <param name="NIC">NIC Address</param>
    /// <param name="priWINS">Primary WINS server address</param>
    /// <param name="secWINS">Secondary WINS server address</param>
    /// <remarks>Requires a reference to the System.Management namespace</remarks>
    public static void SetWINS(string netInterface, string priWINS, string secWINS)
    {

        if (netInterface == "") throw new Exception("Interface not selected!");

        // ----- Get network collection -----
        var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
        var networkCollection = adapterConfig.GetInstances();

        foreach (ManagementObject adapter in networkCollection)
        {
            // ----- Find network adapter -----
            string description = adapter["Description"] as string;
            //if (string.Compare(description, netInterface, StringComparison.InvariantCultureIgnoreCase) == 0)
            if (description.IndexOf(netInterface, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                // ----- Setting a WINS -----
                ManagementBaseObject setWINS;
                ManagementBaseObject wins =
                adapter.GetMethodParameters("SetWINSServer");
                wins.SetPropertyValue("WINSPrimaryServer", priWINS);
                wins.SetPropertyValue("WINSSecondaryServer", secWINS);

                setWINS = adapter.InvokeMethod("SetWINSServer", wins, null);
            }
        }
    }

    public static List<string> GetInterfaces()
    {
        List<string> ifaceList = new List<string>();
        var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
        var networkCollection = adapterConfig.GetInstances();

        foreach (ManagementObject adapter in networkCollection)
        {
            if ((bool)adapter["IPEnabled"])
            {
                string description = adapter["Description"] as string;
                //string caption = adapter["Caption"] as string;

                if (!(description.Contains("VirtualBox"))) //description.Contains("Linux USB Ethernet") ||
                {
                    // ----- remove end number -----
                    var split = description.Split(new string[] { "#" }, StringSplitOptions.None);
                    if (split.Length == 2)
                    {
                        description = split[0].Trim();
                    }

                    ifaceList.Add(description);
                }

            }
        }

        return ifaceList;
    }

    public static string GetIpList()
    {
        string res = "";
        int ethCount = 0;
        int WiFiCount = 0;
        List<string> adapterList = new List<string>();

        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        IPHostEntry host;
        host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (NetworkInterface adapter in interfaces)
        {
            var ipProps = adapter.GetIPProperties();

            foreach (var ip in ipProps.UnicastAddresses)
            {
                if ((adapter.OperationalStatus == OperationalStatus.Up)
                    && (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    && adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    && adapter.NetworkInterfaceType != NetworkInterfaceType.Tunnel
                    && adapter.NetworkInterfaceType != NetworkInterfaceType.Unknown
                    //&& !adapter.Description.Contains("Linux USB Ethernet")
                    && !adapter.Description.Contains("VirtualBox"))
                {
                    if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    {
                        if (adapterList.Contains(adapter.Description))
                        {
                            res += "/" + ip.Address;
                        }
                        else
                        {
                            adapterList.Add(adapter.Description);
                            if (res != "") res += Environment.NewLine;
                            res += "WiFi";
                            if (WiFiCount > 0) res += (WiFiCount + 1).ToString();
                            else res += " ";
                            res += ":  " + ip.Address;
                            WiFiCount++;
                        }
                    }
                    else
                    {
                        if (adapterList.Contains(adapter.Description))
                        {
                            res += "/" + ip.Address;
                        }
                        else
                        {
                            adapterList.Add(adapter.Description);
                            if (res != "") res += Environment.NewLine;
                            res += "LAN";
                            if (ethCount > 0) res += (ethCount + 1).ToString();
                            else res += " ";
                            res += ":  " + ip.Address;
                            ethCount++;
                        }
                    }
                }
            }
        }
        return res;

    }
}
