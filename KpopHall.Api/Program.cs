using KpopHall.Application.Albums.CreateAlbum;
using KpopHall.Application.Albums.GetAlbum;
using KpopHall.Application.Albums.ListAlbums;
using KpopHall.Application.Artists.CreateArtist;
using KpopHall.Application.Artists.GetArtistById;
using KpopHall.Application.Artists.ListArtists;
using KpopHall.Application.Auth.Login;
using KpopHall.Application.Auth.Register;
using KpopHall.Application.Interfaces;
using KpopHall.Application.Photocards.CreatePhotocard;
using KpopHall.Application.Photocards.GetPhotocard;
using KpopHall.Application.Photocards.ListPhotocard;
using KpopHall.Infrastructure.Persistence.Fakes;
using KpopHall.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);


// Dependency Injection
builder.Services.AddScoped<ListArtistsUseCase>();
builder.Services.AddScoped<GetArtistByIdUseCase>();
builder.Services.AddSingleton<IArtistRepository, FakeArtistRepository>();
builder.Services.AddScoped<CreateArtistUseCase>();
builder.Services.AddSingleton<IAlbumRepository, FakeAlbumRepository>();
builder.Services.AddScoped<CreateAlbumUseCase>();
builder.Services.AddScoped<ListAlbumsUseCase>();
builder.Services.AddScoped<GetAlbumByIdUseCase>();
builder.Services.AddSingleton<IPhotoCardRepository, FakePhotocardRepository>();
builder.Services.AddScoped<CreatePhotocardUseCase>();
builder.Services.AddScoped<ListPhotocardUseCase>();
builder.Services.AddScoped<GetPhotocardUseCase>();
builder.Services.AddSingleton<IUserRepository, FakeUserRepository>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<IUserRepository, FakeUserRepository>();

builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUseCase>();

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("Configuration value 'Jwt:Key' is missing. Set it in appsettings.json or environment variables.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});


builder.Services.AddControllers();

//builder
var app = builder.Build();
 
app.UseMiddleware<KpopHall.Api.Middlewares.GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
