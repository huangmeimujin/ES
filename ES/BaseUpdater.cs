using System;
using System.Collections.Generic;
using System.Text;

namespace ES
{
    public abstract class BaseUpdater<T> : BaseWriter<T> where T : class
    {
    }
}
