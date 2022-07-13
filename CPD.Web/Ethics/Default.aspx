<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CPD.Web._Default" EnableSessionState="True" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  
    <asp:Table ID="Main"  Width="980px" runat="server" Height="225px">
    <asp:TableRow>
    <asp:TableCell style="width:65%; vertical-align:top;">
    <br />
    <asp:Table runat="server" ID="LabelTable">
            <asp:TableRow>
                <asp:TableCell><asp:Label ID="LabelGreeting" runat="server" Text="Welcome to this CPD Test facility" ForeColor="#999"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label ID="Label2" runat="server" Text="" ForeColor="#999"></asp:Label></asp:TableCell>
            </asp:TableRow>
           <asp:TableRow>
                <asp:TableCell><br /><asp:Button ID="Button1" CssClass="button-generic" runat="server" Text="Login" onclick="Button1_Click" /></asp:TableCell>
                <asp:TableCell><br /><asp:Button ID="Buttonh" CssClass="button-generic" runat="server" Text="View History" onclick="Buttonh_Click" Visible="False" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><br /><br /><asp:Label ID="Label1" CssClass="subheading" runat="server" Text="Catalogue of available publications"></asp:Label></asp:TableCell>
            </asp:TableRow>
    </asp:Table>
    <br />

    <asp:GridView ID="GridViewCatalogue" CssClass="publication-table" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="SurveyId,IssueId" DataSourceID="ObjectDataSourceCatalogue" 
        AllowSorting="True" AutoGenerateSelectButton="True" 
        SelectedRowStyle-BackColor="Red" 
        onselectedindexchanged="GridViewCatalogue_SelectedIndexChanged" >
        <Columns>
            <asp:BoundField DataField="SurveyId" visible="false"/>
            <asp:BoundField DataField="Publication" HeaderText="Publication" 
                SortExpression="Publication" />
            <asp:BoundField DataField="IssueId" Visible="false" />
        </Columns>
        <SelectedRowStyle BackColor="Red"></SelectedRowStyle>
    </asp:GridView>

    <br />
    <br />
    <asp:Label ID="LabelResponse" runat="server" Text="" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <br />
    </asp:TableCell>
    
    </asp:TableRow>
    </asp:Table>
    
    <asp:ObjectDataSource ID="ObjectDataSourceCatalogue" runat="server" 
        SelectMethod="GetAvailibleSurveys" TypeName="CPD.Data.ModuleData" 
         OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter Name="CustomerId" SessionField="CustomerId" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
