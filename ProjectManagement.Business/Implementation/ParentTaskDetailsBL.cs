using ProjecManagement.BusinessLayer.Interface;
using ProjecManagement.BusinessLayer.ViewModel;
using ProjecManagement.EntityLayer;
using ProjecManagement.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ProjecManagement.BusinessLayer.Implementation
{ 

    public class ParentTaskDetailsBL : IParentTaskDetailsBL
    {
        readonly IRepository<ParentTask> _parentTaskRepository;
//Constructor
        public ParentTaskDetailsBL(IRepository<ParentTask> parentTaskRepository)
        {
            _parentTaskRepository = parentTaskRepository;
        }

        public IEnumerable<ParentTaskViewModel> GetAllParentTaskDetail()
        {
            //getParent Task Details
            var entities = _parentTaskRepository.GetAllRecord();
            var models = new List<ParentTaskViewModel>();
            entities.ToList().ForEach(p => models.Add(ToModel(p)));

            return models;
        }

        public ParentTaskViewModel SaveParentTaskDetail(ParentTaskViewModel model)
        {
            //Save ParentTask Details
            var entity = _parentTaskRepository.GetRecordById(model.ParentTaskId);
            if (entity == null)
            {
                entity = ToEntity(model);
                _parentTaskRepository.InsertRecord(entity);
                model.ParentTaskId = entity.ParentTaskId;
            }
            else
            {
                entity.ParentTaskTitle = model.ParentTaskName;
                _parentTaskRepository.UpdateRecord(entity);
            }

            return model;
        }

        private ParentTask ToEntity(ParentTaskViewModel model)
        {
            return new ParentTask
            {
                ParentTaskId = model.ParentTaskId,
                ParentTaskTitle = model.ParentTaskName
            };
        }

        private ParentTaskViewModel ToModel(ParentTask entity)
        {
            return new ParentTaskViewModel
            {
                ParentTaskId = entity.ParentTaskId,
                ParentTaskName = entity.ParentTaskTitle
            };
        }
    }
}
