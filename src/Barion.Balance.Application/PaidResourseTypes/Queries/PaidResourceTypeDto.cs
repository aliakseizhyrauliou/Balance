using AutoMapper;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Application.PaidResourseTypes.Queries;

public class PaidResourceTypeDto
{
    public required int Id { get; set; }
    public required string TypeName { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PaidResourceType,PaidResourceTypeDto>();
        }
    }
}