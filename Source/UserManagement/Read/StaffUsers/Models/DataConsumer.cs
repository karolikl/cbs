using System;
using Concepts;
using MongoDB.Bson.Serialization.Attributes;

namespace Read.StaffUsers.Models
{
    public class DataConsumer : BaseUser
    {
        public DataConsumer(Guid staffUserId, string fullName, string displayName, 
            string email, DateTimeOffset registrationDate, Location location, 
            Guid nationalSociety, Language preferredLanguage, int birthYear, Sex sex) 
            : base(staffUserId, fullName, displayName, email, registrationDate)
        {
            Location = location;
            NationalSociety = nationalSociety;
            PreferredLanguage = preferredLanguage;
            BirthYear = birthYear;
            Sex = sex;
        }

        [BsonRequired]
        public Location Location { get; set; }
        [BsonRequired]
        public Guid NationalSociety { get; set; }

        public Language PreferredLanguage { get; set; }
        public int BirthYear { get; set; }
        public Sex Sex { get; set; }
    }
}