using AutoMapper;
using FluentResults;
using TeleDoc.Application.DTOs;
using TeleDoc.Application.DTOs.Client;
using TeleDoc.Application.Interfaces.Services;
using TeleDoc.Domain.Entities;
using TeleDoc.Domain.Enums;
using TeleDoc.Infrastructure.Interfaces;

namespace TeleDoc.Application.Services;

public class ClientService(
    IUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IMapper mapper)
    : IClientService
{
    public async Task<IEnumerable<ClientReadDto>> GetAllAsync()
    {
        var clients = await clientRepository.GetAllAsync();
        return mapper.Map<IEnumerable<ClientReadDto>>(clients);
    }

    public async Task<IEnumerable<LegalEntityReadDto>> GetAllLegalEntityAsync()
    {
        var clients = await clientRepository.GetAllLegalEntityAsync();
        return mapper.Map<IEnumerable<LegalEntityReadDto>>(clients);
    }

    public async Task<IEnumerable<IndividualEntrepreneurReadDto>> GetAllIndividualEntrepreneurAsync()
    {
        var clients = await clientRepository.GetAllIndividualEntrepreneurAsync();
        return mapper.Map<IEnumerable<IndividualEntrepreneurReadDto>>(clients);
    }


    public async Task<ClientReadDto?> GetByIdAsync(Guid id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        return client != null ? mapper.Map<ClientReadDto>(client) : null;
    }

    public async Task<Result<ClientReadDto>> CreateLegalEntityAsync(LegalEntityCreateDto dto)
    {
        if (await clientRepository.TaxIdExistsAsync(dto.TaxId))
            return Result.Fail("Клиент с таким ИНН уже существует");

        var legalEntity = mapper.Map<LegalEntity>(dto);
        legalEntity.Type = ClientType.LegalEntity;

        await clientRepository.AddAsync(legalEntity);
        await unitOfWork.CommitAsync();

        return mapper.Map<LegalEntityReadDto>(legalEntity);
    }

    public async Task<Result<ClientReadDto>> CreateIndividualEntrepreneurAsync(IndividualEntrepreneurCreateDto dto)
    {
        if (await clientRepository.TaxIdExistsAsync(dto.TaxId))
            return Result.Fail<ClientReadDto>("Клиент с таким ИНН уже существует");

        var entrepreneur = mapper.Map<IndividualEntrepreneur>(dto);
        entrepreneur.Type = ClientType.IndividualEntrepreneur;

        await clientRepository.AddAsync(entrepreneur);
        await unitOfWork.CommitAsync();

        return mapper.Map<IndividualEntrepreneurReadDto>(entrepreneur);
    }

    public async Task<Result<ClientReadDto>> UpdateAsync(ClientUpdateDto dto)
    {
        var client = await clientRepository.GetByIdAsync(dto.Id);
        if (client == null)
            return Result.Fail<ClientReadDto>("Клиент не найден");

        mapper.Map(dto, client);
        clientRepository.Update(client);
        await unitOfWork.CommitAsync();

        return mapper.Map<ClientReadDto>(client);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client == null)
            return Result.Fail("Клиент не найден");

        clientRepository.Delete(client);
        await unitOfWork.CommitAsync();

        return Result.Ok();
    }
}