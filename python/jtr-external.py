import os
import re
import argparse
import subprocess
import signal

######## BEGIN CONFIGURATION ########
johnConf = "/home/awillard/src/john/run/john.conf"
johnLocalConf = "/home/awillard/src/john/run/john-local.conf"
jtrLocation = "/home/awillard/src/john/run/john"
######## END CONFIGURATION   ######## 

def setJohnFork():
    global johnFork
    johnFork = input("Enter the --fork value for John (1 to " + str(os.cpu_count()) + "): ")    

    if (johnFork.isnumeric() == False):
        print("--fork was not numeric. Please set --fork to a value between 1 and " + str(os.cpu_count()))
        exit()
    elif (int(johnFork) < 1 or int(johnFork) > os.cpu_count()):
        print("Please set --fork to a value between 1 and " + str(os.cpu_count()))
        exit()

def readConf():
    i = 0
    if (os.path.exists(johnConf) == False):
        print("Unable to locate john.conf: " + johnConf + "\r\nExiting")
        exit()
    jtrconf = open(johnConf, "r")
    for line in jtrconf:
        match = re.match(r"^\[List.External\:(.*)\].*$", line)
        if match:
            rule = match.group(1)
            print("["+str(i)+"] "+rule)
            ruleList.append(rule)
            i = i + 1
    if (os.path.exists(johnLocalConf)):
        jtrlocalconf = open(johnLocalConf,"r")
        for line in jtrlocalconf:
            match = re.match(r"^\[List.External:(.*)\].*$", line)
            if match:
                rule = match.group(1)
                print("["+str(i)+"] "+rule)
                ruleList.append(rule)
                i = i + 1

def createRuleList():
    readConf()
    print("\r\nIf you want to run all the rules listed, enter * and press enter")
    print("If you want to run some rules, comma separate the numbers and press enter\r\n")
    val = input("Enter the number of the rule to run: ")    
    if ("," in val):
        #try:
        listNumberRule = val.split(",")
        for ruleNumber in listNumberRule:
            if (ruleNumber.isnumeric() and int(ruleNumber) >= 0 and int(ruleNumber) <= len(ruleList)):
                rule = ruleList[int(ruleNumber)]
                print(rule + " ruleset will be used")
                crackpwds(rule)
        #except:
        print("unable to split and run jtr")
        exit();    
    elif (val == "*"):
        for r in ruleList:
            print("Rule: " + r)
            crackpwds(r)
    elif (val.isnumeric() and int(val)>=0 and int(val) <= len(ruleList)):
        rule = ruleList[int(val)] 
        print(rule + " ruleset will be used")
        crackpwds(rule)
    else:
        exit()

def crackpwds(rule):
    global isRunning
    isRunning = True
    
    if (int(johnFork) <= 1):
        subprocess.call(jtrLocation + " " + hashFile + " --format:" + hashFormat + " --external:" + rule + " --force-tty", shell = True)
    else:
        subprocess.call(jtrLocation + " " + hashFile + " --format:" + hashFormat + " --external:" + rule + " --fork:" + johnFork + " --force-tty", shell = True)

def main():
    setJohnFork()
    verifyPaths()
    createRuleList()
    
def verifyPaths():
    if (os.path.exists(hashFile.replace("*","")) == False):
        print("Hash file(s) could not be found:" + hashFile + "\r\nExiting")
        exit()

def handler(signal_received, frame):
    try:
        if (isRunning):
            print("\r\n\r\nAborting current john external rule\r\nIf another external rule is available, cracking will continue.\r\n")
        else:
            exit(0)
    except:
        exit(0)

if __name__ == '__main__':
    #Signal implementation is really ugly, need to revist
    #Use ctrl+c during cracking to skip a wordlist/rule combination
    #hold ctrl+c during cracking to kill jtr and this script
    signal.signal(signal.SIGINT, handler)
    print("__________________________________")
    print("JTR External 0.1")
    print("__________________________________\n\n")
    parser = argparse.ArgumentParser()
    parser.add_argument("-f", "--format", help="specify the jtr hash format")
    parser.add_argument("-hash", "--hashes", help="specify the file with hashes")
    
    args = parser.parse_args()
    if args.format and args.hashes:
        hashFormat=args.format
        hashFile = args.hashes
        ruleList = []
    else:
        parser.print_help()
        exit()
    main()
