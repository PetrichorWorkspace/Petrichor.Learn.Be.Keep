using Keep.Driving.Users.ForAuth0.RestApi;

namespace Keep.Driving;

public static class EndpointRegistration
{
    public static void RegisterMinimalApis(this WebApplication app)
    {
        RegisterUserFromAuth0.Map(app);
    }
}