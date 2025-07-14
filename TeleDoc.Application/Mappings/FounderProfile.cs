using AutoMapper;
using TeleDoc.Application.DTOs.Founder;
using TeleDoc.Domain.Entities;

namespace TeleDoc.Application.Mappings;

public class FounderProfile : Profile
{
    public FounderProfile()
    {
        CreateMap<Founder, FounderReadDto>();
        CreateMap<FounderCreateDto, Founder>();
    }
}