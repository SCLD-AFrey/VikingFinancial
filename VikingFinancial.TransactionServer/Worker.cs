using VikingFinancial.TransactionServer.Services;

namespace VikingFinancial.TransactionServer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Worker
{
    public void ConfigureServices(IServiceCollection p_services)
    {
        p_services.AddGrpc();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(p_endpoints =>
        {
            p_endpoints.MapGrpcService<TransactionService>();

            p_endpoints.MapGet("/", async context =>
            {                    
                await context.Response.WriteAsync("Hello World!");
            });
        });
    }
}