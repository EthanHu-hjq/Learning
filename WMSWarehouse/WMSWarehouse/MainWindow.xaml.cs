using System.Windows;
using WMSWarehouse.ViewModels;

namespace WMSWarehouse;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}