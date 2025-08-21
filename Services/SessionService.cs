namespace EventEase.Services;

public class SessionService : ISessionService
{
    public string? CurrentUserName { get; private set; }
    public string? CurrentUserEmail { get; private set; }
    public int? LastRegisteredEventId { get; private set; }

    public event Action? OnChange;

    public void SetUser(string name, string email, int? lastEventId = null)
    {
        CurrentUserName = name;
        CurrentUserEmail = email;
        LastRegisteredEventId = lastEventId;
        OnChange?.Invoke();
    }

    public void Clear()
    {
        CurrentUserName = null;
        CurrentUserEmail = null;
        LastRegisteredEventId = null;
        OnChange?.Invoke();
    }
}
