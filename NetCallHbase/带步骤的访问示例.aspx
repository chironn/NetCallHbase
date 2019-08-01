<%@ Page Language="C#" AutoEventWireup="true" CodeFile="带步骤的访问示例.aspx.cs" Inherits="带步骤的访问示例" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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

            <h2><span class="auto-style1">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                步骤1:联接HBase</strong></span><strong><br class="auto-style1" /></h2>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Button ID="测试联接是否成功" runat="server" Text="测试联接是否成功 " OnClick="测试联接是否成功_Click" />
                    <br />
                    联接地址:&quot;192.168.2.111&quot;, 9090<br />
                    <asp:Label ID="Label1" runat="server" Text="返回状态..." Style="color: #339966"></asp:Label>
                    <br />
                    <br />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>

            <h2>步骤2:创建数据表</h2>
            <strong>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        表名:<asp:TextBox ID="txt表名" runat="server"></asp:TextBox>
                        <strong>&nbsp;表的数据列名(多个以,分开):<asp:TextBox ID="txt列名集合" runat="server"></asp:TextBox>
                            <asp:Button ID="获取表" runat="server" Text="获取表" OnClick="获取表_Click" />
                            &nbsp;
                <asp:Button ID="创建数据表" runat="server" Text="创建数据表" OnClick="创建数据表_Click" />
                            <asp:Button ID="删除表" runat="server" Text="删除表" OnClick="删除表_Click" />

                            <br />
                            <asp:Label ID="lab创建表信息状态" runat="server" Style="color: #339966" Text="返回状态..."></asp:Label>
                            <br />
                            当前数据库所有表<br />
                            <asp:GridView ID="gri当前数据库所有表" runat="server">
                            </asp:GridView>
                            <br />
                            <br />
                            表<asp:Label ID="lab当前新增表名" runat="server" Text=""></asp:Label>
                            的示例


                <asp:GridView ID="gri创建数据表示例数据" runat="server">
                </asp:GridView>
                            <br />
                            <br />
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <h2>步骤3:添加数据</h2>
                <strong>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="添加数据" runat="server" Text="添加数据" OnClick="添加数据_Click" />
                            <br />
                            <strong>
                                <asp:Label ID="lab添加数据状态" runat="server" Style="color: #339966" Text="返回状态..."></asp:Label>
                            </strong>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </strong>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <h3>步骤4:获取数据</h3>
                <strong>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <strong>
                                <asp:Button ID="获取数据" runat="server" Text="获取数据" OnClick="获取数据_Click" />
                                <br />
                                <asp:Label ID="lab获取数据" runat="server" Style="color: #339966" Text="返回状态..."></asp:Label>

                                <br />

                            </strong>
                            <asp:GridView ID="gri获取数据" runat="server">
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </strong>
                <br />
                <br />
                <br />
                <br />
                <br />
                <h2>步骤5:删除数据</h2>
                <p>
                    &nbsp;
                </p>
                <p>
                    <strong>
                </p>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="删除单元格数据" runat="server" Text="删除单元格数据" OnClick="删除单元格数据_Click" />
                        &nbsp;单元格列名:<strong><asp:TextBox ID="txt单元格列名" runat="server"></asp:TextBox>
                        </strong>
                        <br />
                        <br />
                        <asp:Label ID="lab删除数据" runat="server" Style="color: #339966" Text="返回状态..."></asp:Label>
                        <br />
                        <br />
                        <asp:Button ID="删除数据行" runat="server" Text="删除数据行" OnClick="删除数据行_Click" Width="131px" />
                        &nbsp; 数据行key:<asp:TextBox ID="txt数据行key" runat="server"></asp:TextBox>
                    </ContentTemplate>

                </asp:UpdatePanel>
                <p>
            </strong>
            </p>
            <h2>步骤6:一次加入多行数据</h2>
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
<asp:Button ID="一次加入多行数据" runat="server" Text="一次加入多行数据" OnClick="一次加入多行数据_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
