using DataBase.Entities;
using SQLite;

namespace DataBase.Database
{
    public class DB
    {
        private readonly SQLiteConnection _connection;
        public DB(string path)
        {
            _connection = new SQLiteConnection(path);
            _connection.CreateTable<UserEntity>();
        }

        public List<UserEntity> GetAllUsers()
        {
            return _connection.Table<UserEntity>().ToList();
        }

        public int SaveUser(UserEntity user)
        {
            return _connection.Insert(user);
        }
    }
}
