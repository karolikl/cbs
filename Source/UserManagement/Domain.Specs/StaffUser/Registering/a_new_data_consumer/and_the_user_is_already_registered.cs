using Machine.Specifications;
using su = Domain.StaffUser;
using System;
using Domain.StaffUser.Registering;

namespace Domain.Specs.StaffUser.Registering.a_new_data_consumer
{
    [Subject("Registering")]
    public class and_the_user_is_already_registered
    {
        static su.StaffUser sut;
        static DateTimeOffset now;
        static Exception result;
        static RegisterNewStaffDataConsumer command;

        Establish context = () => 
        {
            now = DateTimeOffset.UtcNow;
            command = given.commands.build_valid_instance<RegisterNewStaffDataConsumer>();
            sut = new su.StaffUser(command.Role.StaffUserId);

            //register the user so that they are already registered
            sut.RegisterNewDataConsumer(command.Role.FullName,command.Role.DisplayName,command.Role.Email,now,
                    command.Role.NationalSociety, command.Role.PreferredLanguage.Value, command.Role.BirthYear, 
                    command.Role.Sex, constants.valid_location);
        };

        Because of = () => result = Catch.Exception(
            () =>  sut.RegisterNewDataConsumer(command.Role.FullName,command.Role.DisplayName,command.Role.Email,now,
                    command.Role.NationalSociety, command.Role.PreferredLanguage.Value, command.Role.BirthYear, command.Role.Sex, 
                    constants.valid_location)
        );

        It should_throw_an_exception = () => result.ShouldNotBeNull();
        It should_be_a_user_already_registered_exception = () => result.ShouldBeOfExactType<UserAlreadyRegistered>();
    }
}