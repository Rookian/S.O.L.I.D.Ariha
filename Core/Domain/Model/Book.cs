namespace Core.Domain.Model
{
    public  class Book : LoanedItem
    {
        public virtual string Isbn { get; set; }
        public virtual string Author { get; set; }
    }
}