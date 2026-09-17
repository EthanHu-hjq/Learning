using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WMS.Entities;

namespace WMS
{
    public class AppData
    {
        public static Member? CurrentMember { get; set; }
        public static MainWindow? MainWindow { get; set; }
        public static ContentControl? Container { get; set; }
        public static ContentControl? MainRegion { get; set; }
    }
}
