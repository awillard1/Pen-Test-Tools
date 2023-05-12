import os
import argparse
import subprocess

def main():
    userAgent='Mozilla/5.0 (X11; Linux x86_64; rv:58.0) Gecko/20100101 Firefox/58.0'
    tracker = " -H X-Tracking-Host:" + targetHost +" "
    userAgnt = " -H \"User-Agent:" + userAgent + "\" "
    print("###### Test 1 ######\r\n")
    subprocess.call('curl https://' + targetHost + '/jsonapi/ ' + proxy + ' -L -m 4 -H X-Tracking-Type:1 ' + tracker + ' --connect-timeout 4 -svk --http1.1 '+ userAgnt, shell=True)
    print("###### Test 2 ######\r\n")
    subprocess.call('curl https://' + targetHost + '/cm/jsonapi/ ' +proxy + ' -L -m 4 -H X-Tracking-Type:2 ' + tracker + ' --connect-timeout 4 -svk --http1.1 '+ userAgnt, shell=True)
    print("\r\n\r\n\r\n###### Test 3 ######\r\n")
    subprocess.call('curl https://' + targetHost + '/content-management/jsonapi/ ' +proxy + ' -L -m 4 -H X-Tracking-Type:3 ' + tracker + ' --connect-timeout 4 -svk --http1.1 '+ userAgnt, shell=True)


if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-t", "--target", help="specify the vulnerable target")
    parser.add_argument("-p", "--proxy", help="specify your proxy eg. http://127.0.0.1:8080")
    args = parser.parse_args()
    if args.target:
        targetHost = args.target
    else:
        parser.print_help()
        exit()
    if args.proxy is not None:
        proxy = " --proxy " + args.proxy + " "
    else:
        proxy = ""
    main()