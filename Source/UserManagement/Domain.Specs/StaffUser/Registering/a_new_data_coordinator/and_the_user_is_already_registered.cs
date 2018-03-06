using Machine.Specifications;
using su = Domain.StaffUser;
using System;
using Domain.StaffUser.Registering;

namespace Domain.Specs.StaffUser.Registering.a_new_data_coordinator
{
    [Subject("Registering")]
    public class and_the_user_is_already_registered
    {
        static su.StaffUser sut;
        static DateTimeOffset now;
        static Exception result;
        static RegisterNewDataCoordinator cmd;

        Establish context = () => 
        {
            now = DateTimeOffset.UtcNow;
            cmd = given.commands.build_valid_instance<RegisterNewDataCoordinator>();
            sut = new su.StaffUser(cmd.Role.StaffUserId);

            //register the user so that they are already registered
            sut.RegisterNewDataCoordinator(cmd.Role.FullName,cmd.Role.DisplayName,cmd.Role.Email,now,
                    cmd.Role.NationalSociety, cmd.Role.PreferredLanguage.Value, cmd.Role.PhoneNumbers, cmd.Role.AssignedNationalSocieties,
                    cmd.Role.BirthYear, cmd.Role.Sex);
        };

        Because of = () => result = Catch.Exception(
            () =>  sut.RegisterNewDataCoordinator(cmd.Role.FullName,cmd.Role.DisplayName,cmd.Role.Email,now,
                    cmd.Role.NationalSociety, cmd.Role.PreferredLanguage.Value, cmd.Role.PhoneNumbers, cmd.Role.AssignedNationalSocieties,
                    cmd.Role.BirthYear, cmd.Role.Sex)
        );

        It should_throw_an_exception = () => result.ShouldNotBeNull();
        It should_be_a_user_already_registered_exception = () => result.ShouldBeOfExactType<UserAlreadyRegistered>();
    }
}