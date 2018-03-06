using System;
using Domain.DataCollector.PhoneNumber;
using FluentValidation.Results;
using Machine.Specifications;

namespace Domain.Specs.DataCollector.when_removing_a_phone_number
{
    [Subject(typeof(RemovePhoneNumberFromDataCollectorValidator))]
    public class and_validating_a_command_with_a_missing_data_collector_id
    {
        static RemovePhoneNumberFromDataCollectorValidator validator;
        static ValidationResult validation_results;

        static RemovePhoneNumberFromDataCollector cmd;

        private Establish context = () =>
        {
            validator = new RemovePhoneNumberFromDataCollectorValidator();
            cmd = new RemovePhoneNumberFromDataCollector
            {
                DataCollectorId = Guid.Empty,
                PhoneNumber = "11223344"
            };
        };

        Because of = () => { validation_results = validator.Validate(cmd); };
        It should_be_invalid = () => validation_results.ShouldBeInvalid();

        It should_identitfy_the_data_collector_id_as_the_error = () =>
            validation_results.ShouldHaveInvalidProperty(nameof(cmd.DataCollectorId));

    }
}