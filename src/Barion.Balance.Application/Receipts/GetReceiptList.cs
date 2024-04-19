using AutoMapper;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Application.Holds.Queries;
using Barion.Balance.Domain.Entities;
using MediatR;

namespace Barion.Balance.Application.Receipts;

public class GetReceiptListQuery : IRequest<List<ReceiptDto>>;

public class GetReceiptListQueryHandler(IReceiptRepository repository,
    IMapper mapper) 
    : IRequestHandler<GetReceiptListQuery, List<ReceiptDto>>
{
    public async Task<List<ReceiptDto>> Handle(GetReceiptListQuery request, CancellationToken cancellationToken)
    {
        var receipts = await repository.GetListAsync(cancellationToken);

        return mapper.Map<List<ReceiptDto>>(receipts);
    }
}

public class ReceiptDto
{
    public string UserId { get; set; }
    
    /// <summary>
    /// Id транзакции в платежной системе
    /// </summary>
    public string? PaymentSystemTransactionId { get; set; } 
    
    /// <summary>
    /// Id того, за что платим
    /// </summary>
    public string PaidResourceId { get; set; } = null!;

    /// <summary>
    /// Ссылка на чек
    /// </summary>
    public string Url { get; set; } = null!;
    
    /// <summary>
    /// Инентефикатор конфигурации платежной системы
    /// </summary>
    public int? PaymentSystemConfigurationId { get; set; }

    
    /// <summary>
    /// Карта
    /// </summary>
    public int? PaymentMethodId { get; set; }


    /// <summary>
    /// Платеж
    /// </summary>
    public int? PaymentId { get; set; }
    /// <summary>
    /// Холд
    /// </summary>
    public int? HoldId { get; set; }
    
    public bool IsReceiptForHold { get; set; }
    public bool IsReceiptForPayment { get; set; }

    public int PaidResourceTypeId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Receipt, ReceiptDto>();
        }
    }
}