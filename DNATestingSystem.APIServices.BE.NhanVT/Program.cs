using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DNATestingSystem.APIServices.BE.NhanVT.Helpers;
using DNATestingSystem.Services.NhanVT;
using DNATestingSystem.Services.TienDM;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//NhanVT------------------------------------------------------------------------------------
builder.Services.AddScoped<IServicesNhanVTService, ServicesNhanVTService>();
builder.Services.AddScoped<IServicesCategoryNhanVTService, ServicesCategoryNhanVTService>();

// PhienNT Services
builder.Services.AddScoped<IAlleleResultsPhienNtService, AlleleResultsPhienNtService>();
builder.Services.AddScoped<IDnaTestsPhienNtService, DnaTestsPhienNtService>();
builder.Services.AddScoped<ILociPhienNtService, LociPhienNtService>();
builder.Services.AddScoped<ILocusMatchResultsPhienNtService, LocusMatchResultsPhienNtService>();

// ThinhLC Services
builder.Services.AddScoped<IProfileThinhLcService, ProfileThinhLcService>();
builder.Services.AddScoped<IProfileRelationshipThinhLcService, ProfileRelationshipThinhLcService>();
builder.Services.AddScoped<ISampleThinhLcService, SampleThinhLcService>();
builder.Services.AddScoped<ISampleTypeThinhLcService, SampleTypeThinhLcService>();

// HuyLHG Services
builder.Services.AddScoped<IBlogCategoriesHuyLhgService, BlogCategoriesHuyLhgService>();
builder.Services.AddScoped<IBlogsHuyLhgService, BlogsHuyLhgService>();

// GiapHD Services
builder.Services.AddScoped<IOrderGiapHdService, OrderGiapHdService>();
builder.Services.AddScoped<ITransactionsGiapHdService, TransactionsGiapHdService>();

// UserService
builder.Services.AddScoped<IUserServiceNhanVtService, UserServiceNhanVtService>();
//------------------------------------------------------------------------------------------

//TienDm------------------------------------------------------------------------------------
builder.Services.AddScoped<IAppointmentsTienDmService, AppointmentsTienDmService>();
builder.Services.AddScoped<IAppointmentStatusesTienDmService, AppointmentStatusesTienDmService>();
//------------------------------------------------------------------------------------------

builder.Services.AddScoped<ISystemUserAccountService, SystemUserAccountService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new DNATestingSystem.APIServices.BE.NhanVT.Helpers.JsonIgnoreVirtualMembersConverter());
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "DefaultIssuer",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "DefaultAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "defaultkeyfordevthisshouldbereplacedprod12345")),
            // ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    ////JWT Config
    option.DescribeAllParametersInCamelCase();
    option.ResolveConflictingActions(conf => conf.First());     // duplicate API name if any, ex: Get() & Get(string id)
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
