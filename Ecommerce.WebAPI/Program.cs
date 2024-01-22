
using Ecommerce.WebAPI.Data;
using Ecommerce.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Ecommerce.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddAuthentication();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(

           config =>
           {
            config.Password.RequiredLength = 6;
           config.Password.RequireDigit = false;
           config.Password.RequireNonAlphanumeric = false;
           config.SignIn.RequireConfirmedEmail = true;
           config.Password.RequireUppercase = false;
           }
           ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/Account/Login");


            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey

                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

           

            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            //app.MapIdentityApi<IdentityUser>();
            app.UseAuthorization();

            app.MapGet("api/todo", async (ApplicationDbContext context) =>
            {
                var items = await context.Todos.ToListAsync();
                return Results.Ok(items);

            });

            app.MapPost("api/todo", async (ApplicationDbContext context, [FromBody] Todo todo) =>
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Results.Created($"api/todo/{todo.Id}", todo);

            });

            app.MapPut("api/todo/{id}", async (ApplicationDbContext context, int id, [FromBody] Todo todo) =>
            {

               var data = context.Todos.FirstOrDefault(x => x.Id == id);

                if (data == null)
                {
                    return Results.NotFound();
                }

                data.TodoName = todo.TodoName;
                await context.SaveChangesAsync();
                return Results.Ok(data);
            
            });


            app.MapDelete("api/todo/{id}", async (ApplicationDbContext context, int id, [FromBody] Todo todo) =>
            {
                var data = context.Todos.FirstOrDefault(y => y.Id == id);
                if (data == null)
                {
                    return Results.NotFound();

                }

                context.Todos.Remove(data);
                await context.SaveChangesAsync();
                return Results.NoContent();

            });


         //   app.Mapidentity
            app.MapControllers();

            app.Run();
        }
    }
}