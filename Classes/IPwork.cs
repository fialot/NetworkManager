using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using myFunctions;

namespace IPchanger
{
    [Serializable]
    struct ipItem
    {
        public string name;
        public string network;
        public bool changeIP;
        public bool autoIP;
        public string IP;
        public string mask;
        public string gateway;
        public bool changeDNS;
        public bool autoDNS;
        public string DNS1;
        public string DNS2;
    }

    class IPwork
    {
        List<ipItem> IPlist;
        string path;

        public IPwork(string Path)
        {
            path = Path;
            IPlist = loadIPlist(path);

        }

        public void Save()
        {
            saveIPlist(path, IPlist);
        }

        public  List<ipItem> GetList()
        {
            return IPlist;
        }

        public void EditList(int index, ipItem item)
        {
            IPlist = editList(IPlist, index, item);
        }

        public void AddList(ipItem item)
        {
            IPlist = addToList(IPlist, item);
        }

        public void DelList(int index)
        {
            IPlist = delFromList(IPlist, index);
        }

        public void ChangeIP(int index)
        {
            if (index >= IPlist.Count) return;
            ipItem item = IPlist[index];
            changeIP(item);
        }

        public void changeIP(ipItem item)
        {
            if (item.changeIP)
            {
                if (!item.autoIP) setIP(item.network, item.IP, item.mask, item.gateway);
                else setIP(item.network);
                
            }

            if (item.changeDNS)
            {
                if (!item.autoDNS) setDNS(item.network, item.DNS1 + "," + item.DNS2);
                else setDNS(item.network, "");
            }
        }

        private List<ipItem> editList(List<ipItem> list, int index, ipItem item)
        {
            list.RemoveAt(index);
            list.Insert(index, item);
            return list;
        }

        private List<ipItem> addToList(List<ipItem> list, ipItem item)
        {
            list.Add(item);
            return list;
        }

        private List<ipItem> delFromList(List<ipItem> list, int index)
        {
            list.RemoveAt(index);
            return list;
        }

        public List<ipItem> loadIPlist(string path)
        {
            List<ipItem> list = new List<ipItem>();

            if (!System.IO.File.Exists(path))
            {
                string fileName = System.IO.Path.GetFileName(path);
                string newPath = Files.ReplaceVarPaths("%AppData%" + System.IO.Path.DirectorySeparatorChar + "IPChanger") + System.IO.Path.DirectorySeparatorChar + fileName;
                if (System.IO.File.Exists(newPath))
                {
                    path = newPath;
                }
            }

            list = (List<ipItem>)Files.ImportXml(path, list);
            if (list == null) list = new List<ipItem>();

            return list;
        }

        public void saveIPlist(string path, List<ipItem> list)
        {
            
            Files.ExportXml(Files.ReplaceVarPaths(path), list);
            try
            {
                if (!System.IO.File.Exists(path))
                {
                    string fileName = System.IO.Path.GetFileName(path);
                    string dir = Files.ReplaceVarPaths("%AppData%" + System.IO.Path.DirectorySeparatorChar + "IPChanger");
                    System.IO.Directory.CreateDirectory(dir);
                    path = Files.ReplaceVarPaths(dir + System.IO.Path.DirectorySeparatorChar + fileName);
                    Files.ExportXml(path, list);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Set static IP to selected network 
        /// </summary>
        /// <param name="network">Network adapter</param>
        /// <param name="address">IP address</param>
        /// <param name="subnetMask">Subnet mask</param>
        /// <param name="gateway">Default gateway</param>
        private void setIP(string network, string address, string subnetMask, string gateway)
        {
            // ----- Get network collection -----
            var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var networkCollection = adapterConfig.GetInstances();

            bool findAdapter = false;
            foreach (ManagementObject adapter in networkCollection)
            {
                // ----- Find network adapter -----
                string description = adapter["Description"] as string;
                if (string.Compare(description, network, StringComparison.InvariantCultureIgnoreCase) == 0)
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
        /// Set DHCP (dynamic IP) to selected network 
        /// </summary>
        /// <param name="network">Network adapter</param>
        private void setIP(string network)
        {
            // ----- Get network collection -----
            var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var networkCollection = adapterConfig.GetInstances();

            bool findAdapter = false;
            foreach (ManagementObject adapter in networkCollection)
            {
                // ----- Find network adapter -----
                string description = adapter["Description"] as string;
                if (string.Compare(description, network, StringComparison.InvariantCultureIgnoreCase) == 0)
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
        /// Set's the DNS Server of the local machine
        /// </summary>
        /// <param name="network">NIC address</param>
        /// <param name="DNS">DNS server address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void setDNS(string network, string DNS)
        {
            // ----- Get network collection -----
            var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var networkCollection = adapterConfig.GetInstances();

            bool findAdapter = false;
            foreach (ManagementObject adapter in networkCollection)
            {
                // ----- Find network adapter -----
                string description = adapter["Description"] as string;
                if (string.Compare(description, network, StringComparison.InvariantCultureIgnoreCase) == 0)
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
        public void setWINS(string network, string priWINS, string secWINS)
        {
            // ----- Get network collection -----
            var adapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var networkCollection = adapterConfig.GetInstances();

            foreach (ManagementObject adapter in networkCollection)
            {
                // ----- Find network adapter -----
                string description = adapter["Description"] as string;
                if (string.Compare(description, network, StringComparison.InvariantCultureIgnoreCase) == 0)
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

        public string getIPstring(string network)
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
                        && !adapter.Description.Contains("Linux USB Ethernet")
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
}
