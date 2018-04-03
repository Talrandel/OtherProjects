<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ms="http://schemas.microsoft.com/developer/msbuild/2003"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                exclude-result-prefixes="msxsl ms">
  <xsl:output method="xml" omit-xml-declaration="no" indent="yes"/>

  <!--<xsl:template match="ms:Project">
    <Project ToolsVersion="14.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
      <xsl:copy>
        <xsl:apply-templates select="node()|@*"/>
      </xsl:copy>
    </Project>
  </xsl:template>


  <xsl:template match="ms:PropertyGroup">
    <ms:PropertyGroup>
      <ms:Configuration Condition=" '$(Configuration)' == '' ">DebugAll</ms:Configuration>
      <ms:Platform Condition=" '$(Platform)' == '' ">AnyCPU</ms:Platform>
      <xsl:if test="node()">
        <xsl:apply-templates select="node()"/>
      </xsl:if>
    </ms:PropertyGroup>
  </xsl:template>-->


  <xsl:template match="ms:EmbeddedResource">
    <xsl:if test="contains(@Include, '.de.resx') or contains(@Include, '.es.resx') or contains(@Include, '.ja.resx')">
    </xsl:if>
    <xsl:if test="not(contains(@Include, '.de.resx') or contains(@Include, '.es.resx') or contains(@Include, '.ja.resx'))">
      <xsl:copy>
        <xsl:attribute name="Include">
          <xsl:value-of select="@Include" />
        </xsl:attribute>
        <xsl:if test="node()">
          <xsl:apply-templates select="node()"/>
        </xsl:if>
      </xsl:copy>
    </xsl:if>
  </xsl:template>



  <xsl:template match="node()|@*">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>