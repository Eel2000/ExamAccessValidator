using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LiteDB.Async;

namespace ExamAccessValidator.Service
{
    public static class LiteDB
    {
        private static LiteDatabaseAsync _database;

        /// <summary>
        /// Initialize a new service of the liteDbAsync database service otherwise it return the existing service.
        /// </summary>
        /// <returns><see cref="LiteDatabaseAsync"/> The LiteDbAsync service(engine).</returns>
        public static LiteDatabaseAsync Database
        {
            get
            {
                if (_database is null)
                {
                    var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ExamValidator-data.db");
                    _database = new LiteDatabaseAsync($"Filename={path};Connection=shared;Password=Exam2021");
                }
                return _database;
            }
        }

        /// <summary>
        /// Remove dabase and dispose resources allocated by it.
        /// </summary>
        public static void Dispose()
        {
            if (_database != null)
            {
                _database.Dispose();
                if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ExamValidator-data.db")))
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ExamValidator-data.db"));
                else
                    throw new ApplicationException("Database not found");
            }
        }
    }
}
