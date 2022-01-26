using MediatR;

namespace Application.UseCases.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommand : IRequest
    {
        public int Id { get; set; }

        public string LastName { get; set; }
    }
}