using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashMoney.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using FlashMoney.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using FlashMoney.Filters;
using FlashMoney.Hubs;
using Microsoft.AspNetCore.SignalR;
using FlashMoney.Models;

namespace FlashMoney
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser,IdentityRole>(options=> {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(config =>
            {
              
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(typeof(SingleLogonActionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                               .AddRazorPagesOptions(options =>
                               {
                                   options.AllowAreas = true;
                                   options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                                   options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                               });
            services.AddSignalR();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Auth/SignIn";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = "/Home/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });
                //.AddOpenIdConnect("oidc", options =>
                //{
                //    options.SignInScheme = "Cookies";
                //    options.Authority = "https://localhost:44333/";
                //    options.ClientId = "flashmoneyclient";
                //   // options.CallbackPath = new PathString("/Home/Index");
                //    options.ResponseType = "code id_token";
                //    options.Scope.Add("openid");
                //    options.Scope.Add("profile");
                //    options.Scope.Add("address");
                //    options.Scope.Add("roles");
                //    options.SaveTokens = true;
                //    options.ClientSecret = "secret";
                //    options.GetClaimsFromUserInfoEndpoint = true;
                //    options.ClaimActions.Remove("amr");
                //    options.ClaimActions.DeleteClaim("idp");
                //    options.ClaimActions.MapUniqueJsonKey("role", "role");
                //});

            services.AddHttpContextAccessor();
            services.AddScoped<IFlashMoneyHttpClient, FlashMoneyHttpClient>();
            services.AddSingleton<IUserIdProvider, PhoneBasedUserIdProvider>();
            services.AddScoped<IFlashHttpClient, FlashHttpClient>();
            services.AddScoped<SingleLogonActionFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/ErrorPage");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSerilogRequestLogging();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            //app.Use(async (context, next) =>
            //{
            //    var hubContext =  context.RequestServices
            //                            .GetRequiredService<IHubContext<ChatHub>>();
            //    //...
            //});
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
