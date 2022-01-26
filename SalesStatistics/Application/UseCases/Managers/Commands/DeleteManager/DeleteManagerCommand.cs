using MediatR;

namespace Application.UseCases.Managers.Commands.DeleteManager
{
    public class DeleteManagerCommand : IRequest
    {
        public int Id { get; set; }
    }
}