namespace Domain.Entities;

public class UserProfile : BaseEntity<int>
{
    private UserProfile()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        PersonalNumber = string.Empty;
    }

    public UserProfile(string firstName, string lastName, string personalNumber)
    {
        FirstName = firstName ?? throw new ArgumentNullException($"User {nameof(firstName)} cannot be null or empty");
        LastName = lastName ?? throw new ArgumentNullException($"User {nameof(lastName)} cannot be null or empty");
        PersonalNumber = personalNumber ?? throw new ArgumentNullException($"User {nameof(personalNumber)} cannot be null or empty");
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PersonalNumber { get; private set; }

    public User? User { get; set; }

    public void ChangeDetails(string firstName, string lastName, string personalNumber)
    {
        FirstName = firstName ?? throw new ArgumentNullException($"User {nameof(firstName)} cannot be null or empty");
        LastName = lastName ?? throw new ArgumentNullException($"User {nameof(lastName)} cannot be null or empty");
        PersonalNumber = personalNumber ?? throw new ArgumentNullException($"User {nameof(personalNumber)} cannot be null or empty");
    }
}