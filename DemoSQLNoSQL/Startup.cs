using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using DryIoc.Microsoft.DependencyInjection;
using System.IO;
using DryIoc;
using SqlAccess.Repositories;
using DemoSQLNoSQL.App_Start;
using System;
using SqlAccess.Connection;
using DotNetStarter.Configure;
using DotNetStarter.Abstractions;
using DotNetStarter.Locators;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using NoSqlAccess.Cqrs;
using NoSqlAccess.Cqrs.Commands;
using NoSqlAccess.Cqrs.Dependencies;
using NoSqlAccess.Configuration;
using NoSqlAccess.Cqrs.Queries;
using NoSqlAccess.Domain.Queries.Handler;
using NoSqlAccess.Domain.Queries.Query;
using NoSqlAccess.Domain.Commands.Command;
using System.Collections.Generic;
using NoSqlAccess.Domain.Commands.Handler;
using Entities.NoSQLEntities.Domain.Commands.Handler;

namespace DemoSQLNoSQL
{
	public class Startup
	{
		private readonly IWebHostEnvironment env;

		private readonly StartupBuilder startupBuilder;

		public string[] CorsOrigins { get; set; }
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			this.env = env;
			 startupBuilder = StartupBuilder.Create();
			var builder = new ConfigurationBuilder();
			Configuration = builder.Build();
			CorsOrigins = new string[]
			{
				configuration.GetValue<string>("Cors:OriginLocal")
			};
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo SQL NoSQL", Version = "v1" });
			}).AddControllers();

			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
			var config = builder.Build();

			services.AddSingleton<IConfiguration>(config);
			//DryIoc
			var container = new Container().WithDependencyInjectionAdapter(services);

			container.Register<IClientRepository, ClientRepository>();
			container.Register<IConnectionFactory, ConnectionFactory>();
			container.Register<IDispatcher, Dispatcher>();
			container.Register<ICommandSender, CommandSender>();
			container.Register<IHandlerResolver, HandlerResolver>();
			container.Register<NoSqlAccess.Cqrs.Dependencies.IResolver, NoSqlAccess.Cqrs.Dependencies.Resolver>();
			container.Register<ICommand, Command>();
			container.Register<IMongoClientConfig, MongoClientConfig>();
			container.Register<IDomainDbContext, DomainDbContext>();
			container.Register<IQueryProcessor, QueryProcessor>();
			container.Register<IQueryHandlerAsync<GetClientsQuery, List<GetClientCommand>>, GetClientListHandler>();
			container.Register<IQueryHandlerAsync<GetClientsQuery, GetClientCommand>, GetClientHandler>();
			container.Register<ICommandHandlerAsync<PostClientCommand>, PostClientHandler>();
			container.Register<ICommandHandlerAsync<DeleteClientCommand>, DeleteClientHandler>();

			var serviceProvider = container.Resolve<IServiceProvider>();

			AutoMapperConfiguration.Configure();

			return serviceProvider;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo SQL NoSQL");
			});

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private IServiceProvider ConfigureDotNetStarter(IServiceCollection services)
		{
			startupBuilder

				// environment allows for conditional check to swap services, perform tasks only in production, etc
				.UseEnvironment(new DotNetStarter.StartupEnvironment(env.EnvironmentName, env.ContentRootPath))
				.ConfigureAssemblies(assemblies =>
				{
					assemblies

						// scan all types with [assembly: DotNetStarter.Abstractions.DiscoverableAssembly]
						.WithDiscoverableAssemblies()

						// can types in this projectz
						.WithAssemblyFromType<Startup>();
				})

				// provide an ILocator registry factory based on one of the support ILocator packages
				// listed at https://bmcdavid.github.io/DotNetStarter/locators.html
				.OverrideDefaults(d => d.UseLocatorRegistryFactory(CreateDryIocRegistryFactory(services)))
				.Build(useApplicationContext: false) // executes all ILocatorConfigure instances
				.Run(); // executes all IStartupModule instances

			return startupBuilder.StartupContext.Locator.Get<IServiceProvider>();
		}

		private static ILocatorRegistryFactory CreateDryIocRegistryFactory(IServiceCollection services)
		{
			// create default container Rules for dotnet core
			var rules = DryIoc.Rules.Default
				.With(DryIoc.FactoryMethod.ConstructorWithResolvableArguments)
				.WithFactorySelector(DryIoc.Rules.SelectLastRegisteredFactory())
				.WithTrackingDisposableTransients();

			var container = new DryIoc.Container(rules);

			// configures DryIoc with IServiceCollection using DryIoc.Microsoft.DependencyInjection
			DryIocAdapter.Populate(container, services);

			return new DryIocLocatorFactory(container);
		}
	}
}
