namespace Core.Domain.Model
{
    public class Publisher
    {
        public virtual string PublisherName { get; set; }
        public virtual string PublisherHomepage { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Publisher)) return false;
            return Equals((Publisher) obj);
        }

        public bool Equals(Publisher other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.PublisherName, PublisherName) && Equals(other.PublisherHomepage, PublisherHomepage);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((PublisherName != null ? PublisherName.GetHashCode() : 0)*397) ^ (PublisherHomepage != null ? PublisherHomepage.GetHashCode() : 0);
            }
        }
    }
}