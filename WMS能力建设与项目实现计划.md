# WMS 项目：个人能力提升与实现计划

> 技术栈：C# + WPF + Prism + CommunityToolkit.Mvvm + EF Core + SQL Server
> 目标范围：教学级最小闭环 WMS（入库收货 → 上架 → 库存查询 → 拣货 → 出库 → 盘点）
> 时间基线：每周 ≤5 小时
> 制定日期：2026-10-19

---

## 一、先说三个必须知道的结论

在动手之前，有三件事会影响你后续所有决策，先摆在最前面。

### 1. 必须用 .NET 10，不能用 .NET 8

| 版本 | 类型 | 支持截止 | 状态 |
|---|---|---|---|
| .NET 8 | LTS | **2026-11-11** | 距今不到 1 个月，即将停止支持 |
| .NET 9 | STS | 2026-11-11 | 同上 |
| .NET 10 | LTS | 2028-11-15 | **本项目应采用** |

网上大量 WPF + Prism 教程基于 .NET 6 / .NET 8 编写。你照着学会遇到"教程代码能跑、但项目一建就是过时框架"的错位。**从第一天起就把目标框架锁死在 `net10.0-windows`**，EF Core 用 10.x（LTS，支持到 2028-11-10）。

### 2. Prism 存在两个真实风险，阶段 0 必须先验证

**风险 A：版本停滞。** Prism.Wpf 当前最新稳定版是 **9.0.537，发布于 2024-08-20**，此后一年多没有新的稳定版。它的目标框架是 `net6.0-windows7.0` 和 `net462`，**没有针对 .NET 10 的专门构建**（NuGet 上 `net10.0-windows` 标记为"计算兼容"）。

**风险 B：许可证已变更。** Prism 从 9.x 起不再是纯粹的开源免费。它现在要求你持有 Community License 或 Commercial License。Community License 的资格条件是：年营收 < 100 万美元、开发者 < 5 人、外部融资 < 300 万美元。

> **对你的影响**：个人学习项目符合 Community License，可以免费用。但如果这个项目将来要放进公司、或作为商业交付物，需要重新核对许可证条款。现在知道，比将来踩坑好。

**应对**：阶段 0 的第一个任务就是做一个 15 分钟的"Prism 9.0.537 + .NET 10 + WPF"最小启动验证。跑通再继续，跑不通就走备选方案（见第七节风险应对）。**不要等学了三周才发现框架起不来。**

### 3. Prism 和 CommunityToolkit.Mvvm 功能高度重叠，必须先划边界

这是初学者最容易翻车的地方。两个框架都提供 ViewModel 基类、命令、消息机制：

| 能力 | Prism 提供 | CommunityToolkit.Mvvm 提供 |
|---|---|---|
| ViewModel 基类 | `BindableBase` | `ObservableObject` |
| 命令 | `DelegateCommand` | `RelayCommand` / `AsyncRelayCommand` |
| 属性通知 | `SetProperty()` 手写 | `[ObservableProperty]` 源生成器 |
| 数据验证 | 无内置 | `ObservableValidator` |
| 消息机制 | `IEventAggregator` | `IMessenger` |
| DI 容器 | ✅ 内置 | 仅 `Ioc` 轻量辅助 |
| 区域导航 | ✅ 独有 | ❌ 无 |
| 模块化 | ✅ 独有 | ❌ 无 |
| 对话框服务 | ✅ 独有 | ❌ 无 |

**如果两边混着用，你会得到两套命令类型、两套消息总线、两套属性通知写法**，代码风格割裂，排查问题时要在两种心智模型间来回切换。第四节的分工边界请在阶段 1 开始前就记住。

---

## 二、技术栈锁定

| 组件 | 版本 | 说明 |
|---|---|---|
| .NET SDK | 10.0.x | LTS，目标框架 `net10.0-windows` |
| WPF | 随 .NET 10 | — |
| Prism.Wpf | 9.0.537 | ⚠️ 阶段 0 需验证兼容性 |
| Prism.Unity 或 Prism.DryIoc | 9.0.537 | 容器二选一，见阶段 3 |
| CommunityToolkit.Mvvm | 8.4.2 | target net8.0，兼容 .NET 10 |
| EF Core | 10.0.x | `Microsoft.EntityFrameworkCore.SqlServer` |
| SQL Server | 2022 Express 或 Developer | Express 免费够用；Developer 版功能全 |
| SSMS | 最新版 | SQL Server Management Studio |
| IDE | Visual Studio 2022 或 Rider | VS 2022 需 17.12+ 才支持 .NET 10 |
| 测试框架 | xUnit + NSubstitute | 阶段 6 |

**NuGet 包名速查**（阶段 0 会用到）：
```
Prism.Unity
CommunityToolkit.Mvvm
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
```

---

## 三、总体路线图

| 阶段 | 主题 | 预估工时 | 周期（5h/周） |
|---|---|---|---|
| 0 | 环境搭建与技术验证 | 4h | 1 周 |
| 1 | WPF + MVVM 深化 | 20h | 4 周 |
| 2 | EF Core + SQL Server 实战 | 30h | 6 周 |
| 3 | DI 与 Prism 框架 | 30h | 6 周 |
| 4 | WMS 业务建模 | 20h | 4 周 |
| 5 | WMS 最小闭环实现 | 80h | 16 周 |
| 6 | 测试与部署 | 20h | 4 周 |
| **合计** | | **≈204h** | **≈41 周（9-10 个月）** |

### 关于这个周期，你需要做决定

按每周 5 小时算，全流程约 **9-10 个月**。这是诚实的估算，不是吓唬你。但有两个现实的调整空间：

**调整一：提高每周投入。** 每周 10 小时 → 约 5 个月；每周 15 小时 → 约 3.5 个月。学习类任务的进度对时间投入近似线性，但**连续性比总量更重要**——每周稳定 5 小时，好过某周猛干 20 小时然后停三周。

**调整二：合并阶段 1-3 的练习载体。** 原计划每个阶段各做一个"图书管理"练习项目，做完三遍同一个程序。可以改为：**三个阶段共用一个练习项目，逐步演进**（阶段 1 手写 MVVM 版 → 阶段 2 加 EF Core 持久化 → 阶段 3 重构为 Prism 模块化）。这样省掉重复搭架子的时间，约可压缩 15-20 小时，而且能亲身体会"重构"这件事——这本身就是最有价值的学习。

**调整三：阶段 4 可以穿插。** WMS 业务建模主要是阅读、画图、写文档，不需要写代码。它可以作为编码疲劳时的调剂，穿插在阶段 2、3 期间完成。**不建议严格串行**——串行 10 个月最容易在中途放弃。

### 我的建议

采用「调整二 + 调整三」，把周期压到 **7-8 个月**，练习项目统一命名为 `Wms.Lab`（阶段 1-3 的演进载体），与最终的 `Wms` 正式项目区分开。

---

## 四、关键架构决策：Prism 与 CommunityToolkit.Mvvm 分工

**这个边界要在写第一行业务代码前就定下来，之后不再摇摆。**

### CommunityToolkit.Mvvm 负责：ViewModel 内部的一切

- ViewModel 基类 → 继承 `ObservableObject`（**不要用 Prism 的 `BindableBase`**）
- 属性通知 → `[ObservableProperty]` 源生成器（**不要手写 `SetProperty`**）
- 命令 → `[RelayCommand]` / `AsyncRelayCommand`（**不要用 Prism 的 `DelegateCommand`**）
- 数据验证 → 继承 `ObservableValidator` + DataAnnotations
- ViewModel 之间的轻量通信 → `IMessenger`

### Prism 负责：应用结构与导航

- 依赖注入容器 → `RegisterTypes()` 中注册服务
- 应用生命周期 → `PrismApplication`、`CreateWindow()`
- 区域与导航 → `IRegionManager`、`RequestNavigate`、`INavigationAware`
- 模块化 → `IModule`、`ModuleCatalog`
- 对话框服务 → `IDialogService`（比手写 Window 优雅得多）
- 全局跨模块事件 → `IEventAggregator`

### 两套消息机制怎么选

这是唯一的模糊地带，给一个可执行的判断标准：

| 场景 | 用哪个 |
|---|---|
| 同一模块内、ViewModel 与其子组件通信 | `IMessenger` |
| 跨模块广播（如"库存变动"要通知多个模块刷新） | `IEventAggregator` |
| 拿不准 | 先用 `IMessenger`，等真的跨模块了再改 |

> **理由**：`IEventAggregator` 是全局单例总线，滥用会让数据流变得难以追踪——任何地方都能发、任何地方都能收，出问题时你找不到源头。把跨模块通信收敛到少数几个明确定义的事件上，是可维护性的关键。

### DI 容器选 Unity 还是 DryIoc

选 **DryIoc**（`Prism.DryIoc` 包）。理由：DryIoc 性能更好、依赖更少、错误信息更清晰；Unity 容器在 2023 年后维护活跃度下降。两者 API 在 Prism 中基本一致，后期切换成本低。

---

## 五、各阶段详细计划

### 阶段 0：环境搭建与技术验证（4h / 1 周）

**目标**：本机环境就绪，并确认 Prism 9.0.537 能在 .NET 10 上正常启动。

**任务清单**
- [ ] 安装 .NET 10 SDK，`dotnet --list-sdks` 确认
- [ ] 安装 SQL Server 2022 Express（或 Developer）+ SSMS，能连上本地实例
- [ ] 安装 IDE（VS 2022 17.12+ 或 Rider）
- [ ] 建解决方案 `D:\Project\Git\Learning`，初始化 git
- [ ] **关键验证**：`dotnet new wpf` 建一个空项目，装 `Prism.Unity` 9.0.537，改 `App.xaml.cs` 继承 `PrismApplication`，实现 `CreateWindow()` 返回一个 MainWindow，能正常启动并显示窗口

**验收标准**
- 空 Prism 项目能启动、能显示窗口、无运行时异常
- 能在 SSMS 中连上本地 SQL Server 并新建数据库

**⚠️ 如果验证失败**：立刻停止，不要硬撑。走第七节的备选方案（去掉 Prism，用 CommunityToolkit.Mvvm + Microsoft.Extensions.DependencyInjection + 自建导航）。这个决定要在第 1 周做，而不是第 6 周。

**Prism 9.x 注意**：9.x 相比 8.x 有破坏性变更，最主要的是 **`OnAppStart` 已改为 `CreateWindow`**。网上大量教程基于 8.x，照抄会编译不过。遇到 API 对不上，先怀疑是版本差异。

---

### 阶段 1：WPF + MVVM 深化（20h / 4 周）

**目标**：不依赖任何框架，理解 MVVM 每一层的原理。你做过 WPF 小项目，所以这个阶段的重点不是"学会 WPF"，而是"理解为什么必须这么写"。

**为什么先手写、后框架**：Prism 和 CommunityToolkit 都在帮你自动生成代码。如果不知道 `[ObservableProperty]` 展开后是什么、`RelayCommand` 内部怎么实现 `CanExecute`，出问题时你只会盯着框架源码发呆。**手写一遍，之后用框架时你会知道每一行魔法背后发生了什么。**

**任务清单**

*Week 1 — 手写 MVVM 三件套*
- [ ] 自己实现 `ViewModelBase`（实现 `INotifyPropertyChanged`）
- [ ] 自己实现 `RelayCommand<T>`（实现 `ICommand`，含 `CanExecute` / `CanExecuteChanged`）
- [ ] 理解 `DataContext` 的继承链：为什么子控件能找到父级的 ViewModel
- [ ] 用这套手写基础做一个单窗口 CRUD（建议：商品主数据维护）

*Week 2 — 数据绑定深水区*
- [ ] `Binding.Mode` 四种模式的实际差异（OneWay / TwoWay / OneTime / OneWayToSource）
- [ ] `UpdateSourceTrigger`（LostFocus vs PropertyChanged）——这个在 WMS 的录入界面会天天遇到
- [ ] `INotifyDataErrorInfo` 实现表单校验与错误提示
- [ ] `IValueConverter`、`MultiBinding`、`DataTrigger`
- [ ] `RelativeSource`、`ElementName`、`FindAncestor`——在 DataGrid 模板列里绑定外层 ViewModel 的命令，必须靠这个

*Week 3 — 集合、DataGrid 与视图*
- [ ] `ObservableCollection<T>` 的局限：它只通知集合变更，**不通知元素内部属性变更**
- [ ] `ICollectionView` / `CollectionViewSource`：实现排序、筛选、分组（WMS 的库存查询界面核心能力）
- [ ] `DataGrid` 的列类型、模板列、行样式、虚拟化（`VirtualizingStackPanel`）
- [ ] 大数据量下的性能陷阱：为什么绑定 1 万行会卡

*Week 4 — 样式、模板与综合练习*
- [ ] `Style`、`ControlTemplate`、`DataTemplate`、资源字典与作用域
- [ ] 用 `DataTemplate` + `DataType` 实现 ViewModel 到 View 的自动映射（Prism 导航的底层原理）
- [ ] **输出物**：完成 `Wms.Lab` 第一版——纯手写 MVVM 的商品主数据管理，含 CRUD、校验、筛选、排序

**验收标准**
- 能不看资料说清楚：`INotifyPropertyChanged` 不触发会怎样、`CanExecute` 什么时候被重新求值
- `Wms.Lab` 里没有任何 code-behind 业务逻辑（`xaml.cs` 只有构造函数）
- 表单校验能在用户输入时实时提示，且非法状态下命令按钮自动禁用

**常见坑**
- 在 `xaml.cs` 里写业务逻辑——一旦开了这个口子，MVVM 就废了
- 忘记 ViewModel 里的集合属性变化时手动触发通知
- 绑定路径写错时不报错，只在 VS 输出窗口打一行调试信息。**养成看"输出"窗口的习惯**，这能省掉一半排查时间

---

### 阶段 2：EF Core + SQL Server 实战（30h / 6 周）

**目标**：这是你明确说缺经验的部分，所以给最充足的工时。要达到的水平是：能独立设计表结构、写迁移、处理并发、看懂生成的 SQL。

**任务清单**

*Week 1 — 数据库基础（不要跳过）*
- [ ] 关系模型：主键、外键、唯一约束、默认值、NULL 的语义
- [ ] 索引：聚集 vs 非聚集，什么字段该建索引
- [ ] **事务与隔离级别**：这个对 WMS 极其关键——库存扣减如果没有正确的事务边界，会直接导致账实不符
- [ ] 在 SSMS 里手写 SQL 建表、插数据、写 JOIN 查询
- [ ] 范式基础：为什么单据要拆"单据头 + 单据明细"两张表

*Week 2 — EF Core Code First 入门*
- [ ] `DbContext`、`DbSet<T>`、实体类定义
- [ ] Fluent API 配置：字段长度、精度、必填、索引、默认值
- [ ] 关系配置：一对多、多对多、一对一一、自关联
- [ ] 级联删除（`OnDelete`）的几种行为及其陷阱
- [ ] 导航属性与反向导航

*Week 3 — Migration 全流程（你缺的"数据库部署"经验，核心在这）*
- [ ] `dotnet ef migrations add` / `database update` / `migrations remove`
- [ ] **读懂生成的迁移代码**：`Up()` / `Down()` 在做什么
- [ ] `migrations script` 生成 SQL 脚本、`--idempotent` 幂等脚本（生产部署用）
- [ ] Migration Bundle：`dotnet ef migrations bundle` 生成可执行部署包
- [ ] 多环境配置：开发库 / 测试库 / 生产库的连接串管理
- [ ] `HasData()` 种子数据
- [ ] 已经上线后如何安全地改表结构（加列 vs 改列 vs 删列的风险差异）

*Week 4 — 查询与性能*
- [ ] LINQ to Entities：哪些写法能翻译成 SQL，哪些会在内存里执行
- [ ] `Include` / `ThenInclude` 与**懒加载**的区别，为什么生产环境通常关掉懒加载
- [ ] **N+1 问题**：怎么发现、怎么解决
- [ ] `AsNoTracking()`：只读查询为什么要加
- [ ] 投影到 DTO（`Select(x => new XDto {...})`）——不要直接把实体返给 UI 层
- [ ] 分页：`Skip` / `Take`，以及 `OrderBy` 为什么是必需的
- [ ] 用 EF Core 的日志或 SQL Server Profiler 看实际执行的 SQL

*Week 5 — 写入、事务与并发*
- [ ] `SaveChanges` 的变更跟踪机制：Added / Modified / Deleted / Unchanged
- [ ] 显式事务：`BeginTransaction`，什么时候需要
- [ ] **乐观并发**：`RowVersion` / `[Timestamp]`，处理 `DbUpdateConcurrencyException`
- [ ] 悲观并发的适用场景
- [ ] 批量操作的性能问题与 EF Core 的 `ExecuteUpdate` / `ExecuteDelete`
- [ ] **库存扣减的正确写法**：这是 WMS 的核心难点，要练习"检查库存 + 扣减"在同一事务内且能防并发超卖

*Week 6 — 综合演练*
- [ ] **输出物**：把 `Wms.Lab` 从内存集合改为 SQL Server 持久化
- [ ] 加迁移、加种子数据、加分页查询、加表单校验落库
- [ ] 写一个"库存调整"功能，正确处理并发冲突

**验收标准**
- 能独立从零建库：设计实体 → 配置 Fluent API → 生成迁移 → 应用到数据库 → 种子数据
- 能生成幂等 SQL 脚本，并在一个空库上执行成功（这就是最基本的"数据库部署"能力）
- 能用 EF Core 日志证明自己没有 N+1 问题
- 能实现一个不会超卖的库存扣减方法，并解释为什么它不会超卖

**常见坑**
- 把 EF Core 实体直接绑定到 UI：实体的导航属性会引发意外的懒加载和循环引用。**一定要用 DTO**
- `DbContext` 生命周期用错：桌面应用中不能做成全局单例，否则变更跟踪会累积、内存泄漏、并发异常
- 迁移文件手工改乱后数据库与代码不同步
- 忽略 `DbUpdateConcurrencyException`

---

### 阶段 3：DI 与 Prism 框架（30h / 6 周）

**目标**：理解依赖注入解决了什么问题，然后掌握 Prism 的导航、模块化、对话框、事件机制。

**任务清单**

*Week 1 — DI 原理（先不用 Prism）*
- [ ] 为什么需要 DI：从"紧耦合难测试"这个痛点出发理解
- [ ] 控制反转 vs 依赖注入的关系
- [ ] **三种生命周期**：Singleton / Transient / Scoped——WPF 桌面应用里 Scoped 怎么用（配合工作单元）
- [ ] 用 `Microsoft.Extensions.DependencyInjection` 手写一个容器注册与解析
- [ ] 构造函数注入 vs 属性注入 vs 方法注入
- [ ] 接口抽象：为什么服务要有接口

*Week 2 — Prism 启动与容器*
- [ ] `PrismApplication` 生命周期：`OnInitialized` / `RegisterTypes` / `CreateWindow` / `ConfigureModuleCatalog`
- [ ] 在 `RegisterTypes` 中注册服务，理解 Prism 容器扩展（`IContainerRegistry`）
- [ ] `containerRegistry.RegisterSingleton` / `Register` / `RegisterScoped` 的对应关系
- [ ] 通过构造函数注入获取服务
- [ ] **注意 9.x 的 API 变更**：`OnAppStart` → `CreateWindow`，旧教程会编译不过

*Week 3 — Region 与导航（Prism 最核心的价值）*
- [ ] Region 是什么：XAML 中的占位符，运行时填充 View
- [ ] `RegionManager`、`prism:RegionManager.RegionName` 附加属性
- [ ] `IRegionManager.RequestNavigate` 导航
- [ ] `INavigationAware` 接口：`OnNavigatedTo` / `OnNavigatedFrom` / `IsNavigationTarget`
- [ ] 导航参数传递（`NavigationParameters`）
- [ ] View 与 ViewModel 的自动装配：`ViewModelLocator` 约定
- [ ] 导航日志（`IRegionNavigationJournal`）实现前进后退
- [ ] **理解 Region 导航与 Page 导航的区别**，以及为什么 Prism 用 Region

*Week 4 — 对话框与事件*
- [ ] `IDialogService`：注册对话框、传参、返回结果——比手动 `new Window().ShowDialog()` 优雅在哪
- [ ] `IEventAggregator`：发布订阅、`ThreadOption`（UI 线程 vs 后台线程）、弱引用与内存泄漏
- [ ] 事件载荷设计：什么时候该带数据、什么时候只带信号
- [ ] 结合阶段 1 的 `IMessenger`，实践第四节的分工边界

*Week 5 — 模块化*
- [ ] `IModule`、`ModuleCatalog`、模块加载时机（`WhenAvailable` / `OnDemand`）
- [ ] 模块间依赖声明
- [ ] **项目拆分**：把 `Wms.Lab` 拆成多个类库项目（Core / Modules.Inventory / Modules.Inbound / Shell）
- [ ] 模块如何向 Shell 注册自己的导航项

*Week 6 — 重构输出*
- [ ] **输出物**：把 `Wms.Lab` 重构为 Prism 模块化应用
  - 引入 CommunityToolkit.Mvvm 替换手写的 `ViewModelBase` 和 `RelayCommand`
  - 用 Region 导航实现多页面切换（主数据维护 / 库存查询 / 库存调整）
  - 用 `IDialogService` 实现新增编辑弹窗
  - 用 `IEventAggregator` 实现"库存变动后自动刷新查询页"
  - 服务通过 DI 注入，ViewModel 不再 `new` 任何依赖

**验收标准**
- 能解释：如果没有 DI，想给 `InventoryService` 写单元测试会有多难
- `Wms.Lab` 的 ViewModel 全部通过构造函数接收服务，没有一个 `new Service()`
- 能用 Region 导航切换页面，且导航时 ViewModel 生命周期正确（该复用复用、该新建新建）
- 至少 3 个模块，模块间不直接引用，只通过接口和事件通信
- **能给 ViewModel 写一个单元测试**（用 Mock 替换 EF Core 服务）——这是 DI 价值的直接证明

**常见坑**
- ViewModel 里偷偷 `new` 服务，DI 白做
- `IEventAggregator` 订阅后不退订导致内存泄漏（Prism 用弱引用可缓解，但持有强引用字段时仍会泄漏）
- Region 名字符串硬编码散落各处——应该集中定义常量
- 过度模块化：一个只有 6 个页面的应用拆 15 个模块，纯属自找麻烦

---

### 阶段 4：WMS 业务建模（20h / 4 周）

**目标**：这是你另一个明确的知识缺口。**这个阶段不写代码**，产出是设计文档。可以穿插在阶段 2、3 期间进行，作为编码疲劳时的调剂。

**为什么单独设一个阶段**：WMS 的难点不在技术，在业务规则。库存怎么算、批次怎么追、库位怎么推荐、单据状态怎么流转——这些想不清楚，代码写得再漂亮也是个跑不通的玩具。**先建模再编码，能省掉后期大量返工。**

**任务清单**

*Week 1 — 仓储业务全景*
- [ ] 仓库的物理结构：仓库 → 库区 → 巷道 → 货架 → 库位
- [ ] 入库全流程：预约到货 → 收货 → 质检 → 上架
- [ ] 出库全流程：订单 → 波次 → 拣货 → 复核 → 打包 → 发货
- [ ] 库内作业：移库、盘点、补货、库存调整
- [ ] 每个环节的**单据**是什么、**谁**操作、**产生什么数据**

*Week 2 — 核心概念与数据模型*
- [ ] **SKU**：最小库存单位。理解 SKU / SPU / 商品 / 物料 的区别
- [ ] **批次（Lot）**：为什么需要批次管理，批次号从哪来
- [ ] **库位（Location）**：库位编码规则，库位类型（存储位 / 拣货位 / 暂存位）
- [ ] **托盘 / 容器（LPN）**：什么时候需要
- [ ] **库存的三个维度**：在哪个库位、什么 SKU、什么状态（可用 / 冻结 / 待检 / 待上架）
- [ ] **关键设计决策：库存快照 vs 库存流水**
  - 快照表：当前有多少（查询快）
  - 流水表：每一次变动记录（可追溯、可对账）
  - **正确做法是两者都要**，流水是不可变台账，快照由流水汇总而来
- [ ] **输出物**：ER 图（用 SSMS 或 draw.io 画），至少包含：商品、库位、库存快照、库存流水、入库单、入库明细、出库单、出库明细、盘点单

*Week 3 — 业务规则与状态机*
- [ ] **单据状态机**：入库单（草稿 → 已提交 → 收货中 → 已完成 → 已关闭）、出库单同理。**画出状态转换图，标清每个转换的触发条件和前置校验**
- [ ] **库存状态流转**：待检 → 可用 → 冻结 → 已分配 → 已出库
- [ ] **先进先出（FIFO）**：按什么排序（入库时间 / 生产日期 / 效期）
- [ ] **库存可用量计算**：`可用量 = 在库量 - 已分配量 - 冻结量`。这个公式看似简单，但"已分配"什么时候加、什么时候释放，是出库流程的核心
- [ ] **负库存控制**：允不允许、在哪些环节允许（比如盘点调整）
- [ ] **上架策略**：固定库位 / 就近上架 / 同 SKU 集中——教学级选最简单的
- [ ] **拣货策略**：按单拣货 vs 波次拣货——教学级只做按单拣货

*Week 4 — 需求文档与范围裁剪*
- [ ] **输出物 1**：`WMS需求说明书.md`——功能列表、每个功能的输入输出、业务规则、异常处理
- [ ] **输出物 2**：`数据模型设计.md`——表结构、字段、关系、索引设计、状态枚举
- [ ] **输出物 3**：`状态机与流程图.md`——单据状态流转图、库存变动流程图
- [ ] **明确划定最小闭环边界**，写清"本期不做什么"：
  - ✅ 做：单仓、单库区、按单拣货、批次管理（简化）、FIFO 推荐、手动盘点
  - ❌ 不做：波次、月台预约、RF 手持、计费、多仓调拨、与 ERP 对接、复杂权限

**验收标准**
- 能用白板画出完整的入库和出库流程图，说清每一步产生什么单据、改什么库存
- ER 图能支撑所有已划定功能，没有冗余表也没有缺失字段
- 每个单据的状态机都有明确的转换条件，不存在"状态死锁"或"无法回退"的情况
- **能回答这个问题**：一个 SKU 在 3 个库位、2 个批次、2 种库存状态下，你的表结构怎么存、怎么查出"可用量"

**常见坑**
- 库存只存一张快照表，没有流水——出问题时完全无法追溯，也无法对账
- 状态机设计得太随意，后期加功能时发现状态转换走不通
- 范围失控：想做的功能越来越多，最后什么都做不完。**教学级项目的价值在于跑通闭环，不在于功能多**
- 忽视"库存已分配"这个中间态，直接扣减在库量，导致多人同时拣货时超卖

---

### 阶段 5：WMS 最小闭环实现（80h / 16 周）

**目标**：把前四个阶段的能力整合成正式项目 `Wms`。

**迭代节奏**：每 2 周一个迭代，每个迭代产出可运行的增量。**不要一次写完所有模块再调试**——那样你会在第 14 周陷入调试地狱。

**任务清单**

| 迭代 | 周期 | 内容 | 交付标准 |
|---|---|---|---|
| 1 | Week 1-2 | 解决方案骨架 + 数据库 | 项目分层建好，EF Core 迁移建库成功，种子数据就绪，空 Shell 能启动 |
| 2 | Week 3-4 | 基础主数据 | 商品（SKU）管理 CRUD、库位管理 CRUD，含校验与分页 |
| 3 | Week 5-6 | 入库：收货 | 创建入库单、录入收货明细、提交，库存进入"待上架"状态，写流水 |
| 4 | Week 7-8 | 入库：上架 | 从待上架库位转到存储库位，FIFO 批次记录，库存状态转"可用" |
| 5 | Week 9-10 | 库存查询 | 多维筛选（SKU / 库位 / 批次 / 状态）、汇总视图、库存流水查询 |
| 6 | Week 11-12 | 出库：分配与拣货 | 创建出库单、按 FIFO 推荐批次库位、分配库存（占用可用量）、拣货确认 |
| 7 | Week 13-14 | 出库：复核发货 | 复核、发货确认，库存扣减，流水记录，单据关闭 |
| 8 | Week 15-16 | 盘点与收尾 | 创建盘点单、录入实盘数、生成差异调整、UI 打磨、异常处理完善 |

**架构分层**（迭代 1 就要定好）

```
Wms.sln
├── Wms.Core              # 实体、DTO、枚举、接口定义（无 UI、无 EF 依赖）
├── Wms.Data              # EF Core DbContext、仓储实现、迁移
├── Wms.Services          # 业务服务（入库、出库、库存、盘点），事务边界在此
├── Wms.Modules.MasterData    # Prism 模块：主数据
├── Wms.Modules.Inbound       # Prism 模块：入库
├── Wms.Modules.Inventory     # Prism 模块：库存查询
├── Wms.Modules.Outbound      # Prism 模块：出库
├── Wms.Modules.Stocktake     # Prism 模块：盘点
├── Wms.Shell             # Prism 主程序、Region 布局、导航菜单
└── Wms.Tests             # xUnit 单元与集成测试
```

**每个迭代的固定动作**
1. 设计：明确本迭代的表结构变更、服务接口、UI 页面
2. 迁移：`dotnet ef migrations add` 并检查生成的 SQL
3. 服务层：先写接口，再写实现，事务边界放在服务层
4. UI：View + ViewModel，通过 DI 注入服务
5. 测试：至少给核心业务规则写单元测试（尤其是库存计算）
6. 提交：git commit，写清本次增量

**验收标准（最终）**
- 能完整跑通：新建商品 → 建库位 → 收货入库 → 上架 → 查询库存 → 建出库单 → 拣货 → 发货 → 盘点
- 库存数量在任何操作后都账实相符，流水可完整追溯
- 并发场景下不会超卖（有测试证明）
- 所有业务规则都有对应的单元测试
- 应用启动后连接数据库失败时有友好提示，不崩溃

---

### 阶段 6：测试与部署（20h / 4 周）

**目标**：把项目从"我机器上能跑"变成"能交付给别人安装使用"。

**任务清单**

*Week 1-2 — 测试*
- [ ] xUnit 基础：`[Fact]` / `[Theory]`、断言、测试命名规范
- [ ] 用 NSubstitute mock 服务接口，测试 ViewModel 逻辑
- [ ] **EF Core 测试的正确姿势**：
  - ⚠️ `InMemory` provider 有坑——它不支持事务、不强制执行外键约束、不校验唯一索引。**用它测出来的"通过"可能是假的**
  - 推荐：`SQLite in-memory`（更接近真实行为）或 **Testcontainers**（跑真实 SQL Server 容器，最可靠）
- [ ] 集成测试：覆盖完整业务链路（收货 → 上架 → 拣货 → 发货）
- [ ] 测试并发场景：模拟两个线程同时拣同一批库存
- [ ] 目标覆盖率：核心业务服务（库存计算、状态流转）≥80%，UI 层不强求

*Week 3 — 打包发布*
- [ ] `dotnet publish` 参数：`-c Release -r win-x64`
- [ ] **自包含 vs 框架依赖**：
  - 自包含（`--self-contained true`）：体积大（~150MB），但目标机器不用装 .NET
  - 框架依赖（`--self-contained false`）：体积小（~5MB），但目标机器要装 .NET 10 Desktop Runtime
  - **给人用选自包含，给自己用选框架依赖**
- [ ] 单文件发布 `--p:PublishSingleFile=true` 的利弊（启动稍慢、调试不便）
- [ ] `appsettings.json` 配置外置，连接串不要硬编码进程序
- [ ] 日志：接入 Serilog，输出到文件，含异常堆栈——**没有日志的桌面应用出了问题根本没法排查**

*Week 4 — 数据库部署*
- [ ] 生产库部署方案：Migration Bundle（`dotnet ef migrations bundle`）或幂等 SQL 脚本
- [ ] 编写部署手册：全新环境从零安装 SQL Server → 建库 → 跑迁移 → 初始化种子数据
- [ ] 数据库备份与还原演练：完整备份、差异备份、事务日志备份，实际还原一次
- [ ] 连接串安全：不要明文放配置文件（至少了解 DPAPI 或环境变量方案）
- [ ] **实际演练一次**：找一台没装过开发环境的机器（或新建一个虚拟机），照着你的部署手册从零装起来。**装不成功的部署手册等于没有**

**验收标准**
- 核心业务规则有单元测试保护，改代码时测试会告诉你有没有改坏
- 能在一个干净环境按手册完成部署并跑通完整业务闭环
- 应用有日志文件，异常能被记录
- 数据库能备份能还原

---

## 六、进度追踪表

建议每完成一项就在文件里打勾，并把实际日期填进去。**记录实际耗时**，如果某阶段实际工时远超预估，说明这里有你没意识到的知识缺口，值得回头补。

| 阶段 | 预估工时 | 实际工时 | 开始日期 | 完成日期 | 状态 |
|---|---|---|---|---|---|
| 0 环境搭建与技术验证 | 4h | | | | ⬜ 未开始 |
| 1 WPF + MVVM 深化 | 20h | | | | ⬜ 未开始 |
| 2 EF Core + SQL Server | 30h | | | | ⬜ 未开始 |
| 3 DI 与 Prism | 30h | | | | ⬜ 未开始 |
| 4 WMS 业务建模 | 20h | | | | ⬜ 未开始 |
| 5 WMS 最小闭环实现 | 80h | | | | ⬜ 未开始 |
| 6 测试与部署 | 20h | | | | ⬜ 未开始 |

状态标记：⬜ 未开始 / 🔄 进行中 / ✅ 已完成 / ⏸️ 暂停

---

## 七、风险与应对

| 风险 | 可能性 | 影响 | 应对 |
|---|---|---|---|
| **Prism 9 在 .NET 10 上异常** | 中 | 高 | 阶段 0 就验证。若失败：改用 CommunityToolkit.Mvvm + `Microsoft.Extensions.DependencyInjection` + 自建 Region 导航（用 `ContentControl` + `DataTemplate` 即可实现，约 100 行代码）。这套方案无许可证顾虑，且是 2026 年 WPF 社区的主流选择 |
| **每周 5 小时难以持续** | 高 | 高 | 允许阶段性暂停，但**不要跳过阶段**。宁可慢，不要断层。每次重新开始前先花 30 分钟回顾上次代码 |
| **教程版本对不上** | 高 | 中 | Prism 8 → 9 有破坏性变更；EF Core 各版本 API 也有调整。**以官方文档为准，博客教程只做入门**。官方文档：docs.prismlibrary.com、learn.microsoft.com/ef |
| **范围失控想做太多** | 高 | 高 | 阶段 4 明确写下"本期不做什么"，贴在显眼处。每想加功能先问：这会影响最小闭环跑通吗？不会就先记到 backlog |
| **业务规则想不清就开写** | 中 | 高 | 阶段 4 不许跳过。库存计算和状态机没想明白就写代码，后期返工成本极高 |
| **过度追求架构完美** | 中 | 中 | 教学级项目，能用、能跑通、能讲清原理就够了。不要引入 CQRS、事件溯源、微服务这些超纲概念 |
| **Prism 许可证（若商用）** | 低 | 中 | 个人学习无碍。若将来商用，核对 Community License 三条资格线，不符则购买商业许可或改用纯开源方案 |

---

## 八、学习资源

### 官方文档（第一优先级）
- **Prism 官方文档**：docs.prismlibrary.com —— 有完整的 WPF 章节
- **Prism 官方示例仓库**：github.com/PrismLibrary/Prism-Samples-Wpf —— 每个特性都有独立示例，比看博客有效
- **EF Core 官方文档**：learn.microsoft.com/ef/core —— 中文，质量很高，"入门"和"什么是 EF Core"两章必读
- **CommunityToolkit.Mvvm 文档**：learn.microsoft.com/dotnet/communitytoolkit/mvvm/ —— 源生成器的用法讲得很清楚
- **WPF 官方文档**：learn.microsoft.com/dotnet/desktop/wpf/ —— 数据绑定和模板章节值得反复看
- **SQL Server Express 下载**：microsoft.com/sql-server/sql-server-downloads

### 关于视频/博客教程的建议
- 中文 WPF + Prism 教程数量不少，但**大部分基于 Prism 7/8 和 .NET Framework / .NET 6**，API 与 9.x 有差异。看的时候注意甄别
- 优先看**近一年内**的内容，或明确标注 Prism 9 的
- 博客适合建立直觉，官方文档适合查准确 API。**别用博客当 API 参考**

### WMS 业务知识来源
这块中文系统资料较少，建议：
- 搜索关键词：`WMS 仓库管理系统 业务流程`、`仓储管理 入库 出库 盘点 流程`、`库存管理 批次 FIFO`、`WMS 数据模型设计`
- 找几个开源 WMS 项目读代码和数据库设计（GitHub 搜 `WMS`、`仓库管理`）——**看别人怎么设计表结构，比自己空想快得多**
- 如果身边有做仓储/物流的朋友，聊 30 分钟胜过读 10 篇文章

---

## 九、下一步：本周就做这三件事

1. **装环境**：.NET 10 SDK、SQL Server Express、SSMS、IDE
2. **跑通 Prism 验证**：新建空 WPF 项目 → 装 `Prism.Unity` 9.0.537 → 改 `App` 继承 `PrismApplication` → 实现 `CreateWindow` → 启动成功。**这一步决定后续走 Prism 路线还是备选路线，不要跳过**
3. **建仓库**：在 `D:\Project\Git\Learning` 下 `git init`，把这份计划提交进去，之后每个阶段的练习都在这个仓库里演进

完成这三件事后，你就正式进入阶段 1。

---

## 附录：关于这份计划的说明

- 工时为预估值，基于"每周 5 小时、有效学习"的假设。实际会因基础差异浮动 ±30%
- 各阶段的"验收标准"是硬门槛，**没达到就不要进下一阶段**。跳阶段的代价通常在 3 个月后才显现，那时补救成本远高于当初多花两周
- 版本信息核实于 2026-10-19（Prism.Wpf 9.0.537、CommunityToolkit.Mvvm 8.4.2、.NET 10 LTS、EF Core 10 LTS）。开工前建议再确认一次是否有新版本发布
