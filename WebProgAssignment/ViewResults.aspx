<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewResults.aspx.cs" Inherits="WebProgAssignment.ViewResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvPlacedBets" runat="server"></asp:GridView>
    <br />
    <asp:GridView ID="gvCorrectBets" runat="server"></asp:GridView>
</asp:Content>
