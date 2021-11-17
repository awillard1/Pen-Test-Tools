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

class RespResult:
    """Simple Object"""
    def __init__(self, status_code="", contentLength="", isLenDifferent=False, isPayloadInBody=False):
        self.status_code = status_code
        self.contentLength = contentLength
        self.isLenDifferent = isLenDifferent
        self.isPayloadInBody = isPayloadInBody
        
def process_response(resp, baselineLen):
    retval = RespResult()
    if baselineLen != len(resp.content):
        retval.isLenDifferent = True
    else:
        retval.isLenDifferent = False
    
    if headerPayload in str(resp.content):
        retval.isPayloadInBody = True
    else:
        retval.isPayloadInBody = False
        
    retval.status_code = str(resp.status_code)
    retval.contentLength = str(len(resp.content))
    return retval 
  
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

def headersList(filename):
    items = []
    with open(dir + filename, "r") as f:
        for line in f:
            items.append(line.rstrip('\n'))
    return items

def injectHeaders(url, cookies):
    _headers = headersList("/headers.txt")
    _loadedheaders = headersList("/headers-small.txt")
    print("\nPayload to be passed:\n" + headerPayload)
    ua = get_random_useragent()
    print("\nUsing the User-Agent:  " +ua)

    with requests.Session() as s:
        s.proxies['http'] = _proxy
        s.proxies['https'] = _proxy
        
        print("\nSend Initial Request - TODO use for comparison\n")
        resp = requests.get(url, headers={'User-Agent': ua},cookies=cookies, verify=False)
        baselineLen = len(resp.content)
        print("Baseline Request - Status: " + str(baselineLen) + " - Content-Length: " + str(len(resp.content)) + "\n")
        
        print("\nSend header key/value pair\n")
        for h in _loadedheaders:
            item = h.split(":",1)
            hdrs = {'User-Agent':ua, item[0].lstrip() : item[1].lstrip() }
            resp = requests.get(url, cookies=cookies,headers=hdrs, verify=False)
            result = process_response(resp, baselineLen)
            print(h + ": Status: " + result.status_code + " - Content-Length: " + ("","*")[result.isLenDifferent] + result.contentLength + "\r")
        
        print("\nSend 1 header in each request.\n")
        for h in _headers:
            hdrs = {'User-Agent':ua, h : headerPayload }
            resp = requests.get(url, cookies=cookies,headers=hdrs, verify=False)
            result = process_response(resp, baselineLen)
            print(h + ": Status: " + result.status_code + " - Content-Length: " + ("","*")[result.isLenDifferent] + result.contentLength + " - Contains Payload: " + str(result.isPayloadInBody) +"\r")
        
        
        
        print("\nSend 1 massive request\n")
        allheaders = {}

        for h in _headers: 
            s = h.lower()
            if s == "host":
                print("Ignoring host header")
            elif s == "content-length":
                print("Ignorning content-length header")
            else:
                allheaders[h]=headerPayload
        
        resp = requests.get(url, cookies=cookies, headers=allheaders, verify=False)
        result = process_response(resp, baselineLen)
        print("All Header Request - Status: " + result.status_code + " - Content-Length: " + result.contentLength + " - Contains Payload: " + str(result.isPayloadInBody) +"\n")
  

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