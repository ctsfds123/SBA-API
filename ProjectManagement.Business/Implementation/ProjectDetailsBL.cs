using ProjecManagement.BusinessLayer.Interface;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.EntityLayer;
using ProjecManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjecManagement.BusinessLayer.Implementation
{

    public class ProjectDetailsBL : IProjectDetailsBL
    {
        readonly IRepository<ProjectDetails> _projectRepository;
        readonly IRepository<UserDetails> _userRepository;
        readonly IRepository<ProjectTaskDetails> _taskRepository;

        public ProjectDetailsBL(IRepository<ProjectDetails> projectRepository,
            IRepository<UserDetails> userRepository,
            IRepository<ProjectTaskDetails> taskRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public void DeleteProjectDetail(int id)
        {
            _projectRepository.DeleteRecord(id);
        }

        public IEnumerable<ProjectViewModel> GetAllProjectDetail()
        {
            var projects = new List<ProjectViewModel>();
            var entities = _projectRepository.GetAllRecord();

            entities.ToList().ForEach(u => projects.Add(ToProjectViewModel(u)));

            return projects;
        }

        public void SaveProjectDetails(ProjectViewModel model)
        {
            var entity = _projectRepository.GetRecordById(model.ProjectId);
            if (entity != null)
            {
                entity.Title = model.ProjectName;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.Priority = model.Priority;
                _projectRepository.UpdateRecord(entity);
            }
            else
            {
                entity = ToProjectEntity(model);
                _projectRepository.InsertRecord(entity);
            }

            model.ProjectId = entity.ProjectId;
            UpdateUser(model);
        }

        private void UpdateUser(ProjectViewModel model)
        {
            if (model.ProjectId == 0) return;

            var user = _userRepository.GetRecordById(model.UserId);
            if (user != null)
            {
                user.ProjectId = model.ProjectId;
                _userRepository.UpdateRecord(user);
            }
        }

        private ProjectViewModel ToProjectViewModel(ProjectDetails project)
        {
            var tasks = _taskRepository.GetAllRecord().Where(p => p.ProjectId == project.ProjectId);
            var taskCount = tasks.Count();

            var isAllTasksCompleted = tasks
                .All(s => !string.IsNullOrEmpty(s.Status) && 
                string.Equals("Yes", s.Status, StringComparison.InvariantCultureIgnoreCase));

            var manager = _userRepository.GetAllRecord().FirstOrDefault(p => p.ProjectId == project.ProjectId);
            var managerName = "";
            var managerId = 0;

            if (manager != null)
            {
                managerName = string.Format("{0} {1}", manager.FirstName, manager.LastName);
                managerId = manager.UserId;
            }

            return new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                ProjectName = project.Title,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                Manager = managerName,
                UserId = managerId,
                Completed = isAllTasksCompleted ? "Yes" : "No",
                NumberOfTasks = taskCount
            };
        }

        private ProjectDetails ToProjectEntity(ProjectViewModel model)
        {
            return new ProjectDetails
            {
                ProjectId = model.ProjectId,
                Title = model.ProjectName,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Priority = model.Priority
            };
        }
    }
}
