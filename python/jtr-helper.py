import os
import re
import argparse
import subprocess

######## BEGIN CONFIGURATION ########
johnConf = "/home/awillard/src/john/run/john.conf"
johnLocalConf = "/home/awillard/src/john/run/john-local.conf"
jtrLocation = "/home/awillard/src/john/run/john"
johnFork = "12"
######## END CONFIGURATION   ######## 

def readConf():
    i = 0
    jtrconf = open(johnConf, "r")
    for line in jtrconf:
        match = re.match(r"^\[List.Rules.(.*)\].*$", line)
        if match:
            rule = match.group(1)
            print("["+str(i)+"] "+rule)
            ruleList.append(rule)
            i = i + 1
    if (os.path.exists(johnLocalConf)):
        jtrlocalconf = open(johnLocalConf,"r")
        for line in jtrlocalconf:
            match = re.match(r"^\[List.Rules.(.*)\].*$", line)
            if match:
                rule = match.group(1)
                print("["+str(i)+"]"+rule)
                ruleList.append(rule)
                i = i + 1
    

def createRuleList():
    readConf()
    print("\r\nIf you want to run all the rules listed, enter * and press enter")
    print("If you want to run some rules, comma separate the numbers and press enter\r\n")
    val = input("Enter the number of the rule to run: ")    
    if ("," in val):
        try:
            listNumberRule = val.split(",")
            for ruleNumber in listNumberRule:
                rule = ruleList[int(ruleNumber)]
                print(rule + " ruleset will be used")
                crackpwds(rule)
        except:
            print("unable to split and run jtr")
            exit();    
    elif (val == "*"):
        for r in ruleList:
            print("Rule: " + r)
            crackpwds(r)
    elif (int(val)):
        rule = ruleList[int(val)] 
        print(rule + " ruleset will be used")
        crackpwds(rule)
    else:
        exit()

def crackpwds(rule):
    subprocess.call(jtrLocation + " " + hashFile + " --wordlist:" + wordlist + " --format:" + hashFormat + " --rules:" + rule + " --fork:" + johnFork, shell = True)

def main():
    createRuleList()

if __name__ == '__main__':
    print("__________________________________")
    print("JTR HELPER 0.1")
    print("__________________________________\n\n")
    parser = argparse.ArgumentParser()
    parser.add_argument("-f", "--format", help="specify the jtr hash format")
    parser.add_argument("-w", "--wordlist", help="specify the file with wordlist")
    parser.add_argument("-hash", "--hashes", help="specify the file with wordlist")
    
    args = parser.parse_args()
    
    if args.format and args.wordlist and args.hashes:
        hashFormat=args.format
        wordlist = args.wordlist
        hashFile = args.hashes
        ruleList = []
    else:
        parser.print_help()
        exit()
    main()
