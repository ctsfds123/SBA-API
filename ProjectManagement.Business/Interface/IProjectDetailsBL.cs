using ProjecManagement.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecManagement.BusinessLayer.Interface
{
    public interface IProjectDetailsBL

    {
        void SaveProjectDetails(ProjectViewModel user);
        IEnumerable<ProjectViewModel> GetAllProjectDetail();
        void DeleteProjectDetail(int id);
    }
}
