#!/usr/bin/env python3
import sys
import argparse
import os
import requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning

proxies = {}
headers = {'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0'}

def isValidDomain(domain):
    url = 'https://login.microsoft.com/getuserrealm.srf?login=doesnotmatter@' + domain + '&json=1'
    response = requests.get(url, verify=False, proxies=proxies, headers=headers)
    retval = False
    try:
        nsType = response.json().get('NameSpaceType').lower()
        errdomain = f"\nDomain: {domain}"
        if nsType == "federated":
            print (f"{errdomain} is a federated domain. Exiting\n" )
            retval = False
        elif nsType == "unknown":
            print (f"{errdomain} is not managed by O365. Exiting\n" )
            retval = False
        elif nsType == "managed":
            print (f"{errdomain} is managed by O365.\n" )
            retval = True
        else:
            print ("\n** Unknown response for domain. Exiting\n")
            retval = False
    except:
        print("Unable to communicate with the microsoft.com domain")
    return retval

def getUsersFromFile(fileNames):
    try:
        with open(fileNames, "r") as enum_users:
            lines = enum_users.readlines()
    except Exception as theFile:
        print(f"\nUnable to open the username file: {fileNames}")
        print(f"{theFile}\n")
        exit()
    return lines

def getDataForUsers(fileNames,domain):
    url = 'https://login.microsoft.com/common/GetCredentialType'
    names = getUsersFromFile(fileNames)
    usersTested = list()
    print ("Testing usernames (this may take awhile)\n")
    for user in names:
        user2enum = user.strip() + "@" + domain
        try:
            responseResult = requests.post(url, verify=False, proxies=proxies, headers=headers, json={"Username": user2enum}).json().get('IfExistsResult')
            if responseResult == 0:
                userTuple = (1,user2enum)
                usersTested.append(userTuple)
            elif responseResult == 1:
                userTuple = (2, user2enum)
                usersTested.append(userTuple)
            else:
                userTuple = (3, user2enum)
                usersTested.append(userTuple)
        except:
            print("Unable to communicate with microsoft.com. Exiting\n")
            exit()
    displayResults(usersTested)
            
def displayResults(userResults):
    print("Tested Valid Users:")
    validUsers=False
    countUsers = 0
    for usernames in userResults:
        if usernames[0]==1:
            print(usernames[1])
            validUsers = True
            countUsers += 1
    if not validUsers:
        print("0 valid usernames discovered\n")
    else:
        print("\n" + f"{countUsers} valid usernames were discovered")

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-p","--proxy", help="proxy")
    parser.add_argument("-d","--domain", help="domain")
    parser.add_argument("-u","--usersfile", help="usersfile")
    args = parser.parse_args()
    requests.packages.urllib3.disable_warnings(InsecureRequestWarning)
    if not args.domain:
        print("\nERROR: Domain was not specified\n")
        exit()
    if not args.usersfile:
        print("\nERROR: User file was not specified\n")
        exit()
    if not os.path.isfile(args.usersfile):
        print("\nERROR: User file was not found\n")
        exit()
    if args.proxy:
        proxy = args.proxy
        proxies = {
            "http": "http://" + proxy, "https": "https://" + proxy,}
    fileuserenum=args.usersfile

    if isValidDomain(args.domain):
        getDataForUsers(fileuserenum,args.domain)
    else:
        exit()
