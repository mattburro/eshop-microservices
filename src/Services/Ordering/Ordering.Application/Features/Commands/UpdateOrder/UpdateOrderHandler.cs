
namespace Ordering.Application.Features.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.FindAsync(command.Order.Id, cancellationToken);
    }
}
