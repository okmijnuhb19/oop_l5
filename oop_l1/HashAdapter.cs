using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using oop_l1;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.ComponentModel.Composition;

namespace oop_l1
{
    public static class HashAdapter
    {
        public static string name;
        static IHashPlugin plug;

        public static IHashPlugin Addplugin() 
        {
            string dllPath = @"C:\Users\Andrew\Desktop\3456\OOP_laba3\HashCounter\bin\Release\HashCounter.dll";
            var pluginAssembly = Assembly.LoadFrom(dllPath);
            foreach (Type type in pluginAssembly.GetExportedTypes())
            {
                var plugin = (IHashPlugin)type.GetConstructor(Type.EmptyTypes).Invoke(new Object[0]);
                plug = plugin;
            }
            return plug;
        }

        public static bool GetHash(byte[] arr)
        {
            string hash = "";
                try
                {
                    hash = File.ReadAllText("hash.txt");
                }
                catch (FileNotFoundException) { }
                return plug.CompareHash(arr, hash);
        }
        public static void PutHash(byte [] arr)
        {
            File.WriteAllText("hash.txt",  plug.CreateHash(arr));
        }
        
    }
}
