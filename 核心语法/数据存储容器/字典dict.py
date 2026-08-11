# 字典是一种以键值对形式存储的容器，键（key）不可重复，如果重复，前一个key的值会被后一个key的值覆盖
# 定义：dict1 = { "key1":value,"key2":value} key不能用可修改的类型例如集合、列表等，可以用字符串、int、float、元组等类型
# 定义空字典 dict2 = dict() / dict2 = {}

# 示例：开发一个购物车系统，支持增删改查，数据内容包括商品名称、价格、数量
shopping_cart = {}
menu = """
########### 购物车系统 ############
#           1.添加商品            #
#           2.修改商品            #
#           3.删除商品            #
#           4.查询商品            #
#           5.列举商品            #
#           6.退出购物车          #
##################################
"""

print('欢迎使用购物车系统')

while True:
    print(menu)
    choice = input('请选择功能:')
    match choice:
        case "1": #add
            goods_name = input('请输入商品名称：')
            if goods_name in shopping_cart:
                print('商品已存在，请确认')
                continue
            goods_price = float(input('请输入价格：'))
            goods_num = int(input('请输入数量：'))
            shopping_cart[goods_name] = {'price': goods_price, 'num': goods_num}
        case "2": #modify
            goods_name = input('请输入需要修改的商品名称：')
            if goods_name not in shopping_cart:
                print('该商品不存在，请确认')
                continue
            goods_price = float(input('请输入最新价格：'))
            goods_num = int(input('请输入最新数量：'))
            shopping_cart[goods_name] = {'price': goods_price, 'num': goods_num}
        case "3": #del
            goods_name = input('请输入要删除的商品名称：')
            if goods_name not in shopping_cart:
                print('该商品不存在')
                continue
            shopping_cart.pop(goods_name)
            print(f'商品{goods_name}已删除')
        case "4": #query
            goods_name = input('请输入要查询的商品名称：')
            if goods_name not in shopping_cart:
                print('该商品不存在')
                continue
            print(f'商品:{goods_name} 价格:{shopping_cart[goods_name]['price']} 数量:{shopping_cart[goods_name]['num']}')
        case "5": #show
            for goods_name in shopping_cart.keys():
                print(f'商品:{goods_name} 价格:{shopping_cart[goods_name]['price']} 数量:{shopping_cart[goods_name]['num']}')
        case "6":
            print("bye ~")
            break
        case _:
            print('操作有误，请重新选择')
            pass
