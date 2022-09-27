using AB.BeerQuest.CsvHelper;
using AB.BeerQuest.DAL;
using AB.BeerQuest.DAL.Repositories;
using AB.BeerQuest.DataModel;
using AB.BeerQuest.DataModel.Enums;
using AB.BeerQuest.Dto;
using AutoMapper;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Configure Web Api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Razor Pages / MVC
builder.Services.AddRazorPages();

// Configure Persistance
builder.Services.AddDbContext<VenueDb>(opt => opt.UseInMemoryDatabase("VenueDb"));
builder.Services.AddScoped<IVenueRepository, VenueRepository>();

// Configure Automapper
var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<VenueFiltersDto, VenueFilters>();
    cfg.CreateMap<Venue, VenueDto>()
        .ForMember(dto => dto.Tags, o => o.MapFrom(v => v.Tags.Select(t => t.Tag)));
});
builder.Services.AddScoped<IMapper>(s => new Mapper(autoMapperConfig));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Build Application
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

// Web Api Definition
app.MapGet("/Venues",
    async (SortOrder? sortOrder, IVenueRepository venueRepository, IMapper mapper) =>
    {
        var venues = await venueRepository.GetAll(sortOrder);
        return venues.Select(mapper.Map<VenueDto>);
    });
app.MapGet("/Venues/{id}",
    async (int id, IVenueRepository venueRepository, IMapper mapper) =>
    {
        return await venueRepository.GetById(id)
            is Venue venue
                ? Results.Ok(mapper.Map<VenueDto>(venue))
                : Results.NotFound();
    });
app.MapGet("/Venues/Tags",
    async (IVenueRepository venueRepository) =>
    {
        return await venueRepository.GetActiveTags();
    });
app.MapPost("/Venues/Filter",
    async (IVenueRepository venueRepository, VenueFiltersDto filters, IMapper mapper) =>
    {
        var venues = await venueRepository.GetByFilters(mapper.Map<VenueFilters>(filters));
        return venues.Select(mapper.Map<VenueDto>);
    });

// Statup Tasks - Seed the database with our set of venue data.
await ImportVenues();

// Run Application
app.Run();

async Task ImportVenues()
{
    using var reader = new StreamReader(Path.GetFullPath("Content/leedsbeerquest.csv"));
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
    csv.Context.RegisterClassMap<VenueMap>();
    using var scope = app.Services.CreateScope();
    var venueRepository = scope.ServiceProvider.GetRequiredService<IVenueRepository>();
    var venues = csv.GetRecords<Venue>();
    await venueRepository.AddRange(venues);
}