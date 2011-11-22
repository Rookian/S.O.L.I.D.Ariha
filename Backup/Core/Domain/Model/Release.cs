using System;

namespace Core.Domain.Model
{
    public class Release
    {
        public virtual DateTime ReleaseDate { get; set; }
        public virtual int ReleaseNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Release)) return false;
            return Equals((Release) obj);
        }

        public bool Equals(Release other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.ReleaseDate.Equals(ReleaseDate) && other.ReleaseNumber == ReleaseNumber;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ReleaseDate.GetHashCode()*397) ^ ReleaseNumber;
            }
        }
    }
}