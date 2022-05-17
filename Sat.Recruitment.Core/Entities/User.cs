using Sat.Recruitment.Core.Common;

namespace Sat.Recruitment.Core.Entities
{
    public class User : ValueObject<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }

        protected override bool EqualsCore(User other)
        {
            return Name == other.Name
                && Email == other.Email
                && Address == other.Address
                && Phone == other.Phone;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Email.GetHashCode();
                hashCode = (hashCode * 397) ^ Address.GetHashCode();
                hashCode = (hashCode * 397) ^ Phone.GetHashCode();
                return hashCode;
            }
        }
    }
}
