using System.Collections.Generic;
using SQLite;
namespace AssignmentApp.Helpers
{
    public static class Db
    {
        private static string dbPath = string.Empty;

        private static SQLiteConnection GetConnection<T>()
        {
            SQLiteConnection connection = new SQLiteConnection(dbPath);
            connection.CreateTable<T>();
            return connection;
        }

        public static void SetDBPath(string path)
        {
            dbPath = path;
        }

        public static bool Insert<T>(T entity)
        {
            int insertResult;
            using (var connecton = GetConnection<T>())
            {
                insertResult = connecton.Insert(entity);
            }

            return insertResult > 0;
        }

        public static bool Update<T>(T entity)
        {
            int updateResult = 0;
            using (var connection = GetConnection<T>())
            {
                updateResult = connection.Update(entity);
            }
            
            return updateResult > 0;
        }

        public static IEnumerable<T> GetAll<T>() where T: new()
        {
            using(var connection = GetConnection<T>())
            {
                return connection.Table<T>().ToList();
            }
        }

        public static T GetById<T>(long Id) where T : new()
        {
            using (var connection = GetConnection<T>())
            {
                return connection.Find<T>(Id);
            }
        }
    }
}

