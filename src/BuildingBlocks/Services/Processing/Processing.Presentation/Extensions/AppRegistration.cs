namespace Processing.Presentation.Extensions;

public static class AppRegistration
{
    public static void AddPresentationApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) app.MapOpenApi();
        app.UseHttpsRedirection();
        app.Run();
    }
}