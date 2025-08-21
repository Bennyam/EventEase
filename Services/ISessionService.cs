namespace EventEase.Services;

public interface ISessionService
{
    string? CurrentUserName { get; }
    string? CurrentUserEmail { get; }
    int? LastRegisteredEventId { get; }

    event Action? OnChange;

    void SetUser(string name, string email, int? lastEventId = null);
    void Clear();
}
