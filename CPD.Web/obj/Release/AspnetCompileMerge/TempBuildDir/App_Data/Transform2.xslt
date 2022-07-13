<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <xsl:for-each select="/ModuleDoc/Module">
      <xsl:element name="Module">
        <xsl:attribute name="Naam">
          <xsl:value-of select="Naam"/>
        </xsl:attribute>

      <xsl:for-each select="//ModuleDoc/Module/Article">
        <xsl:element name="Article">
          <xsl:attribute name="Naam">
            <xsl:value-of select="Naam"/>
          </xsl:attribute>
          <xsl:for-each select="Question">
            <xsl:element name="Question">
              <xsl:attribute name="Question">
                <xsl:value-of select="Question"/>
              </xsl:attribute>
              <xsl:attribute name ="QuestionId">
                <xsl:value-of select="QuestionId"/>
              </xsl:attribute>
            </xsl:element>
          </xsl:for-each>
        </xsl:element>
      </xsl:for-each>
     </xsl:element>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
