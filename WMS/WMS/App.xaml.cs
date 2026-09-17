using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;
using WMS.Entities;
using WMS.Services;
using WMS.ViewModels;

namespace WMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 全局 DI 容器。ViewModel / View 中不方便做构造函数注入时（例如 XAML 或 new 出来的对象），
        /// 可通过它解析已注册的服务。
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // 从DI容器中获取MainWindow实例并显示
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        /// <summary>
        /// 创建服务作用域。AddDbContext 注册的 DbContext 默认是 Scoped 生命周期，
        /// 必须从作用域中解析并在使用后释放，避免整个程序共用同一个 DbContext（脏读、内存只增不减）。
        /// </summary>
        public static IServiceScope CreateScope()
        {
            if (ServiceProvider is null)
                throw new InvalidOperationException("DI 容器尚未初始化，请在 App.OnStartup 完成后再解析服务。");

            return ServiceProvider.CreateScope();
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

            // 注册ViewModel（当 View 也交给 DI 创建后，即可改为构造函数注入 MemberService）
            services.AddTransient<LoginViewModel>();

            // 注册窗口
            services.AddTransient<MainWindow>();
        }
    }

}
