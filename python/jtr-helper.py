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
                print("["+str(i)+"] "+rule)
                ruleList.append(rule)
                i = i + 1
    #AddKorelogic
    print("["+str(i)+"] korelogic")
    ruleList.append("korelogic")
    
def loopCrack(rule):
    wordlistdir = wordlistDir.replace("*","")
    for root, dirs, files in os.walk(wordlistdir):
        for file in files:
            if (root == wordlistdir):
                wordlist = root + file
            else:
                wordlist = root +"/"+ file
            
            print("Loading: " + wordlist)
            crackpwds(rule, wordlist)

def createRuleList():
    readConf()
    print("\r\nIf you want to run all the rules listed, enter * and press enter")
    print("If you want to run some rules, comma separate the numbers and press enter\r\n")
    val = input("Enter the number of the rule to run: ")    
    if ("," in val):
        try:
            listNumberRule = val.split(",")
            for ruleNumber in listNumberRule:
                if (ruleNumber.isnumeric() and int(ruleNumber) >= 0 and int(ruleNumber) <= len(ruleList)):
                    rule = ruleList[int(ruleNumber)]
                    print(rule + " ruleset will be used")
                    if (isWordlists):
                        loopCrack(rule)
                    else:
                        crackpwds(rule, wordlist)
        except:
            print("unable to split and run jtr")
            exit();    
    elif (val == "*"):
        for r in ruleList:
            print("Rule: " + r)
            if (isWordlists):
                loopCrack(r)
            else:
                crackpwds(r,wordlist)
    elif (val.isnumeric() and int(val)>=0 and int(val) <= len(ruleList)):
        rule = ruleList[int(val)] 
        print(rule + " ruleset will be used")
        if (isWordlists):
            loopCrack(rule)
        else:
            crackpwds(rule, wordlist)
    else:
        exit()

def updateShell():
    print(shFile)
    if (os.path.exists(shFile) == True):
        subprocess.call(shFile, shell = True)

def crackpwds(rule, wordlist):
    global isRunning
    isRunning = True
    
    if (int(johnFork) <= 1):
        subprocess.call(jtrLocation + " " + hashFile + " --min-length:8 --max-length:30 --wordlist:" + wordlist + " --format:" + hashFormat + " --rules:" + rule + " --force-tty", shell = True)
    else:
        subprocess.call(jtrLocation + " " + hashFile + " --min-length:8 --max-length:30 --wordlist:" + wordlist + " --format:" + hashFormat + " --rules:" + rule + " --fork:" + johnFork + " --force-tty", shell = True)
     
    updateShell()

def main():
    setJohnFork()
    verifyPaths()
    createRuleList()
    
def verifyPaths():
    global wordlistDir
    if (isWordlists):
        wordlistDir = wordlist.replace("*","")
        if (os.path.exists(wordlistDir) == False):
            print("The wordlist directory could not be found:" + wordlistDir + "\r\nExiting")
            exit()
    else:
         if (os.path.exists(wordlist) == False):
            print("The wordlist could not be found:" + wordlist + "\r\nExiting")
            exit()
    
    if (os.path.exists(hashFile.replace("*","")) == False):
        print("Hash file(s) could not be found:" + hashFile + "\r\nExiting")
        exit()

def handler(signal_received, frame):
    try:
        if (isRunning):
            print("\r\n\r\nAborting current john wordlist/rule\r\nIf another wordlist is available, cracking will continue.\r\n")
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
    print("JTR HELPER 0.1")
    print("__________________________________\n\n")
    parser = argparse.ArgumentParser()
    parser.add_argument("-f", "--format", help="specify the jtr hash format")
    parser.add_argument("-w", "--wordlist", help="specify the file with wordlist")
    parser.add_argument("-r", "--recursive", help="used with wordlists if a directory is defined: -w /wordlistDIR/*",action='store_const', const=True)
    parser.add_argument("-hash", "--hashes", help="specify the file with hashes")
    parser.add_argument("-s", "--script", help="specify the bash file to run after each rule runs")
    
    args = parser.parse_args()
    if args.format and args.wordlist and args.hashes:
        hashFormat=args.format
        wordlist = args.wordlist
        hashFile = args.hashes
        shFile = args.script

        if (args.recursive is None and "/*" in args.wordlist):
            print("You must specify a wordlist file. \r\n* can not be used with out the -r option for wordlist.\r\nPlease correct: " + args.wordlist)
            exit()
        elif (args.recursive and "/*" in args.wordlist):
            isWordlists = True
        else:
            isWordlists = False
        ruleList = []
    else:
        parser.print_help()
        exit()
    main()
