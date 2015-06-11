using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace oop_l1
{
    public interface ISerializer<T>
    {
        MemoryStream Serialize(List<T> items);

        List<T> Deserialize(string path);
    }
}
