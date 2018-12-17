using ProjecManagement.BusinessLayer;
using ProjecManagement.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecManagement.Business.Interface
{
    public interface IUserDetailsBL
    {
        void SaveUserDetailsRecord(UserDetailsViewModel user);
        IEnumerable<UserDetailsViewModel> GetAllUserDetailsRecord();
        UserDetailsViewModel GetUserDetailsRecordByUSerId(int id);
        void DeleteUserDetailsRecordByUserId(int id);
    }
}
