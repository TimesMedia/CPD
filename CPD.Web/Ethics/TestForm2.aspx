<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm2.aspx.cs" Inherits="CPD.Web.TestForm2"
    MasterPageFile="~/Site.Master" Title="Test page" %>

<%--<%@ Register Src="~/UserControls/Advert.ascx" TagName="addControl" TagPrefix="uc1" %>--%>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <asp:Image ID="HorisontalImage" runat="server" height="90px" width="940px"/>
    <table style="width: 1035px;">
        <tr>
            <td style="width: 675px" >
                <br /><br />
                <asp:Button ID="ButtonHelp" CssClass="button-generic" style="margin-left:10px;" runat="server" Text="How to take the test " OnClick ="ButtonHelp_Click" />
                <div id="ebookDiv" runat="server" style="float:right; text-align: center;">
                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Width="100" >EBook</asp:HyperLink>
                    <br />
                    E-Book
                </div>
  
               <br />
                <p style="width: 650px;">
                    <span><b>Please click on the question you want to answer or use the 'Next Question' button
                        to answer the questions sequentially.</b>
                    </span>
                </p>
 
                <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSourceQuestion" Width="650"
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
                                    <asp:TableCell><b>Question:</b></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" ID="SingleQuestion"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top"><b>Answer:</b></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBoxList ID="CheckBoxListAnswer" runat="server" DataSourceID="ObjectDataSourceAnswer"
                                            DataTextField="Answer" DataValueField="Correct" AutoPostBack="false">
                                        </asp:CheckBoxList>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Label ID="LabelResponse" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <br />
                            <asp:Button ID="ButtonNext" CssClass="button-generic" runat="server" Text="Next Question" OnClick="ButtonNext_Click" />
                            <br />
                            <br />
                            <asp:Button ID="ButtonSave" CssClass="button-generic" title="Save all my answers" runat="server" Text="Save" OnClick="ButtonSave_Click" />&nbsp;
                            <asp:Button ID="ButtonQuit" CssClass="button-generic" title="Quit as though I never took the test" runat="server" Text="Quit" OnClick="ButtonQuit_Click" />&nbsp;
                            <asp:Button ID="ButtonSubmit" CssClass="button-generic" title="Submit the module to be marked, before continuing" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" ToolTip="You have to submit the current module to be marked, before continuing to the next module" />&nbsp;
                            <asp:Button ID="ButtonHome" CssClass="button-generic" runat="server" OnClick="ButtonHome_Click" Text="Home" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


                <asp:XmlDataSource ID="XmlDataSourceQuestion" runat="server" 
                    ontransforming="XmlDataSourceQuestion_Transforming" />
                <asp:ObjectDataSource ID="ObjectDataSourceAnswer" runat="server" TypeName="CPD.Data.ResultData"
                    SelectMethod="GetAnswer" EnableViewState="true">
                    <SelectParameters>
                        <asp:SessionParameter Name="CustomerId" SessionField="CustomerId" />
                        <asp:ControlParameter ControlID="TreeView1" Name="QuestionId" PropertyName="SelectedValue"
                            Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:SessionParameter Name="CustomerId" SessionField="CustomerId" />
                        <asp:Parameter Name="AnswerId" Type="Int32" />
                        <asp:Parameter Name="Correct" Type="Boolean" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
            </td>

              <td>
                  <asp:Image ID="Image10" runat="server" width="230"/>
              </td>
         </tr>
    </table>
</asp:Content>
