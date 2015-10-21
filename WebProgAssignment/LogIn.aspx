<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="WebProgAssignment.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:Label ID="lblUserName" runat="server" Text="UserName:"></asp:Label>
    <asp:TextBox ID="tbxUserName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="ValReqUserNameLogIn" runat="server" ControlToValidate="tbxUserName" ErrorMessage="Please Enter your User Name" ForeColor="Red"></asp:RequiredFieldValidator>
    <br/>
    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
    <asp:TextBox ID="tbxPassword" runat="server" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="ValReqPasswordLogIn" runat="server" ControlToValidate="tbxPassword" ErrorMessage="Please Enter your Password" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btnLogIn" runat="server" Text="Log In" OnClick="btnLogIn_Click" />
</asp:Content>
