using ProjecManagement.BusinessLayer.ViewModel;
using System.Collections.Generic;

namespace ProjecManagement.BusinessLayer.Interface
{
    public interface IParentTaskDetailsBL
    {
        ParentTaskViewModel SaveParentTaskDetail(ParentTaskViewModel model);
        IEnumerable<ParentTaskViewModel> GetAllParentTaskDetail();
    }
}
