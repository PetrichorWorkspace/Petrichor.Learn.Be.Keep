using Keep.Driving.ForAuth0.RestApi.Users;
using Keep.Driving.ForKeepFe.RestApi.Notes;

namespace Keep.Driving;

// TODO make it better
public static class EndpointRegistration
{
    public static void RegisterMinimalApis(this WebApplication app)
    {
        RegisterUser.Map(app);
        CreateNote.Map(app);
    }
}
