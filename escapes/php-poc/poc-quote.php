<?php
$fp = @fopen('test.txt','r');
$hack = $_GET['s'];
header('Content-Disposition: attachment; filename="test.html'.$hack.'.txt"');
header("Content-Length:".filesize($fp));
fpassthru($fp);
fclose($fp)
?>
