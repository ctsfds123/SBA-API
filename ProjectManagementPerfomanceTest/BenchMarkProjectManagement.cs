using NBench;
using ProjecManagement.BusinessLayer.Implementation;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.EntityLayer;
using ProjecManagement.Repositories;
using System;
using System.Collections.Generic;

namespace ProjectManagementPerfomanceTest
{
    public class BenchMarkProjectManagement
    {
        int deleteUserId;
        int fetchUserId;
        int EmployeeId;
        int SaveIter;

        TaskDetailsBL taskDetailsBL;
        ProjectDetailsBL projectDetailsBL;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {

            EmployeeId = 12345;
            SaveIter = 20;
            deleteUserId = 10;
            fetchUserId = 10;

            IRepository<ProjectTaskDetails> projectRepository = new Repository<ProjectTaskDetails>();
            ParentTaskDetailsBL parentTaskBusiness = new ParentTaskDetailsBL(new Repository<ParentTask>());
            IRepository<TaskViewModel> taskRepository = new Repository<TaskViewModel>();
            IRepository<ProjectDetails> projectDetailsRepository = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            ProjectDetailsBL projectDetails = new ProjectDetailsBL(projectDetailsRepository, userRepository, projectRepository);
            taskDetailsBL = new TaskDetailsBL(projectRepository, parentTaskBusiness, projectDetails, userRepository);

            IRepository<ProjectDetails> projectRepository1 = new Repository<ProjectDetails>();
            IRepository<UserDetails> userRepository1 = new Repository<UserDetails>();
            IRepository<ProjectTaskDetails> taskRepository1 = new Repository<ProjectTaskDetails>();
            projectDetailsBL = new ProjectDetailsBL(projectRepository1, userRepository1, taskRepository1);
        }



        #region UserDetails
        #region Create User

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for Create UserDetails  Method\n * ***************************************************************",
              NumberOfIterations = 1,
              RunMode = RunMode.Throughput,
              TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkCreateUserDetail()
        {
            UserDetailsViewModel Input = new UserDetailsViewModel();
            Input.FirstName = "TestUserFirstName";
            Input.LastName = "TestUserLastName";
            Input.EmployeeId = EmployeeId + 5;

            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);
            if (Input != null)
                df.SaveUserDetailsRecord(Input);

        }
        #endregion
        #region GetAllUserDetails

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for GetAllUserDetailsRecord Method\n * ***************************************************************",
          NumberOfIterations = 1,
          RunMode = RunMode.Throughput,
          TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetAllUserDetails()
        {
            IEnumerable<UserDetailsViewModel> userList;
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);
            userList = df.GetAllUserDetailsRecord();
            //Console.WriteLine(tt.ToString());
        }
        #endregion
        #region GetUserDetails ById
        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for GetUserDetailsRecordByUSerId Method\n * ***************************************************************",
          NumberOfIterations = 1,
          RunMode = RunMode.Throughput,
          TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkUserDetailsById()
        {
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);
            var tt = df.GetUserDetailsRecordByUSerId(fetchUserId);
        }
        #endregion
        #region Delete UserDetails byId
        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for Delete UserDetails by Id Method\n * ***************************************************************",
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkUserDetailDeleteById()
        {
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);

            if (!string.IsNullOrWhiteSpace(df.GetUserDetailsRecordByUSerId(deleteUserId).FirstName))
                df.DeleteUserDetailsRecordByUserId(deleteUserId);

        }
        #endregion
        #endregion

        #region TaskDetails
        #region Create Task

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for Create TaskDetails  Method\n * ***************************************************************",
              NumberOfIterations = 2,
              RunMode = RunMode.Throughput,
              TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkSaveTaskDetils()
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
            Input.TaskId = fetchUserId+1;
            Input.TaskName = "Child of parent task unit testing";

            if (Input != null)
                taskDetailsBL.SaveTaskDetils(Input);

        }
        #endregion

        #region GetDetailsById

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for GetDetailsById Method\n * ***************************************************************",
          NumberOfIterations = 1,
          RunMode = RunMode.Throughput,
          TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetDetailsById()
        {
            TaskViewModel userList = taskDetailsBL.GetDetailsById(1);
        }
        #endregion

        #region GetUserDetails ById
        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for GetAllParentTaskDetails Method\n * ***************************************************************",
          NumberOfIterations = 1,
          RunMode = RunMode.Throughput,
          TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkGetAllParentTaskDetails()
        {
            taskDetailsBL.GetAllParentTaskDetails();
        }
        #endregion

        #region GetAllTaskDetails
        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for GetAllTaskDetails Method\n * ***************************************************************",
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkGetAllTaskDetails()
        {
            taskDetailsBL.GetAllTaskDetails();
        }
        #endregion


        #endregion

        #region ProjectDetails
        #region Create Project

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for Create ProjectDetails  Method\n * ***************************************************************",
              NumberOfIterations = 2,
              RunMode = RunMode.Throughput,
              TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkSaveProjectDetails()
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
            Input.UserId = fetchUserId +1;


            if (Input != null)
                projectDetailsBL.SaveProjectDetails(Input);

        }
        #endregion

        #region GetAllProjectDetail

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for GetAllProjectDetail Method\n * ***************************************************************",
          NumberOfIterations = 2,
          RunMode = RunMode.Throughput,
          TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkGetAllProjectDetail()
        {
            projectDetailsBL.GetAllProjectDetail();
        }
        #endregion

        #region DeleteProjectDetail

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for DeleteProjectDetail Method\n * ***************************************************************",
          NumberOfIterations = 2,
          RunMode = RunMode.Throughput,
          TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void BenchMarkDeleteProjectDetail()
        {
            projectDetailsBL.DeleteProjectDetail(deleteUserId+1);
        }
        #endregion


        #endregion

        [PerfCleanup]
        public void Cleanup()
        {
        }
    }
}
