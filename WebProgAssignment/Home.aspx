<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebProgAssignment.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="lblFor1" runat="server" Text="1"/>
    <%--<asp:DropDownList ID="ddlFixture1" runat="server" AutoPostBack="true" DataTextField="HTeamName" DataValueField="HTeamName" DataSourceID="SqlDataSourceFixtures"></asp:DropDownList>
    <asp:DropDownList ID="ddlPlayer1" runat="server" DataSourceID="SqlDataSourcePlayers" DataTextField="PlayerName" DataValueField="PlayerName"></asp:DropDownList>--%>
    <asp:DropDownList ID="ddlFixture1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFixture1_SelectedIndexChanged"></asp:DropDownList>
    <asp:DropDownList ID="ddlPlayer1" runat="server"></asp:DropDownList>
    <br />
    <asp:Label ID="lblFor2" runat="server" Text="2"/>
    <asp:DropDownList ID="ddlFixture2" runat="server"></asp:DropDownList>
    <asp:DropDownList ID="ddlPlayer2" runat="server"></asp:DropDownList>
    <br />
    <asp:Label ID="lblFor3" runat="server" Text="3"/>
    <asp:DropDownList ID="ddlFixture3" runat="server"></asp:DropDownList>
    <asp:DropDownList ID="ddlPlayer3" runat="server"></asp:DropDownList>
    <br />
    <asp:Label ID="lblFor4" runat="server" Text="4"/>
    <asp:DropDownList ID="ddlFixture4" runat="server"></asp:DropDownList>
    <asp:DropDownList ID="ddlPlayer4" runat="server"></asp:DropDownList>
    <br />
    <asp:Label ID="lblFor5" runat="server" Text="4"/>
    <asp:DropDownList ID="ddlFixture5" runat="server"></asp:DropDownList>
    <asp:DropDownList ID="ddlPlayer5" runat="server"></asp:DropDownList>
    <br />
    <asp:Label ID="lblFor6" runat="server" Text="4"/>
    <asp:DropDownList ID="ddlFixture6" runat="server"></asp:DropDownList>
    <asp:DropDownList ID="ddlPlayer6" runat="server"></asp:DropDownList>
    <br />
<%--    <asp:SqlDataSource ID="SqlDataSourceFixtures" 
        runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"  
        SelectCommand="SELECT [HTeamName], [ATeamName] FROM [FixtureTbl] ORDER BY [Date]"
        >

    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourcePlayers" 
        runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT PlayerName FROM  PlayerTbl WHERE TeamName=@HTeamName">
    </asp:SqlDataSource>--%>
</asp:Content>
