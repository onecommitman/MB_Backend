using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ModulSchool.Models;
using ModulSchool.Services.Interfaces;
using Npgsql;
using Microsoft.AspNetCore.Mvc;

namespace ModulSchool.Services
{
    public class UserInfoService : IUserInfoService
    {
        private const string ConnectionString = "host=localhost;port=5432;database=postgres;username=postgres;password=pass1234";

        public async Task<User> GetById(Guid id)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return await connection.QuerySingleAsync<User>("SELECT * FROM users WHERE Id = @id", new { id });
            }
        }

        public async Task<IActionResult> AppendUserPost(User user)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                string query = "INSERT INTO users (id, email, nickname, phone) VALUES (@id, @email, @nickname, @phone)";

                await connection.ExecuteAsync(query, user);
            }
            return new OkResult();
        }
        //public async Task<User> GetById(Guid id)
        //{
        //    User user = new User
        //    {
        //        Email = "test@test.ru",
        //        Id = id,
        //        Nickname = "test",
        //        Phone = "+7 987 654 32 10"
        //    };

        //    return await Task.FromResult<User>(user);
        //}
    }
    
}
