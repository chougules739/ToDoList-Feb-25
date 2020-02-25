using ServiceStack.Redis;
using System.Collections.Generic;
using BAL.Settings;
using DataModels;

namespace BAL
{
    public class ProjectManager
    {
        /// <summary>  
        /// To get value from Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="key">Key as string</param>  
        /// <returns></returns>  
        public IEnumerable<Project> GetProjects()
        {
            using (var objRedisClient = new RedisClient(AppSettings.RedisServer))
            {
                return objRedisClient.As<Project>().GetAll();
            }
        }

        /// <summary>  
        /// To Save Key Value Pair in Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="key">Key as string</param>  
        /// <param name="value">Value as string</param>  
        /// <returns></returns>  
        public void SaveProject()
        {
            using (var objRedisClient = new RedisClient(AppSettings.RedisServer))
            {
                if (objRedisClient.As<Project>().GetById(AppSettings.NormalProjectId) == null)
                {
                    objRedisClient.As<Project>().StoreAll(
                        new List<Project>()
                        {
                            //new Project { Id = AppSettings.AgileProjectId, Type = 1, Name = "Proj 1", IsActive = true },
                            new Project { Id = AppSettings.NormalProjectId, Type = 2, Name = "Proj 2", IsActive = true }
                        }
                    );
                }
            }
        }

        /// <summary>  
        /// To Save Key Value Pair in Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="key">Key as string</param>  
        /// <param name="value">Value as string</param>  
        /// <returns></returns>  
        public void DeleteProjects()
        {
            using (var objRedisClient = new RedisClient(AppSettings.RedisServer))
            {
                objRedisClient.As<Project>().DeleteAll();
            }
        }
    }
}
