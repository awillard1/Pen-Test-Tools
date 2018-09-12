import argparse
import re

def decode_hex(s):
    idx1 = s.index("[")+1
    idx2 = len(s)-1
    hexdata = s[idx1:idx2]
    return bytes.fromhex(hexdata).decode()

def convert_data(data):
    decoded = decode_hex(data)
    while True:
        decoded = decode_hex(decoded)
        if 'HEX' not in decoded:
            break
    print(decoded)

def convert_file():
    print("Begin convert of HEX for JTR and wordlist for usage with rules")
    data_out = []
    try:
        for line in lines:
            if 'HEX' not in line:
                data_out.append(line)
                continue
            decoded = decode_hex(line)
            while True:
                decoded = decode_hex(decoded)
                if 'HEX' not in decoded:
                    break
            data_out.append(decoded)
    finally:
        wordlist=[]
        password_list=[]
        print("Generating unique list of a-zA-Z...")
        for item in data_out:
            stripped = re.sub(r'[^a-zA-Z]+', '', item)
            if lower_items:
                stripped = stripped.lower()
            if not stripped in wordlist:
                wordlist.append(stripped)
        print("Generating passwords to save...")
        for item in data_out:
            if not item in password_list:
                password_list.append(item)

        create_password_file(password_list)
        create_wordlist_file(wordlist)
        call_complete()
def call_complete():
    print("Conversion complete")

def create_password_file(password_list):
    print("Saving password list...")
    fh =  open(outfile, 'w')      
    for item in password_list:
        fh.write(item + '\n')
    fh.close()

def create_wordlist_file(wordlist):
    print("Saving worlist...")
    fh = open(outfile + "lw.lst",'w')
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
    parser.add_argument("-pl", "--password_list", help="specify file of hashcat hashes")
    parser.add_argument("-o", "--out", help="output file name")

    args = parser.parse_args()
    
    if args.password_list and args.out:
        outfile = args.out
        lower_items = True
        lines = open(args.password_list).read().splitlines()
    else:
        parser.print_help()
        exit()
    main()

    
