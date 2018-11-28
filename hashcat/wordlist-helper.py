import argparse
import re
import sys

def decode_hex(s):
    idx1 = s.index("[")+1
    idx2 = len(s)-1
    hexdata = s[idx1:idx2]
    return bytes.fromhex(hexdata).decode()

def convert_data(data):
    while data.startswith('$HEX['):
        data = decode_hex(data)
    return data

def convert_file():
    print("[*] Begin convert of HEX for JTR and wordlist for usage with rules")
    data_out = []
    try:
        for line in lines:
            temp_str = line.strip()
            if not temp_str:
                continue
            elif not line.startswith('$HEX['):
                data_out.append(line)
            else:
                decoded = convert_data(line)
                data_out.append(decoded)
    finally:
        words=[]
        password_list=[]
        print("[*] Processing {} words.".format(len(data_out)))
        print("[*] Changing all words to lowercase...")
        words = [word.lower() for word in data_out]
        print("[*] Removing numbers and special characters...")
        words = [re.sub(r'[^a-z]+', '', word) for word in words]
        print("[*] Removing duplicate words...")
        words = list(set(words))
        print("[*] Generating passwords to save from hashcat...")
        password_list = list(set(data_out))
        create_password_file(password_list)
        create_wordlist_file(words)
        call_complete()
        
def call_complete():
    print("[*] Conversion complete")

#Creates file with all passwords including those with
#the characters escaped by hashcat
def create_password_file(password_list):
    print("[*] Saving password list...")
    fh =  open(outfile, 'w')      
    for item in password_list:
        try:
            fh.write(item + '\n')
        except:
            pass
    fh.close()

#Creates a stripped wordlist for use with rules
def create_wordlist_file(wordlist):
    print("[*] Saving worlist...")
    fh = open(outfile + "wl.lst",'w')
    for item in wordlist:
        fh.write(item+'\n')
    fh.close()
    
def main():
    convert_file()
    exit()

if __name__ == '__main__':
    print("__________________________________")
    print("_ _ _   ")
    print("\\\/\\/ordlist-helper 0.1")
    print("__________________________________\n\n")
    parser = argparse.ArgumentParser()
    parser.add_argument("-pl", "--password_list", help="specify the password plaintext file")
    parser.add_argument("-o", "--out", help="output file name")

    args = parser.parse_args()
    
    if args.password_list and args.out:
        outfile = args.out
        lower_items = True
        print("[*] Attempting to open hashcat export")
        lines = open(args.password_list, encoding="utf8", errors='ignore').read().splitlines()
    else:
        parser.print_help()
        exit()
    main()
