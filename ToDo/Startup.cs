using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using ToDo.Models;


namespace ToDo
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

            services.AddDbContext<ToDoContext>(options => 
            options.UseSqlite( 
                Configuration.GetConnectionString("DefaultConnection")));





            services.AddMvc().AddMvcOptions(Mvcoption => {
                Mvcoption.EnableEndpointRouting = false;
            });

            // DRB 02/17/2021
            // Add Auto Mapper so I don't have to write mapping code from model to VM.
            // uncomment later if we need AutoMapper. 
            //services.AddAutoMapper(typeof(Startup));




            // DRB 02/17/2021
            // Add runtime razor view compilation to fix Cntrl-F5 issue.
            services.AddRazorPages()
                    .AddRazorRuntimeCompilation();

            services.AddControllersWithViews();

            // DRB 03/05/2021
            // Changes to get current logged in user.
            services.AddHttpContextAccessor();

            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //});

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });



            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ToDoList}/{action=Index}/");

            });
        }

    }
}
