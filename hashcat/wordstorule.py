import argparse

def convert_data(rtype,word):
    arr = list(word)
    tmpstr=""
    for l in arr:
        tmpstr=tmpstr + rtype + l
    return tmpstr

def convert_file(rtype,isRevstr):
    print("[*] Begin convert to rules")
    try:
        for line in lines:
            temp_str = line.strip()
            if not temp_str:
                continue
            else:
                if isRevstr:
                    data = convert_data(rtype,line[::-1])
                else:
                    data = convert_data(rtype,line)
                data_out.append(data)
    except:
        print("Error converting")
       
def call_complete():
    print("[*] Conversion complete")

def create_rule_file(rule_list):
    print("[*] Saving rules")
    fh =  open(outfile, 'w')      
    for item in rule_list:
        try:
            fh.write(item + '\n')
        except:
            pass
    fh.close()

def save_file():
    word_list=[]
    word_list = list(set(data_out))
    create_rule_file(word_list)
    call_complete()

def main():
    processed=False
    if isAppend:
        convert_file("$",False)
        processed=True
    if isPrepend:
        convert_file("^",True)
        processed=True
    if processed==False:
        convert_file("$",False);
    save_file()
    exit()

if __name__ == '__main__':
    print("__________________________________")
    print("                          __        __           __               __               __                __         ")
    print(" _      ______  _________/ /____   / /_____     / /_  ____ ______/ /_  _________ _/ /_   _______  __/ /__  _____")
    print("| | /| / / __ \/ ___/ __  / ___/  / __/ __ \   / __ \/ __ `/ ___/ __ \/ ___/ __ `/ __/  / ___/ / / / / _ \/ ___/")
    print("| |/ |/ / /_/ / /  / /_/ (__  )  / /_/ /_/ /  / / / / /_/ (__  ) / / / /__/ /_/ / /_   / /  / /_/ / /  __(__  ) ")
    print("|__/|__/\____/_/   \__,_/____/   \__/\____/  /_/ /_/\__,_/____/_/ /_/\___/\__,_/\__/  /_/   \__,_/_/\___/____/  ")
    print("    ")
    print("\r\nwthr 0.03")
    print("__________________________________\n\n")
    parser = argparse.ArgumentParser()
    parser.add_argument("-w", "--words", help="specify the plaintext file containing words")
    parser.add_argument("-o", "--out", help="output rule file name - use .rule as the extension")
    parser.add_argument("-a", "--append", help="append rule type",action='store_const', const=True)
    parser.add_argument("-p", "--prepend", help="prepend rule type",action='store_const', const=True)

    args = parser.parse_args()
    data_out = []
    if args.words and args.out:
        outfile = args.out
        isPrepend = True if args.prepend is not None else False
        isAppend = True if args.append is not None else False
        lower_items = True
        lines = open(args.words, encoding="utf8", errors='ignore').read().splitlines()
    else:
        parser.print_help()
        exit()
    main()