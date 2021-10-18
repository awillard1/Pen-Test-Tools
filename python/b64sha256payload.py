#usage:
#python3.9 /mnt/d/data/scripts/python/b64sha256payload.py 'eyJ1c2VySWQiOjkwNDR9.af180ec9c42ac5092f65c12df5730d9a626525b81015be19e3b68b14851181af'
#test POSTED base64 data concat with . of the sha256 value of the base64 string
import hashlib
import sys
import base64
a = sys.argv[1:]
b = a[0].split('.')
c = base64.b64decode(b[0].encode())
print("Below are the values being passed.")
print("Copy and change the value and paste here.")
print(c.decode())
d = input("What is your new input?")
e=base64.b64encode(d.encode())
print(e.decode()+'.'+hashlib.sha256(e).hexdigest())