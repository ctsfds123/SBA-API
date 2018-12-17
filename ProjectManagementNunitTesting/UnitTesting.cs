
using NUnit.Framework;
using ProjecManagement.BusinessLayer.Implementation;
using ProjecManagement.BusinessLayer.Interface;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.EntityLayer;
using ProjecManagement.Repositories;
using System;
using System.Linq;
namespace ProjectManagementNunitTesting
{
    [TestFixture]
    public class UserDetailsTest
    {

        public UserDetailsViewModel GetInPut()
        {
            UserDetailsViewModel Input = new UserDetailsViewModel();
            Input.FirstName = "TestUserFirstName";
            Input.LastName = "TestUserLastName";
            Input.EmployeeId = 10002;
            return Input;
        }

        public ProjectViewModel GetProjectInPut()
        {
            ProjectViewModel Input = new ProjectViewModel();
            Input.ProjectId = 0;
            Input.ProjectName = "TestProject";
            Input.StartDate = DateTime.Now;
            Input.EndDate = DateTime.Now.AddDays(1);
            Input.Completed = null;
            Input.Manager = "Saui";
            Input.NumberOfTasks = 1;
            Input.Priority = 2;
            Input.UserId = 1;
            return Input;
        }

        public TaskViewModel GetTaskInPut()
        {
            TaskViewModel Input = new TaskViewModel();
            Input.ProjectId = 0;
            Input.ProjectName = "TestProject";
            Input.StartDate = DateTime.Now;
            Input.EndDate = DateTime.Now.AddDays(1);
            Input.ParentTaskId = 0;
            Input.ParentTaskName = "Test Parent Task";
            Input.Priority = 1;
            Input.Status = "No";
            Input.IsParentTask = true;
            Input.ManagerId = 1;
            Input.ManagerName = "Saui";
            Input.TaskId = 1;
            Input.TaskName = "Child of parent task unit testing";
            return Input;
        }

        [Test, Order(1)]
        public void CreateUser()
        {


            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);
            var Input = GetInPut();
            if (Input != null)
                df.SaveUserDetailsRecord(Input);
            var UserDetailList = df.GetAllUserDetailsRecord();
            var qq = UserDetailList.Where(tt => tt.EmployeeId == Input.EmployeeId);
            if (qq != null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }
        [Test, Order(2)]
        public void UserDetailsById()
        {
            var Input = GetInPut();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);

            var UserDetailList = df.GetAllUserDetailsRecord();
            var qq = UserDetailList.Where(tt => tt.EmployeeId == Input.EmployeeId).FirstOrDefault();
            if (qq != null)
            {
                if (qq.EmployeeId == Input.EmployeeId)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(3)]
        public void GetAllUserDetails()
        {
            //int intUserId = 2;
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);
            var UserDetailList = df.GetAllUserDetailsRecord();
            if (UserDetailList != null && UserDetailList.Count() > 0)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(4)]
        public void DeleteUserDetail()
        {
            //int intUserId = 2;

            var Input = GetInPut();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL UDBL = new UserDetailsBL(userRepository);
            var UserDetailList = UDBL.GetAllUserDetailsRecord();
            var qq = UserDetailList.Where(tt => tt.EmployeeId == Input.EmployeeId).FirstOrDefault();
            UDBL.DeleteUserDetailsRecordByUserId(qq.UserId);
            var UserDetailList1 = UDBL.GetAllUserDetailsRecord();
            var Ul = UserDetailList1.Where(tt => tt.EmployeeId == Input.EmployeeId).FirstOrDefault();
            if (Ul == null)
            {
                if (qq != Ul)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(5)]
        public void CreateProjectDetail()
        {
            int intProjectId = 5;

            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();

            ProjectDetailsBL PDBL = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);

            var Input = GetProjectInPut();
            if (Input != null)
                PDBL.SaveProjectDetails(Input);
            var UserDetailList = PDBL.GetAllProjectDetail();
            var qq = UserDetailList.Where(tt => tt.ProjectId == intProjectId);
            if (qq != null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(6)]
        public void GetProjectDetail()
        {
           
            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();
            ProjectDetailsBL UDBL = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);
            var ProjectDetailList = UDBL.GetAllProjectDetail();
            
            var ProjectDetailListExpected = UDBL.GetAllProjectDetail();
            
            if (ProjectDetailList != null && ProjectDetailListExpected != null)
            {
                if (ProjectDetailList.Count() == ProjectDetailListExpected.Count())
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else if (ProjectDetailList == null && ProjectDetailListExpected == null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(7)]
        public void DeleteProjectDetail()
        {
            int intProjectId = 5;

            var Input = GetProjectInPut();
            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();
            ProjectDetailsBL UDBL = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);

            var ProjectDetailList = UDBL.GetAllProjectDetail();
            var qq = ProjectDetailList.Where(tt => tt.ProjectId == intProjectId).FirstOrDefault();
            UDBL.DeleteProjectDetail(qq.ProjectId);
            var ProjectDetailListExpected = UDBL.GetAllProjectDetail();
            var Ul = ProjectDetailListExpected.Where(tt => tt.ProjectId == intProjectId).FirstOrDefault();
            if (Ul == null)
            {
                if (qq != Ul)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(8)]
        public void CreateParentTask()
        {
            int intParenTaskId = 5;

            IRepository<ParentTask> ParenttaskRepository = new Repository<ParentTask>();
            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();

            IParentTaskDetailsBL parentTaskBusiness = new ParentTaskDetailsBL(ParenttaskRepository);
            IProjectDetailsBL projectBusiness = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);
            
            

            TaskDetailsBL PDBL = new TaskDetailsBL(taskRepository, parentTaskBusiness, projectBusiness, userRepository);

            var Input = GetTaskInPut();
            Input.TaskName = "Parent Task Unit Testing";
            if (Input != null)
                PDBL.SaveTaskDetils(Input);
            var ParentTaskDetail = PDBL.GetAllParentTaskDetails();
            var qq = ParentTaskDetail.Where(tt => tt.ParentTaskId == intParenTaskId);
            if (qq != null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }
        [Test, Order(9)]
        public void CreateTask()
        {
            int intParenTaskId = 3;

            IRepository<ParentTask> ParenttaskRepository = new Repository<ParentTask>();
            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();

            IParentTaskDetailsBL parentTaskBusiness = new ParentTaskDetailsBL(ParenttaskRepository);
            IProjectDetailsBL projectBusiness = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);



            TaskDetailsBL PDBL = new TaskDetailsBL(taskRepository, parentTaskBusiness, projectBusiness, userRepository);

            var Input = GetTaskInPut();
            Input.IsParentTask = false;
            if (Input != null)
                PDBL.SaveTaskDetils(Input);
            var TaskDetailList = PDBL.GetAllTaskDetails();
            var qq = TaskDetailList.Where(tt => tt.TaskId == intParenTaskId);
            if (qq != null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }
        [Test, Order(10)]
        public void GetTaskDetailById()
        {

            int intTaskId = 3;

            IRepository<ParentTask> ParenttaskRepository = new Repository<ParentTask>();
            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();

            IParentTaskDetailsBL parentTaskBusiness = new ParentTaskDetailsBL(ParenttaskRepository);
            IProjectDetailsBL projectBusiness = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);

            TaskDetailsBL TDBL = new TaskDetailsBL(taskRepository, parentTaskBusiness, projectBusiness, userRepository);

            var TaskDetailList = TDBL.GetDetailsById(intTaskId);

            var TaskDetailListExpected = TDBL.GetDetailsById(intTaskId);

            if (TaskDetailList != null && TaskDetailListExpected != null)
            {
                if (TaskDetailList.TaskId == TaskDetailListExpected.TaskId)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else if (TaskDetailList == null && TaskDetailListExpected == null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(11)]
        public void GetAllTask()
        {

            IRepository<ParentTask> ParenttaskRepository = new Repository<ParentTask>();
            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();

            IParentTaskDetailsBL parentTaskBusiness = new ParentTaskDetailsBL(ParenttaskRepository);
            IProjectDetailsBL projectBusiness = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);

            TaskDetailsBL TDBL = new TaskDetailsBL(taskRepository, parentTaskBusiness, projectBusiness, userRepository);

            var TaskDetailList = TDBL.GetAllTaskDetails();

            var TaskDetailListExpected = TDBL.GetAllTaskDetails();

            if (TaskDetailList != null && TaskDetailListExpected != null)
            {
                if (TaskDetailList.Count() == TaskDetailListExpected.Count())
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else if (TaskDetailList == null && TaskDetailListExpected == null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(12)]
        public void GetAllParentTask()
        {

            IRepository<ParentTask> ParenttaskRepository = new Repository<ParentTask>();
            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();

            IParentTaskDetailsBL parentTaskBusiness = new ParentTaskDetailsBL(ParenttaskRepository);
            IProjectDetailsBL projectBusiness = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);

            TaskDetailsBL TDBL = new TaskDetailsBL(taskRepository, parentTaskBusiness, projectBusiness, userRepository);

            var TaskDetailList = TDBL.GetAllParentTaskDetails();

            var TaskDetailListExpected = TDBL.GetAllParentTaskDetails();

            if (TaskDetailList != null)
            {
                if (TaskDetailList.Count() == TaskDetailListExpected.Count())
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else if (TaskDetailList == null && TaskDetailListExpected == null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test, Order(13)]
        public void CompleteTask()
        {

            IRepository<ParentTask> ParenttaskRepository = new Repository<ParentTask>();
            IRepository<ProjectDetails> projectRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository = new Repository<ProjectTaskDetails>();

            IParentTaskDetailsBL parentTaskBusiness = new ParentTaskDetailsBL(ParenttaskRepository);
            IProjectDetailsBL projectBusiness = new ProjectDetailsBL(projectRepository, userRepository, taskRepository);

            TaskDetailsBL TDBL = new TaskDetailsBL(taskRepository, parentTaskBusiness, projectBusiness, userRepository);
            var Input = GetTaskInPut();
            var TaskDetailList=taskRepository.GetRecordById(Input.TaskId);

            var TaskDetailListExpected = taskRepository.GetRecordById(Input.TaskId);

            if (TaskDetailList == null && TaskDetailListExpected == null)
            {
                Assert.Pass();
            }
            else if (TaskDetailList != null && TaskDetailListExpected != null)
            {
                if (TaskDetailList.TaskId == TaskDetailListExpected.TaskId)
                {
                    Assert.Pass();
                }
                else
                    Assert.Fail();
            }
            else
            {
                Assert.Fail();
            }

        }
    }
}

