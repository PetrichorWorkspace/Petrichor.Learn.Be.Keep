using Keep.Driving.ForAuth0.RestApi.Users;

namespace Keep.Driving;

public static class EndpointRegistration
{
    public static void RegisterMinimalApis(this WebApplication app)
    {
        RegisterUserFromAuth0.Map(app);
    }
}