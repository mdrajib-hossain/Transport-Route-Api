var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();








/////  Perfrom CRUD Opteration

List<Category> Categories = new List<Category>();


/// Read Data MapGet() "/api/categories"


app.MapGet("/", () =>
{
    return Results.Content("<h1>Green University Transport Management System</h1>", "text/html");

});


app.MapGet("/home", () => {

    var response = new
    {
        Title = "Green University Transport Management System",
        Developer = "MD. Rajab Hossain",
        Tools = "ASP .Net Core Web API"
    };


    return Results.Ok(response);


});


app.MapGet("/api/categories", () =>
{
    return Results.Ok(Categories);
});


/// Creat MapPost() "/api/categories"


app.MapPost("/api/categories", () =>
{
    var DefualtCategoryData = new Category
    {
        CategoryId = Guid.Parse("d03def7d-e45d-4431-95c1-2a8cb3668561"),
        Name = "Electronic",
        Description = "The Eelectornic category encompasses all products involved in the design, manufacturing, and use of electronic devices, including components like semiconductors, and consumer electronics like mobile phones, computers, and accessories. This category is often used for retail and involves everything from basic electrical components to fully assembled systems",
        CreatedAt = new DateOnly()

    };

    var CategoryData = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = "Electronic",
        Description = "The Eelectornic category encompasses all products involved in the design, manufacturing, and use of electronic devices, including components like semiconductors, and consumer electronics like mobile phones, computers, and accessories. This category is often used for retail and involves everything from basic electrical components to fully assembled systems",
        CreatedAt = new DateOnly()

    };

    Categories.Add(DefualtCategoryData);
    Categories.Add(CategoryData);

    return Results.NoContent();
    

});


/// Update MapPut() "/api/categories"

app.MapPut("/api/categories", () =>
{

    var foundGuid = Categories.FirstOrDefault(id => id.CategoryId == Guid.Parse("d03def7d-e45d-4431-95c1-2a8cb3668561"));

    if(foundGuid == null)
    {
        return Results.NotFound("Record Not Found!!!!");
    }
    else
    {
        foundGuid.Name = "Electronic Pro";
        foundGuid.Description = "Updated the Description session";
        foundGuid.CreatedAt = new DateOnly();

        return Results.Ok("The Category Updated Successfuly.");
    }


});


/// Delete MapDelete() "/api/categories"

app.MapDelete("/api/categories", () =>
{
    var foundGuid = Categories.FirstOrDefault(id => id.CategoryId == Guid.Parse("d03def7d-e45d-4431-95c1-2a8cb3668561"));

    if (foundGuid == null) {
        return Results.NotFound("This item Not Found");
    }
    else
    {
        Categories.Remove(foundGuid);

        return Results.Content("Delete Successfuly....");
    }


});





app.Run();



public record Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly CreatedAt { get; set; }
}