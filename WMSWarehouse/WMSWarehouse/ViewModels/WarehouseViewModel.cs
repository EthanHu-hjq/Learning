using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WMSWarehouse.Models;
using WMSWarehouse.Services;
using System.Collections.ObjectModel;

namespace WMSWarehouse.ViewModels;

public partial class WarehouseViewModel : ObservableObject
{
    private readonly WarehouseService _warehouseService;

    [ObservableProperty]
    private ObservableCollection<Warehouse> _warehouseList = new();

    [ObservableProperty]
    private Warehouse? _selectedWarehouse;

    // 输入框绑定字段
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _telephone = string.Empty;
    [ObservableProperty] private string _tag = string.Empty;

    public WarehouseViewModel(WarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    #region 增删改查命令
    [RelayCommand]
    public async Task LoadAllWarehouse()
    {
        var list = await _warehouseService.GetAllWarehouseAsync();
        WarehouseList.Clear();
        foreach (var item in list)
        {
            WarehouseList.Add(item);
        }
    }

    [RelayCommand]
    public async Task AddWarehouse()
    {
        var newWare = new Warehouse
        {
            Name = Name,
            Telephone = Telephone,
            Tag = Tag
        };
        await _warehouseService.AddWarehouseAsync(newWare);
        await LoadAllWarehouse();
        ClearInput();
    }

    [RelayCommand]
    public void FillEditBox()
    {
        if (SelectedWarehouse == null) return;
        Name = SelectedWarehouse.Name;
        Telephone = SelectedWarehouse.Telephone;
        Tag = SelectedWarehouse.Tag;
    }

    [RelayCommand]
    public async Task EditWarehouse()
    {
        if (SelectedWarehouse == null) return;
        SelectedWarehouse.Name = Name;
        SelectedWarehouse.Telephone = Telephone;
        SelectedWarehouse.Tag = Tag;
        await _warehouseService.UpdateWarehouseAsync(SelectedWarehouse);
        await LoadAllWarehouse();
        ClearInput();
    }

    [RelayCommand]
    public async Task RemoveWarehouse()
    {
        if (SelectedWarehouse == null) return;
        await _warehouseService.DeleteWarehouseAsync(SelectedWarehouse.Id);
        await LoadAllWarehouse();
    }

    [RelayCommand]
    public void ClearInput()
    {
        Name = string.Empty;
        Telephone = string.Empty;
        Tag = string.Empty;
        SelectedWarehouse = null;
    }
    #endregion
}