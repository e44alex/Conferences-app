using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Backend.Common.Data;
using GraphQL.Common.Loaders;
using GraphQL.Common.Subscriptions;
using GraphQL.Common.Types;
using GraphQL.Common.Types.Mutations;
using GraphQL.Common.Types.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
                else
                {
                    options.UseSqlite("Data Source=conferences.db");
                }
            });

            services.AddScoped<ApplicationDbContext>(s =>
                s.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

            services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

            services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new OpenApiInfo { Title = "Conference Planner API", Version = "v1" }); });

            services.AddCors();
            // ReSharper disable BadIndent
            services.AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<SpeakerQueries>()
                    .AddTypeExtension<SessionQueries>()
                    .AddTypeExtension<TrackQueries>()
                    .AddTypeExtension<AttendeeQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<SpeakerMutations>()
                    .AddTypeExtension<SessionMutations>()
                    .AddTypeExtension<TrackMutations>()
                    .AddTypeExtension<AttendeeMutations>()
                .AddSubscriptionType(d => d.Name("Subscription"))
                    .AddTypeExtension<SessionSubscriptions>()
                    .AddTypeExtension<AttendeeSubscriptions>()
                .AddType<SpeakerType>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<TrackType>()
                .AddGlobalObjectIdentification()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions()
                .AddDataLoader<SpeakerByIdDataLoader>()
                .AddDataLoader<SessionByIdDataLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseWebSockets();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Conference Planner API v1")
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            });

            app.Run(context =>
            {
                context.Response.Redirect("/swagger");

                return Task.CompletedTask;
            });
        }
    }
}