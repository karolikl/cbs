using Domain.DataCollector.Registering;
using FluentValidation.Results;
using Machine.Specifications;
using Concepts;
namespace Domain.Specs.DataCollector.when_registering_a_data_collector
{
    [Subject(typeof(AddDataCollectorValidator))]
    public class and_validating_a_command_with_a_missing_sex
    {
        static RegisterDataCollector cmd;
        static AddDataCollectorValidator validator;
        static ValidationResult validation_results;

        Establish context = () => 
        {
            validator = new AddDataCollectorValidator();

            cmd = given.a_command_builder.get_invalid_command((cmd) => cmd.Sex = (Sex)(-1));
        };

        Because of = () => { validation_results = validator.Validate(cmd); };
        It should_be_invalid = () => validation_results.ShouldBeInvalid();
        It should_identify_the_first_name_as_the_problem = () => validation_results.ShouldHaveInvalidProperty(nameof(cmd.Sex));
    }
}