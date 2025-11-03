using Microsoft.AspNetCore.Mvc;

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

app.MapGet("/api/categories/search", ([FromQuery] string Name="") =>
{

    //var foundValue = Categories.FirstOrDefault(c => c.Name == Name );

    //var foundValue = Categories.Contains(Name, StringComparer.OrdinalIgnoreCase)

    if (!string.IsNullOrEmpty(Name))
    {
       var searchFound = Categories.Where(c => c.Name.Contains(Name, StringComparison.OrdinalIgnoreCase)).ToList();

        return Results.Ok(searchFound);

        //return Results.Ok(foundValue);
    }
    else
    {
        return Results.NotFound($"{Name} is Not Found");
    }
    
});


/// Creat MapPost() "/api/categories"


app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{


    //var CategoryData = new Category
    //{
    //    CategoryId = Guid.NewGuid(),
    //    Name = "Electronic",
    //    Description = "The Eelectornic category encompasses all products involved in the design, manufacturing, and use of electronic devices, including components like semiconductors, and consumer electronics like mobile phones, computers, and accessories. This category is often used for retail and involves everything from basic electrical components to fully assembled systems",
    //    CreatedAt = new DateOnly()

    //};

    var CategoryData = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description,
        CreatedAt = new DateOnly(),
        UpdateAt = new DateOnly()
        

    };

    Categories.Add(CategoryData);

    //return Results.Created(CategoryData,"text/html");
    return Results.Ok(CategoryData);
    

});





/// Update MapPut() "/api/categories"

app.MapPut("/api/categories/{categoryID}", (Guid categoryID, [FromBody] Category categoryData) =>
{

    //var foundGuid = Categories.FirstOrDefault(id => id.CategoryId == Guid.Parse("d03def7d-e45d-4431-95c1-2a8cb3668561"));
    var foundGuid = Categories.FirstOrDefault(listdata => listdata.CategoryId == categoryID);

    if(foundGuid != null)
    {
        

        foundGuid.Name = categoryData.Name;
        foundGuid.Description = categoryData.Description;
        foundGuid.UpdateAt = DateOnly.FromDateTime(DateTime.Now);



        return Results.Ok("The Category Updated Successfuly.");

    }
    else
    {       

        return Results.NotFound("Record Not Found!!!!");
    }


});



/// Delete MapDelete() "/api/categories"

app.MapDelete("/api/categories/{categoryID}", (Guid categoryID) =>
{
    var foundGuid = Categories.FirstOrDefault(id => id.CategoryId == categoryID);

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
    public DateOnly UpdateAt { get; set; }
}