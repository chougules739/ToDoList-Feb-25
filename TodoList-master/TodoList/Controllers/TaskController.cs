using BAL;
using BAL.Settings;
using DataModels;
using DataModels.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TodoList.Common;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class TaskController : Controller
    {
        [AuthorizeUser]
        public ActionResult GetAll(int? page)
        {
            ProjectManager projectManager = new ProjectManager();

            //To populate hard caded project data
            projectManager.SaveProject();

            ToDoTaskComplexType toDoTaskComplexType = new ToDoTaskComplexType();

            toDoTaskComplexType.Project =
                projectManager.GetProjects().Where(x => x.Type == (int)ProjectType.Normal).FirstOrDefault();

            ToDoTask toDoList = new ToDoTask();

            if (toDoTaskComplexType.Project.Type == (int)ProjectType.Agile)
                toDoList.SetToDoTaskStrategy(new AgileTask());
            else if (toDoTaskComplexType.Project.Type == (int)ProjectType.Normal)
                toDoList.SetToDoTaskStrategy(new NormalTask());

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);

            toDoTaskComplexType.TodoTasks =
                toDoList.GetAll(toDoTaskComplexType.Project.Id).
                    Where(x => x.UserId == Guid.Parse(HttpContext.User.Identity.Name.Split('$')[1])).
                    OrderByDescending(x => x.UpdatedDate == DateTime.MinValue ? x.CreatedDate : x.UpdatedDate).ToList();

            toDoTaskComplexType.TodoTaskCount = toDoTaskComplexType.TodoTasks.Count;

            toDoTaskComplexType.TodoTasks = toDoTaskComplexType.TodoTasks.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            toDoTaskComplexType.TaskStatus =
                from TaskStatus status in Enum.GetValues(typeof(TaskStatus))
                select new SelectListItem { Value = Convert.ToInt32(status).ToString(), Text = status.ToString() };

            toDoTaskComplexType.TaskPriorities =
                from TaskPriority priority in Enum.GetValues(typeof(TaskPriority))
                select new SelectListItem { Value = Convert.ToInt32(priority).ToString(), Text = priority.ToString() };

            return View(toDoTaskComplexType);
        }

        public JsonResult PopulateTask(string projectType, string prjectId, string taskId)
        {
            ToDoTask toDoTask = new ToDoTask();

            if (Convert.ToInt32(projectType) == (int)ProjectType.Agile)
                toDoTask.SetToDoTaskStrategy(new AgileTask());
            else if (Convert.ToInt32(projectType) == (int)ProjectType.Normal)
                toDoTask.SetToDoTaskStrategy(new NormalTask());

            TodoTaskModel todoTaskModel = toDoTask.GetById(new Guid(taskId));

            return Json(todoTaskModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeUser]
        public JsonResult InsertNormalTask(NormalTodoTaskModel todoTaskModel)
        {
            todoTaskModel.Id = Guid.NewGuid();
            todoTaskModel.IsActive = true;
            todoTaskModel.CreatedDate = DateTime.Now;

            todoTaskModel.CreatedBy = Guid.Parse(HttpContext.User.Identity.Name.Split('$')[1]);
            todoTaskModel.UserId = Guid.Parse(HttpContext.User.Identity.Name.Split('$')[1]);
            todoTaskModel.ProjectId = AppSettings.NormalProjectId;

            ToDoTask toDoTask = new ToDoTask();

            toDoTask.SetToDoTaskStrategy(new NormalTask());

            TodoTaskModel normalTodoTaskModel = toDoTask.Save(todoTaskModel);

            return Json(normalTodoTaskModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeUser]
        public JsonResult UpdateNormalTask(NormalTodoTaskModel todoTaskModel)
        {
            todoTaskModel.IsActive = true;
            todoTaskModel.UpdatedDate = DateTime.Now;

            string[] dateParts = todoTaskModel.CreatedDateString.Split('/');

            if (dateParts.Length > 0)
                todoTaskModel.CreatedDate = Convert.ToDateTime(dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2]);

            todoTaskModel.UpdatedBy = Guid.Parse(HttpContext.User.Identity.Name.Split('$')[1]);
            todoTaskModel.UserId = Guid.Parse(HttpContext.User.Identity.Name.Split('$')[1]);

            ToDoTask toDoTask = new ToDoTask();

            toDoTask.SetToDoTaskStrategy(new NormalTask());

            TodoTaskModel normalTodoTaskModel = toDoTask.Save(todoTaskModel);

            return Json(normalTodoTaskModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeUser]
        public HttpStatusCodeResult DeleteNormalTask(string taskId)
        {
            ToDoTask toDoTask = new ToDoTask();

            toDoTask.SetToDoTaskStrategy(new NormalTask());

            TodoTaskModel normalTodoTaskModel = toDoTask.DeleteById(new Guid(taskId));

            return normalTodoTaskModel == null ? new HttpStatusCodeResult(HttpStatusCode.OK, "Record deleted") :
                new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
        }

        [HttpPost]
        [AuthorizeUser]
        public HttpStatusCodeResult DeleteTasks(List<Guid> taskIds)
        {
            ToDoTask toDoTask = new ToDoTask();

            toDoTask.SetToDoTaskStrategy(new NormalTask());

            IEnumerable<TodoTaskModel> normalTodoTaskModel = toDoTask.DeleteByIds(taskIds);

            return normalTodoTaskModel.Count() == 0 ? new HttpStatusCodeResult(HttpStatusCode.OK, "Record deleted") :
                new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}