<%@ Page Language="C#" AutoEventWireup="true" CodeFile="历史数据导入HBase.aspx.cs" Inherits="历史数据导入HBase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>历史数据导入HBase</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        终端号:<asp:TextBox ID="txtTerminalcode" runat="server"></asp:TextBox>

        <asp:Button ID="Button1" runat="server" Text="开始导入" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
