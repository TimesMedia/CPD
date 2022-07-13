<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CPD.Web.Login" EnableSessionState="True" MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:Label ID="LabelResponse" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="LabelVersion" runat="server" Text=""></asp:Label>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonHome" CssClass="button-generic" runat="server" onclick="ButtonHome_Click" 
            Text="Home" />
    </div>
</asp:Content>

