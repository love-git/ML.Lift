using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using ML.Lift.CallBoxes.Abstractions.Models;
using ML.Lift.CallBoxes.Composition;
using ML.Lift.Common.Composition;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Buffers;

namespace ML.Lift.CallBoxes.Api.v1
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public ILogger JsonInputFormatterLogger { get; set; }

        public IContainer Container { get; set; }

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            const string settingsFile = "appsettings.json";
            var envSettingsFile = $"appsettings.{env.EnvironmentName}.json";

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(settingsFile, optional: true, reloadOnChange: true)
                .AddJsonFile(envSettingsFile, optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            
            JsonInputFormatterLogger = loggerFactory.CreateLogger("JsonInputFormatter");
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy("AllowAll", policyBuilder =>
                    policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                )
            );
            services.AddMvc();

            services.Configure<MvcOptions>(options =>
            {
                // Output Json Formatter
                var outputSerializerSettings = new JsonSerializerSettings();
                outputSerializerSettings.Converters.Add(new StringEnumConverter());
                outputSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                outputSerializerSettings.Formatting = Formatting.Indented;
                outputSerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                var jsonOutputFormatter = new JsonOutputFormatter(outputSerializerSettings, ArrayPool<char>.Shared);
                options.OutputFormatters.RemoveType<JsonOutputFormatter>();
                options.OutputFormatters.Insert(0, jsonOutputFormatter);
                // Input Json Formatter
                var inputSerializerSettings = new JsonSerializerSettings();
                inputSerializerSettings.Converters.Add(new StringEnumConverter());
                //inputSerializerSettings.Converters.Add(new CreateCallBoxRequestConverter());
                //inputSerializerSettings.Converters.Add(new UpdateCallBoxRequestConverter());
                var jsonInputFormatter = new JsonInputFormatter(JsonInputFormatterLogger, inputSerializerSettings, ArrayPool<char>.Shared, new DefaultObjectPoolProvider());
                options.InputFormatters.RemoveType<JsonInputFormatter>();
                options.InputFormatters.Insert(0, jsonInputFormatter);
            });

            services.AddOptions();

            services.Configure<MongoOptions>(options => Configuration.GetSection("MongoOptions").Bind(options));
            
            CallBoxesMongoClassMapRegister.Register();


            // Create the Autofac container builder.
            var builder = new ContainerBuilder();

            // Add any Autofac modules or registrations.
            builder.RegisterModule(new MongoAutofacModule
            {
                ConnectionString = Configuration["MongoConnectionString"]
            });
            // todo: get message bus working.
            //builder.RegisterModule(new RabbitAutofacModule
            //{
            //    RabbitMQConnectionString = Configuration["RabbitMQConnectionString"],
            //    RabbitMQUsername = Configuration["RabbitMQUsername"],
            //    RabbitMQPassword = Configuration["RabbitMQPassword"]
            //});
            builder.RegisterModule(new CommonAutofacModule());
            builder.RegisterModule(new CallBoxesAutofacModule());

            // Populate the services.
            builder.Populate(services);

            // Build the container.
            Container = builder.Build();

            // Resolve and return the service provider.
            return Container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddNLog();
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //env.ConfigureNLog("nlog.config");

            //app.UseCors("AllowAll");
            
            //app.UseMvc();
            
            
            
            
            
            
            
            
            //loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
