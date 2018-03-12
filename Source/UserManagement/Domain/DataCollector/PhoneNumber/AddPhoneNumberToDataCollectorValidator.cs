using FluentValidation;

namespace Domain.DataCollector.PhoneNumber
{
    public class AddPhoneNumberToDataCollectorValidator : AbstractValidator<AddPhoneNumberToDataCollector>
    {
        public AddPhoneNumberToDataCollectorValidator()
        {
            RuleFor(_ => _.DataCollectorId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Data Collector Id is required");

            RuleFor(_ => _.PhoneNumber)
                .NotEmpty().WithMessage("At least one Phone Number is required");
                //TODO: Add validation on phonenumber format

        }
    }
}