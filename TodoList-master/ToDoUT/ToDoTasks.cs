using DataModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class ToDoTasks
    {
        [Test]
        public void InsertAgileTask()
        {
            List<AgileTodoTaskModel> agileTodoTaskModel = new List<AgileTodoTaskModel>();

            agileTodoTaskModel.Add(new AgileTodoTaskModel
            {
                Id = new Guid("b444b67b-85ad-480a-8dc8-577155587356"),
                ProjectId = new Guid("f67a9154-117b-4fc8-951b-86312a0d11ec"),
                Name = "Demo task2",
                Description = "Demo Description2",
                Efforts = 5,
                StoryPoints = 5,
                BurnedHours = 0,
                Status = 1,
                CreatedDate = DateTime.Now
            });

            Assert.AreEqual(1, agileTodoTaskModel.Count);
        }

        [Test]
        public void InsertNormalTask()
        {
            List<NormalTodoTaskModel> normalTodoTaskModel = new List<NormalTodoTaskModel>();

            normalTodoTaskModel.Add(new NormalTodoTaskModel
            {
                Id = new Guid(),
                ProjectId = new Guid(),
                Name = "Demo task4",
                Description = "Demo Description4",
                Priority = 1,
                EstimatedCompletionDate = DateTime.Now,
                Status = 1,
                CreatedDate = DateTime.Now
            });

            Assert.AreEqual(1, normalTodoTaskModel.Count);
        }

        [Test]
        public void UpdateAgileTask()
        {
            List<AgileTodoTaskModel> agileTodoTaskModel = new List<AgileTodoTaskModel>();

            agileTodoTaskModel.Add(new AgileTodoTaskModel
            {
                Id = new Guid("b444b67b-85ad-480a-8dc8-577155587356"),
                ProjectId = new Guid("f67a9154-117b-4fc8-951b-86312a0d11ec"),
                Name = "Demo task2",
                Description = "Demo Description2",
                Efforts = 5,
                StoryPoints = 5,
                BurnedHours = 0,
                Status = 1,
                CreatedDate = DateTime.Now
            });

            agileTodoTaskModel[0].Description = "Updated";
            agileTodoTaskModel[0].Name = "Updated";
            agileTodoTaskModel[0].Status = (int)DataModels.Enum.TaskStatus.Completed;

            Assert.AreEqual("Updated", agileTodoTaskModel[0].Description);
            Assert.AreEqual("Updated", agileTodoTaskModel[0].Name);
            Assert.AreEqual((int)DataModels.Enum.TaskStatus.Completed, agileTodoTaskModel[0].Status);
        }


        [Test]
        public void UpdateNormalTask()
        {
            List<NormalTodoTaskModel> normalTodoTaskModel = new List<NormalTodoTaskModel>();

            normalTodoTaskModel.Add(new NormalTodoTaskModel
            {
                Id = new Guid(),
                ProjectId = new Guid(),
                Name = "Demo task4",
                Description = "Demo Description4",
                Priority = 1,
                EstimatedCompletionDate = DateTime.Now,
                Status = 1,
                CreatedDate = DateTime.Now
            });

            normalTodoTaskModel[0].Description = "Updated";
            normalTodoTaskModel[0].Name = "Updated";
            normalTodoTaskModel[0].Status = (int)DataModels.Enum.TaskStatus.Completed;

            Assert.AreEqual("Updated", normalTodoTaskModel[0].Description);
            Assert.AreEqual("Updated", normalTodoTaskModel[0].Name);
            Assert.AreEqual((int)DataModels.Enum.TaskStatus.Completed, normalTodoTaskModel[0].Status);
        }

        [Test]
        public void DeleteAgileTask()
        {
            List<AgileTodoTaskModel> agileTodoTaskModel = new List<AgileTodoTaskModel>();

            AgileTodoTaskModel agileTodoTask = new AgileTodoTaskModel
            {
                Id = new Guid("b444b67b-85ad-480a-8dc8-577155587356"),
                ProjectId = new Guid("f67a9154-117b-4fc8-951b-86312a0d11ec"),
                Name = "Demo task2",
                Description = "Demo Description2",
                Efforts = 5,
                StoryPoints = 5,
                BurnedHours = 0,
                Status = 1,
                CreatedDate = DateTime.Now
            };

            agileTodoTaskModel.Add(agileTodoTask);

            agileTodoTaskModel.Remove(agileTodoTask);

            Assert.AreEqual(0, agileTodoTaskModel.Count);
        }

        [Test]
        public void DeleteNormalTask()
        {
            List<NormalTodoTaskModel> normalTodoTaskModel = new List<NormalTodoTaskModel>();

            NormalTodoTaskModel normalTodoTask = new NormalTodoTaskModel
            {
                Id = new Guid(),
                ProjectId = new Guid(),
                Name = "Demo task4",
                Description = "Demo Description4",
                Priority = 1,
                EstimatedCompletionDate = DateTime.Now,
                Status = 1,
                CreatedDate = DateTime.Now
            };

            normalTodoTaskModel.Add(normalTodoTask);

            normalTodoTaskModel.Remove(normalTodoTask);

            Assert.AreEqual(0, normalTodoTaskModel.Count);
        }
    }
}