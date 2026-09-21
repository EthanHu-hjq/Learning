using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.ViewModels
{
    public class MainViewModel
    {
        public RelayCommand<string> NavigateCommand { get; set; }
        public MainViewModel()
        {
            NavigateCommand = new RelayCommand<string>(OnNavigateCommand);
        }

        private void OnNavigateCommand(string? viewName)
        {
            var type = Type.GetType($"WMS.Views.{viewName}");
            if (type != null)
            {
                AppData.MainRegion!.Content = Activator.CreateInstance(type);
            }
        }
    }
}
