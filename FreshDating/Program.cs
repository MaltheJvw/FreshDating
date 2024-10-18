using FreshDating.Components;
using Microsoft.EntityFrameworkCore;
using FreshDating.Data;
using FreshDating.Services;

namespace FreshDating
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ProfileService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IGenderService, GenderService>();
            builder.Services.AddScoped<FilterService>();
            builder.Services.AddSingleton<ProfileStateService>();

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Seed the database with initial data
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await DataGenerator.SeedUsersAndProfilesAsync(dbContext);
            }

            app.Run();
        }
    }
}
