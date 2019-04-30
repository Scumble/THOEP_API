using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using THOEP.DAL.DbContext;
using THOEP.DAL.Interfaces;

namespace THOEP.DAL.Repositories
{
    public class DBManager: IDBManager
    {
        private readonly Context _context;
        public DBManager(Context context)
        {
            _context = context;
        }

        public void BackUpDB()
        {

            var connectionString = _context.Database.GetDbConnection().ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                "C:\\Users\\lucky\\source\\repos\\THOEP_API\\", sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd"));
     
            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RestoreDB()
        {
            var connectionString = _context.Database.GetDbConnection().ConnectionString;
            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                "C:\\Users\\lucky\\source\\repos\\THOEP_API\\", sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd"));

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                var query = String.Format(" use [master] ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE {0} FROM DISK='{1}' ALTER DATABASE {0} SET MULTI_USER",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
