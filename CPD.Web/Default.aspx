<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CPD.Web._Default" EnableSessionState="True" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  
    <asp:Table ID="Main"  Width="993px" runat="server" Height="67px">
        <asp:TableRow>
            <asp:TableCell style="vertical-align:top;">
                <asp:Table runat="server" ID="LabelTable">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label ID="LabelGreeting" width="1000" runat="server" Text="Welcome to this CPD Test facility version 5.26" ForeColor="Red"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                </asp:Table>
  
                <asp:Table runat="server" >
                       <asp:TableRow >
                            <asp:TableCell><br /><asp:Button ID="ButtonLogin" CssClass="button-generic" runat="server" Text="Login" onclick="ButtonLogin_Click" />
                            </asp:TableCell>
                            <asp:TableCell><br /><asp:Button ID="ButtonHistory" CssClass="button-generic" runat="server" Text="View History" onclick="ButtonHistory_Click" Visible="False" />
                            </asp:TableCell>
                           <%-- <asp:TableCell><br /><asp:Button ID="ButtonMIMS" CssClass="button-generic" runat="server" Text="View MIMS online" onclick="ButtonMimsOnline_Click" Visible="False" />
                            </asp:TableCell>--%>
                            <asp:TableCell><br /><asp:Button ID="ButtonHome" CssClass="button-generic" runat="server" Text="Return to MIMS portal" onclick="ButtonMimsPortal_Click" Visible="True" />
                            </asp:TableCell>
                          </asp:TableRow>
                       
                    </asp:Table>
                </asp:TableCell>
        </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell><br /><br /><asp:Label ID="LabelCurrent" CssClass="subheading" visible="false"  runat="server" Text="You are currently busy with the following test"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
             <asp:TableCell>
                <asp:GridView ID="GridViewCurrent" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="ResultId" Height="16px" Width="952px" AutoGenerateSelectButton="True"
                    onselectedindexchanged="GridViewCurrent_SelectedIndexChanged">
                    <Columns>
                   <asp:BoundField DataField="ResultId" ReadOnly="True"
                        SortExpression="ResultId" HeaderText="ResultId" /> 
                    <asp:BoundField DataField="Publication" HeaderText="Publication" 
                        SortExpression="Publication" />
                    <asp:BoundField DataField="Issue" HeaderText="Issue" SortExpression="Issue" />
                    <asp:BoundField DataField="Module" HeaderText="Module" 
                        SortExpression="Module" />
                    <asp:BoundField DataField="ModuleId"  SortExpression="ModuleId" 
                            HeaderText="ModuleId"/>
                    <asp:BoundField DataField="Attempt"
                        SortExpression="Attempt" HeaderText="Attempt" />
                    <asp:BoundField DataField="URL" 
                        SortExpression="URL" HeaderText="URL" Visible="false" />
                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" Visible="false" />
                    <asp:BoundField DataField="Score" HeaderText="Score" SortExpression="Score" Visible="false" />
                    <asp:BoundField DataField="Verdict" HeaderText="Verdict" 
                        SortExpression="Verdict" Visible="false" />
                     </Columns>
                </asp:GridView>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell><br /><br /><asp:Label ID="LabelCatalogue" CssClass="subheading" visible="false" runat="server" Text="Catalogue of available publications"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow visible="true">
            <asp:TableCell >
                <asp:GridView ID="GridViewCatalogue" CssClass="publication-table" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="SurveyId,IssueId, Facility, EBookURL"
                    AllowSorting="True" AutoGenerateSelectButton="True" 
                    SelectedRowStyle-BackColor="Red" 
                    onselectedindexchanged="GridViewCatalogue_SelectedIndexChanged" >
                    <Columns >
                        <asp:BoundField DataField="SurveyId" visible="false"/>
                        <asp:BoundField DataField="Publication" HeaderText="Publication" 
                            SortExpression="Publication" HeaderStyle-ForeColor="#3399FF" />
                        <asp:BoundField DataField="IssueId" Visible="false" />
                        <asp:BoundField DataField="Facility" HeaderText="Properties" HeaderStyle-ForeColor="#3399FF" />
                        <asp:BoundField DataField="ExpirationDateString" HeaderText="Expiration date" HeaderStyle-ForeColor="#3399FF"/>
                    </Columns>
                    <%--<SelectedRowStyle BackColor="Red"></SelectedRowStyle>--%>
                </asp:GridView>
                </asp:TableCell>
       </asp:TableRow>
        
    </asp:Table>
 

    <asp:Label ID="LabelResponse" runat="server" Text="" ForeColor="Red"></asp:Label>
   
<!--Start of Tawk.to Script-->
      <script type="text/javascript">
          var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
          (function () {
              var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];
              s1.async = true;
              s1.src = 'https://embed.tawk.to/6603fe591ec1082f04dbe36e/1hpvpj3o4';
              s1.charset = 'UTF-8';
              s1.setAttribute('crossorigin', '*');
              s0.parentNode.insertBefore(s1, s0);
          })();
      </script>
<!--End of Tawk.to Script-->
   
    </asp:Content>
