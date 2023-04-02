using System.IO;
using System;
using DataBase.Database;
using DataBase.Entities;
using Services.Services.Base;
using System.Runtime.CompilerServices;
using Services.Services;

[assembly:Dependency(typeof(UserService))]
namespace Services.Services
{
    public class UserService : IUserService
    {
       
    }
}
