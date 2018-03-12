using System;
using doLittle.FluentValidation.Commands;
using FluentValidation;

namespace Domain.StaffUser.Registering
{
    public abstract class NewStaffRegistrationBusinessRulesValidator<TCommand,TRole> : CommandBusinessValidator<TCommand> 
    where TCommand : NewStaffRegistration<TRole>
    where TRole : Roles.StaffRole
    {
        readonly StaffUserIsRegistered _isRegistered;

        public NewStaffRegistrationBusinessRulesValidator(StaffUserIsRegistered isRegistered)
        {
            _isRegistered = isRegistered;

            //Use ModelRule when the rule applies to the command as a whole or to multiple properties, not a specific property
            RuleFor(_ => _.Role.StaffUserId).Must(NotBeAlreadyRegistered).WithMessage(_ => $"User '{_.Role.StaffUserId}' is already registered.");
        }

        bool NotBeAlreadyRegistered(Guid staffUserId)
        {
            return !_isRegistered(staffUserId);
        }
    }
}