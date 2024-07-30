using CuddleCompanions.Domain.Common;
using CuddleCompanions.Domain.Enums;
using CuddleCompanions.Domain.Errors;
using CuddleCompanions.Domain.ValueObjects;
using ErrorOr;

namespace CuddleCompanions.Domain.Entities;

public class Pet : Entity
{
    public string Name { get; private set; }
    public Specie Specie { get; private set; }
    public string Breed { get; private set; }
    public PetAge Age { get; private set; }
    public Gender Gender { get; private set; }
    public PetStatus PetStatus { get; private set; }
    public DateTime DateArrived { get; private set; }
    public string Description { get; private set; }

    private Pet() { }

    private Pet(Guid id, string name, Specie specie, string breed, PetAge age,
       Gender gender, PetStatus petStatus, DateTime dateArrived, string description) : base(id)
    {
        Name = name;
        Specie = specie;
        Breed = breed;
        Age = age;
        Gender = gender;
        PetStatus = petStatus;
        DateArrived = dateArrived;
        Description = description;
    }

    public static ErrorOr<Pet> Create(Guid id, string name, Specie specie, string breed, PetAge age,
    Gender gender, DateTime dateArrived, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return PetErrors.NameEmpty;
        }
        if (name.Length > 50)
        {
            return PetErrors.TooLong(nameof(name));
        }
        if (description.Length > 500)
        {
            return PetErrors.TooLong(nameof(description));
        }

        return new Pet(
            id,
            name,
            specie,
            breed,
            age,
            gender,
            PetStatus.Available,
            dateArrived,
            description);
    }

    public ErrorOr<Success> ChangeStatus(PetStatus newStatus)
    {     
        PetStatus = newStatus;
        return Result.Success;
    }
}

