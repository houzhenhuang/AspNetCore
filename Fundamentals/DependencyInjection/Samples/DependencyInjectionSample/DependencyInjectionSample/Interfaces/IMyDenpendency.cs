﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Interfaces
{
    public interface IMyDenpendency
    {
        Task WriteMessage(string message);
    }
}
