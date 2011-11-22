using AutoMapper;
using Core.Domain.Bases;
using Core.Domain.Bases.Repositories;

namespace Infrastructure.Automapper.ConfigurationProfiles
{
    public class IdToEntityObjectTypeConverter<I, E> : ITypeConverter<I, E> where E : Entity
    {
        private readonly IRepository<E> _repository;

        public IdToEntityObjectTypeConverter(IRepository<E> repository)
        {
            _repository = repository;
        }

        public E Convert(ResolutionContext context)
        {
            return _repository.GetById(context.SourceValue);
        }
    }
}