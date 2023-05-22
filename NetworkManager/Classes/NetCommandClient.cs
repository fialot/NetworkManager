using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OneOf;
using Windows.UI.StartScreen;
using static System.Net.Mime.MediaTypeNames;

namespace NetworkManager;
internal class NetCommandClient
{
    readonly string commandDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + Path.DirectorySeparatorChar + "NetManager" + Path.DirectorySeparatorChar;


    public OneOf<string, Exception> WriteCommand(IpSetting setting)
    {
        var file = commandDir + "command.xml";

        try {
            // ----- Create work folder -----
            if (!Directory.Exists(commandDir)) Directory.CreateDirectory(commandDir);

            // ----- Parse XML to Structure -----
            var xml = new XDocument(new XDeclaration("1.0", "utf-8", null));
            var mainElement = new XElement("settings");
            xml.Add(mainElement);
            mainElement.Add(setting.GetXmlElement());
            xml.Save(file);
        } 
        catch 
        {
            return new Exception("Writing to file error!");
        }

        return WaitForReply(10000);
    }

    public OneOf<string, Exception> WaitForReply(int timeout)
    {
        var file = commandDir + "reply.xml";

        Stopwatch timer = new();
        timer.Start();

        
        while (timer.ElapsedMilliseconds < timeout)
        {
            System.Threading.Thread.Sleep(500);

            if (File.Exists(file))
            {
                try
                {
                    var text = File.ReadAllText(file);
                    File.Delete(file);

                    // ----- Parse XML to Structure -----
                    var xml = XDocument.Parse(text);
                    var reply = xml.Element("reply");
                    if (reply != null)
                    {
                        string result = "", message = "";
                        var resultElement = reply.Element("result");
                        if (resultElement != null)
                        {
                            result = resultElement.Value;
                        }
                        var messageElement = reply.Element("message");
                        if (messageElement != null)
                        {
                            message = messageElement.Value;
                        }
                        if (result.ToLower() == "ok")
                            return message;
                        else
                            return new Exception(message);
                    }
                }
                catch { }
            }

        }

        return new Exception("Command timeout");
    }
}
