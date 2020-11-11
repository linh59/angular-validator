using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SSC.Database;
using SSC.Database.Entity;

namespace SSC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // TODO: Hard code seed value, Should delete at R2
#if DEBUG
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<SSCContext>();
                var category = new Category()
                {
                    CreatedAt = DateTime.Now,
                    Description = "Bao gồm các đơn cho phép đặt từ xa",
                    //Id = 1,
                    Name = "Đặt dịch vụ",
                    LastModifiedAt = DateTime.Now
                };
                if (!context.Categories.Any())
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
                if (!context.FormTemplates.Any())
                {
                    context.FormTemplates.Add(new FormTemplate()
                    {
                        Category = category,
                        Controls = new List<Control>()
                        {
                            new Control()
                            {
                                CreatedAt = DateTime.Now,
                                Hint = "",
                                //Id = Guid.Parse("7cc755db-b7d6-4fd7-91f8-6b3c3cd09cd3"),
                                LastModifiedAt = DateTime.Now,
                                Placeholder = "Vui lòng nhập số lượng",
                                Style = "material",
                                Title = "Số lượng"
                            }
                        },
                        CreatedAt = DateTime.Now,
                        Creator = "ThongHM",
                        Description = "Đơn đặt nước",
                        //Id = 1,
                        LastModifiedAt = DateTime.Now,
                        Name = "Đơn đặt nước",
                    });
                    context.SaveChanges();
                }
            }
#endif
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
