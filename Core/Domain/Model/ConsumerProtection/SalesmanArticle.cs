using System;
using Core.Domain.Bases;

namespace Core.Domain.Model.ConsumerProtection
{
    public class SalesmanArticle : Entity
    {
        public virtual Salesman Salesman { get; set; }
        public virtual Article Article { get; set; }
        public virtual int Amount { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual decimal Cost { get; set; }
        public virtual decimal Sum
        {
            get { return Amount*Cost; }
        }
    }
}