<?php
$fp = @fopen('test.txt','r');
header('Content-Type: "text/plain"');
header('Content-Disposition: attachment; filename="test.html;.txt"');
header("Content-Length:".filesize($fp));
fpassthru($fp);
fclose($fp)
?>
