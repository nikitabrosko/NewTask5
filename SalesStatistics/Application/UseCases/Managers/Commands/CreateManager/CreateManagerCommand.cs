using MediatR;

namespace Application.UseCases.Managers.Commands.CreateManager
{
    public class CreateManagerCommand : IRequest<int>
    {
        public string LastName { get; set; }
    }
}