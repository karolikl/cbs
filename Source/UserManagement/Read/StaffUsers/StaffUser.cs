using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Concepts;
using Events.StaffUser;

namespace Read.StaffUsers
{
    /// <summary>
    /// The idea is that this class can contain all the different kinds of StaffUsers.
    /// We used this idea in VolunteerReporting to supply the view with a single class that can contain the diferent 
    /// read models that are derived from the same read model type.
    /// 
    /// We probaably don't need all of these fields, we just need the fields specific for the view. Fields can be removed
    /// here as we see fit.
    /// </summary>
    public class StaffUser
    {
        //TODO: Update to the new system
        public Guid Id { get; set; }
        public _Role Role { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public int YearOfBirth { get; set; }
        public Sex Sex { get; set; }
        public Guid NationalSociety { get; set; }
        public Language PreferredLanguage { get; set; }
        public Location Location { get; set; }
        public List<PhoneNumber> MobilePhoneNumbers { get; set; }
        public List<Guid> AssignedNationalSocities { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }

        public string Position { get; set; }
        public string DutyStation { get; set; }

        #region ConstructorsForCreatingStaffUser

        //TODO: Update to the new system
        //public StaffUser(AdminAdded @event)
        //{
        //    Role = _Role.Admin;
        //    Id = @event.StaffUserId;

        //    FullName = @event.FullName;
        //    DisplayName = @event.DisplayName;
        //    Email = @event.Email;
        //}

        //public StaffUser(DataConsumerAdded @event)
        //{
        //    Role = _Role.DataConsumer;
        //    Id = @event.StaffUserId;

        //    FullName = @event.FullName;
        //    DisplayName = @event.DisplayName;
        //    Email = @event.Email;

        //    Location = new Location(@event.LocationLatitude, @event.LocationLongitude);

        //}
        //public StaffUser(DataCoordinatorAdded @event)
        //{
        //    Role = _Role.DataCoordinator;
        //    Id = @event.StaffUserId;

        //    FullName = @event.FullName;
        //    DisplayName = @event.DisplayName;
        //    Email = @event.Email;

        //    YearOfBirth = @event.YearOfBirth;
        //    Sex = (Sex)@event.Sex;
        //    NationalSociety = @event.NationalSociety;
        //    PreferredLanguage = (Language)@event.PreferredLanguage;
        //    Location = new Location(@event.LocationLatitude, @event.LocationLongitude);
        //    MobilePhoneNumbers = new List<PhoneNumber>();
        //    AssignedNationalSocities = new List<Guid>();

        //}

        //public StaffUser(DataOwnerAdded @event)
        //{
        //    Role = _Role.DataOwner;
        //    Id = @event.StaffUserId;

        //    FullName = @event.FullName;
        //    DisplayName = @event.DisplayName;
        //    Email = @event.Email;

        //    YearOfBirth = @event.YearOfBirth; YearOfBirth = @event.YearOfBirth;
        //    Sex = (Sex)@event.Sex;
        //    NationalSociety = @event.NationalSociety;
        //    PreferredLanguage = (Language)@event.PreferredLanguage;
        //    Location = new Location(@event.LocationLatitude, @event.LocationLongitude);
        //    MobilePhoneNumbers = new List<PhoneNumber>();
        //    AssignedNationalSocities = new List<Guid>();
        //    Position = @event.Position;
        //    DutyStation = @event.DutyStation;

        //}
        //public StaffUser(DataVerifierAdded @event)
        //{
        //    Role = _Role.DataVerifier;
        //    Id = @event.StaffUserId;

        //    FullName = @event.FullName;
        //    DisplayName = @event.DisplayName;
        //    Email = @event.Email;

        //    YearOfBirth = @event.YearOfBirth; YearOfBirth = @event.YearOfBirth;
        //    Sex = (Sex)@event.Sex;
        //    NationalSociety = @event.NationalSociety;
        //    PreferredLanguage = (Language)@event.PreferredLanguage;
        //    Location = new Location(@event.LocationLatitude, @event.LocationLongitude);
        //    MobilePhoneNumbers = new List<PhoneNumber>();
        //    AssignedNationalSocities = new List<Guid>();
        //    RegistrationDate = @event.RegistrationDate;
        //}

        //public StaffUser(SystemCoordinatorAdded @event)
        //{
        //    Role = _Role.SystemCoordinator;
        //    Id = @event.StaffUserId;

        //    FullName = @event.FullName;
        //    DisplayName = @event.DisplayName;
        //    Email = @event.Email;

        //    YearOfBirth = @event.YearOfBirth; YearOfBirth = @event.YearOfBirth;
        //    Sex = (Sex)@event.Sex;
        //    NationalSociety = @event.NationalSociety;
        //    PreferredLanguage = (Language)@event.PreferredLanguage;
        //    Location = new Location(@event.LocationLatitude, @event.LocationLongitude);
        //    MobilePhoneNumbers = new List<PhoneNumber>();
        //    AssignedNationalSocities = new List<Guid>();
        //}

        #endregion

    }

}
