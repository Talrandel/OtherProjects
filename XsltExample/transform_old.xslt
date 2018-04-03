<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ms="http://schemas.microsoft.com/developer/msbuild/2003"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                exclude-result-prefixes="msxsl ms">
  <xsl:output method="xml" omit-xml-declaration="no" indent="yes"/>

  <xsl:param name="ExternalLibPathDebug">..\..\Build\External\Debug\</xsl:param>
  <xsl:param name="ExternalLibPathRelease">..\..\Build\External\Release\</xsl:param>
  <xsl:param name="ClientLibPath">..\..\Build\Client\Debug\</xsl:param>

  <xsl:template match="ms:ItemGroup">

      <xsl:if test="ms:ProjectReference">
        
        <xsl:for-each select="ms:ProjectReference">
          
          <xsl:if test="not(contains(ms:Name, 'Cognitive'))">
            <Choose xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
	            <When Condition="$(Configuration.EndsWith('All'))">
		          <ItemGroup>
		            <ProjectReference xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
                  <xsl:attribute name="Include">
                    <xsl:value-of select="ms:ProjectReference/@Include"/>
                  </xsl:attribute>
			            <xsl:apply-templates select="node()|@*"/>
		            </ProjectReference>
		          </ItemGroup>
	            </When>
	            <Otherwise xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
                <ItemGroup Condition="$(Configuration.Contains('Debug'))">
                  <Reference>
                    <xsl:attribute name="Include">
                      <xsl:value-of select="ms:Name" />
                    </xsl:attribute>
                    <HintPath><xsl:value-of select="$ExternalLibPathDebug"/><xsl:value-of select="ms:Name" />.dll</HintPath>                        
                  </Reference>
                </ItemGroup>
                <ItemGroup Condition="$(Configuration.Contains('Release'))">
                  <Reference>
                    <xsl:attribute name="Include">
                      <xsl:value-of select="ms:Name" />
                    </xsl:attribute>
                    <HintPath><xsl:value-of select="$ExternalLibPathRelease"/><xsl:value-of select="ms:Name" />.dll</HintPath>  
                  </Reference>
                </ItemGroup>
	            </Otherwise>
	          </Choose>
          </xsl:if>

          <xsl:if test="contains(ms:Name, 'Cognitive')">
            <ItemGroup xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
              <ProjectReference xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
                <xsl:attribute name="Include">
                  <xsl:value-of select="ms:ProjectReference/@Include"/>
                </xsl:attribute>
                <xsl:apply-templates select="node()|@*"/>
              </ProjectReference>
            </ItemGroup>
          </xsl:if>
          
        </xsl:for-each>
        
      </xsl:if>
    
      <xsl:if test="not(ms:ProjectReference)">
        <ItemGroup xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
          <xsl:apply-templates select="node()|@*"/>
        </ItemGroup>
      </xsl:if>
    
  </xsl:template>

  <xsl:template match="node()|@*">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
    </xsl:copy>
  </xsl:template>
  
</xsl:stylesheet>