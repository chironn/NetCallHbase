<%@ Page Language="C#" AutoEventWireup="true" CodeFile="访问数据示例.aspx.cs" Inherits="访问数据示例" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: larger;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            hbase地址 &nbsp; &nbsp;&nbsp;<span style="FONT-SIZE: 10.5pt; LINE-HEIGHT: 1.5">http://192.168.2.111:60010</span></div>
        <div>
            <br />
        </div>
        <div>
            集群IP nameNode &nbsp;192.168.2.111</div>
        <div>
            <span style="">&nbsp; &nbsp;&nbsp;</span><span style="FONT-SIZE: 10.5pt; LINE-HEIGHT: 1.5">&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;dataNode01~03 &nbsp; 192.168.2.101~103</span></div>
        <div>
            <br />
            <br />
        </div>
        <div>
            hbase的thrift服务已启动，服务端口为9090</div>
        <br />
        <h2><span class="auto-style1"><strong>步骤1:联接HBase</strong></span><strong><br class="auto-style1" />
            </strong></h2>
        <br />
        <br />
        <h2>步骤2:创建数据表</h2>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <h2>步骤3:添加数据</h2>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <h3>步骤4:获取数据</h3>
        <br />
        <br />
        <br />
        <br />
        <br />
        <h2>步骤5:删除数据</h2>
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="获取数据-Key:aaaa_1" OnClick="Button1_Click" Width="199px" />
        <asp:Button ID="Button5" runat="server" Text="获取数据abc添加的Key" OnClick="Button5_Click1" />

        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="用SCAN获取多行数据" />
        <asp:Button ID="Button7" runat="server"  Text="用SCAN获取一行数据" OnClick="Button7_Click" />

        &nbsp;<asp:GridView ID="GridView1" runat="server"  >
        </asp:GridView>
        
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="添加数据" OnClick="Button3_Click" />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="删除单元格数据" OnClick="Button5_Click"  />
    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Text="删除数据行" OnClick="Button2_Click" />
    </div>
    </form>
</body>
</html>
