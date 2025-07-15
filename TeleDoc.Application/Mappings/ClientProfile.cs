using AutoMapper;
using TeleDoc.Application.DTOs.Client;
using TeleDoc.Domain.Entities;

namespace TeleDoc.Application.Mappings;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<LegalEntityCreateDto, LegalEntity>();
        CreateMap<IndividualEntrepreneurCreateDto, IndividualEntrepreneur>();

        CreateMap<ClientUpdateDto, Client>()
            .Include<LegalEntityUpdateDto, LegalEntity>()
            .Include<IndividualEntrepreneurUpdateDto, IndividualEntrepreneur>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore());

        CreateMap<LegalEntityUpdateDto, LegalEntity>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore());

        CreateMap<IndividualEntrepreneurUpdateDto, IndividualEntrepreneur>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore());

        CreateMap<LegalEntity, LegalEntityReadDto>();
        CreateMap<IndividualEntrepreneur, IndividualEntrepreneurReadDto>();
        CreateMap<Client, ClientReadDto>()
            .As<LegalEntityReadDto>();
        CreateMap<Client, ClientReadDto>()
            .As<IndividualEntrepreneurReadDto>();
        CreateMap<LegalEntity, LegalEntityReadDto>();
        CreateMap<IndividualEntrepreneur, IndividualEntrepreneurReadDto>();
        CreateMap<Client, LegalEntityReadDto>();
        CreateMap<Client, IndividualEntrepreneurReadDto>();
    }
}