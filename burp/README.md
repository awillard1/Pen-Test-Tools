# Process Files
Turn HTML pages for burp issues to XML (not all sections were transposed in this script due to not needing them all). 
* Ensure node is installed
* Ensure jsdom installed
* Unzip the burp jar, locate resources/KnowledgeBase/Issues
* Move those files somewhere and create an output directory similar to
```
burpissues/issues/
burpissues/outputissues/
```
make sure the values are correct in the processfiles.js script
```
npm install jsdom
node procfiles/processfiles.js
```
