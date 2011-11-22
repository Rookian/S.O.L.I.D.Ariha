using Core.Domain.Bases;

namespace Core.Domain.Model.ConsumerProtection
{
    public class Article : Entity
    {
        public virtual string Description { get; set; }
        public virtual GoodsGroup GoodsGroup { get; set; }

    }
}