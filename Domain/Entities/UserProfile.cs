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

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }

    public User? User { get; set; }
}