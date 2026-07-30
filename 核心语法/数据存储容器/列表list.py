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
s=[1,2,3,'a',True,99,100,2.0,3.13]
# 2.索引/查看
first_value = s[0]
last_value = s[-1]
# 3.修改
print('before modify')
print(' '.join(str(x) for x in s))
s[1] = 9
print('after modified')
print(' '.join(str(x) for x in s))
# 4.删除
print('before delete')
print(' '.join(str(x) for x in s))
del s[0]
print('after delete')
print(' '.join(str(x) for x in s))
# 5.切片
print('切片功能')
l = [1,2,3,4,5,6,7,8]
# list[start:end:step] 下面几种等同写法
print(l[0:5:1])
print(l[:5:])
print(l[:5])



