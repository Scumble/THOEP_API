using System;
using System.Collections.Generic;
using System.Text;

namespace THOEP.Services.Interfaces
{
    public interface IDBManagerService
    {
        void BackUpDB();
        void RestoreDB();
    }
}
