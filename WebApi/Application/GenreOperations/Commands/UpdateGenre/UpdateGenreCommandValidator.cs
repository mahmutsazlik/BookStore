using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command=>command.Model.Name).MinimumLength(4).When(name=>name.Model.Name.Trim()!=string.Empty);
        }
    }
}