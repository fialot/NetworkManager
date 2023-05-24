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

namespace NetworkManager
{
    

    class IPwork
    {
        List<IpSetting> IPlist;
        string path;

        public IPwork(string Path)
        {
            path = Path;
            IPlist = LoadIPlist(path);

        }

        public void Save()
        {
            saveIPlist(path, IPlist);
        }

        public  List<IpSetting> GetList()
        {
            return IPlist;
        }

        public IpSetting GetProfile(int index)
        {
            if (index < IPlist.Count && index >= 0)
            {
                return IPlist[index];
            }

            return new IpSetting();
        }

        public IpSetting GetProfile(string name)
        {
            return GetProfile(GetProfileIndex(name));
        }

        public int GetProfileIndex(string name)
        {
            int result = -1;

            for (int i = 0; i < IPlist.Count; i++)
            {
                if (IPlist[i].Name == name)
                {
                    return i;
                }
            }

            return result;
        }

        public List<string> GetProfileNames()
        {
            List<string> list = new List<string>();

            foreach (var item in IPlist)
            {
                list.Add(item.Name);

            }
            return list;
        }

        public void EditList(int index, IpSetting item)
        {
            IPlist = editList(IPlist, index, item);
        }

        public void AddList(IpSetting item)
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
            IpSetting item = IPlist[index];
            IPv4.Set(item);
        }


        private List<IpSetting> editList(List<IpSetting> list, int index, IpSetting item)
        {
            list.RemoveAt(index);
            list.Insert(index, item);
            return list;
        }

        private List<IpSetting> addToList(List<IpSetting> list, IpSetting item)
        {
            list.Add(item);
            return list;
        }

        private List<IpSetting> delFromList(List<IpSetting> list, int index)
        {
            list.RemoveAt(index);
            return list;
        }

        public List<IpSetting> LoadIPlist()
        {
            IPlist = LoadIPlist(path);
            return IPlist;
        }

        public List<IpSetting> LoadIPlist(string path)
        {
            List<IpSetting> list = new List<IpSetting>();

            if (!System.IO.File.Exists(path))
            {
                string fileName = System.IO.Path.GetFileName(path);
                string newPath = Files.ReplaceVarPaths("%AppData%" + System.IO.Path.DirectorySeparatorChar + "IPChanger") + System.IO.Path.DirectorySeparatorChar + fileName;
                if (System.IO.File.Exists(newPath))
                {
                    path = newPath;
                }
            }

            list = (List<IpSetting>)Files.ImportXml(path, list);
            if (list == null) list = new List<IpSetting>();

            return list;
        }

        public void saveIPlist(string path, List<IpSetting> list)
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

        

        
        

        
    }
}
