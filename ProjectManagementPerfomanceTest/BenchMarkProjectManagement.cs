using NBench;
using ProjecManagement.BusinessLayer.Implementation;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.EntityLayer;
using ProjecManagement.Repositories;
using System.Collections.Generic;

namespace ProjectManagementPerfomanceTest
{
    public class BenchMarkProjectManagement
    {
        int UserId = 6;
        //public static void Start()
        //{
        //    Bootstrapper.Configure();
        //}

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {


            //UserDetailsRepo = _userDetailsRepository;
        }

        #region UserDetails
        #region Create User

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for Create UserDetails  Method\n * ***************************************************************",
              NumberOfIterations = 5,
              RunMode = RunMode.Throughput,
              TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkCreateUserDetail()
        {
            UserDetailsViewModel Input = new UserDetailsViewModel();
            Input.FirstName = "TestUserFirstName";
            Input.LastName = "TestUserLastName";
            Input.EmployeeId = 12345;




            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);
            if (Input != null)
                df.SaveUserDetailsRecord(Input);

        }
        #endregion
        #region GetAllUserDetails

        [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for GetAllUserDetailsRecord Method\n * ***************************************************************",
          NumberOfIterations = 2,
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
      NumberOfIterations = 2,
      RunMode = RunMode.Throughput,
      TestMode = TestMode.Measurement, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void BenchMarkUserDetailsById()
        {
            IRepository<UserDetails> userRepository = new Repository<UserDetails>();
            UserDetailsBL df = new UserDetailsBL(userRepository);
            var tt = df.GetUserDetailsRecordByUSerId(2);
            //  Console.WriteLine(tt.ToString());
        }
        #endregion
        //    #region Delete UserDetails byId
        //    [PerfBenchmark(Description = "****************************************************************\n Bench Mark Details for Delete UserDetails by Id Method\n * ***************************************************************",
        //NumberOfIterations = 1,
        //RunMode = RunMode.Throughput,
        //TestMode = TestMode.Measurement, SkipWarmups = true)]
        //    [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        //    public void BenchMarkUserDetailDeleteById()
        //    {
        //        IRepository<UserDetails> userRepository = new Repository<UserDetails>();
        //        UserDetailsBL df = new UserDetailsBL(userRepository);

        //        bool isDeleted = false;
        //        if(df.GetUserDetailsRecordByUSerId(UserId)!=null )
        //        df.DeleteUserDetailsRecordByUserId(UserId);
        //        else
        //            df.DeleteUserDetailsRecordByUserId(UserId+1);
        //        //  Console.WriteLine(tt.ToString());
        //    }
        //    #endregion
        #endregion

        [PerfCleanup]
        public void Cleanup()
        {
        }
    }
}
