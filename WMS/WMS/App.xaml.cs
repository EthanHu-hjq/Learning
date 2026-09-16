using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WMS.Entities;
using WMS.Services;

namespace WMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // 从DI容器中获取MainWindow实例并显示
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // 注册数据库上下文，从App.config中读取连接字符串
            services.AddDbContext<WarehouseDbContext>(options =>
            {
                string connStr = ConfigurationManager.ConnectionStrings["WarehouseDbContext"].ConnectionString;
                options.UseSqlServer(connStr);
            });

            // 注册业务Services （后面新建的业务服务类可以在这里注册）
            services.AddTransient<MemberService>();

            // 注册窗口
            services.AddTransient<MainWindow>();
        }
    }

}
