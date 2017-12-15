<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"  xmlns="http://www.w3.org/1999/xhtml"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8" indent="yes" />
  <xsl:template match="/">
    <html>
      <head>
        <title>Application Security Information</title>
        <style>
          .coverPage {margin:auto; position:relative;width:350px; display:block; padding-left: 4px; padding-top:4px;padding-bottom:4px; float:center; border-left:1px solid black; }
          h1 { font-size: 1.1 em; padding-bottom:0; color:#606060; border-bottom:solid 1px black}
          .project {font-style:italic;font-size: medium!important;color:#696969;border-bottom:solid 1px #606060}
          h3 {padding: 2px; width: 100%; color:black; font-variant: small-caps; font-size:.96em!important }
          body { font-family:Georgia, calibri,sans-serif; font-size: .8em;}
          table {border-collapse:collapse; width:100%; table-layout: fixed;}
          tr, td, th{vertical-align:top; border: 1px solid black; padding:2px}
          .alert {color:red;}
          .info {color:blue;}
          td {font-size: .8em;}
          th {text-align:left;font-size: .8em;background-color:#C0C0C0}
          .tight { padding-top:0}
          .smallColumn {width: 100px;}
          pre {font-family:courier, calibri,sans-serif;  }
          .wrapLarge {word-wrap: break-word; width: 700px;}
          .wrapMedium {word-wrap: break-word; width: 250px;}
        </style>
      </head>
      <body>
        <img src="data: image/png; base64,iVBORw0KGgoAAAANSUhEUgAAAK0AAAAwCAIAAADsNW2JAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NTc1NUUwNDZCRDYwMTFFNjkzMkY5MTNDNjBGQTBBOEIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6NTc1NUUwNDdCRDYwMTFFNjkzMkY5MTNDNjBGQTBBOEIiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo1NzU1RTA0NEJENjAxMUU2OTMyRjkxM0M2MEZBMEE4QiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDo1NzU1RTA0NUJENjAxMUU2OTMyRjkxM0M2MEZBMEE4QiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Phw0kOEAAARASURBVHja7Jo/LCxRFMZJXqKgpVKQ6EgkOhqFUqFRCfVGSyMRHVFRStRbUlDo1GwlkaxOQqWQKIlG9v3ybt59N+feGTMea81+X7GZnPtnZu/5znfO3Du9rVarR+h69IoHgnggiAeCeCCIB4J4IIgHgnggiAeCeCCIB0IVefDy8nJ3dxdahoaGBgcH5cLPQastaDab8a0x5o96fHw8ODio1Wo5z7+4uLi7u+umiu/CDAUfZmxs7Pn5Oe58fn5epNtPRzt4wMKxfPHSz87O5qwpDChFaOjCbMwZGuv1ejxzFrGKdIZzrSqiHTzICeiNjY3kEFxiGENckhdCbjUaDTMz2hCHbxExyIp15KSIwIgH78N4FMebkMVz71Iniy6AploA3Gy0x8wfz5wjCUR/N4jBl/PABJ8LuNgYBroDsR4HKy7ET/iVGXLi0jCPUVnP41JJyJtQP+J09m5BIx6kywIT+ng3mfvR83g4nc3wZJFIQOP4kEmxmPtWIwbOr4Y3Xj9Mign5JB6UgJFcI6q4MGyFGclJYANNpnMSrobI0XPz2un9auKeeZJP6EksHpTAycmJUXXWsRnAdCj4GukGEsGQLEmOpMtdPjLkCOuGWBJMBvHkEA9KwLihIHy5znDcAG/qfxBXD2Hqifnk+psUYDKR8WssCTmkEQ8+WBaU2gOIo5O4z9lmoCmpKya7G8R+NTfthr2j/+UBAbe9vT09Pd3X18cv12HImrIgWQOGUm9qcuKbRTeaD7Gwm8ThcoQZHt4ui45Jkc/a7MraX+p2HuDypaWlzc3Ni4uL19dXfrnG4qgQR2GOqifLcjckTudFAAXDwI1TRr7IZ0lC5cXgIzwg+nG8MWJxqmBCqmAkxRLilh61YIb88wWXTUj/8Y5CMsRzKr5k/wrvHYUofd44MzOzt7dHOgiNl5eX6+vraMOXHond3NwYy/j4uE4Kv+e8kZqAdGCMWLBXfq3e3t68cqBAvv5w7xdew7j2TWgbkuYzCynJyxvaQ88OObAozQOUgLg3RizYq59E/4aNqzx83coFqc2VNca7GHG8q16hDq1Qwb8eu/3QTvhrv8rGxPz8/NnZmckLWLB3j4hOTk4S8RMTE/h4ZGRkeHh4eXmZi9PT09vb2/v7+62tLd/56enp+PgY4/7+/tra2tzcnLPT/+jo6Kd+j8T/oSocHR3F8VNTU1dXV5AAau/s7PDHqv/9Vu+/FaNewesPDw/X19eHh4cE98LCwsrKSqPRGBgYMANZsf7+fiRhdXU1/MgKbeiIr6o+ff+gG1KD27fwJw74EnnH4rZDIIRfEJ8XXEnh8oLfO6GpQzapelpCeSqYOtHVBM73pk50JaQvFyCB3yILK4m2fSD4ae+NQpgaOn/aqn2v3GlU+Lo0LR5IZqQHIsG3OkI8EMQDQTwQxANBPBDEA0E8EMQDQTwQxANBPBDK4LcAAwD/TFu7rpW3XwAAAABJRU5ErkJggg=="></img>
        <h1>Projects</h1>
        <ul>
          <xsl:for-each select="projects/project">
            <li>
              <a href="#{name}">
                <xsl:value-of select="name/." disable-output-escaping="yes"/>
              </a>
            </li>
          </xsl:for-each>
        </ul>
        <xsl:for-each select="projects/project">
          <h1 style="page-break-before:always">Application Security Information</h1>
          <h1 class="project">
            Project Name: <a name="{name}">
              <xsl:value-of select="name/." disable-output-escaping="yes"/>
            </a>
          </h1>
          <h2>Contact Details</h2>
          <p>
            <strong>ISSO/IAM: </strong>
            <xsl:value-of select="contacts/isso/." disable-output-escaping="yes"/>
          <br/>
            <strong>Development Lead(s): </strong>
            <xsl:value-of select="contacts/devLead/." disable-output-escaping="yes"/>
            <br/>
            <strong>Production URL: </strong>
            <xsl:value-of select="productionURL/." disable-output-escaping="yes"/>
          </p>
          <h2>Code Details</h2>
          <p>
            <strong>Is Code Currently Scanned: </strong>
            <xsl:value-of select="codeDetail/@isCurrentlyScanned" disable-output-escaping="yes"/>
          <br/>
            <strong>Static Analysis Scan Type: </strong>
            <xsl:value-of select="codeDetail/@scanConfiguration" disable-output-escaping="yes"/>
          <br/>
            <strong>Source Code Location/Repository: </strong>
            <xsl:value-of select="codeDetail/repository/." disable-output-escaping="yes"/>
          </p>

          <h2>Programming Languages</h2>
          <xsl:variable name="totalLang" select ="count(codeDetail/languages/language)"></xsl:variable>
          <xsl:if test="$totalLang &gt; 0">
            <ul>
              <xsl:for-each select="codeDetail/languages/language">
                <li>
                  <xsl:value-of select="." disable-output-escaping="yes"/>
                </li>
              </xsl:for-each>
            </ul>
          </xsl:if>
          <xsl:if test="$totalLang &lt; 1">
            <p>No known programming languages</p>          
          </xsl:if>
          <h2>Release Schedule/Expected Code Drops for Scans</h2>
          <xsl:variable name="totalDrops" select ="count(codeAnalysis/scanDate)"></xsl:variable>
          <xsl:if test="$totalDrops &gt; 0">
            <ul>
              <xsl:for-each select="codeAnalysis/scanDate">
                <li>
                  <xsl:value-of select="." disable-output-escaping="yes"/>
                </li>
              </xsl:for-each>
            </ul>
          </xsl:if>
          <xsl:if test="$totalDrops &lt; 1">
            <p>No known release dates</p>          
          </xsl:if>
          <h2>Expected Penetration Testing Schedule</h2>
          <xsl:variable name="totalTesting" select ="count(penetrationTests/scan)"></xsl:variable>
          <xsl:if test="$totalTesting &gt; 0">
            <table style="width:800px">
              <tr>
                <th>Start Date</th>
                <th>End Date</th>
                <th>Tester</th>
              </tr>
              <xsl:for-each select="penetrationTests/scan">
                <tr>
                  <td>
                    <xsl:value-of select="startDate/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="endDate/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="tester/." disable-output-escaping="yes"/>
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </xsl:if>
          <xsl:if test="$totalTesting &lt; 1">
            <p>No items recorded</p>
          </xsl:if>

          <!-- Generate the Findings from the xml file -->
          <h2>Identified Vulnerabilities</h2>
          <xsl:variable name="totalVulns" select ="count(vulnerabilities/item)"></xsl:variable>
          <xsl:if test="$totalVulns &gt; 0">
            <table>
              <tr>
                <th width="10%">Type</th>
                <th width="30%">Title</th>
           <!--     <th width="30%">Vulnerability</th>-->
                <th>Risk</th>
                <th>Tester</th>
                <th width="8%">Discovered</th>
                <th>Status</th>
                <th width="8%">Fixed</th>
                <th width="8%">CVSS2</th>
              </tr>
              <xsl:for-each select="vulnerabilities/item">
                <tr>
                  <td>
                    <xsl:value-of select="type/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="title/." disable-output-escaping="yes"/>
                  </td>
             <!--     <td style="word-wrap: break-word;">
                    <pre>
                    <xsl:value-of select="vulnerability/." disable-output-escaping="yes"/></pre>
                  </td>-->
                  <td>
                    <xsl:value-of select="risk/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="tester/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="discoveredDate/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="status/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="completedDate/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="cvss2/." disable-output-escaping="yes"/>
                  </td>
                </tr>
                <tr width="100%">
                  <td  width="100%" style="word-wrap: break-word;" colspan="8">
                    <pre>
                      <xsl:value-of select="vulnerability/." disable-output-escaping="yes"/>
                    </pre>
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </xsl:if>
          <xsl:if test="$totalVulns &lt; 1">
            <p>No items recorded</p>
          </xsl:if>
          <!-- End Findings -->


          <h2>Documentation</h2>
          <xsl:variable name="totalDoco" select ="count(documentation/item)"></xsl:variable>
          <xsl:if test="$totalDoco &gt; 0">
            <table style="width:100%">
              <tr>
                <th width="9%">Date</th>
                <th>Category</th>
                <th width="60%">Details</th>
              </tr>
              <xsl:for-each select="documentation/item">
                <tr>
                  <td>
                    <xsl:value-of select="dateOfIssue/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:variable name="cat" select="category/."></xsl:variable>
                    <xsl:choose>
                      <xsl:when test="$cat = 'Risk'">
                        <span class="alert">
                          <xsl:value-of select="category/." disable-output-escaping="yes"/>
                        </span>
                      </xsl:when>
                      <xsl:when test="$cat = 'Information'">
                        <span class="info">
                          <xsl:value-of select="category/." disable-output-escaping="yes"/>
                        </span>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="category/." disable-output-escaping="yes"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </td>
                  <td style="word-wrap: break-word;">
                    <pre>
                    <xsl:value-of select="details/." disable-output-escaping="yes"/>
                      </pre>
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </xsl:if>
          <xsl:if test="$totalDoco &lt; 1">
            <p>No items recorded</p>
          </xsl:if>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

