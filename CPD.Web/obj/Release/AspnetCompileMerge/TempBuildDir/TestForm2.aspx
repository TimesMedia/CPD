<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm2.aspx.cs" Inherits="CPD.Web.TestForm2"
    MasterPageFile="~/Site.Master" Title="Test page" %>

<%--<%@ Register Src="~/UserControls/Advert.ascx" TagName="addControl" TagPrefix="uc1" %>--%>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
   <%-- <asp:Image ID="HorisontalImage" runat="server" height="90px" width="940px"/>--%>
    <asp:table style="width: 1035px;" runat="server">
        <asp:TableRow>
            <asp:TableCell style="width: 675px" >
  
                <p style="width: 650px;">
                    <span><b>Please click on the question you want to answer or use the 'Next Question' button
                        to answer the questions sequentially. Mark ALL the correct answers - sometimes more than one answer is correct.</b>
                    </span>
                </p>
              
                <asp:Label ID="LabelResponse" runat="server" Text="" ForeColor="Red"></asp:Label>
                     <br />

                <asp:TreeView ID="TreeView1" runat="server" Width="650"
                    SelectedNodeStyle-ForeColor="Red" ExpandDepth="1" NodeStyle-Font-Names="Arial"
                    NodeWrap="True" NodeIndent="10">
                    <DataBindings>
                        <asp:TreeNodeBinding DataMember="Module" TextField="Naam" FormatString="MODULE: {0}"
                            SelectAction="None" ToolTip="Module" />
                        <asp:TreeNodeBinding DataMember="Article" TextField="Naam" SelectAction="None" ToolTip="Article"
                            FormatString="ARTICLE: {0}" />
                        <asp:TreeNodeBinding DataMember="Question" TextField="Question" ValueField="QuestionId"
                            ToolTip="Question" FormatString="QUESTION {0}"/>
                    </DataBindings>
                    <NodeStyle Font-Names="Arial" VerticalPadding="5px"></NodeStyle>
                    <SelectedNodeStyle ForeColor="Red"></SelectedNodeStyle>
                </asp:TreeView>
 
                <asp:Table ID="Main" Width="650px" runat="server">
                    <asp:TableRow>
                        <asp:TableCell Width="650px" Style="vertical-align: top;">
                            <asp:Table ID="TableQuestion" CssClass="question-table" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell><b>Article: </b></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" ID="SingleArticle" ></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><b>Question:</b></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" ID="SingleQuestion" ></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top"><b>Answer:</b></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBoxList ID="CheckBoxListAnswer" runat="server" 
                                            DataTextField="Answer" DataValueField="Correct" AutoPostBack="false">
                                        </asp:CheckBoxList>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Button ID="ButtonNext" CssClass="button-generic" runat="server" Text="Next Question" OnClick="ButtonNext_Click" />
                            <br />
                            <br />
                            <asp:Button ID="ButtonSave" CssClass="button-generic" title="Save my last answer" runat="server" Text="Save" OnClick="ButtonSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ButtonHome" CssClass="button-generic" runat="server" OnClick="ButtonHome_Click" Text="Home" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ButtonQuit" CssClass="button-generic" title="Quit as though I never took the test" runat="server" Text="Cancel test" OnClick="ButtonQuit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ButtonSubmit" CssClass="button-generic" title="Submit the test for marking" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" ToolTip="You have to submit the current module to be marked, before continuing to the next module" />
                            </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:XmlDataSource ID="XmlDataSourceQuestion" runat="server" />
                  
            </asp:TableCell>

              <asp:TableCell  style="vertical-align:top;">
                ePub
                <br />
                <a id="HyperLink2" runat="server"  style="display:inline-block; width: 174px;" >
                   <img id="HyperLink1Image" runat="server"   height="250" width="180" alt="ePub">
                </a>

                <br />
                <asp:Image ID="VerticalAdvert" runat="server" width="230"/>
              </asp:TableCell>
         </asp:TableRow>
    </asp:table>
</asp:Content>
