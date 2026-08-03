# 定义
# --列表名 = [元素1,元素2,......]
# --可以存放不同类型的数据、可重复、有序、可修改元素
# 索引
# --正向：从0开始  反向：从-1开始（最后一个元素）
# 元素的查看、修改、删除
# --查看：list[index]
# --修改：list[index] = new value
# --删除：del list[index]
# 切片 ： 对操作的list截取其中一部分
# --语法：序列数据[start:end:step]（不包括end）正向反向都可以 默认step=1
# 示例：
# 1.定义
# s=[1,2,3,'a',True,99,100,2.0,3.13]
# 2.索引/查看
# first_value = s[0]
# last_value = s[-1]
# 3.修改
# print('before modify')
# print(' '.join(str(x) for x in s))
# s[1] = 9
# print('after modified')
# print(' '.join(str(x) for x in s))
# 4.删除
# print('before delete')
# print(' '.join(str(x) for x in s))
# del s[0]
# print('after delete')
# print(' '.join(str(x) for x in s))
# 5.切片
# print('切片功能')
# l = [1,2,3,4,5,6,7,8,'a','b','c']
# list[start:end:step] 下面几种等同写法
# print(l[0:5:1])
# print(l[:5:])
# print(l[:5])
#from random import random

# 反向截取
# print(l[0:-2:1]) #从0开始到-2（'a'）
# print(l[-5:-2:1])

# 列表常用方法
# append() 尾部追加
# insert(index,value) 插入
# remove(value) 移除列表中匹配到的值
# pop(index) 删除指定索引位置元素（没有索引默认删除最后一个元素)
# sort() 排序（需要同一类型元素)
# revert() 反转列表

# 示例：对列表排序并输出最大最小平均值
# list_int = [12, 2, 23, 14, 55, 26, 87, 18, 9, 13]
# print(f"before sorting: {list_int}")
# list_int.sort()
# print(f"after sorting: {list_int}")
# min_value = min(list_int)
# max_value = max(list_int)
# print(f"min value: {min_value}")
# print(f"max value: {max_value}")
# sum_value = sum(list_int)
# print(f"sum value: {sum_value}")
# average_value = sum(list_int) / len(list_int)
# print(f"average value: {average_value}")

# 示例2：合并列表并去重
#list_int1 = [12, 2, 23, 14, 55, 26, 87, 18, 9, 13]
#list_int2 = [16, 1, 23, 4, 55, 21, 87, 19, 9, 17]
#print(list_int1)
#print(list_int2)
#print(f"set方法不保证顺序{list(set(list_int1+list_int2))}")

# 方法2：循环（保持顺序）
#result2 = []
# for item in list_int1 + list_int2:
#    if item not in result2:
#        result2.append(item)
#print(f"循环方法（保持顺序）: {result2}")

# 方法3： dict.fromkeys 保持原有顺序
#result3 = list(dict.fromkeys(list_int1 + list_int2))
#print(f"dict.fromkeys(保持原先列表顺序) : {result3}")

# 示例3：按照一定规则快速生成一个列表的方法 --> 语法格式：[要插入的值 for i in 序列、列表]
# 列表推导式
#num_list = [i**2 for i in range(1,21)] #生成1-20的平方插入列表,range(start,end)这个方法用于生成一定范围内的数字
#print(num_list)

# 示例4：从一个数字列表中提取所有偶数，并计算其平方，组成一个新列表
# 列表推导式 --> [要插入的值 for i in list if]
# import random
# num_list3 = [random.randint(1,10) for i in range(10)]
# print(num_list2)
# num_list3 = [i**2 for i in num_list2 if i%2==0]
# print(num_list3)

# 练习1：将如下多个列表合并为一个列表并去重，升序后输出
# list1 = ['m','a','c','e','l','h']
# list2 = ['o','a','e','g','k','h']
# list3 = ['j','s','c','l','z']
# list_dick = list(dict.fromkeys(list1+list2+list3))
# print(' '.join(map(str,list_dick)))
# list_sort = sorted(list_dick)
# print(' '.join(map(str,list_sort)))
# print(' '.join(map(str,list_dick)))
# list_dick.sort(reverse=True)
# print(' '.join(map(str,list_sort)))

# 练习2：模拟加载进度百分比
# import time
# my_list = [10, 20, 30, 40, 50]
# for item in my_list:
#     print(f'\r当前值: {item}%', end='', flush=True)
#     time.sleep(0.5)  # 模拟耗时操作
# print()  # 最后换行

# 练习3：将列表中能被3或5整除的元素提取出来并取平方，组成一个新的列表
# import random
# list4 = [random.randint(1,101) for _ in range(20)]
# print(list4)
# list_multi = [x**2 for x in list4 if (x % 3 == 0 or x % 5 == 0) ] # 列表推导式
# print(list_multi)


# 练习4：将列表中正数提取出来组成一个新列表
# import random
# list6 = [random.randint(-10,10) for i in range(10)]
# print(list6)
# print(','.join(map(str,list6)))
# list_z = [x for x in list6 if x>0]
# print(list_z)

