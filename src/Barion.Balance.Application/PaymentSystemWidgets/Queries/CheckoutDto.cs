using AutoMapper;

namespace Barion.Balance.Application.PaymentSystemWidgets.Queries;

public class CheckoutDto
{
    public required string Url { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.PaymentSystemWidget, CheckoutDto>();
        }
    }
}