# 集合（set）是一种无序的、不可重复的、可修改的数据容器
# 定义集合：
s1 = {"c","b","a","z"}
print(s1)
# 定义空集合
s2 = set()

# 常用操作：
# add(..)
s1.add("ok")
print('After add')
print(s1)

# remove(..) 删除指定元素
s1.remove("a")
print('After remove')
print(s1)

# pop() 随即删除集合中的元素并返回
popVal = s1.pop()
print(f'pop value {popVal}')

# clear() 清空集合
s1.clear()

# 求两个集合的差集 difference()
s3 = {1,2,3,4,5,6,7,8,9,10}
s4 = {1,2,3,4,5,12,11,13,15}
dif = s3.difference(s4)
print(f"diff {dif}")

# 求两个集合的交集 union()
s3.union(s4)
print(f"union {s3.union(s4)}")

# 求两个集合的并集
s3.intersection(s4)
print(f"intersection {s3.intersection(s4)}")