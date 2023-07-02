using PMS.Api;
using PMS.Application;
using PMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();
    
    

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}


var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

