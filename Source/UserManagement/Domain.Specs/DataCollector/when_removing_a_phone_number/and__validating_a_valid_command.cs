using System;
using Domain.DataCollector.PhoneNumber;
using FluentValidation.Results;
using Machine.Specifications;

namespace Domain.Specs.DataCollector.when_removing_a_phone_number
{
    [Subject(typeof(RemovePhoneNumberFromDataCollectorValidator))]
    public class and__validating_a_valid_command
    {
        static RemovePhoneNumberFromDataCollector cmd;
        static RemovePhoneNumberFromDataCollectorValidator validator;
        static ValidationResult validation_results;

        Establish context = () => 
        {
            validator = new RemovePhoneNumberFromDataCollectorValidator();

            cmd = new RemovePhoneNumberFromDataCollector 
            {
                DataCollectorId = Guid.NewGuid(),
                PhoneNumber = "11223344"
            };
        };

        Because of = () => { validation_results = validator.Validate(cmd); };
        It should_be_invalid = () => validation_results.ShouldBeValid();
    }
}