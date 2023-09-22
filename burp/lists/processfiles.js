const fs = require('fs');
const path = require('path');
const { JSDOM } = require('jsdom');

// Input and output directories
// ./resources/KnowledgeBase/Issues
// THIS NEEDS TO BE SET
const inputDirectory = 'burpissues/Issues/';
const outputDirectory = 'burpissues/outIssues/';

// Define your custom processing function
function processFile(inputFilePath, outputFilePath) {
  fs.readFile(inputFilePath, 'utf8', (err, data) => {
    if (err) {
      console.error(`Error reading input file ${inputFilePath}:`, err);
      return;
    }

    // Parse the HTML content of the input file using jsdom
    const dom = new JSDOM(data);

    // Your existing JavaScript code for extracting data
    function getText(node) {
      return node.nextSibling.nodeValue.replace(/\r|\n/g, "").trim();
    }

    function getData(node) {
      return `<${node.nodeName}>${node.innerHTML}</${node.nodeName}>`;
    }

    const h1Elements = dom.window.document.getElementsByTagName('H1');
    const nm = `<name>${getText(h1Elements[0])}</name>`;
    const sev = `<severity>${getText(h1Elements[1])}</severity>`;
    const descElements = [];
    let i = h1Elements[2].nextElementSibling;
    while (i && i.nodeName !== 'H1') {
      descElements.push(getData(i));
      i = i.nextElementSibling;
    }
    const desc = `<description><![CDATA[${descElements.join('')}]]></description>`;
    const reEleElements = [];
    i = h1Elements[3].nextElementSibling;
    while (i && i.nodeName !== 'H1') {
      reEleElements.push(getData(i));
      i = i.nextElementSibling;
    }
    const reEle = `<remediation><![CDATA[${reEleElements.join('')}]]></remediation>`;
    const root = `<issue>${nm}${sev}${desc}${reEle}</issue>`;

    // Write the processed content to the output file as XML
    fs.writeFile(outputFilePath, root, 'utf8', (err) => {
      if (err) {
        console.error(`Error writing output file ${outputFilePath}:`, err);
        return;
      }
      console.log(`Processed and saved: ${outputFilePath}`);
    });
  });
}

// Read the list of files in the input directory
fs.readdir(inputDirectory, (err, files) => {
  if (err) {
    console.error('Error reading directory:', err);
    return;
  }

  // Process each file
  files.forEach((file) => {
    const inputFilePath = path.join(inputDirectory, file);
    const outputFilePath = path.join(outputDirectory, file.replace(/\.[^.]+$/, '.xml')); // Change the file extension to .xml
    processFile(inputFilePath, outputFilePath);
  });
});
