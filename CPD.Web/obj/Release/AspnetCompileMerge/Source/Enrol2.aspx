<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Enrol2.aspx.cs" Inherits="CPD.Web.Enrol2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Image ID="HorisontalImage" runat="server" height="90px" width="940px"/>
    <br />
  
    <table style="width: 1138px" runat="server">
        <tr>
            <td style="width: 675px" >
                <table>
                    <tr>
                        <td style="width: 575px">
                            <asp:Label ID="LabelGreeting" runat="server" Text="" ForeColor="Green"></asp:Label> <br />
                            <asp:Label ID="Label2" runat="server" Text="" ForeColor="Green"></asp:Label> <br />
                              <asp:Label ID="Label3" CssClass="subheading" runat="server"  /> <br />
                            <asp:Label ID="Label1" CssClass="subheading" runat="server" Text="Catalogue of available modules" /> <br />
                         </td>

                        <td style="width: 100px">
                            <asp:Button ID="ButtonHelp" CssClass="button-generic" style="margin-left:10px;" runat="server" Text="How to take the test " OnClick ="ButtonHelp_Click" />
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
                DataKeyNames="ModuleId"
                AllowSorting="True" AutoGenerateSelectButton="True" 
                SelectedRowStyle-BackColor="Red" Width="675px"
                onselectedindexchanged="GridViewCatalogue_SelectedIndexChanged" >
                    <Columns>
                        <asp:BoundField DataField="ModuleId" HeaderText="ModuleId" ReadOnly="True" visible="false"
                            SortExpression="ModuleId" />
                        <asp:BoundField DataField="Publication" HeaderText="Publication" 
                            SortExpression="Publication" visible="false"/>
                        <asp:BoundField DataField="Issue" HeaderText="Issue" SortExpression="Issue" visible="false"/>
                        <asp:BoundField DataField="Module" HeaderText="Module" 
                            SortExpression="Module" />
                        <asp:BoundField DataField="NormalPoints" HeaderText="NormalPoints" 
                            SortExpression="NormalPoints" />
                        <asp:BoundField DataField="EthicsPoints" HeaderText="Ethics Points"
                            SortExpression="EthicsPoints"/>
                        <asp:BoundField DataField="PassRate" HeaderText="Pass Rate" 
                            SortExpression="PassRate" />
                        <asp:BoundField DataField="Expiration" HeaderText="Expiration" SortExpression="Expiration" visible="false" />
                    </Columns>
                <SelectedRowStyle BackColor="Red"></SelectedRowStyle>
                </asp:GridView>
                             
                <br />
                <br />
                <%--<asp:Button ID="ButtonTest" CssClass="button-generic button-generic-block" runat="server" onclick="ButtonTestContinue_Click" 
                Text="Continue with current test" />--%>
                <asp:Button ID="ButtonHistory" CssClass="button-generic button-generic-block" runat="server" onclick="ButtonHistory_Click" 
                Text="View test history" />
                <asp:Button ID="ButtonHome" CssClass="button-generic button-generic-block" runat="server" onclick="ButtonHome_Click" 
                Text="Home" />

            </td>
                <td  style="vertical-align:top;">
                     ePub 
                     <br />
                     <a id="MainContent_HyperLink1" runat="server"  style="display:inline-block; width: 174px; ">
                     <img id="HyperLink1Image" runat="server"   height="250" width="180" alt="ePub"></a>
                    <br />
                    <asp:Image ID="VerticalImage" runat="server" width="180"/>
                </td>
        </tr>
    </table> 

</asp:Content>

