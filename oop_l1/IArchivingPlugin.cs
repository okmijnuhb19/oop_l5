﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_l1
{
    public interface IArchivingPlugin
    {
        void Save(IForm _view);
        void Load(IForm _view);
    }
}
