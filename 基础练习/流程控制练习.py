# 需求：根据输入的用户名密码执行登录操作，具体要求如下：
# 1.正确的用户名密码为 admin/123456 ， zhangsan/234567 , hjq/999999
# 2.输入用户名和密码进行登录，直到登录成功，程序结束运行，否则继续输入
# 3.输入的用户密码不能为空
# 4.登录成功输出“登录成功”否则输出“用户名或密码错误，请重新输入"

# 1.传统写法
# account1,account2,account3  = 'admin','zhangsan','hjq'
# password1,password2,password3 = '123456','234567','999999'
#
# while True:
#     in_account =  input('请输入用户名：')
#     in_password = input('请输入密码：')
#     if in_account == account1 and in_password == password1:
#         print('登录成功')
#         break
#     elif in_account == account2 and in_password == password2:
#         print('ok')
#         break
#     elif in_account == account3 and in_password == password3:
#         print('ok')
#         break
#     else:
#         print('ng')
# ------------------------------------------------------------
# 2.使用字典简化代码
# users = {
#     'admin':'123456',
#     'zhangsan':'234567',
#     'hjq':'999999'
# }
#
# while True:
#     input_user = input('please input your name:')
#     input_password = input('please input your password:')
#     if input_user in users and input_password == users[input_user]:
#         print('OK')
#         break
#     else:
#         print('Wrong username or password')