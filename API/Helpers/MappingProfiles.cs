using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Payable, PayableDto>()
                .ForMember(d => d.Entity, o => o.MapFrom(s => s.Entity.Name));

            CreateMap<Receivable, ReceivableDto>()
                .ForMember(d => d.Entity, o => o.MapFrom(s => s.Entity.Name));

            CreateMap<Gig, GigDto>()
                .ForMember(d => d.Venue, o => o.MapFrom(s => s.Venue.Name));

        }
    }
}