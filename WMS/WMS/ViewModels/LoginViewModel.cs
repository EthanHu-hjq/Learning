using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Entities;
using WMS.Services;
using WMS.Views;

namespace WMS.ViewModels
{
    public class LoginViewModel
    {
        #region properties
        public Member Member { get; set; }
        #endregion

        #region commands
        public IAsyncRelayCommand LoginCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        #endregion

        public LoginViewModel()
        {
            Member = new Member();
            LoginCommand = new AsyncRelayCommand(OnLoginAsync);
            CancelCommand = new RelayCommand(OnCancel);
#if DEBUG
            Member.Name = "admin";
            Member.Password = "123456";
            LoginCommand.Execute(Member);
#endif
        }

        private void OnCancel()
        {
            AppData.MainWindow?.Close();
        }

        private async Task OnLoginAsync()
        {
            // 防止重复点击导致并发提交
            if (LoginCommand.IsRunning) return;

            if (string.IsNullOrWhiteSpace(Member.Name) || string.IsNullOrWhiteSpace(Member.Password))
            {
                System.Windows.MessageBox.Show("用户名或密码不能为空！");
                return;
            }

            try
            {
                // 从 DI 容器创建作用域：WarehouseDbContext 已在 App.ConfigureServices 中注册（Scoped），
                // 其连接字符串、数据库提供程序都由容器负责，这里直接取用 MemberService 即可。
                // using 保证本次登录用到的 DbContext / 数据库连接被及时释放。
                using var scope = App.CreateScope();
                var memberService = scope.ServiceProvider.GetRequiredService<MemberService>();
                var members = await memberService.GetAllMemberAsync();
                var member = members.FirstOrDefault(m => m.Name == Member.Name && m.Password == Member.Password);
                if (member == null)
                {
                    System.Windows.MessageBox.Show("用户名或密码错误！");
                    return;
                }

                // 保存数据库中查到的实体（含 Id / Role），而不是仅包含输入内容的 Member
                AppData.CurrentMember = member;
                NavigateToMainView();
            }
            catch (Exception ex)
            {
                // 数据库不可达、连接字符串错误等情况在此统一提示，避免异常逃逸导致程序崩溃
                System.Windows.MessageBox.Show($"登录失败：{ex.Message}");
            }
        }

        /// <summary>登录成功后切换到主界面</summary>
        private static void NavigateToMainView()
        {
            var mainView = new MainView();
            // 若容器已不在视觉树中（如主窗口 Content 被整体替换），则直接切换主窗口内容
            if (AppData.Container != null && AppData.Container.IsLoaded)
                AppData.Container.Content = mainView;
            else if (AppData.MainWindow != null)
                AppData.MainWindow.Content = mainView;
        }
    }
}
