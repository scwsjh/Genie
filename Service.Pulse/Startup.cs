using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Service.Pulse
{
    public class Startup : RainStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            #region 配置注册

            services.AddConfigurableOptions<WebConfigOptions>();
            services.AddConfigurableOptions<SystemConfigOptions>();

            #endregion 配置注册

            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddRemoteRequest();
            services.AddSession();

            // 注册 EventBus 服务
            services.AddEventBus(builder =>
            {
                // 注册 ToDo 事件订阅者
                builder.AddSubscriber<ToDoEventSubscriber>();
            });

            services.AddSensitiveDetection();//注册脱敏
            services.Configure<WebEncoderOptions>(options => options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs));//html编码转码
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
            //app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");

                //请求错误提示配置
                app.UseErrorHandling();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
                    {
                { ".apng","image/png"}
               })
            });
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseInjectBase();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Index}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}