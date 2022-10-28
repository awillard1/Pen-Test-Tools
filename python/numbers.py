# based on https://www.codesansar.com/python-programming-examples/number-words-conversion-no-library-used.htm
import argparse

ones = ('Zero', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine')
twos = ('Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen')
tens = ('Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety', 'Hundred')
suffixes = ('', 'Thousand', 'Million', 'Billion')

def process(number, index, ln):
    if number=='0':
        return 'Zero'
    length = len(number)
    if(length > 3):
        return False
    number = number.zfill(3)
    words = ''
    hdigit = int(number[0])
    tdigit = int(number[1])
    odigit = int(number[2])
    words += '' if number[0] == '0' else ones[hdigit]
    words += 'Hundred' if not words == '' else ''
    if(tdigit > 1):
        words += tens[tdigit - 2]
        words += ones[odigit]
    elif(tdigit == 1):
        words += twos[(int(tdigit + odigit) % 10) - 1]
    elif(tdigit == 0):
        words += ones[odigit]
    if(words.endswith('Zero')):
        words = words[:-len('Zero')]
    if(not len(words) == 0):    
        words += suffixes[index]
    return words;
    
def getWords(number):
    length = len(str(number))
    if length>12:
        return 'This program supports upto 12 digit numbers.'
    count = length // 3 if length % 3 == 0 else length // 3 + 1
    copy = count
    words = []
    for i in range(length - 1, -1, -3):
        words.append(process(str(number)[0 if i - 2 < 0 else i - 2 : i + 1], copy - count, length))
        count -= 1;
    final_words = ''
    for s in reversed(words):
        final_words += s
    return final_words

def createOutFile():
    count=lower
    data=[]
    while count<=upper:
        data.append(getWords(count))
        count=count+1;
    fh =  open(outfile, 'w')      
    for item in data:
        try:
            fh.write(item + '\n')
        except:
            pass
    fh.close()
    print("file saved at: " + outfile)
    exit()

def main():
    if upper<lower:
        print("***Error: The max value is less than the start value.")
        exit()
    createOutFile()

if __name__ == '__main__':
    print("_________________________________________________")
    print("                          __                  ")
    print("   ____  __  ______ ___  / /_  ___  __________")
    print("  / __ \/ / / / __ `__ \/ __ \/ _ \/ ___/ ___/")
    print(" / / / / /_/ / / / / / / /_/ /  __/ /  (__  ) ")
    print("/_/ /_/\__,_/_/ /_/ /_/_.___/\___/_/  /____/  ")
    print("                                              ")
    print("\r\nnumbers to string rep 0.4")
    print("_________________________________________________\n\n")
    parser = argparse.ArgumentParser()
    parser.add_argument("-u", "--upper", help="number to end with")
    parser.add_argument("-l", "--lower", help="number to start with")
    parser.add_argument("-o", "--out", help="output rule file name - use .rule as the extension")
    args = parser.parse_args()
    if args.upper and args.lower:
        if args.upper.isnumeric() and args.lower.isnumeric() and args.out:
            upper = int(args.upper)
            lower = int(args.lower)
            outfile = args.out
        else:
            print("***Error: Ensure numbers are numeric and an outfile has been assigned.")
            exit()
    else:
        parser.print_help()
        exit()
    main()