using FluentValidation;

namespace MinimalApiWithDapper.Validators;

public class UserValidator : AbstractValidator<UserModel>
{
	public UserValidator()
	{
		RuleFor(u => u.FirstName)
			.NotEmpty().WithMessage("{PropertyName} shouldnt be empty")
			.Length(3, 10).WithMessage("{PropertyName} {TotalLength} pls provide valid message")
			.Must(BeAValidName).WithMessage("first_name no digits");

        RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("last_name shouldnt be empty")
            .Length(3, 10).WithMessage("last_name pls provide valid message")
			.Must(BeAValidName).WithMessage("last_name no digits");
        
    }


	protected bool BeAValidName(string name)
	{
		name  = name.Replace(" ", "");
		name  = name.Replace("-", "");
		return name.All(char.IsLetter);
	}

}
