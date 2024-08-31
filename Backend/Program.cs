using JobApplicationTracker.Models;
using JobApplicationTracker.Repository;
using JobApplicationTracker.Repository.Interface;
using JobApplicationTracker.Utility;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var corsPolicyName = "CustomCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName,
        policy =>
        {
            policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine("CUSTOM LOG | Is build environment development ? => " + builder.Environment.IsDevelopment());
var configurations = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettings>(
    File.ReadAllText(builder.Environment.IsDevelopment() ? "appsettings-dev.json" : "appsettings.json"));
if (!builder.Environment.IsDevelopment())
{
    configurations.DbConnectionString = UtilityMethods.GetDbUrlFromAWS();
    Console.WriteLine($"APP | Update configurations: {JsonConvert.SerializeObject(configurations)}");
}

builder.Services.AddSingleton(configurations);
builder.Services.AddSingleton<IJobApplicationRepository, JobApplicationRepositoryMySql>();
builder.Services.AddAutoMapper(typeof(DatabaseMappers));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(corsPolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

new Task(()=>UtilityMethods.SendSnsNotification()).Start();

app.Run();
