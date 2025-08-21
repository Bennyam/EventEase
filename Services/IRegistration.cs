using EventEase.Models;

namespace EventEase.Services;

public interface IRegistrationService
{
    Registration Register(Registration reg);           
    int CountByEvent(int eventId);                    
    IReadOnlyList<Registration> GetByEvent(int eventId);
    IReadOnlyList<Registration> GetAll();
}
