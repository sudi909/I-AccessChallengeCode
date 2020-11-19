<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="I_AccessChallengeCode.WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Button ID="btnInsert" runat="server" OnClick="BtnInsert_Click" Text="Insert Data" />
        </p>
        <p>
            <asp:TextBox ID="txtBoxSearch" runat="server" placeholder="Keywords" /><asp:Button ID="btnSearch" runat="server" OnClick="BtnSearch_Click" Text="Search Data" />
        </p>
        <p>
            &nbsp;</p>
        <asp:GridView ID="GridViewResult" runat="server">
        </asp:GridView>
    </form>
</body>
</html>
