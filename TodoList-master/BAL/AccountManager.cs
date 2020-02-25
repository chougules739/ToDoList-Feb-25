using BAL.Settings;
using DataModels;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL
{
    public class AccountManager
    {
        /// <summary>  
        /// To save the user
        /// </summary>  
        /// <param name="user">to insert the user inside redis</param>  
        /// <returns></returns>
        public void SaveUser(User user)
        {
            using (var objRedisClient = new RedisClient(AppSettings.RedisServer))
            {
                objRedisClient.As<User>().StoreAll(new List<User>() { user });
            }
        }

        /// <summary>  
        /// To get the user by id
        /// </summary>  
        /// <param name="userId">to get the specific user</param>  
        /// <returns>User object for the requested userId</returns>
        public User GetUserById(Guid userId)
        {
            using (var redisClient = new RedisClient(AppSettings.RedisServer))
            {
                return redisClient.As<User>().GetById(userId);
            }
        }

        /// <summary>  
        /// To validates the user with username and password
        /// </summary>  
        /// <param name="userName">user's username</param>  
        /// <param name="userId">user's password</param>  
        /// <returns>User object for the requested userId</returns>
        public User ValidateUser(string userName, string password)
        {
            using (var redisClient = new RedisClient(AppSettings.RedisServer))
            {
                return redisClient.As<User>().GetAll().ToList().
                    Where(x => x.UserName == userName && x.Password == password).SingleOrDefault();
            }
        }

        /// <summary>  
        /// To check is user already exists
        /// </summary>  
        /// <param name="userName">user's username</param>
        /// <returns>user existance (either yes or no)</returns>
        public bool IsUserExists(string userName)
        {
            using (var redisClient = new RedisClient(AppSettings.RedisServer))
            {
                return redisClient.As<User>().GetAll().ToList().
                    Any<User>(x => x.UserName == userName);
            }
        }
    }
}
