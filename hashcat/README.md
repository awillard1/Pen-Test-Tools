Save your debug rules when cracking, especially with using multiple rules passed to hashcat:
```
hashcat.exe -m 1000 -o crackpwd.txt -r rules/hob0.rule -r rules/ippr.rule ../hashes/nt.txt ../wordlists/* -O --debug-mode=1 --debug-file=rules/cracked.rule
```
Use random rules generated:
```
hashcat.exe -m 1000 -o crackpwd.txt  -g 5000 --generate-rules-func-min=1 --generate-rules-func-max=8 ../hashes/nt.txt ../wordlists/* -O --debug-mode=1 --debug-file=rules/cracked.rule --generate-rules-seed 32
```
