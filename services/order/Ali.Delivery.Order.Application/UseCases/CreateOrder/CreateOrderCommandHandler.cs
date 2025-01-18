﻿using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Application.Extensions;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.CreateOrder;

/// <summary>
/// Представляет обработчик команды создания заказа.
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateOrderCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="currentUser">Текущий пользователь</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public CreateOrderCommandHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var sender = await _context.Users.FirstOrDefaultAsync(u => (Guid)u.Id == _currentUser.Id, cancellationToken) ?? throw new NotFoundException(typeof(User), _currentUser.Id);

        if (sender.PassportInfo != null)
        {
            var receiver = await _context.Users.FirstOrDefaultAsync(u => (Guid)u.Id == request.ReceiverId, cancellationToken) ??
                           throw new NotFoundException(typeof(User), request.ReceiverId);

            var orderInfo = new OrderInfo(SequentialGuid.Create(),
                                          new OrderInfoWeight(request.Weight),
                                          request.Size.ToSize(),
                                          new OrderInfoPrice(request.Price),
                                          new OrderInfoAddressFrom(request.AddressFrom),
                                          new OrderInfoAddressTo(request.AddressTo));

            var order = new Domain.Entities.Order(SequentialGuid.Create(), new OrderName(request.OrderName), orderInfo, OrderStatus.Created.ToOrderStatus(), sender, receiver);

            _context.Orders.Add(order);

            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }

        throw new InvalidOperationException("Пожалуйста заполните паспортные данные для создания заказа");
    }
}
