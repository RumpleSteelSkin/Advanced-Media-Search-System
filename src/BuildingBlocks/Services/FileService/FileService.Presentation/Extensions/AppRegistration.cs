namespace FileService.Presentation.Extensions;

public static class AppRegistration
{
    public static void AddPresentationApp(this WebApplication app)
    {
        #region Development

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        #endregion

        #region Other Services

        app.UseHttpsRedirection();
        app.MapControllers();

        #endregion

        app.Run();
    }
}