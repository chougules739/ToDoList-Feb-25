using DataModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ToDoUT
{
    public class Account
    {
        [Test]
        public void InsertUser()
        {
            List<User> user = new List<User>();

            user.Add(new User
            {
                Id = new Guid(),
                Name = "Demo user",
                EmailId = "Demo@demo.com",
                ContactNo = "999999999",
                RoleId = 1,
                Password = "Test@123",
                UserName = "Demo@demo.com",
                IsActive = true,
                CreatedDate = DateTime.Now
            });

            Assert.AreEqual(1, user.Count);
        }

        [Test]
        public void UpdateAgileTask()
        {
            List<User> user = new List<User>();

            user.Add(new User
            {
                Id = new Guid(),
                Name = "Demo user",
                EmailId = "Demo@demo.com",
                ContactNo = "999999999",
                RoleId = 1,
                Password = "Test@123",
                UserName = "Demo@demo.com",
                IsActive = true,
                CreatedDate = DateTime.Now
            });

            user[0].Name = "Updated";
            user[0].UserName = "Updated";
            user[0].Password = "Updated";

            Assert.AreEqual("Updated", user[0].Name);
            Assert.AreEqual("Updated", user[0].UserName);
            Assert.AreEqual("Updated", user[0].Password);
        }

        [Test]
        public void DeleteUser()
        {
            List<User> users = new List<User>();
            
            User user = new User() 
            {
                Id = new Guid(),
                Name = "Demo user",
                EmailId = "Demo@demo.com",
                ContactNo = "999999999",
                RoleId = 1,
                Password = "Test@123",
                UserName = "Demo@demo.com",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            users.Add(user);

            users.Remove(user);

            Assert.AreEqual(0, users.Count);
        }
    }
}
