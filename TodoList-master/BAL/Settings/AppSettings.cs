using System;
using System.Configuration;

namespace BAL.Settings
{
    public static class AppSettings
    {
        public static string RedisServer
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisServer"].ToString();
            }
        }

        public static Guid NormalProjectId
        {
            get
            {
                return Guid.Parse(ConfigurationManager.AppSettings["NormalProjectId"]);
            }
        }

        public static Guid AgileProjectId
        {
            get
            {
                return Guid.Parse(ConfigurationManager.AppSettings["AgileProjectId"]);
            }
        }

        public static string ToDoUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ToDoUrl"].ToString();
            }
        }
    }
}
