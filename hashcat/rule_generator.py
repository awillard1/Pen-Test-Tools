import argparse
import os

leet_dict = {
    'a': ['a','A','/-\\', '/\\', '4', '@'],
    'b': ['b','B','|3', '8', '|o'],
    'c': ['c','C','(', '<', 'K', 'S'],
    'd': ['d','D','|)', 'o|', '|>', '<|'],
    'e': ['e','E','3'],
    'f': ['f','F','|=', 'ph'],
    'g': ['g','G','(', '9', '6'],
    'h': ['h','H','|-|', ']-[','}-{', '(-)', ')-(', '#'],
    'i': ['i','I','l', '1', '|', '!', ']['],
    'j': ['j','J','_|'],
    'k': ['k','K','|<', '/<', '\\<', '|{'],
    'l': ['l','L','|_', '|', '1'],
    'm': ['m','M','|\\/|', '/\\/\\', '|\'|\'|', '(\\/)', '/|\\', '/v\\'],
    'n': ['n','N','|\\|', '/\\/', '|\\|', '/|/'],
    'o': ['o','O','0', '()', '[]', '{}'],
    'p': ['p','P','|2', '|D'],
    'q': ['q','Q','(,)', 'kw'],
    'r': ['r','R','|2', '|Z', '|?'],
    's': ['s','S','5', '$'],
    't': ['t','T','+', '\'][\'', '7'],
    'u': ['u','U','|_|'],
    'v': ['v','V','|/', '\\|', '\\/', '/'],
    'w': ['w','W','\\/\\/', '\\|\\|', '|/|/', '\\|/', '\\^/', '//'],
    'x': ['x','X','><', '}{'],
    'y': ['y','Y','`/', '\'/', 'j'],
    'z': ['z','Z','2', '(\\)']
}


def permute(dict_word):
    """Looks up each letter of a word to its leet equivalent and
    returns a list of words permutated with the leet values.
    Adapted from: https://github.com/madglory/permute_wordlist/blob/master/permuteWordlist.py
    """
    if len(dict_word) > 0:
        current_letter = dict_word[0]
        rest_of_word = dict_word[1:]
        if current_letter in leet_dict:
            substitutions = leet_dict[current_letter] + [current_letter]
        else:
            substitutions = [current_letter]
        if len(rest_of_word) > 0:
            perms = [s + p for s in substitutions for p in permute(rest_of_word)]
        else:
            perms = substitutions
        return perms

def create_append(mutated_word):
    items = list(mutated_word)
    data = "$"
    for item in items:
        if data != "$":
            data = data + "$" + item
        else:
            data = data + item
    return data

def create_pre_word(mutated_word):
    items = list(mutated_word)[::-1]
    data = "^"
    for item in items:
        if data != "^":
            data = data + "^" + item
        else:
            data = data + item
    return data

def main():
    """Passes a wordlist through a series of functions to generate
    a rule file.
    """
    global words
    words = [word.lower() for word in words]
    fh = open(outfile,'w')	
    for word in words:
        permuted = permute(word)
        unique_list=set(permuted)
        for i in unique_list:
            fh.write(create_append(i) + "\n")
            fh.write(create_pre_word(i) + "\n")
    fh.close()

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-f", "--file",
                        help="Specify a file containing words.")
    parser.add_argument("-w", "--word",
                        help="Specify a single word.")
    parser.add_argument("-a", '--append_words',
                        action='store_true',
                        help="Generate permuted words for an append rule list")
    parser.add_argument("-o", "--out", help="Output file name")

    args = parser.parse_args()

    if not args.word and not args.file:
        parser.print_help()
        print("\n[-] Please specify a word (-w) or an input file containing words (-f).\n")
        exit()

    if args.word and args.file:
        parser.print_help()
        print("\n[-] Please specify a word (-w) or an input file containing words (-f). Not both\n")
        exit()
        
    if not args.out:
        parser.print_help()
        print("\n[-] Please specify a word (-o) for the filename to save the rule to.\n")
        exit()
    else:
        outfile = args.out
        
    if args.file:
        file = args.file
        if not os.path.exists(file):
            print("\n[-] The file cannot be found or you do not have permission to open the file. Please check the path and try again\n")
            exit()
        with open(file) as fh:
        	words = fh.read().splitlines()

    if args.word:
        words = [args.word]
    main()
