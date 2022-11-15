<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" Inherits="CPD.Web.Login2" EnableSessionState="True" MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <asp:Label ID="LabelResponse" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="LabelVersion" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="MIMS Customer Id"></asp:Label>
        <asp:TextBox ID="TextCustomerId" CssClass="input-generic" runat="server"  TextMode="SingleLine"  
            Width="107px" ></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="  Email address"></asp:Label>
        <asp:TextBox ID="TextEMail" CssClass="input-generic" runat="server" Width="267px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonLogin" CssClass="button-generic" runat="server" onclick="ButtonLogin_Click" 
            Text="Login" />
        <asp:Button ID="ButtonHome" CssClass="button-generic" runat="server" onclick="ButtonHome_Click" 
            Text="Home" />
    </div>
</asp:Content>

