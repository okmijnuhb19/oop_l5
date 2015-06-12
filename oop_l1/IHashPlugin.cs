using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_l1
{
    public interface IHashPlugin
    {
        string CreateHash(byte[] data);
        bool CompareHash(byte[] data, string hash);
    }
}
