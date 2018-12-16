using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using ProjectManagerService.ViewModel;
using System.Collections.Generic;

namespace ProjectManagerUnitTesting
{
    [TestClass]
    public class UnitTest
    {
        private DAL dal = new DAL();

        [TestMethod]
        public void TestMethod1()
        {
            dal.TestData("Raj");
        }

        [TestMethod]
        public void TestMethod2()
        {
            dal.AddParentTaskDetails("ParentTask");
        }

        [TestMethod]
        public void TestMethod3()
        {
            ProjectDetails projectDetails = new ProjectDetails();
            dal.AddProjectDetails(projectDetails);
        }

        [TestMethod]
        public void TestMethod4()
        {
            TaskDetails taskDetails = new TaskDetails();
            dal.AddTaskDetails(taskDetails);
        }

        [TestMethod]
        public void TestMethod5()
        {
            UserDetails userDetails = new UserDetails();
            dal.AddUserDetails(userDetails);
        }

        [TestMethod]
        public void TestMethod6()
        {   
            dal.DeleteProject(1);
        }

        [TestMethod]
        public void TestMethod7()
        {
            dal.DeleteUser(1);
        }

        [TestMethod]
        public void TestMethod8()
        {
            ProjectDetails projectDetails = new ProjectDetails();
            dal.EditProjectDetail(projectDetails);
        }

        [TestMethod]
        public void TestMethod9()
        {
            TaskDetails taskDetails = new TaskDetails();
            dal.EditTaskDetails(taskDetails);
        }

        [TestMethod]
        public void TestMethod10()
        {
            UserDetails userDetails = new UserDetails();
            dal.EditUserDetails(userDetails);
        }

        [TestMethod]
        public void TestMethod11()
        {
            TaskDetails taskDetails = new TaskDetails();
            dal.EndTaskDetails("1");
        }

        [TestMethod]
        public void TestMethod12()
        {
            List<ProjectDetails> projectDetails = dal.GetProjectDetails();
        }

        [TestMethod]
        public void TestMethod13()
        {
            ProjectDetails projectDetails =  dal.GetProjectDetailsByProjectName("project");
        }

        [TestMethod]
        public void TestMethod14()
        {
            List<TaskDetails> taskDetails = dal.GetTaskDetails();
        }


        [TestMethod]
        public void TestMethod15()
        {
            TaskDetails taskDetails = dal.GetTaskDetailsByProjectName("task");
        }

        [TestMethod]
        public void TestMethod16()
        {
            List<UserDetails> userDetails = dal.GetUserDetails();
        }

        [TestMethod]
        public void TestMethod17()
        {
            UserDetails userDetails = dal.GetUserDetailsByUserName("user");
        }
    }
}
