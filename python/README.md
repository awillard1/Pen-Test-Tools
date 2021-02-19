# random_char_url.py
## USAGE: 

* u - url with the value that needs to be replaced
* vl - Length of random string of the following characters A-Za-z0-9
* cnt - how many requests to send
* r replacement value in the url
* pr - optional proxy

```
python3 /mnt/d/data/scripts/python/random_char_url.py -u https://localhost/sadf -r sadf -cnt 3 -vl 7

python3 /mnt/d/data/scripts/python/random_char_url.py -u https://localhost/sadf -r sadf -cnt 3 -vl 7 -pr 127.0.0.1:8080
```
