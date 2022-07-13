<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Enrol2.aspx.cs" Inherits="CPD.Web.Enrol2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Image ID="HorisontalImage" runat="server" height="90px" width="940px"/>
    <br />
  
    <table style="width: 992px" >
        <tr>
            <td style="width: 675px" >
                <table>
                    <tr>
                        <td style="width: 575px">
                            <asp:Label ID="LabelGreeting" runat="server" Text="" ForeColor="Green"></asp:Label> <br />
                            <asp:Label ID="Label2" runat="server" Text="" ForeColor="Green"></asp:Label> <br />
                            <asp:Label ID="Label1" CssClass="subheading" runat="server" Text="Catalogue of available modules" /> <br />
                         </td>
                        <%-- <td style="width: 100px">
                            <div id="ebookDiv" runat="server" style="float:right; text-align: center;">
                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Width="100" >EBook</asp:HyperLink>
                                <br />
                                E-Book
                            </div>
                         </td>--%>
                    </tr>
                </table>

                <asp:Label ID="LabelResponse" runat="server" Text="" ForeColor="Red"></asp:Label>
                <br /><br />

                <asp:GridView ID="GridViewCatalogue" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ModuleId" DataSourceID="ObjectDataSourceCatalogue" 
                AllowSorting="True" AutoGenerateSelectButton="True" 
                SelectedRowStyle-BackColor="Red" Width="675px"
                onselectedindexchanged="GridViewCatalogue_SelectedIndexChanged" >
                    <Columns>
                        <asp:BoundField DataField="ModuleId" HeaderText="ModuleId" ReadOnly="True" Visible="false" 
                            SortExpression="ModuleId" />
                        <asp:BoundField DataField="Publication" HeaderText="Publication" 
                            SortExpression="Publication" />
                        <asp:BoundField DataField="Issue" HeaderText="Issue" SortExpression="Issue" />
                        <asp:BoundField DataField="Module" HeaderText="Module" 
                            SortExpression="Module" />
                        <asp:BoundField DataField="Points" HeaderText="Points" 
                            SortExpression="Points" />
                        <asp:BoundField DataField="PassRate" HeaderText="PassRate" 
                            SortExpression="PassRate" />
                        <asp:BoundField DataField="Expiration" HeaderText="Expiration" 
                            SortExpression="Expiration" />
                    </Columns>
                <SelectedRowStyle BackColor="Red"></SelectedRowStyle>
                </asp:GridView>
                             
                <br />
                <br />
                <asp:Button ID="Button1" CssClass="button-generic button-generic-block" runat="server" 
                Text="Enrol for a module in the selected publication" onclick="ButtonEnrol_Click" />
                <asp:Button ID="ButtonTest" CssClass="button-generic button-generic-block" runat="server" onclick="ButtonTest2_Click" 
                Text="Continue with current test" />
                <asp:Button ID="ButtonHistory" CssClass="button-generic button-generic-block" runat="server" onclick="ButtonHistory_Click" 
                Text="View test history" />
                <asp:Button ID="ButtonHome" CssClass="button-generic button-generic-block" runat="server" onclick="ButtonHome_Click" 
                Text="Home" />

                <asp:ObjectDataSource ID="ObjectDataSourceCatalogue" runat="server" 
                SelectMethod="GetAvailibleModules" TypeName="CPD.Data.ModuleData" 
                OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:SessionParameter Name="CustomerId" SessionField="CustomerId" 
                        Type="Int32" />
                    <asp:SessionParameter Name="SurveyId" SessionField="SurveyId" Type="Int32" />
                </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            
            <td class="top-align">
                <a id="MainContent_HyperLink1" runat="server" href="EBooks/ethics/index.html" target="_blank" style="display:inline-block;">
                    <img id="HyperLink1Image" runat="server"  src ="EBooks/ethics/ThumbnailVertical.jpg" height="600" width="280" alt="EBook">
                </a>
                <br/>E-Book
            </td>
        </tr>
    </table> 
</asp:Content>
