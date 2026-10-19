using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WMSWarehouse.Views;

namespace WMSWarehouse.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object? _currentView;

    public ObservableCollection<MenuModel> MenuList { get; set; } = new();

    public MainViewModel()
    {
        // 初始化左侧菜单，后续新增表在这里追加
        MenuList.Add(new MenuModel { MenuName = "仓库管理(Warehouse)", PageView = new WarehouseView() });
        MenuList.Add(new MenuModel { MenuName = "货物管理(Cargo)", PageView = null });
        MenuList.Add(new MenuModel { MenuName = "入库管理(Instore)", PageView = null });
        MenuList.Add(new MenuModel { MenuName = "出库管理(Outstore)", PageView = null });
    }

    [RelayCommand]
    public void SwitchPage(MenuModel menu)
    {
        if (menu.PageView != null)
        {
            CurrentView = menu.PageView;
        }
    }
}

// 菜单模型
public partial class MenuModel : ObservableObject
{
    [ObservableProperty]
    private string _menuName = string.Empty;

    [ObservableProperty]
    private object? _pageView;
}