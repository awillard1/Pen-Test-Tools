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
          body { font-family:calibri,sans-serif; font-size: .8em;}
          table {border-collapse:collapse; width:100%; table-layout: fixed;}
          tr, td, th{vertical-align:top; border: 1px solid black; padding:2px}
          .alert {color:red;}
          .info {color:blue;}
          td {font-size: .8em;}
          th {text-align:left;font-size: .8em;background-color:#C0C0C0}
          .tight { padding-top:0}
          .smallColumn {width: 100px;}
          pre {font-family:calibri,sans-serif; }
          .wrapLarge {word-wrap: break-word; width: 700px;}
          .wrapMedium {word-wrap: break-word; width: 250px;}
          .thvulns {background-color: rgb(141,180,226);font-weight:bold;}
          .thdoco {background-color: rgb(141,180,226);font-weight:bold;}
          .threcon {background-color: rgb(204,192,218);font-weight:bold;}
          .thsugg {background-color: rgb(230,184,183);font-weight:bold;}
        </style>
      </head>
      <body>
        <img src="data: image/png; base64,iVBORw0KGgoAAAANSUhEUgAAAK0AAAAwCAIAAADsNW2JAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6NTc1NUUwNDZCRDYwMTFFNjkzMkY5MTNDNjBGQTBBOEIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6NTc1NUUwNDdCRDYwMTFFNjkzMkY5MTNDNjBGQTBBOEIiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo1NzU1RTA0NEJENjAxMUU2OTMyRjkxM0M2MEZBMEE4QiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDo1NzU1RTA0NUJENjAxMUU2OTMyRjkxM0M2MEZBMEE4QiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Phw0kOEAAARASURBVHja7Jo/LCxRFMZJXqKgpVKQ6EgkOhqFUqFRCfVGSyMRHVFRStRbUlDo1GwlkaxOQqWQKIlG9v3ybt59N+feGTMea81+X7GZnPtnZu/5znfO3Du9rVarR+h69IoHgnggiAeCeCCIB4J4IIgHgnggiAeCeCCIB0IVefDy8nJ3dxdahoaGBgcH5cLPQastaDab8a0x5o96fHw8ODio1Wo5z7+4uLi7u+umiu/CDAUfZmxs7Pn5Oe58fn5epNtPRzt4wMKxfPHSz87O5qwpDChFaOjCbMwZGuv1ejxzFrGKdIZzrSqiHTzICeiNjY3kEFxiGENckhdCbjUaDTMz2hCHbxExyIp15KSIwIgH78N4FMebkMVz71Iniy6AploA3Gy0x8wfz5wjCUR/N4jBl/PABJ8LuNgYBroDsR4HKy7ET/iVGXLi0jCPUVnP41JJyJtQP+J09m5BIx6kywIT+ng3mfvR83g4nc3wZJFIQOP4kEmxmPtWIwbOr4Y3Xj9Mign5JB6UgJFcI6q4MGyFGclJYANNpnMSrobI0XPz2un9auKeeZJP6EksHpTAycmJUXXWsRnAdCj4GukGEsGQLEmOpMtdPjLkCOuGWBJMBvHkEA9KwLihIHy5znDcAG/qfxBXD2Hqifnk+psUYDKR8WssCTmkEQ8+WBaU2gOIo5O4z9lmoCmpKya7G8R+NTfthr2j/+UBAbe9vT09Pd3X18cv12HImrIgWQOGUm9qcuKbRTeaD7Gwm8ThcoQZHt4ui45Jkc/a7MraX+p2HuDypaWlzc3Ni4uL19dXfrnG4qgQR2GOqifLcjckTudFAAXDwI1TRr7IZ0lC5cXgIzwg+nG8MWJxqmBCqmAkxRLilh61YIb88wWXTUj/8Y5CMsRzKr5k/wrvHYUofd44MzOzt7dHOgiNl5eX6+vraMOXHond3NwYy/j4uE4Kv+e8kZqAdGCMWLBXfq3e3t68cqBAvv5w7xdew7j2TWgbkuYzCynJyxvaQ88OObAozQOUgLg3RizYq59E/4aNqzx83coFqc2VNca7GHG8q16hDq1Qwb8eu/3QTvhrv8rGxPz8/NnZmckLWLB3j4hOTk4S8RMTE/h4ZGRkeHh4eXmZi9PT09vb2/v7+62tLd/56enp+PgY4/7+/tra2tzcnLPT/+jo6Kd+j8T/oSocHR3F8VNTU1dXV5AAau/s7PDHqv/9Vu+/FaNewesPDw/X19eHh4cE98LCwsrKSqPRGBgYMANZsf7+fiRhdXU1/MgKbeiIr6o+ff+gG1KD27fwJw74EnnH4rZDIIRfEJ8XXEnh8oLfO6GpQzapelpCeSqYOtHVBM73pk50JaQvFyCB3yILK4m2fSD4ae+NQpgaOn/aqn2v3GlU+Lo0LR5IZqQHIsG3OkI8EMQDQTwQxANBPBDEA0E8EMQDQTwQxANBPBDK4LcAAwD/TFu7rpW3XwAAAABJRU5ErkJggg=="></img>
        <h1>Weekly Report</h1>
       

          <!-- Generate the Findings from the xml file -->
          <xsl:variable name="totalVulns" select ="count(report/vulnerabilities)"></xsl:variable>
          <xsl:if test="$totalVulns &gt; 0">
            <table>
              <tr>
                <th width="10%" class="thvulns">Environment Testing</th>
                <th width="80%" class="thvulns">Vulnerabilities</th>
                <th width="10%" class="thvulns">Date</th>
              </tr>
              <xsl:for-each select="report/vulnerabilities">
                <tr>
                  <td>
                    <xsl:value-of select="project/." disable-output-escaping="yes"/>
                  </td>
                  <td>
                    <xsl:value-of select="title/." disable-output-escaping="yes"/>
                  </td>
                  <td style="word-wrap: break-word;">
                    <xsl:value-of select="date/." disable-output-escaping="yes"/>
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </xsl:if>
          <xsl:if test="$totalVulns &lt; 1">
            <p>No items recorded</p>
          </xsl:if>
          <!-- End Findings -->

          <xsl:variable name="totalDoco" select ="count(report/documentation)"></xsl:variable>
          <xsl:if test="$totalDoco &gt; 0">
            <table style="width:100%">
              <tr>
                <th width="10%" class="thvulns">Project</th>
                <th width="80%" class="thvulns">Documentation</th>
                <th width="10%" class="thvulns">Date</th>
              </tr>
              <xsl:for-each select="report/documentation">
                <tr>
                  <td>
                    <xsl:value-of select="project/." disable-output-escaping="yes"/>
                  </td>
                  <td style="word-wrap: break-word;">
                    <pre>
                    <xsl:value-of select="details/." disable-output-escaping="yes"/></pre>
                  </td>
                  <td>
                <xsl:value-of select="date/." disable-output-escaping="yes"/>
              </td>
                </tr>
              </xsl:for-each>
            </table>
          </xsl:if>
          <xsl:if test="$totalDoco &lt; 1">
            <p>No items recorded</p>
          </xsl:if>
        <table style="width:100%">
          <tr>
            <th class="threcon">Recon</th>
            <th class="threcon"> </th>
          </tr>
          <tr>
          <td> </td>
        <td style="word-wrap: break-word;">
                    <pre>
                    <xsl:value-of select="report/summary/." disable-output-escaping="yes"/></pre>
                  </td>
        </tr>
        </table>
        <table style="width:100%">
          <tr>
            <th class="thsugg">Areas for Improvement</th>
            <th class="thsugg">Description</th>
            <th class="thsugg">Suggestions for Remediation</th>
          </tr>
</table>
    </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

