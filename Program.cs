using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using policyConfigurations_pnamespace;
using System.Text;

class ProgramEntry
{
	static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });

			// Add JWT Authentication support in Swagger UI
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme",
				Type = SecuritySchemeType.Http,
				Scheme = "bearer"
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["JwtOptions:issuer"],
					ValidAudience = builder.Configuration["JwtOptions:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:secrete_Key"]))
				};
			});

		builder.Services.AddAuthorization(options =>
		{
			PolicyConfiguration.ConfigurePolicies(options);
		});

		builder.Services.AddControllers();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API");
			});
		}

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
