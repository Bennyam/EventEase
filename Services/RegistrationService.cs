using EventEase.Models;
using Microsoft.Extensions.Logging;

namespace EventEase.Services;

public class RegistrationService : IRegistrationService
{
    private readonly ILogger<RegistrationService> _logger;
    private readonly List<Registration> _registrations = new();
    private int _nextId = 1;

    public RegistrationService(ILogger<RegistrationService> logger)
    {
        _logger = logger;
    }

    public Registration Register(Registration reg)
    {
        if (string.IsNullOrWhiteSpace(reg.AttendeeName)) throw new ArgumentException("Name required");
        if (string.IsNullOrWhiteSpace(reg.Email)) throw new ArgumentException("Email required");
        if (reg.EventId <= 0) throw new ArgumentException("Invalid EventId");

        var final = new Registration
        {
            Id = _nextId++,
            EventId = reg.EventId,
            AttendeeName = reg.AttendeeName.Trim(),
            Email = reg.Email.Trim(),
            RegisteredAt = DateTime.UtcNow
        };
        _registrations.Add(final);
        _logger.LogInformation("Registered {@Reg}", final);
        return final;
    }

    public int CountByEvent(int eventId) => _registrations.Count(r => r.EventId == eventId);

    public IReadOnlyList<Registration> GetByEvent(int eventId) =>
        _registrations.Where(r => r.EventId == eventId)
                      .OrderByDescending(r => r.RegisteredAt)
                      .ToList();

    public IReadOnlyList<Registration> GetAll() =>
        _registrations.OrderByDescending(r => r.RegisteredAt).ToList();
}
