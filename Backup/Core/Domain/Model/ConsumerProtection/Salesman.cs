using Core.Domain.Bases;

namespace Core.Domain.Model.ConsumerProtection
{
    public class Salesman : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Place { get; set; }
    }
}