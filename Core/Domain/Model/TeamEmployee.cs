using Core.Domain.Bases;

namespace Core.Domain.Model
{
    public class TeamEmployee : Entity
    {
        public virtual Employee Employee { get; set; }
        public virtual Team Team { get; set; }
    }
}