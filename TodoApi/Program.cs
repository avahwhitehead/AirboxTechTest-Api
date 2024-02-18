using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TodoApi.Managers;

namespace TodoApi;

public class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);

		//Initialise controllers
		builder.Services.AddControllers()
			//Use Newtonsoft for JSOn conversion
			.AddNewtonsoftJson(options => {
				//Convert enums to their string values instead of numerical
				options.SerializerSettings.Converters.Add(new StringEnumConverter());
				//Don't write null values
				options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			});

		//Add Swagger description to the endpoints
		//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services
			.AddEndpointsApiExplorer()
			.AddSwaggerGen()
			.AddSwaggerGenNewtonsoftSupport();

		//Items Manager
		builder.Services.AddSingleton<ItemsManager>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.

		//Always use swagger
		app.UseSwagger();
		app.UseSwaggerUI();

		//Redirect to HTTPs
		app.UseHttpsRedirection();

		//Enable Authorization
		app.UseAuthorization();

		//Map controllers
		app.MapControllers();

		//Start listening
		app.Run();
	}
}
