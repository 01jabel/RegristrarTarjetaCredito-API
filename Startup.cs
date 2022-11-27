using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.SqlServer;



namespace FBTarjetas 

{ 

  public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }
    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FBTarjetas", Version = "v1" });
        });

            services.AddDbContext<AplicationDbContext>(options =>
                          options.UseSqlServer(configRoot.GetConnectionString("DevConnection")));
            services.AddCors(options => options.AddPolicy("AllowWebApp",
                                   builder => builder.AllowAnyOrigin()
                                                     .AllowAnyHeader()
                                                     .AllowAnyMethod()));  
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!app.Environment.IsDevelopment())
        {
           
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FBTarjetas v1"));
        }
            app.UseCors("AllowWebApp");
        }
}
}
