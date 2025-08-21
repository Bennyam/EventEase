using EventEase.Models;

namespace EventEase.Services;

public interface IEventService
{
    List<Event> GetAll();
    Event? GetById(int id);
    void Update(Event updated);
    void Add(Event e);
    void Delete(int id);
}
