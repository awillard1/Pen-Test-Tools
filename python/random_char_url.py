import string
import random
import argparse
import ssl
from urllib import request as urlrequest


def main():
    try:
        _create_unverified_https_context = ssl._create_unverified_context
    except AttributeError:
        pass
    else:
        ssl._create_default_https_context = _create_unverified_https_context

    print("###Beginning Random String Attack on URL")
    print(maxlen)
    print(n)
    for x in range(0, maxlen):
        try:
            res = ''.join(random.choices(string.ascii_uppercase + string.ascii_lowercase + string.digits, k = n))
            url = inurl.replace(repval,str(res))
            req = urlrequest.Request(url)
            if '' != my_proxy:
                req.set_proxy(my_proxy, 'http')
                req.set_proxy(my_proxy, 'https')
            response = urlrequest.urlopen(req)
            response.close()
            print(str(res))
        except:
            pass

    print("###Finished")

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-u", "--url",
                        help="url that contains the data to replace")
    parser.add_argument("-cnt", "--loop_count",
                        help="the number of iterations")
    parser.add_argument("-vl", "--val_length",
                        help="the length of random data")
    parser.add_argument("-r", "--replace_value",
                        help="Value to replace in the URL")
    parser.add_argument("-pr", "--proxy", 
                        help="Specify a proxy to use (-p http://127.0.0.1:8080)")
    args, unknown = parser.parse_known_args()
    if not args.url and not args.replace_value and not args.loop_count and not args.val_length:
        parser.print_help()
        print("\n[-] Please specify a URL (-u), number of iterations (-cnt), length of random payload (-vn), and value to replace in url (-r) .\n")
        exit()
    my_proxy = ''
    if args.proxy:
        my_proxy = args.proxy
    else:
        my_proxy = ''
    n=int(args.val_length)
    maxlen=int(args.loop_count)
    inurl=str(args.url)
    repval=str(args.replace_value)
    main()
    
    
