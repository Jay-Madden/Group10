using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group10.API.Security;
using Group10.Data.Contexts;
using Group10.Data.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Group10.API
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
            services.AddControllers();
            
            var jwtTokenConfig = Configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            
            services.AddScoped<IJwtAuthManager, JwtAuthManager>();

            services.AddDbContext<Group10Context>(options =>
                options.UseNpgsql(Configuration["Group10ConnectionString"]));

            services.AddHttpClient();

            services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.User.AllowedUserNameCharacters = string.Empty;
                })
                .AddEntityFrameworkStores<Group10Context>();
                //.AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>(); 


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtTokenConfig.Issuer,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                        ValidAudience = jwtTokenConfig.Audience,
                        ValidateAudience = true
                    };
                }
            );
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Group10Context context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseHttpsRedirection();
            app.UseRouting();
            

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors(builder =>
                builder
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod());
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            context.Database.Migrate();
        }
    }
}