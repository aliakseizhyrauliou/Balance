using AutoMapper;
using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Application.PaidResourseTypes.Queries;

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