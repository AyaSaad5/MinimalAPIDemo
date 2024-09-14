using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IBookServices, BookServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Hello World
app.MapGet("/", () => "Hell World!");

//Ex:1 Get All Books

app.MapGet("/books", (IBookServices bookServices) =>
                      TypedResults.Ok(bookServices.GetBooks()));


//Ex:2 Get a specific book by Id

//app.MapGet("/books/{id}", (IBookServices bookServices, int id) =>
//                          TypedResults.Ok(bookServices.GetBook(id)));

app.MapGet("/books/{id}", Results<Ok<Book>, NotFound> (IBookServices bookServices, int id) =>
                            {
                                var book = bookServices.GetBook(id);
                                return book is { } ? TypedResults.Ok(book) : TypedResults.NotFound();
                            });

//Ex:3 Add Book
app.MapPost("/Addbook", (IBookServices bookServices, Book book) =>
                        {
                            bookServices.AddBook(book);
                            return TypedResults.Created($"/Addbook/{book.Id}", book);
                        });


//Ex:4 Update Book
app.MapPut("/book/{id}", (IBookServices bookServices, int id, Book book) =>
                            {
                                bookServices.UpdateBook(id, book);
                                return TypedResults.Ok();
                            });

//Ex:5 Delete Book
app.MapDelete("/book/{id}", (IBookServices bookServices, int id) =>
                            {
                                bookServices.DeleteBook(id);
                                return  TypedResults.NoContent();
                            });
app.UseAuthorization();

app.MapControllers();

app.Run();
