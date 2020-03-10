using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.UI.Site.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.UI.Site
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Jeito de altera a convenção de pasta "Area" para "Modulos"
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaPageViewLocationFormats.Clear();
                options.AreaPageViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml"); // 2
                options.AreaPageViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaPageViewLocationFormats.Add("/Modulos/Chared/{0}/{0}.cshtml");
            });

            services
                .AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            services.AddTransient<IPedidoRepository, PedidoRepository>();   // Obtem uma nova instancia do objeto quando for solicitado
            // services.AddScoped<IPedidoRepository, PedidoRepository>();   // Reutiliza a mesma instancia do objeto durante todo o request (Recomenda para web)
            //services.AddSingleton<IPedidoRepository, PedidoRepository>(); //

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(); // Carrega os arquivos staticos, que estão na pasta wwwroot
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");

                // Pode ser subistituidos pelos atributos Route na controller
                //routes.MapRoute(
                //    name: "areas",
                //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(name: "AreaProdutos", areaName: "Produtos", template: "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
                routes.MapAreaRoute(name: "AreaVendas", areaName: "Vendas", template: "Vendas/{controller=Pedidos}/{action=Index}/{id?}");
            });
        }
    }
}
