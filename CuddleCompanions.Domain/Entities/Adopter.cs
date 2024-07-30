using CuddleCompanions.Domain.Common;
using CuddleCompanions.Domain.Enums;
using CuddleCompanions.Domain.Errors;
using CuddleCompanions.Domain.Events;
using CuddleCompanions.Domain.ValueObjects;
using ErrorOr;

namespace CuddleCompanions.Domain.Entities;

public class Adopter : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public Address Address { get; private set; }

    private readonly List<AdoptionRecord> _adoptionRecords = new();
    public IReadOnlyCollection<AdoptionRecord> AdoptionRecords => _adoptionRecords;

    private Adopter() { }

    private Adopter(Guid id, string firstName, string lastName, Email email,
        Phone phone, Address address) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;

    }
    public static Adopter Create(Guid id, string firstName, string lastName, Email email,
       Phone phone, Address address)
    {
        var adopter = new Adopter(id, firstName, lastName, email, phone, address);

        adopter.Raise(new AdopterCreatedEvent(adopter.Email, firstName, lastName));
        return adopter;
    }

    public ErrorOr<AdoptionRecord> InitiateAdoption(Guid petId)
    {
        if (_adoptionRecords.Where(x => x.Status == AdoptionStatus.Pending).Any())
        {
            return AdopterErrors.PendingAdoptionsExist;
        }

        var adoptionRecord = new AdoptionRecord(
            Guid.NewGuid(),
            Id,
            petId,
            DateTime.Now,
            AdoptionStatus.Pending
            );

        _adoptionRecords.Add(adoptionRecord);

        Raise(new AdoptionInitiatedEvent(petId));

        return adoptionRecord;
    }

    public ErrorOr<AdoptionRecord> CompleteAdoption(Guid adoptionRecordId)
    {
        var adoptionRecord = _adoptionRecords.FirstOrDefault(x => x.Id == adoptionRecordId);
        if (adoptionRecord is null)
        {
            return AdopterErrors.AdoptionRecordNotFound(adoptionRecordId);
        }

        var adoptionRecordResult = adoptionRecord.CompleteAdoption();
        if (adoptionRecordResult.IsError)
        {
            return adoptionRecordResult.Errors;
        }

        Raise(new AdoptionCompletedEvent(adoptionRecord.PetId));

        return adoptionRecord;
    }

    public ErrorOr<AdoptionRecord> CancelAdoption(Guid adoptionRecordId)
    {
        var adoptionRecord = _adoptionRecords.FirstOrDefault(x => x.Id == adoptionRecordId);
        if (adoptionRecord is null)
        {
            return AdopterErrors.AdoptionRecordNotFound(adoptionRecordId);
        }

        var adoptionRecordResult = adoptionRecord.CancelAdoption();
        if (adoptionRecordResult.IsError)
        {
            return adoptionRecordResult.Errors;
        }

        Raise(new AdoptionCanceledEvent(adoptionRecord.PetId));

        return adoptionRecord;
    }
}
