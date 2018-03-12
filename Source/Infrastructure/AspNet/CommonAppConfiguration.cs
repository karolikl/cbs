/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017-2018 The International Federation of Red Cross and Red Crescent Societies. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Infrastructure.AspNet;
using Infrastructure.TextMessaging;
using Infrastructure.Kafka.BoundedContexts;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    public static class CommonAppConfiguration
    {
        public static IApplicationBuilder UseCommon(this IApplicationBuilder app, IHostingEnvironment env)
        {
            Internals.ServiceProvider = app.ApplicationServices;

            app.UsedoLittle(env);

            // Relaxed CORS policy for example only
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials());
            app.UseMvc();
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            BoundedContextListener.Start(app.ApplicationServices);
            TextMessageListener.Start(app.ApplicationServices);

            return app;
        }
    }
}