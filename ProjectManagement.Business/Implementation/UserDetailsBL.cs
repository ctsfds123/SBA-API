using ProjecManagement.Repositories;
using System.Collections.Generic;
using System.Linq;
using ProjecManagement.Business.Interface;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.EntityLayer;

namespace ProjecManagement.BusinessLayer.Implementation
{


    public class UserDetailsBL : IUserDetailsBL
    {
        readonly IRepository<UserDetails> UserDetailsRepo;

        public UserDetailsBL(IRepository<UserDetails> _userDetailsRepository)
        {
            UserDetailsRepo = _userDetailsRepository;
        }

        public void DeleteUserDetailsRecordByUserId(int id)
        {
            UserDetailsRepo.DeleteRecord(id);
        }

        public IEnumerable<UserDetailsViewModel> GetAllUserDetailsRecord()
        {
            var users = new List<UserDetailsViewModel>();
            var entities = UserDetailsRepo.GetAllRecord();

            entities.ToList().ForEach(u => users.Add(ToUserViewModel(u)));

            return users;
        }

        public void SaveUserDetailsRecord(UserDetailsViewModel user)
        {
            var entity = ToUserEntity(user);
            if (user.UserId == 0)
                UserDetailsRepo.InsertRecord(entity);
            else
            {
                UserDetailsRepo.UpdateRecord(entity);
            }
        }

        public UserDetailsViewModel GetUserDetailsRecordByUSerId(int id)
        {
            var user = UserDetailsRepo.GetRecordById(id);
            if (user == null) return new UserDetailsViewModel();
            return ToUserViewModel(user);
        }

        private UserDetailsViewModel ToUserViewModel(UserDetails user)
        {
            return new UserDetailsViewModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmployeeId = user.EmployeeId,
                ProjectId = user.ProjectId.GetValueOrDefault(),
                TaskId = user.TaskId.GetValueOrDefault()
            };
        }

        private UserDetails ToUserEntity(UserDetailsViewModel userRequest)
        {
            return new UserDetails
            {
                UserId = userRequest.UserId,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                EmployeeId = userRequest.EmployeeId
            };
        }


    }

}
