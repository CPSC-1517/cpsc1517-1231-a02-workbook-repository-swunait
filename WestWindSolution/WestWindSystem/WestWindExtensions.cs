using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WestWindSystem.BLL;
using WestWindSystem.DAL;

namespace WestWindSystem
{
    public static class WestWindExtensions
    {
        // This is an extension method that extends the IServiceCollection interface.
        // It is typically used in ASP.NET Core applications to configure and register services.

        // The method name can be anything, but it must match the name used when calling it in
        // your Program.cs file using builder.Services.XXXX(options => ...).
        public static void AddBackendDependencies(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            // Register the WestWindContext class, which is the DbContext for your application,
            // with the service collection. This allows the DbContext to be injected into other
            // parts of your application as a dependency.
            services.AddTransient<BuildVersionServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetRequiredService<WestWindContext>();
                return new BuildVersionServices(context);
            });

            services.AddTransient<CategoryServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetRequiredService<WestWindContext>();
                return new CategoryServices(context);
            });

            services.AddTransient<ProductServices>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetRequiredService<WestWindContext>();
                return new ProductServices(context);
            });


            // The 'options' parameter is an Action<DbContextOptionsBuilder> that typically
            // configures the options for the DbContext, including specifying the database
            // connection string.

            services.AddDbContext<WestWindContext>(options);

       
        }

    }
}