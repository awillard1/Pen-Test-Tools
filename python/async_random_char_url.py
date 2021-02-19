import string
import random
import argparse
import ssl
import asyncio
import aiohttp
import aiohttp.connector
from urllib import request as urlrequest

async def attack_url(x,out):
    out.append(test_url(x))

async def test_url(s):
    res = ''.join(random.choices(string.ascii_uppercase + string.ascii_lowercase + string.digits, k = n))
    url = inurl.replace(repval,str(res))
    async with s.get(url, proxy=my_proxy) as resp:
        assert resp.status == 200
        await resp.text()
        print(res)


async def main():
    print("###Beginning Random String Attack on URL")

    async with aiohttp.ClientSession(connector=aiohttp.TCPConnector(ssl=False,limit=2)) as session:
        tasks = [(test_url(session)) for x2 in range(0,maxlen)]
        await asyncio.gather(*tasks, return_exceptions=True)
    print("###Finished")
    exit()

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
        my_proxy = "http://" + args.proxy
    else:
        my_proxy = ''
    n=int(args.val_length)
    maxlen=int(args.loop_count)
    inurl=str(args.url)
    repval=str(args.replace_value)
    
    loop = asyncio.get_event_loop()
    try:
        loop.run_until_complete(main())
    finally:
        # Shutdown the loop even if there is an exception
        loop.close()
    
    
