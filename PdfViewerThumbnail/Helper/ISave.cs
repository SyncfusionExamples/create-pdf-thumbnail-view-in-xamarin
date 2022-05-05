using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageExporting
{
    public interface ISave
    {
        Task<string> Save(MemoryStream fileStream, int index);
    }
}
