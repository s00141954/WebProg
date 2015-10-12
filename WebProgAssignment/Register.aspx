<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebProgAssignment.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Registration">
        <asp:TextBox ID="tbxUserName" placeholder="User Name" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqUserName" runat="server" ControlToValidate="tbxUserName" ErrorMessage="UserName is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
        <asp:TextBox ID="tbxEmail" placeholer="Email" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqEmail" ControlToValidate="tbxEmail" runat="server" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegExEmail" runat="server" ErrorMessage="Please enter a valid email" ControlToValidate="tbxEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    <br />
        <asp:TextBox ID="tbxPassword" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqPassword" ControlToValidate="tbxPassword" runat="server" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
        <asp:TextBox ID="tbxConfirmPassword" placeholder="Confirm Password" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqConfirmPasswird" ControlToValidate="tbxConfirmPassword" runat="server" ErrorMessage="Confirm Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompValEmail" runat="server" ErrorMessage="Passwords do not match" ForeColor="Red" ControlToValidate="tbxConfirmPassword" ControlToCompare="tbxPassword"></asp:CompareValidator>
    <br />
        <asp:TextBox ID="tbxDOB" runat="server" TextMode="Date"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqDOB" ControlToValidate="tbxDOB" runat="server" ErrorMessage="Date of Birth is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
        <asp:TextBox ID="tbxContactNum" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ReqContactNum" runat="server" ControlToValidate="tbxContactNum" ErrorMessage="Contact Number is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnReset" runat="server" Text="Reset" />
    </div>
    <div id="ValidateSummary">
    </div>
</asp:Content>
