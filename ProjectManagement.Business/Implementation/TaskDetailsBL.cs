using ProjecManagement.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using ProjecManagement.BusinessLayer.Interface;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.EntityLayer;

namespace ProjecManagement.BusinessLayer.Implementation
{


    public class TaskDetailsBL : ITaskDetailsBL
    {
        readonly IRepository<ProjectTaskDetails> _taskRepository;
        readonly IParentTaskDetailsBL _parentTaskBusiness;
        readonly IProjectDetailsBL _projectBusiness;
        readonly IRepository<UserDetails> _userRepository;

        public TaskDetailsBL(IRepository<ProjectTaskDetails> taskRepository,
            IParentTaskDetailsBL parentTaskBusiness,
            IProjectDetailsBL projectBusiness,
            IRepository<UserDetails> userRepository)
        {
            _taskRepository = taskRepository;
            _parentTaskBusiness = parentTaskBusiness;
            _projectBusiness = projectBusiness;
            _userRepository = userRepository;
        }

        public IEnumerable<TaskViewModel> GetDetailsByProjectId(int id)
        {
            throw new NotImplementedException();
        }

        public TaskViewModel GetDetailsById(int id)
        {
            var allTasks = GetAllTaskDetails();
            return allTasks.FirstOrDefault(t => t.TaskId == id);
        }
        public void SaveTaskDetils(TaskViewModel model)
        {
            ParentTaskViewModel parentTaskViewModel;
            
            // Parent task
            if (model.IsParentTask)
            {
                parentTaskViewModel = SaveParentTask(model);
            }
            else
            {
                var entity = _taskRepository.GetRecordById(model.TaskId);
                if (entity == null)
                {
                    entity = ToEntity(model);
                    _taskRepository.InsertRecord(entity);
                }
                else
                {
                    entity.ParentTaskId = model.ParentTaskId;
                    entity.ProjectId = model.ProjectId;
                    entity.Title = model.TaskName;
                    entity.StartDate = model.StartDate;
                    entity.EndDate = model.EndDate;
                    entity.Priority = model.Priority;                      
                    _taskRepository.UpdateRecord(entity);
                }

                var userEntity = _userRepository.GetRecordById(model.ManagerId);
                if (userEntity != null)
                {
                    userEntity.TaskId = entity.TaskId;
                    _userRepository.UpdateRecord(userEntity);
                }
            }
        }

        public IEnumerable<TaskViewModel> GetAllTaskDetails()
        {
            var parentTasks = _parentTaskBusiness
                .GetAllParentTaskDetail()
                .ToList();

            var projects = _projectBusiness.GetAllProjectDetail();
            var tasks = _taskRepository.GetAllRecord();
            var users = _userRepository.GetAllRecord();

            var models = new List<TaskViewModel>();
            foreach (var task in tasks)
            {
                var parentTaskName = string.Empty;
                var status = "No";
                var projectName = string.Empty;
                var managerName = string.Empty;
                var managerId = 0;

                var pt = parentTasks.FirstOrDefault(p => p.ParentTaskId == task.ParentTaskId);
                if (pt != null)
                    parentTaskName = pt.ParentTaskName;

                var project = projects.FirstOrDefault(p => p.ProjectId == task.ProjectId);
                if (project != null)
                {
                    projectName = project.ProjectName;
                }

                var user = users.FirstOrDefault(p => p.TaskId == task.TaskId);
                if (user != null)
                {
                    managerName = string.Format("{0} {1}", user.FirstName, user.LastName);
                    managerId = user.UserId;
                }

                models.Add(new TaskViewModel
                {
                    TaskId = task.TaskId,
                    TaskName = task.Title,
                    ParentTaskName = parentTaskName,
                    ParentTaskId = task.ParentTaskId,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    ProjectId = task.ProjectId,
                    ProjectName = projectName,
                    Priority = task.Priority,
                    ManagerId = managerId,
                    ManagerName = managerName,
                    Status = string.IsNullOrEmpty(task.Status) ? status : task.Status
                });
            }

            return models;
        }

        public IEnumerable<ParentTaskViewModel> GetAllParentTaskDetails()
        {
            return _parentTaskBusiness.GetAllParentTaskDetail();
        }

        public void Complete(TaskViewModel model)
        {
            var task = _taskRepository.GetRecordById(model.TaskId);
            if (task == null) return;

            task.Status = "Yes";
            task.EndDate = DateTime.Now;
            _taskRepository.UpdateRecord(task);
        }
        private ParentTaskViewModel SaveParentTask(TaskViewModel model)
        {
            var parentTaskModel = new ParentTaskViewModel
            {
                ParentTaskId = model.TaskId,
                ParentTaskName = model.TaskName
            };

            var parentViewModel = _parentTaskBusiness.SaveParentTaskDetail(parentTaskModel);
            return parentViewModel;
        }

        private TaskViewModel ToModel(ProjectTaskDetails entity)
        {
            return new TaskViewModel
            {
                TaskId = entity.TaskId,
                ParentTaskId = entity.ParentTaskId,
                ProjectId = entity.ProjectId,
                TaskName = entity.Title,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status,
                Priority = entity.Priority
            };
        }

        private ProjectTaskDetails ToEntity(TaskViewModel model)
        {
            return new ProjectTaskDetails
            {
                TaskId = model.TaskId,
                ParentTaskId = model.ParentTaskId,
                ProjectId = model.ProjectId,
                Title = model.TaskName,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Priority = model.Priority
            };
        }
    }
}
