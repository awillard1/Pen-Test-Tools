# -*- encoding: utf-8 -*-
# requires a recent enough python with idna support in socket
# pyopenssl, cryptography and idna
# derived from https://gist.github.com/gdamjan/55a8b9eec6cf7b771f92021d93b87b2c
# usage sslexpiry.py -p 443 -f /mnt/d/data/yourhosts.txt
# usage sslexpiry.py -p 443 -s www.YOURSITE.com

import argparse, idna, datetime
from OpenSSL import SSL
from cryptography import x509
from cryptography.x509.oid import NameOID
from socket import socket
from collections import namedtuple

HostInfo = namedtuple(field_names='cert hostname peername', typename='HostInfo')

def load_hosts(li,port):
    result = [] # initialize an empty result list
    for item in li: # loop over the items of the input list
        result.append((item, port)) 
    return result

def verify_cert(cert, hostname):
    cert.has_expired()

def get_certificate(hostname, port):
    try:
        hostname_idna = idna.encode(hostname)
        sock = socket()
        sock.connect((hostname, port))
        peername = sock.getpeername()
        ctx = SSL.Context(SSL.SSLv23_METHOD) # most compatible
        ctx.check_hostname = False
        ctx.verify_mode = SSL.VERIFY_NONE
        sock_ssl = SSL.Connection(ctx, sock)
        sock_ssl.set_connect_state()
        sock_ssl.set_tlsext_host_name(hostname_idna)
        sock_ssl.do_handshake()
        cert = sock_ssl.get_peer_certificate()
        crypto_cert = cert.to_cryptography()
        sock_ssl.close()
        sock.close()
        return HostInfo(cert=crypto_cert, peername=peername, hostname=hostname)
    except Exception as e: 
        print('''» {hostname} « … ERROR : {error}\r\n'''.format(hostname=hostname,error=str(e)))
        return None

def get_alt_names(cert):
    try:
        ext = cert.extensions.get_extension_for_class(x509.SubjectAlternativeName)
        return ext.value.get_values_for_type(x509.DNSName)
    except x509.ExtensionNotFound:
        return None

def get_common_name(cert):
    try:
        names = cert.subject.get_attributes_for_oid(NameOID.COMMON_NAME)
        return names[0].value
    except x509.ExtensionNotFound:
        return None

def get_issuer(cert):
    try:
        names = cert.issuer.get_attributes_for_oid(NameOID.COMMON_NAME)
        return names[0].value
    except x509.ExtensionNotFound:
        return None

def print_basic_info(hostinfo):
    try:
        s = '''» {hostname} « … {peername}
        \tcommonName: {commonname}
        \tSAN: {SAN}
        \tissuer: {issuer}
        \tnotBefore: {notbefore}
        \tnotAfter:  {notafter}
        \texpired or expirying: {expired}
        '''.format(
                hostname=hostinfo.hostname,
                peername=hostinfo.peername,
                commonname=get_common_name(hostinfo.cert),
                SAN=get_alt_names(hostinfo.cert),
                issuer=get_issuer(hostinfo.cert),
                notbefore=hostinfo.cert.not_valid_before,
                notafter=hostinfo.cert.not_valid_after,
                expired=ssl_expires_in(hostinfo.cert.not_valid_after)
        )
        print(s)
    except:
        return
		#do nothing

def check_it_out(hostname, port):
    hostinfo = get_certificate(hostname, port)
    print_basic_info(hostinfo)

def ssl_valid_time_remaining(not_valid_after):
    return not_valid_after - datetime.datetime.utcnow()
	
def ssl_expires_in(not_valid_after, buffer_days=14):
    remaining = ssl_valid_time_remaining(not_valid_after)
    # if the cert expires in less than two weeks, we should reissue it
    if remaining < datetime.timedelta(days=0):
        # cert has expired
        raise AlreadyExpired("Cert expired %s days ago" % remaining.days)
    elif remaining < datetime.timedelta(days=buffer_days):
        # expires sooner than the buffer
        return True
    else:
        # everything is fine
        return False

import concurrent.futures
if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-f","--filename",help="filename for list of hosts")
    parser.add_argument("-p","--port", help="port")
    parser.add_argument("-s","--host",help="host to get alt names from main certificate")
    args = parser.parse_args()
    if not args.port:
        parser.print_help()
        print("\n [-]  Please specify the port to use")
        exit()
    if not args.filename and not args.host:
        parser.print_help()
        print("\n [-]  Please specify either filename using -f or use -s and a single host to check altnames")
        exit()
    if args.filename and args.host:
        parser.print_help()
        print("\n [-]  Please specify either filename using -f or -s single host to check altnames")
        exit()
    host_port = int(args.port)
    HOSTS=[]
    if args.host:
        #Get alternate names from the certificate to process
        host_name = args.host
        hostdata = get_certificate(host_name,host_port)
        HOSTS = load_hosts(get_alt_names(hostdata.cert),host_port)
    if args.filename:
        #Get hostnames from a file to process
        file_name = args.filename
        with open(file_name) as f:
            content = f.readlines()
            content = [x.strip() for x in content] 
        HOSTS = load_hosts(content,host_port)
    with concurrent.futures.ThreadPoolExecutor(max_workers=4) as e:
        for hostinfo in e.map(lambda x: get_certificate(x[0], x[1]), HOSTS):
            print_basic_info(hostinfo)
