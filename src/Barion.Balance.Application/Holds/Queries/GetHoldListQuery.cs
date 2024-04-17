using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using MediatR;

namespace Barion.Balance.Application.Holds.Queries;

public class GetHoldListQuery : IRequest<List<HoldDto>>;

public class GetHoldListQueryHandler(IHoldRepository holdRepository,
    IMapper mapper) 
    : IRequestHandler<GetHoldListQuery, List<HoldDto>>
{
    public async Task<List<HoldDto>> Handle(GetHoldListQuery request, CancellationToken cancellationToken)
    {
        var holdList = await holdRepository.GetListAsync(cancellationToken);

        return mapper.Map<List<HoldDto>>(holdList);
    }
}

public class HoldDto
{
    public int Id { get; set; }

    /// <summary>
    /// Identifier of user
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public int PaidResourceTypeId { get; set; }

    public PaidResourceType PaidResourceType { get; set; }

    public required string PaidResourceId { get; set; }

    /// <summary>
    /// Id в платежной системе
    /// </summary>
    public required string? PaymentSystemTransactionId { get; set; }
    
    
    /// <summary>
    /// Id того, кто является получателем суммы платежа(продавец, оператор и тд.)
    /// </summary>
    public required string OperatorId { get; set; }
    
    /// <summary>
    /// Сумма
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Была ли удрержана сумма
    /// </summary>
    public bool IsCaptured { get; set; } 
    
    /// <summary>
    /// Была ли сумма отдана 
    /// </summary>
    public bool IsVoided { get; set; } 
    
    /// <summary>
    /// Id платежного метода
    /// </summary>
    public required int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    
    
    [Column(TypeName = "jsonb")]
    public string? AdditionalData { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Hold, HoldDto>();
        }
    }
}