# 组包和解包是将多个变量组合一个容器(list/tuple)和将容器(list/tuple)的所有元素赋值个多个变量
a,b,c = 1,2,3
tuple1 = (a,b,c) #组包
d,e,f = tuple1 #解包

# 示例：将a = 10, b = 20，a,b值交换
# a = 10
# b = 20
# t = a,b # 组包
# b,a = t
# print(a,b)
# 最简化写法
# a,b = b,a # b,a相当于组包

# 示例：获取一组学生的总分平均分，获取所有学生各科的总分最高分最低分平均分，获取平均分大于90的学生列表
students = (
    ("S01","Et",83,93,89),
    ("S02","Qt",63,78,89),
    ("S03","Ft",95,93,96),
    ("S04","Jt",77,93,89),
    ("S05","kt",98,99,94),
)

# 打印所有学生的总分，平均分
print('学号\t\t姓名\t\t语文\t\t英语\t\t数学\t\t总分\t\t平均分')
for stuId,name,chinese,english,math in students:
    total = chinese+english+math
    average = float(total/3)
    print(f"{stuId}\t\t{name}\t\t{chinese}\t\t{english}\t\t{math}\t\t{total}\t\t{average:.1f}")

# 打印各科成绩总分、最高最低分、平均分
## 先获取各科成绩列表
chinese = [s[2] for s in students]
english = [s[3] for s in students]
math = [s[4] for s in students]
## 打印各科
print()
print(f'{sum(chinese)}\t{sum(chinese)/len(chinese)}\t{max(chinese)}\t{min(chinese)}')
print(f'{sum(english)}\t{sum(english)/len(english)}\t{max(english)}\t{min(english)}')
print(f'{sum(math)}\t{sum(math)/len(math)}\t{max(math)}\t{min(math)}')

# 打印平均分在90以上的学生列表
print('学号\t\t姓名\t\t语文\t\t英语\t\t数学\t\t总分\t\t平均分')
for stuId,name,chinese,english,math in students:
    total = chinese+english+math
    average = float(total/3)
    if average > 90:
        print(f'{stuId}\t\t{name}\t\t{chinese}\t\t{english}\t\t{math}\t\t{total}\t\t{average:.1f}')





