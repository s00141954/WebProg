<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebProgAssignment.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Registration">
        <asp:Label ID="lblUserName" runat="server" Text="User Name:"></asp:Label>
        <asp:TextBox ID="tbxUserName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqUserName" runat="server" ControlToValidate="tbxUserName" ErrorMessage="UserName is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
        <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
        <asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqEmail" ControlToValidate="tbxEmail" runat="server" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegExEmail" runat="server" ErrorMessage="Please enter a valid email" ControlToValidate="tbxEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    <br />
        <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
        <asp:TextBox ID="tbxPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqPassword" ControlToValidate="tbxPassword" runat="server" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:"></asp:Label>
        <asp:TextBox ID="tbxConfirmPassword"  runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqConfirmPasswird" ControlToValidate="tbxConfirmPassword" runat="server" ErrorMessage="Confirm Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompValEmail" runat="server" ErrorMessage="Passwords do not match" ForeColor="Red" ControlToValidate="tbxConfirmPassword" ControlToCompare="tbxPassword"></asp:CompareValidator>
    <br />
        <asp:Label ID="lblDOB" runat="server" Text="Date of birth:"></asp:Label>
        <asp:TextBox ID="tbxDOB" runat="server" TextMode="Date"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqDOB" ControlToValidate="tbxDOB" runat="server" ErrorMessage="Date of Birth is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
        <asp:Label ID="lblContactNum" runat="server" Text="Contact Number:"></asp:Label>
        <asp:TextBox ID="tbxContactNum" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqContactNum" runat="server" ControlToValidate="tbxContactNum" ErrorMessage="Contact Number is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnReset" runat="server" Text="Reset" />
        <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
    </div>
    <div id="ValidateSummary">
    </div>
</asp:Content>
