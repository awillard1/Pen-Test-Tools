import argparse
import os

def verify_imports(file_name):
    const_imp = 'import '
    const_from = 'from '
    imp_length = len(const_imp)
    modules = []
    failed_imports = []
    with open(file_name) as requirements:
            for line in enumerate(requirements):
                if line[1].startswith(const_imp):
                    data = line[1].replace(const_imp,'')
                    modules.append(data.rstrip())
                elif line[1].startswith(const_from):
                    data = line[1]
                    i = int(data.find(const_imp))+ int(imp_length)
                    modules.append(data[i::].rstrip())

    for module in modules:
        try:
            __import__(module)
        except ImportError as error:
            missing_module = str(error).split(' ')[-1]
            failed_imports.append(missing_module)
    if failed_imports != []:
        print('\nThis script requires several modules that you do not have.')
        for missing_module in failed_imports:
            print('Missing module: {}'.format(missing_module))
            print('Try running "pip install {}", or do an Internet search for installation instructions.'.format(missing_module.strip("'")))

def main(file_path):
    if (os.path.exists(file_path)):
        verify_imports(file_path)
    else:
        print("\n [-] File does not exist\n")
        exit()

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-fp", "--filepath", help="specify a the path to the file (-fp /your/location")
    args = parser.parse_args()   

    if not args.filepath:
        parser.print_help()
        print("\n [-]  Please specify the path to the file. (-fp /your/location\n")
        exit()
    main(args.filepath)
