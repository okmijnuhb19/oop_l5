using System;
using oop_l1;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO.Compression;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ArchivingPlugin
{
    public class ArchivingPlugin : IArchivingPlugin
    {
        public void Save(IForm _view)
        {
            string path = @"F:\Учеба\repository\Pictures\" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_"
                + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".bin";
            using (Stream s = new FileStream(path, FileMode.Create))
            using (Stream ds = new DeflateStream(s, CompressionMode.Compress))
            {
                byte[] buff = Serialize(_view.IMG).ToArray();
                ds.Write(buff, 0, buff.Length);
                _view.IMG = new Bitmap(830, 414);
                _view.PBox.Image = new Bitmap(830, 414);
            }
        }
        public MemoryStream Serialize(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms;

        }

        public void Load(IForm _view)
        {
#region a
            /*BinaryFormatter formatter = new BinaryFormatter();
            _view.openfiledialod.Filter =
                "(*.bin)|*.bin";

            _view.openfiledialod.Title = "bin Browser";
            _view.openfiledialod.InitialDirectory = @"F:\Учеба\repository\Pictures\";
            DialogResult dr = _view.openfiledialod.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                using (FileStream fs = new FileStream(_view.openfiledialod.FileName, FileMode.OpenOrCreate))
                {
                    using (FileStream cfs = new FileStream("temp.cmp", FileMode.OpenOrCreate))
                    {
                        using (DeflateStream decompressionStream = new DeflateStream(cfs, CompressionMode.Decompress))
                        {
                            Image i = (Image)formatter.Deserialize(fs);
                            _view.PBox.Image = _view.IMG;
                            MessageBox.Show("Изображение распаковано и десериализовано!", "Сообщение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            } */
#endregion
            _view.openfiledialod.Filter =
                "(*.bin)|*.bin";

            _view.openfiledialod.Title = "bin Browser";
            _view.openfiledialod.InitialDirectory = @"F:\Учеба\repository\Pictures\";
            DialogResult dr = _view.openfiledialod.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream s = File.OpenRead(_view.openfiledialod.FileName))
                using (Stream ds = new DeflateStream(s, CompressionMode.Decompress))
                using (Stream sd = new MemoryStream())
                {
                    try
                    {
                        ds.CopyTo(sd);
                        sd.Position = 0;
                        _view.IMG = Deserialize(sd);
                        _view.PBox.Image = _view.IMG;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("You must delete old elements");
                    }
                }
            }
        }
        public Image Deserialize(Stream stream)
        {
            Image instances = new Bitmap(830,414);
            BinaryFormatter bf = new BinaryFormatter();
                                       
            while (stream.Position < stream.Length)
            {
                try
                {
                    instances=(Image)bf.Deserialize(stream);

                }
                catch (Exception e)
                {
                    MessageBox.Show("object can't be loaded without required plugin");
                    return instances;
                }
            }

            return instances;
        }
        
    }
}
