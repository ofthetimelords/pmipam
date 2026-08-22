using Microsoft.Extensions.FileProviders;

namespace Vtable.PmIpam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddEnvironmentVariables();
            Variables variables = builder.Configuration.Get<Variables>();
            builder.Services.Configure<Variables>(builder.Configuration);
            builder.Services.AddRazorPages();

            var app = builder.Build();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "static")),
                RequestPath = "/static"
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapRazorPages()
               .WithStaticAssets();

            builder.Logging.AddConsole();
            
            app.Run();
        }
    }
}