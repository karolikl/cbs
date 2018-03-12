using System;
using Domain.DataCollector.Update;
using FluentValidation.Results;
using Machine.Specifications;

namespace Domain.Specs.DataCollector.when_updating_a_data_collector
{
    [Subject(typeof(UpdateDataCollectorValidator))]
    public class and_validating_a_command_with_a_missing_national_society
    {
        static UpdateDataCollector cmd;
        static UpdateDataCollectorValidator validator;
        static ValidationResult validation_results;

        Establish context = () => 
        {
            validator = new UpdateDataCollectorValidator();

            cmd = given.a_command_builder.get_invalid_command((cmd) => cmd.NationalSociety = Guid.Empty);
        };

        Because of = () => { validation_results = validator.Validate(cmd); };

        It should_be_invalid = () => validation_results.ShouldBeInvalid();
        It should_identify_the_national_society_as_the_problem = () => validation_results.ShouldHaveInvalidProperty(nameof(cmd.NationalSociety));
    }
}