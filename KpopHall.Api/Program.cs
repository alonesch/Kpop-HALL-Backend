//using KpopHall.Infrastructure.Persistence.Fakes;
using KpopHall.Application.Albums.CreateAlbum;
using KpopHall.Application.Albums.GetAlbum;
using KpopHall.Application.Albums.ListAlbums;
using KpopHall.Application.Artists.CreateArtist;
using KpopHall.Application.Artists.GetArtistById;
using KpopHall.Application.Artists.ListArtists;
using KpopHall.Application.Artists.UpdateArtists;
using KpopHall.Application.Auth.Login;
using KpopHall.Application.Auth.Register;
using KpopHall.Application.Interfaces;
using KpopHall.Application.Members.CreateMember;
using KpopHall.Application.Members.GetMember;
using KpopHall.Application.Members.ListMember;
using KpopHall.Application.Photocards.CreatePhotocard;
using KpopHall.Application.Photocards.GetPhotocard;
using KpopHall.Application.Photocards.ListPhotocard;
using KpopHall.Infrastructure.Persistence;
using KpopHall.Infrastructure.Persistence.Repositories;
using KpopHall.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

//DI Db
builder.Services.AddDbContext<KpopHallDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<ListArtistsUseCase>();
builder.Services.AddScoped<GetArtistByIdUseCase>();
builder.Services.AddScoped<CreateArtistUseCase>();
builder.Services.AddScoped<IAlbumsRepository, AlbumsRepository>();
builder.Services.AddScoped<CreateAlbumUseCase>();
builder.Services.AddScoped<ListAlbumsUseCase>();
builder.Services.AddScoped<GetAlbumByIdUseCase>();
builder.Services.AddScoped<IPhotoCardsRepository, PhotocardsRepository>();
builder.Services.AddScoped<CreatePhotocardUseCase>();
builder.Services.AddScoped<ListPhotocardUseCase>();
builder.Services.AddScoped<GetPhotocardUseCase>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IArtistsRepository, ArtistsRepository>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<UpdateArtistUseCase>();
builder.Services.AddScoped<IMembersRepository, MembersRepository>();
builder.Services.AddScoped<CreateMemberUseCase>();
builder.Services.AddScoped<ListMemberUseCase>();
builder.Services.AddScoped<GetMemberByIdUseCase>();

///<summary>
///If you're testing on localhost and don't want to create a database, 
///inject fake databases (copy the test databases into the persistence module and inject them).
///</summary>

//DI Fake repos
//builder.Services.AddSingleton<IArtistsRepository, FakeArtistRepository>();
//builder.Services.AddSingleton<IAlbumsRepository, FakeAlbumRepository>();
//builder.Services.AddSingleton<IPhotoCardsRepository, FakePhotocardRepository>();
//builder.Services.AddSingleton<IUsersRepository, FakeUserRepository>();
//builder.Services.AddSingleton<IUsersRepository, FakeUserRepository>();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "https://kpop-hall.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();

//builder
var app = builder.Build();

app.UseMiddleware<KpopHall.Api.Middlewares.GlobalExceptionMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
