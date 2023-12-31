﻿namespace Domain.Entities;

public class User : BaseEntity<int>
{
    private User()
    {
        UserName = string.Empty;
        Password = string.Empty;
        Email = string.Empty;
        UserProfile = new UserProfile(string.Empty, string.Empty, string.Empty);
    }

    public User(string userName, string password, string email, UserProfile userProfile)
    {
        UserName = userName ?? throw new ArgumentNullException($"User {nameof(userName)} cannot be null or empty");
        Password = password ?? throw new ArgumentNullException($"User {nameof(password)} cannot be null or empty");
        Email = email ?? throw new ArgumentNullException($"User {nameof(email)} cannot be null or empty");
        IsActive = true;
        UserProfile = userProfile ?? throw new ArgumentNullException($"User {nameof(userProfile)} cannot be null or empty");
    }

    public string UserName { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }

    public UserProfile UserProfile { get; set; }

    public void ChangeDetails(string userName, string password, string email, string firstName, string lastName, string personalNumber)
    {
        UserName = userName ?? throw new ArgumentNullException($"User {nameof(userName)} cannot be null or empty");
        Password = password ?? throw new ArgumentNullException($"User {nameof(password)} cannot be null or empty");
        Email = email ?? throw new ArgumentNullException($"User {nameof(email)} cannot be null or empty");
        UserProfile.ChangeDetails(firstName, lastName, personalNumber);
    }

    public void DeactivateUser()
    {
        IsActive = false;
    }

    public void ActivateUser()
    {
        IsActive = true;
    }
}