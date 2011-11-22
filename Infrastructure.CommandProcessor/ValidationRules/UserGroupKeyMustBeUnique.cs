using CommandProcessor.Validation;
using Core.Domain.Bases.Repositories;

namespace Infrastructure.CommandProcessor.ValidationRules
{
    public class UserGroupKeyMustBeUnique : IValidationRule
    {
        private readonly ITeamEmployeeRepository _repository;

        public UserGroupKeyMustBeUnique(ITeamEmployeeRepository repository)
        {
            _repository = repository;
        }

        public bool StopProcessing
        {
            get { return false; }
        }

        public string IsValid(object commandMessage)
        {
            //return UserGroupKeyAlreadyExists((UpdateUserGroupCommandMessage)commandMessage) ? "The key must be unique." : null;
            return null;
        }

        //private bool UserGroupKeyAlreadyExists(UpdateUserGroupCommandMessage message)
        //{
        //    var entity = _repository.GetByKey(message.UserGroup.Key);
        //    return entity != null && entity.Id != message.UserGroup.Id;
        //}
    }


}