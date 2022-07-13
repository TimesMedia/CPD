<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="CPD.Web.History" EnableSessionState="ReadOnly" MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:Label ID="LabelCurrent" CssClass="subheading" runat="server" 
            Text="You are currently busy with the following test"></asp:Label>
            <br />
            <br />
        <asp:GridView ID="GridViewCurrent" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ResultId" Height="16px" Width="952px">
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
    
    </div>
    <br />
    <br />
    <asp:Label ID="LabelHistory" CssClass="subheading" runat="server" 
        Text="You have completed the following tests"></asp:Label>
    <br />
    <br />
    <div style="max-height:400px; width:952px;overflow-y:scroll;">
    <asp:GridView ID="GridViewHistory" runat="server" AutoGenerateColumns="False" Width="922px"
        DataKeyNames="ResultId" AutoGenerateSelectButton="True" 
        SelectedRowStyle-BackColor="Red" Height="10" >
        <Columns>
               <asp:BoundField DataField="ResultId" HeaderText="ResultId" ReadOnly="True" 
                    SortExpression="ResultId"  Visible = "false"/>
                <asp:BoundField DataField="Publication" HeaderText="Publication" 
                    SortExpression="Publication" />
             <%--   <asp:BoundField DataField="Issue" HeaderText="Issue" SortExpression="Issue" />--%>
                <asp:BoundField DataField="Module" HeaderText="Module" 
                    SortExpression="Module" >
                <ItemStyle Width="470px" />
               </asp:BoundField>
               <%-- <asp:BoundField DataField="Attempt" HeaderText="Attempt" ItemStyle-HorizontalAlign="Right"   SortExpression="Attempt" >
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
               </asp:BoundField>--%>
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:dd/MM/yyyy}" >
                <ItemStyle Wrap="False" />
               </asp:BoundField>

                <asp:BoundField DataField="Score" HeaderText="Score %" ItemStyle-HorizontalAlign="Right" >
                   <ItemStyle HorizontalAlign="Right"></ItemStyle>
               </asp:BoundField>

                <asp:BoundField DataField="Verdict" HeaderText="Verdict" SortExpression="Verdict" />
    
                <asp:BoundField DataField="NormalPoints" HeaderText="Normal points" 
                SortExpression="NormalPoints">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>

               <asp:BoundField DataField="EthicsPoints" HeaderText="Ethics points" 
                 SortExpression="EthicsPoints">
                 <ItemStyle HorizontalAlign="Right"></ItemStyle>
               </asp:BoundField>
        </Columns>

<%--<SelectedRowStyle BackColor="Red"></SelectedRowStyle>--%>
    </asp:GridView>
    </div>
    <br/>
     <asp:Label ID="LabelResponse" runat="server" Text="" ForeColor="Red"></asp:Label> 
    <br/><br />
    <asp:Button ID="ButtonReissue" CssClass="button-generic" runat="server" 
        Text="Reissue a certificate for a selected passed module" Width="430px" 
        onclick="ButtonReissue_Click" />  &nbsp; &nbsp;
    <asp:Button ID="ButtonHome" CssClass="button-generic" runat="server" onclick="ButtonHome_Click" 
        Text="Home" />
    <br /><br /><br />
    </asp:Content>