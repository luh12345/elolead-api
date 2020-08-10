using Elogroup.Api.Repository;
using Elogroup.Lead.Api.Common;
using Elogroup.Lead.Api.Repository.Entities;
using Elogroup.Lead.Api.Services;
using Elogroup.Lead.Api.Services.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace Elogroup.Lead.Api
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
            services.AddCors();
            services.AddControllers();

            var section = Configuration.GetSection("Settings");
            var settings = new Settings();

            section.Bind(settings);

            services.AddSingleton(settings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });

            services.AddDbContext<Context>(options => options.UseInMemoryDatabase("elogroup-leads"));

            //Services
            services.AddScoped<IStatusLeadService, StatusLeadService>();
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<IOpportunityService, OpportunityService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            ConfigGlobalExceptionHandler(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<Context>();
            LoadData(context);
        }

        private void LoadData(Context context)
        {

            //Lead que foi rejeitado
            context.StatusLeads.Add(new StatusLead
            {
                Description = "Descartado"
            });

            //Lead em processo de qualificação para se tornar ou não uma oportunidade
            //Status inicial na criação de um novo Lead
            context.StatusLeads.Add(new StatusLead
            {
                Description = "Em qualificação"
            });

            //Lead que se tornou uma oportunidade 
            context.StatusLeads.Add(new StatusLead
            {
                Description = "Qualificado"
            });

            //Oportunidade que se tornou um cliente
            context.StatusLeads.Add(new StatusLead
            {
                Description = "Finalizado"
            });

            //Carga de Leads 

            var nomes = new string[3] { "Angela Serpa", "Robson Santos", "Arthur Diniz" };
            var telefones = new string[3] { "61 986255432", "61 984388053", "61 987632232" };
            var emails = new string[3] { "angela_serpa@gmail.com", "robson@gmail.com", "arthur@gmail.com" };

            for (int i = 0; i < 3; i++)
            {
                context.Leads.Add(new Repository.Entities.Lead
                {
                    CustomerName = nomes[i],
                    CustomerEmail = emails[i],
                    CustomerPhone = telefones[i],
                    Date = DateTime.Now,
                    StatusId = (int)Common.Enums.StatusLead.EM_QUALIFICACAO
                });
            }

            context.SaveChanges();
        }

        private void ConfigGlobalExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var status = HttpStatusCode.InternalServerError;
                        var response = context.Response;

                        response.StatusCode = (int)status;
                        response.ContentType = "application/json";

                        var exception = exceptionHandlerFeature.Error;

                        if (exception is NullReferenceException)
                            response.StatusCode = (int)HttpStatusCode.NotFound;

                        if (exception is UnauthorizedAccessException)
                            response.StatusCode = (int)HttpStatusCode.Unauthorized;

                        var stackTrace = new StackTrace(exception);

                        var result = new ApiResponse<string>
                        {
                            Action = stackTrace.GetFrame(0).GetMethod().Name,
                            IsSuccess = false,
                            Data = "Ocorreu um erro: " + exception.Message
                        };

                        var responseData = JsonConvert.SerializeObject(result);
                        await context.Response.WriteAsync(responseData);
                    }
                });
            });
        }

    }
}
