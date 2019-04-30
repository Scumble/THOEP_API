using System;
using System.Collections.Generic;
using System.Text;
using THOEP.DAL.Interfaces;
using THOEP.Services.Interfaces;

namespace THOEP.Services.Services
{
    public class DBManagerService: IDBManagerService
    {
        private readonly IDBManager _dbManager;
        public DBManagerService(IDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        public void BackUpDB()
        {
            _dbManager.BackUpDB();
        }

        public void RestoreDB()
        {
            _dbManager.RestoreDB();
        }
    }
}
