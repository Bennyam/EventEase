using System.ComponentModel.DataAnnotations;
using EventEase.Models;
using Microsoft.Extensions.Logging;

namespace EventEase.Services;

public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;

    private readonly List<Event> _events = new()
    {
        new Event { Id = 1, Name = "Tech Summit",   Date = DateTime.Today.AddDays(7),  Location = "Brussels" },
        new Event { Id = 2, Name = "Marketing Day", Date = DateTime.Today.AddDays(14), Location = "Antwerp"  },
        new Event { Id = 3, Name = "HR Connect",    Date = DateTime.Today.AddDays(21), Location = "Ghent"    },
    };

    private int _nextId;

    public EventService(ILogger<EventService> logger)
    {
        _logger = logger;
        _nextId = _events.Any() ? _events.Max(e => e.Id) + 1 : 1;
        _logger.LogInformation("EventService initialized with {Count} events. NextId={NextId}", _events.Count, _nextId);
    }

    public List<Event> GetAll()
    {
        var list = _events.OrderBy(e => e.Date).ToList();
        _logger.LogDebug("GetAll returned {Count} events", list.Count);
        return list;
    }

    public Event? GetById(int id)
    {
        var found = _events.FirstOrDefault(e => e.Id == id);
        _logger.LogDebug(found is null ? "GetById({Id}) not found" : "GetById({Id}) found {@Event}", id, found);
        return found;
    }

    public void Update(Event updated)
    {
        if (updated is null) throw new ArgumentNullException(nameof(updated));
        Validate(updated, isNew: false);

        var idx = _events.FindIndex(e => e.Id == updated.Id);
        if (idx >= 0)
        {
            _events[idx].Name     = updated.Name.Trim();
            _events[idx].Date     = updated.Date;
            _events[idx].Location = updated.Location.Trim();

            _events.Sort((a, b) => a.Date.CompareTo(b.Date));
            _logger.LogDebug("Updated event id {Id}: {@Event}", updated.Id, _events[idx]);
        }
        else
        {
            _logger.LogWarning("Update called for missing id {Id}", updated.Id);
        }
    }

    public void Add(Event e)
    {
        if (e is null) throw new ArgumentNullException(nameof(e));
        Validate(e, isNew: true);

        var newEvent = new Event
        {
            Id       = _nextId++,
            Name     = e.Name.Trim(),
            Date     = e.Date,
            Location = e.Location.Trim()
        };

        _events.Add(newEvent);
        _events.Sort((a, b) => a.Date.CompareTo(b.Date));
        _logger.LogDebug("Added event {@Event}. NextId now {NextId}", newEvent, _nextId);
    }

    public void Delete(int id)
    {
        var e = _events.FirstOrDefault(x => x.Id == id);
        if (e is not null)
        {
            _events.Remove(e);
            _logger.LogDebug("Deleted event id {Id}", id);
        }
        else
        {
            _logger.LogWarning("Delete called for missing id {Id}", id);
        }
    }

    private static void Validate(Event e, bool isNew)
    {
        if (string.IsNullOrWhiteSpace(e.Name))
            throw new ValidationException("Event name is required.");
        if (string.IsNullOrWhiteSpace(e.Location))
            throw new ValidationException("Event location is required.");
        if (!isNew && e.Id <= 0)
            throw new ValidationException("Valid id required for updates.");
    }
}
