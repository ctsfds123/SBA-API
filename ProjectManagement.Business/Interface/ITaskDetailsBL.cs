using ProjecManagement.BusinessLayer;
using ProjecManagement.BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecManagement.BusinessLayer.Interface
{
    public interface ITaskDetailsBL
    {
        void SaveTaskDetils(TaskViewModel model);
        void Complete(TaskViewModel model);
        IEnumerable<ParentTaskViewModel> GetAllParentTaskDetails();
        IEnumerable<TaskViewModel> GetAllTaskDetails();
        IEnumerable<TaskViewModel> GetDetailsByProjectId(int id);
        TaskViewModel GetDetailsById(int id);
    }
}
