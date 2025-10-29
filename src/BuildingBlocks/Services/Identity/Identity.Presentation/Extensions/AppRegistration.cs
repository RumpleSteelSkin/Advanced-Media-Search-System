namespace Identity.Presentation.Extensions;

public static class AppRegistration
{
    public static void AddPresentationApp(this WebApplication app)
    {
        #region Development

        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        #endregion

        #region Other Services

        app.UseCors("AllowAll");
        app.UseRouting();
        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        #endregion

        app.Run();
    }
}