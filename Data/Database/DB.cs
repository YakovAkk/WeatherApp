using DataBase.Entities;
using SQLite;
using System.Collections.Generic;

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

        public int SaveUser(UserEntity user)
        {
            return _connection.Insert(user);
        }

        public UserEntity GetUserByName(string name)
        {
           return _connection.Table<UserEntity>().FirstOrDefault(x => x.Name == name);
        }
    }
}
