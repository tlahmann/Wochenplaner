<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wochenplaner.aspx.cs" Inherits="Wochenplaner.Wochenplaner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="Wochenplaner und Terminkalender der Uni Ulm." />
    <meta name="author" content="Tobias Lahmann" />
    <meta name="keywords" content="Wochenplaner, Terminkalender, Organisation" />
    <meta name="date" content="2014-11-11" />
    <link rel="stylesheet" type="text/css" href="StyleSheets/style.css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <title>Wochenplaner v 0.17</title>
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="Scripts/WP_MainScript.js"></script>
</head>
<body>
    <form id="form" runat="server">

        <div id="header">
            <div>
                <asp:Label ID="title" runat="server" Text="Wochenplaner"></asp:Label>
            </div>
            <div>
                <asp:Label ID="subtitle" runat="server" Text="Kalenderwoche"></asp:Label>
            </div>
            <asp:ImageButton ID="print" runat="server" ImageUrl="printer.png" OnClick="printCalendar" />
            <div class="navigation">
                <asp:Button ID="btnBkwd" CssClass="navgation-button" runat="server" Text="<" OnClick="moveWeekBackwards"></asp:Button>
                <asp:Button ID="btnFwrd" CssClass="navgation-button" runat="server" Text=">" OnClick="moveWeekForwards"></asp:Button>
            </div>
            <div class="user-login-wrapper">
                <div>
                    <asp:Label ID="loginLabel" CssClass="login-label" runat="server" Text="Benutzer"></asp:Label>
                    <asp:Button ID="loginButton" CssClass="login-button" runat="server" Text="Login" OnClick="loginBtnClick"></asp:Button>
                </div>
                <div>
                    <asp:Label ID="userData" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>

        <asp:Table ID="TablePlanner" runat="server" OnDataBinding="openOverlay">
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="no-background" runat="server"></asp:TableCell>
                <asp:TableCell CssClass="with-background" runat="server">
                    <asp:Button ID="bt1" CssClass="head-button" runat="server" Text="Montag" OnClientClick="this.disabled=true;"></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="with-background" runat="server">
                    <asp:Button ID="bt2" CssClass="head-button" runat="server" Text="Dienstag" OnClientClick="this.disabled=true;"></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="with-background" runat="server">
                    <asp:Button ID="bt3" CssClass="head-button" runat="server" Text="Mittwoch" OnClientClick="this.disabled=true;"></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="with-background" runat="server">
                    <asp:Button ID="bt4" CssClass="head-button" runat="server" Text="Donnerstag" OnClientClick="this.disabled=true;"></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="with-background" runat="server">
                    <asp:Button ID="bt5" CssClass="head-button" runat="server" Text="Freitag" OnClientClick="this.disabled=true;"></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="with-background" runat="server">
                    <asp:Button ID="bt6" CssClass="head-button" runat="server" Text="Samstag" OnClientClick="this.disabled=true;"></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="with-background" runat="server">
                    <asp:Button ID="bt7" CssClass="head-button" runat="server" Text="Sonntag" OnClientClick="this.disabled=true;"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head" runat="server">07:00</asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MO07" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DI07" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MI07" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DO07" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="FR07" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SA07" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SO07" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head-alternating" runat="server">08:00</asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MO08" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DI08" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MI08" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DO08" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="FR08" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SA08" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SO08" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head" runat="server">09:00</asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MO09" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DI09" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MI09" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DO09" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="FR09" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SA09" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SO09" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head-alternating" runat="server">10:00</asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MO10" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DI10" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MI10" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DO10" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="FR10" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SA10" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SO10" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head" runat="server">11:00</asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MO11" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DI11" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MI11" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DO11" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="FR11" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SA11" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SO11" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head-alternating" runat="server">12:00</asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MO12" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DI12" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MI12" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DO12" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="FR12" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SA12" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SO12" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head" runat="server">13:00</asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MO13" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DI13" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MI13" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DO13" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="FR13" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SA13" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SO13" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head-alternating" runat="server">14:00</asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MO14" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DI14" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MI14" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DO14" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="FR14" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SA14" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SO14" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head" runat="server">15:00</asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MO15" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DI15" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MI15" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DO15" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="FR15" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SA15" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SO15" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head-alternating" runat="server">16:00</asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MO16" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DI16" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MI16" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DO16" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="FR16" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SA16" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SO16" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head" runat="server">17:00</asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MO17" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DI17" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MI17" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DO17" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="FR17" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SA17" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SO17" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head-alternating" runat="server">18:00</asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MO18" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DI18" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MI18" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DO18" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="FR18" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SA18" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SO18" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head" runat="server">19:00</asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MO19" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DI19" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MI19" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DO19" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="FR19" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SA19" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SO19" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head-alternating" runat="server">20:00</asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MO20" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DI20" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="MI20" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="DO20" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="FR20" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="SA20" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content-alternating" runat="server">
                    <asp:Button ID="So20" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="special-head" runat="server">21:00</asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MO21" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DI21" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="MI21" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="DO21" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="FR21" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SA21" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
                <asp:TableCell CssClass="content" runat="server">
                    <asp:Button ID="SO21" CssClass="appointment-button" OnClick="openOverlay" runat="server" Text=""></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <div id="overlay-appointment-wrapper">
            <div class="overlay-appointment">
                <h2>Terminbearbeitung:</h2>
                <h3>
                    <asp:Label ID="overlayChoosenDate" runat="server" Text=""></asp:Label></h3>
                <asp:Table ID="table" runat="server">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server"><div class="overlay-appointment-label">Titel: </div></asp:TableCell>
                        <asp:TableCell runat="server">
                            <asp:TextBox ID="overlayTextBoxSmall" runat="server"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server"><div class="overlay-appointment-label">Beschreibung: </div></asp:TableCell>
                        <asp:TableCell runat="server">
                            <asp:TextBox ID="overlayTextBoxLarge" TextMode="multiline" Rows="8" runat="server"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server">
                            <asp:CheckBox ID="cbRepeat" CssClass="overlay-appointment-check-box-repeating" runat="server" Text="Wiederholen" />
                        </asp:TableCell>
                        <asp:TableCell runat="server">
                            <asp:DropDownList ID="ddRepeat" CssClass="overlay-dateTime-dropDown" runat="server">
                                <asp:ListItem Value="01">Täglich</asp:ListItem>
                                <asp:ListItem Value="02">Jeden Wochentag</asp:ListItem>
                                <asp:ListItem Value="03">Wöchentlich</asp:ListItem>
                                <asp:ListItem Value="04">2 Wöchentlich</asp:ListItem>
                                <asp:ListItem Value="05">Monatlich</asp:ListItem>
                                <asp:ListItem Value="06">Jährlich</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%--<asp:TableRow runat="server">
                        <asp:TableCell runat="server">
                            <asp:CheckBox ID="cbEnd" CssClass="overlay-appointment-check-box-repeating" runat="server" Text="Endet" />
                        </asp:TableCell>
                        <asp:TableCell runat="server">
                            <div id="overlay-appointment-date-wrapper">
                                <div class="overlay-appointment-upDown-wrapper-left">
                                    <asp:TextBox ID="textBoxDay" CssClass="overlay-appointment-upDown-textField" runat="server"></asp:TextBox>
                                    <div class="overlay-appointment-upDown-button-wrapper">
                                        <asp:ImageButton ID="dayUp" CssClass="overlay-appointment-upDown-up-button" runat="server" ImageUrl="~/arrowUp.png" />
                                        <asp:ImageButton ID="dayDown" CssClass="overlay-appointment-upDown-down-button" runat="server" ImageUrl="~/arrowDown.png" />
                                    </div>
                                </div>
                                <div class="overlay-appointment-upDown-wrapper-right">
                                    <asp:TextBox ID="textBoxYear" CssClass="overlay-appointment-upDown-textField" runat="server"></asp:TextBox>
                                    <div class="overlay-appointment-upDown-button-wrapper">
                                        <asp:ImageButton ID="yearUp" CssClass="overlay-appointment-upDown-up-button" runat="server" ImageUrl="~/arrowUp.png" />
                                        <asp:ImageButton ID="yearDown" CssClass="overlay-appointment-upDown-down-button" runat="server" ImageUrl="~/arrowDown.png" />
                                    </div>
                                </div>
                                <div class="overlay-appointment-upDown-wrapper-middle">
                                    <asp:TextBox ID="textBoxMonth" CssClass="overlay-appointment-upDown-textField" runat="server"></asp:TextBox>
                                    <div class="overlay-appointment-upDown-button-wrapper">
                                        <asp:ImageButton ID="monthUp" CssClass="overlay-appointment-upDown-up-button" runat="server" ImageUrl="~/arrowUp.png" />
                                        <asp:ImageButton ID="monthDown" CssClass="overlay-appointment-upDown-down-button" runat="server" ImageUrl="~/arrowDown.png" />
                                    </div>
                                </div>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                </asp:Table>
                <div class="overlay-appointment-button-wrapper">
                    <asp:Button ID="buttonOK" OnClick="createAppointmentClick" CssClass="overlay-button" runat="server" Text="Okay"></asp:Button>
                    <asp:Button ID="buttonMove" OnClick="moveAppointmentOverlay" CssClass="overlay-button" runat="server" Text="Verschieben"></asp:Button>
                    <asp:Button ID="buttonErase" OnClick="deleteAppointment" CssClass="overlay-button" runat="server" Text="Löschen"></asp:Button>
                    <asp:Button ID="buttonClose" OnClick="closeOverlay" CssClass="overlay-button" runat="server" Text="Schließen"></asp:Button>
                </div>
            </div>
        </div>

        <div id="overlay-dateTime-wrapper">
            <div class="overlay-dateTime">
                <asp:Calendar ID="Calendar" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="390px" OnSelectionChanged="moveAppointment">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="#6D929B" BorderWidth="3px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                    <TodayDayStyle BackColor="#E5EBEE" />
                </asp:Calendar>
                <div>
                    <asp:DropDownList ID="ddTimePicker" CssClass="overlay-dateTime-dropDown" runat="server">
                        <asp:ListItem Value="00">Bitte Wählen</asp:ListItem>
                        <asp:ListItem Value="07">07:00</asp:ListItem>
                        <asp:ListItem Value="08">08:00</asp:ListItem>
                        <asp:ListItem Value="09">09:00</asp:ListItem>
                        <asp:ListItem Value="10">10:00</asp:ListItem>
                        <asp:ListItem Value="11">11:00</asp:ListItem>
                        <asp:ListItem Value="12">12:00</asp:ListItem>
                        <asp:ListItem Value="13">13:00</asp:ListItem>
                        <asp:ListItem Value="14">14:00</asp:ListItem>
                        <asp:ListItem Value="15">15:00</asp:ListItem>
                        <asp:ListItem Value="16">16:00</asp:ListItem>
                        <asp:ListItem Value="17">17:00</asp:ListItem>
                        <asp:ListItem Value="18">18:00</asp:ListItem>
                        <asp:ListItem Value="19">19:00</asp:ListItem>
                        <asp:ListItem Value="20">20:00</asp:ListItem>
                        <asp:ListItem Value="21">21:00</asp:ListItem>
                    </asp:DropDownList>
                    <div class="overlay-dateTime-button-wrapper">
                        <asp:Button ID="buttonDateOkay" CssClass="overlay-button" runat="server" Text="Okay" OnClick="moveAppointment"></asp:Button>
                        <asp:Button ID="buttonDateCancel" CssClass="overlay-button" runat="server" Text="Abbrechen"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

        <div id="overlay-login-wrapper">
            <div class="overlay-login">
                <div class="overlay-appointment-input-wrapper">
                    <div class="overlay-login-input-wrapper-title" style="width: 100%">
                        <div class="overlay-login-label">Name / Benutzer-ID: </div>
                        <asp:TextBox ID="overlayTextBoxLogin" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="overlay-dateTime-button-wrapper">
                    <asp:Button ID="buttonLoginOkay" CssClass="overlay-button" runat="server" Text="Okay" OnClick="loginUser"></asp:Button>
                    <asp:Button ID="buttonLoginCancel" CssClass="overlay-button" runat="server" Text="Abbrechen"></asp:Button>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
