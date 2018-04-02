using Entites.Request;

namespace InterfaceApi.Services
{
    public interface IRouteServices
    {
        GenericResponse ServiceDispatcher(GenericRequest request);
    }
}
