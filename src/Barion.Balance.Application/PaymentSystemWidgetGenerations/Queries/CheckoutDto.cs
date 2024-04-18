using AutoMapper;
using Barion.Balance.Application.PaymentMethods.Queries;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Application.PaymentSystemWidgetGenerations.Queries;

public class CheckoutDto
{
    public required string Url { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PaymentSystemWidget, CheckoutDto>();
        }
    }
}