using System;
using System.Collections.Generic;
using System.Text;

namespace THOEP.DAL.Interfaces
{
    public interface IDBManager
    {
        void BackUpDB();
        void RestoreDB();
    }
}
