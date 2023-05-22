using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetworkManager;

public class IpSetting
{
    public string Name { get; set; } = "";
    public string Interface { get; set; } = "";
    public bool SetIP { get; set; } = false;
    public bool IsDHCP { get; set; } = true;
    public string IP { get; set; } = "192.168.1.10";
    public string NetMask { get; set; } = "255.255.255.0";
    public string Gateway { get; set; } = "192.168.1.1";
    public bool SetDNS { get; set; } = false;
    public bool IsAutoDNS { get; set; } = true;
    public string DNS1 { get; set; } = "";
    public string DNS2 { get; set; } = "";

    public IpSetting()
    {
    }

    public IpSetting(string Name, string Interface,
        bool SetIP, bool IsDHCP, string IP, string NetMask, string Gateway,
        bool SetDNS, bool IsAutoDNS, string DNS1, string DNS2)
    {
        this.Name = Name;
        this.Interface = Interface;
        this.SetIP = SetIP;
        this.IsDHCP = IsDHCP;
        this.IP = IP;
        this.NetMask = NetMask;
        this.Gateway = Gateway;
        this.SetDNS = SetDNS;
        this.IsAutoDNS = IsAutoDNS;
        this.DNS1 = DNS1;
        this.DNS2 = DNS2;
    }

    public IpSetting(XElement? xml)
    {
        Import(xml);
    }

    public void Import(XElement? xml)
    {
        XAttribute? attribute;
        XElement? element;

        if (xml == null) return;

        attribute = xml.Attribute("name");
        if (attribute != null)
        {
            Name = attribute.Value;
        }

        attribute = xml.Attribute("interface");
        if (attribute != null)
        {
            Interface = attribute.Value;
        }

        element = xml.Element("ip");
        if (element != null)
        {
            attribute = element.Attribute("set");
            if (attribute != null)
            {
                if (attribute.Value.ToLower() == "true") SetIP = true;
                else SetIP = false;
            }

            attribute = element.Attribute("type");
            if (attribute != null)
            {
                if (attribute.Value.ToLower() == "dhcp") IsDHCP = true;
                else IsDHCP = false;
            }

            attribute = element.Attribute("ip");
            if (attribute != null)
            {
                IP = attribute.Value;
            }

            attribute = element.Attribute("mask");
            if (attribute != null)
            {
                NetMask = attribute.Value;
            }

            attribute = element.Attribute("gateway");
            if (attribute != null)
            {
                Gateway = attribute.Value;
            }
        }

        element = xml.Element("dns");
        if (element != null)
        {
            attribute = element.Attribute("set");
            if (attribute != null)
            {
                if (attribute.Value.ToLower() == "true") SetDNS = true;
                else SetDNS = false;
            }

            attribute = element.Attribute("type");
            if (attribute != null)
            {
                if (attribute.Value.ToLower() == "auto") IsAutoDNS = true;
                else IsAutoDNS = false;
            }

            attribute = element.Attribute("dns1");
            if (attribute != null)
            {
                DNS1 = attribute.Value;
            }

            attribute = element.Attribute("dns2");
            if (attribute != null)
            {
                DNS2 = attribute.Value;
            }
        }
    }

    public XElement GetXmlElement()
    {
        // ----- Write settings -----
        var xmlElement = new XElement("network");
        xmlElement.Add(new XAttribute("name", Name));
        xmlElement.Add(new XAttribute("interface", Interface));

        var xmlIp = new XElement("ip");
        xmlIp.Add(new XAttribute("set", SetIP.ToString()));
        if (IsDHCP)
            xmlIp.Add(new XAttribute("type", "dhcp"));
        else
            xmlIp.Add(new XAttribute("type", "static"));
        xmlIp.Add(new XAttribute("ip", IP));
        xmlIp.Add(new XAttribute("mask", NetMask));
        xmlIp.Add(new XAttribute("gateway", Gateway));
        xmlElement.Add(xmlIp);

        var xmlDns = new XElement("dns");
        xmlDns.Add(new XAttribute("set", SetDNS.ToString()));
        if (IsAutoDNS)
            xmlDns.Add(new XAttribute("type", "auto"));
        else
            xmlDns.Add(new XAttribute("type", "static"));
        xmlDns.Add(new XAttribute("dns1", DNS1));
        xmlDns.Add(new XAttribute("dns2", DNS2));
        xmlElement.Add(xmlDns);

        return xmlElement;

    }

    public IpSetting Copy()
    {
        IpSetting copy = (IpSetting)this.MemberwiseClone();
        /*copy.Name = String.Copy(Name);
        copy.Interface = String.Copy(Interface);
        copy.IP = String.Copy(IP);
        copy.NetMask = String.Copy(NetMask);
        copy.Gateway = String.Copy(Gateway);
        copy.DNS1 = String.Copy(DNS1);
        copy.DNS2 = String.Copy(DNS2);*/

        return copy;
    }
}