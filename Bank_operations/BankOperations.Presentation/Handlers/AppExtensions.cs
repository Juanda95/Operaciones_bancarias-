namespace BankOperations.Presentation.Handlers
{
    public static class AppExtensions
    {
        public static void useErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
