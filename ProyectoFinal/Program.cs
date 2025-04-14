
    using Microsoft.Extensions.DependencyInjection;
    using ProyectoFinal.Context;
    using ProyectoFinal.Repository;

    namespace ProyectoFinal
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);


                builder.Services.AddControllers().AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.DateFormatString = "MM/dd/yyyy HH:mm";
                });

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddSqlServer<BibliotecaContext>(builder.Configuration.GetConnectionString("AppConnection"));
                builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryServices<>));
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAllOrigins", policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();    
                    });
                });



                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {

                    app.UseDeveloperExceptionPage(); 
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }


                app.UseHttpsRedirection();

                app.UseCors("AllowAllOrigins");

                app.UseAuthorization(); 

                app.MapControllers();

                app.Run();
            }
        }
    }
