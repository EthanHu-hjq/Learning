using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WMSWarehouse.Models;
using WMSWarehouse.Services;
using WMSWarehouse.ViewModels;

namespace WMSWarehouse;

public partial class App : Application
{
    public static IServiceProvider _serviceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();

        // 从DI容器实例化MainWindow（支持带参构造）
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // 注册数据库上下文
        services.AddDbContext<WarehouseDbContext>(options =>
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["WarehouseDbConn"].ConnectionString;
            options.UseSqlServer(connStr);
        });

        // 注册业务服务
        services.AddScoped<WarehouseService>();

        // 注册ViewModel
        services.AddScoped<WarehouseViewModel>();
        services.AddScoped<MainViewModel>();

        // 注册窗口
        services.AddTransient<MainWindow>();
    }
}