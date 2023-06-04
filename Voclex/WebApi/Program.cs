using System.Text.Json.Serialization;
using Application.DataAccess;
using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using Application.Services;
using Application.Services.Factories.Interfaces;
using Application.Services.Interfaces;
using Infrastructure.Filters;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Services.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.Services.Interfaces;
using WebApi.Binding;
using WebApi.Constants;
using WebApi.Filters;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(c => c.WithOrigins("http://localhost:5181", "https://localhost:7181").AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<DictionaryIdValidationActionFilter>();
        options.ModelBinderProviders.Insert(0, new UserIdBinderProvider());
    })
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureUserIdInputFormatter>();

builder.Services.AddEndpointsApiExplorer();

AddSwagger(builder);

AddDbContext(builder);

AddAuthentication(builder);

builder.Services.AddAuthorization(o =>
    o.AddPolicy(Policies.Admin, 
        b => 
            b.RequireAuthenticatedUser().RequireClaim("role", Role.Admin.ToString())));

builder.Services.AddAutoMapper(typeof(ApplicationDbContext));

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<TermProgressService>();
builder.Services.AddScoped<ICrudService<Term, TermDto>, GenericCrudService<Term, TermDto>>();
builder.Services.AddScoped<ICrudService<TermsDictionary, TermsDictionaryDto>, GenericCrudService<TermsDictionary, TermsDictionaryDto>>();
builder.Services.AddScoped<ICrudService<Definition, DefinitionDto>, GenericCrudService<Definition, DefinitionDto>>();
builder.Services.AddScoped<ICrudService<Example, ExampleDto>, GenericCrudService<Example, ExampleDto>>();
builder.Services.AddScoped<ICrudService<Picture, PictureDto>, GenericCrudService<Picture, PictureDto>>();
builder.Services.AddScoped<ICrudService<ExceptionLog, IExceptionLogDto>, GenericCrudService<ExceptionLog, IExceptionLogDto>>();
builder.Services.AddScoped<IGetListService<Term, TermDto>, GenericGetListService<Term, TermDto>>();
builder.Services.AddScoped<IGetListService<TermsDictionary, TermsDictionaryDto>, GenericGetListService<TermsDictionary, TermsDictionaryDto>>();
builder.Services.AddScoped<ITermRelatedService<DefinitionDto>, TermRelatedService<Definition, DefinitionDto>>();
builder.Services.AddScoped<ITermRelatedService<ExampleDto>, TermRelatedService<Example, ExampleDto>>();
builder.Services.AddScoped<ITermRelatedService<PictureDto>, TermRelatedService<Picture, PictureDto>>();
builder.Services.AddScoped<IFileSavingServiceFactory, DiskFileSavingServiceFactory>();
builder.Services.AddScoped<ICrudService<Picture, IPictureDto>, GenericCrudService<Picture, IPictureDto>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<PicturesService>();
builder.Services.AddScoped<ExceptionLogService>();
builder.Services.AddScoped<IAuthTokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

var app = builder.Build();

UseForwardedFromReverseProxyHeaders(app);

await SeedDevelopmentDb(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseStaticFiles();


Directory.CreateDirectory(DiskFileSavingServiceFactory.PicturesFolderPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(DiskFileSavingServiceFactory.PicturesFolderPath),
    RequestPath = DiskFileSavingServiceFactory.PicturesEndpointFolderPath.TrimEnd('/')
});

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();


async Task SeedDevelopmentDb(WebApplication app)
{
	using var scope = app.Services.CreateScope();
    await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await ApplicationDbContextInitializer.MigrateDb(context);

    if (app.Environment.IsDevelopment())
    {
	    await ApplicationDbContextInitializer.SeedDbIfNeeded(context, scope);
    }
}

void AddSwagger(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.OperationFilter<PropertyHidingFilter>();
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
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });
}

void AddAuthentication(WebApplicationBuilder builder1)
{
    builder1.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    "!SomethingSecret!"u8.ToArray() //todo store safely
                ),
            };
            options.MapInboundClaims = false;
        });
}

void AddDbContext(WebApplicationBuilder webApplicationBuilder1)
{
	webApplicationBuilder1.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseNpgsql(webApplicationBuilder1.Configuration["ConnectionStrings:DefaultConnection"]));
		//options.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")));
	webApplicationBuilder1.Services.AddScoped<IDbContext>(provider =>
		provider.GetRequiredService<ApplicationDbContext>());
}

void UseForwardedFromReverseProxyHeaders(WebApplication webApplication)
{
	var forwardedHeadersOptions = new ForwardedHeadersOptions
	{
		ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
		RequireHeaderSymmetry = false
	};
	forwardedHeadersOptions.KnownNetworks.Clear();
	forwardedHeadersOptions.KnownProxies.Clear();

	webApplication.UseForwardedHeaders(forwardedHeadersOptions);
}