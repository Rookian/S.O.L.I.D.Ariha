using System;
using Core.Domain.Bases;
using Core.Domain.Bases.Enumerations;

namespace Core.Domain.Model
{
    public abstract class LoanedItem : Entity
    {
        public virtual DateTime DateOfIssue { get; set; }
        public virtual bool IsLoaned { get; set; }
        public virtual string Name { get; set; }
        public virtual Employee LoanedBy { get; set; }
        public virtual Release Release { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual bool IncludesCDDVD { get; set; }
        public virtual DiscriminatorValueLoanedItemEnum DiscriminatorValueLoanedItemEnum
        {
            get
            {
                if (GetType() == typeof(Book))
                {
                    return DiscriminatorValueLoanedItemEnum.Book;
                }
                
                if (GetType() == typeof(Magazine))
                {
                    return DiscriminatorValueLoanedItemEnum.Magazine;
                }

                throw new Exception(String.Format("The derived class {0} is not available in the enumeration {1}", GetType().Name, typeof(DiscriminatorValueLoanedItemEnum).Name));
            }
        }
    }
}