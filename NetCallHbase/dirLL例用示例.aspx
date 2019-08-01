<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dirLL例用示例.aspx.cs" Inherits="dirLL例用示例" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        1.下载MapR3.8的ODBC驱动<br />
        <a href="http://package.mapr.com/tools/MapR-ODBC/MapR_Drill/MapRDrill_odbc_v1.2.1.1000/">http://package.mapr.com/tools/MapR-ODBC/MapR_Drill/MapRDrill_odbc_v1.2.1.1000/</a><br />
        2.联接字符串<br />
        DRIVER=MapR Drill ODBC Driver;AdvancedProperties={HandshakeTimeout=0;QueryTimeout=0;TimestampTZDisplayTimezone=utc;ExcludedSchemas=sys,INFORMATION_SCHEMA;};Catalog=DRILL;Schema=hbase;ConnectionType=Direct;Host=192.168.2.113;Port=31010<br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="联接到数据库" />
    
    </div>
    </form>
</body>
</html>
