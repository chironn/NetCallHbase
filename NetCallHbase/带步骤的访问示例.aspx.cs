using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Thrift.Protocol;
using Thrift.Transport;

public partial class 带步骤的访问示例 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void 测试联接是否成功_Click(object sender, EventArgs e)
    {
        TTransport transport = null;

        try
        {
            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();

            Label1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联接成功..";
        }
        catch (Exception ex)
        {
            Label1.Text = "联接失败..";
            Label1.Text += ex;
        }

        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }

    #region 创建表模块步骤处理

    protected void 创建数据表_Click(object sender, EventArgs e)
    {
        TTransport transport = null;

        try
        {


            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();


            #region 创建表
            //判断 表是不是存在
            if (!client.getTableNames().Select(p => p.ToUTF8String()).Contains(txt表名.Text))
            {
                //数据列
                var cols = new List<ColumnDescriptor>();

                foreach (var item in txt列名集合.Text.Split(','))
                {

                    //创建列
                    if (!string.IsNullOrEmpty(item))
                    {
                        string coln = item;
                        if (!coln.Contains(":"))
                        {
                            coln = item + ":";
                        }
                        var col = new ColumnDescriptor() { Name = coln.ToUTF8Bytes(), MaxVersions = 3 };

                        cols.Add(col);
                    }
                }

                //创建数据表
                client.createTable(txt表名.Text.ToUTF8Bytes(), cols);
                lab创建表信息状态.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;创建成功..";
            }
            else
            {
                lab创建表信息状态.Text = "创建失败..表:" + txt表名.Text + "已经存在";
                //return;
            }

            //向表里加数据
            #region 向表里加数据

            当前创建表示例(txt表名.Text, client);

            #endregion


            当前所有表(client);


            #endregion


        }
        catch (Exception ex)
        {
            lab创建表信息状态.Text = "创建失败..";
            lab创建表信息状态.Text += ex;
        }

        finally
        {
            if (transport != null) { transport.Close(); }
        }


    }

    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void 删除表_Click(object sender, EventArgs e)
    {
        TTransport transport = null;

        try
        {
            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();

            client.deleteTable(txt表名.Text.ToUTF8Bytes());

            lab创建表信息状态.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;删除成功..";
        }
        catch (Exception ex)
        {
            lab创建表信息状态.Text = "删除表失败";
            lab创建表信息状态.Text += ex;
        }

        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }

    protected void 获取表_Click(object sender, EventArgs e)
    {
        TTransport transport = null;

        try
        {
            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();

            当前所有表(client);


        }
        catch (Exception ex)
        {
            ;
            lab创建表信息状态.Text += ex;
        }

        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }
    /// <summary>
    /// 当前数据库所有表
    /// </summary>
    public void 当前所有表(Hbase.Client client)
    {
        gri当前数据库所有表.DataSource = client.getTableNames().Select(p => p.ToUTF8String());
        gri当前数据库所有表.DataBind();
    }
    /// <summary>
    /// 数据库表名,及指定key的示例
    /// </summary>
    /// <param name="tabname"></param>
    /// <param name="key"></param>
    public void 当前创建表示例(string tabname, Hbase.Client client)
    {

        string key = "示例数据_Key";
        var list = new List<Mutation>();
        foreach (var item in txt列名集合.Text.Split(','))
        {

            //创建列
            if (!string.IsNullOrEmpty(item))
            {


                string coln = item;
                if (!coln.Contains(":"))
                {
                    coln = item + ":" + "示例";
                }

                var col = new Mutation() { Column = coln.ToUTF8Bytes(), Value = ("abcdef" + item).ToUTF8Bytes() };

                list.Add(col);
            }
        }
        client.mutateRow(tabname.ToUTF8Bytes(), key.ToUTF8Bytes(), list, new Dictionary<byte[], byte[]>());


        //显示示例数据
        显示示例数据(tabname, key, client);

    }

    public void 显示示例数据(string tabname, string keyv, Hbase.Client client)
    {

        //遍历结果集
        List<string> sss = new List<string>();

        var id = client.scannerOpenWithScan(Encoding.UTF8.GetBytes(tabname), new TScan(), new Dictionary<byte[], byte[]>());
        List<TRowResult> reslut;
        //使用key获取值
        //reslut = client.getRow(Encoding.UTF8.GetBytes(tabname), Encoding.UTF8.GetBytes(keyv), null);

        reslut = client.scannerGet(id);



        foreach (var key in reslut)
        {
            sss.Add(string.Format("&nbsp;&nbsp;RowKey:\n{0}", Encoding.UTF8.GetString(key.Row)));
            //打印Qualifier和对应的Value
            foreach (var k in key.Columns)
            {
                sss.Add(string.Format("&nbsp;&nbsp;&nbsp;列名:" + "\n" + Encoding.UTF8.GetString(k.Key)));
                sss.Add(string.Format("&nbsp;&nbsp;&nbsp;值:" + Encoding.UTF8.GetString(k.Value.Value)));

            }
        }

        gri创建数据表示例数据.DataSource = sss;
        gri创建数据表示例数据.DataBind();
    }

    #endregion





    protected void 添加数据_Click(object sender, EventArgs e)
    {
        lab添加数据状态.Text = string.Empty;
        //请在步骤2输入表名
        if (string.IsNullOrEmpty(txt表名.Text))
        {
            lab添加数据状态.Text = "请在步骤2输入表名";

            return;
        }

        string table = txt表名.Text;

        TTransport transport = null;

        try
        {
            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();

            var id = client.scannerOpenWithScan(Encoding.UTF8.GetBytes(table), new TScan(), new Dictionary<byte[], byte[]>());
            List<TRowResult> reslut;
            reslut = client.scannerGet(id);

            List<string> cols = new List<string>();
            //创建表头
            foreach (var item in reslut)
            {
                //所有列
                foreach (var xaa in item.Columns)
                {
                    string col = xaa.Key.ToUTF8String();
                    cols.Add(col);
                }
            }


            foreach (var item in System.Linq.Enumerable.Range(1, 30))
            {
                List<Mutation> mus = new List<Mutation>();
                List<string> info = new List<string>();
                string skey = "_" + item;
                string strx = string.Empty;
                foreach (var col in cols)
                {

                    string scol = col;
                    string svla = (Guid.NewGuid().ToString());
                    mus.Add(new Mutation() { Column = scol.ToUTF8Bytes(), Value = svla.ToUTF8Bytes() });
                    strx = string.Format("col:{0},val:{1},key{2}", scol, svla, skey);
                    info.Add(strx);
                }
                client.mutateRow(table.ToUTF8Bytes(), skey.ToUTF8Bytes(), mus, new Dictionary<byte[], byte[]>());


                lab添加数据状态.Text += string.Format("添加tab:{0},数据{{ {1} }}<br/>", table, string.Join(",", info));
            }


        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }



    }

    protected void 获取数据_Click(object sender, EventArgs e)
    {
        lab获取数据.Text = string.Empty;
        //请在步骤2输入表名
        if (string.IsNullOrEmpty(txt表名.Text))
        {
            lab获取数据.Text = "请在步骤2输入表名";

            return;
        }

        string table = txt表名.Text;

        TTransport transport = null;

        try
        {
            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();

            byte[] tableName = table.ToUTF8Bytes();
            var scan = new TScan() { };
            //读取数据
            var id = client.scannerOpenWithScan(tableName, scan, new Dictionary<byte[], byte[]>());
            //返回100条数据
            var result2 = client.scannerGetList(id, 100);

            DataTable dt = new DataTable();


            #region 创建列

            dt.Columns.Add(new DataColumn()
            {
                ColumnName = "Rowid:Value",
            });
            //创建表头
            foreach (var item in result2)
            {
                //所有列
                foreach (var xaa in item.Columns)
                {
                    string col = xaa.Key.ToUTF8String();
                    //已经存在则跳过 
                    if (dt.Columns.Contains(col))
                    {
                        continue;
                    }
                    dt.Columns.Add(new DataColumn()
                    {
                        ColumnName = col,
                    });
                }
            }
            dt.Columns.Add(new DataColumn()
            {
                ColumnName = "SortedColumns:ColumnName",
            });

            dt.Columns.Add(new DataColumn()
            {
                ColumnName = "SortedColumns:Value",
            });

            dt.Columns.Add(new DataColumn()
            {
                ColumnName = "Rowid",
            });
            #endregion


            foreach (var item in result2)
            {
                //创建行
                var row = dt.NewRow();
                //循环列的所有值
                foreach (var xaa in item.Columns)
                {
                    string col = xaa.Key.ToUTF8String();
                    row[col] = string.Format("{0}", xaa.Value.Value.ToUTF8String());
                }
                //这里有null的东西
                if (item.SortedColumns != null)
                {
                    //所有排序列
                    foreach (var xaac in item.SortedColumns)
                    {
                        string col = xaac.ColumnName.ToUTF8String();

                        row["SortedColumns:ColumnName"] = string.Format("{0}", col);
                        row["SortedColumns:Value"] = string.Format("{0}", xaac.Cell.Value.ToUTF8String());
                    }
                }

                    row["Rowid:Value"] = item.Row.ToUTF8String();
                    dt.Rows.Add(row);
               
                
            }

            gri获取数据.DataSource = dt;
            gri获取数据.DataBind();
        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }


    #region 删除数据
    protected void 删除单元格数据_Click(object sender, EventArgs e)
    {
        lab删除数据.Text = string.Empty;
        //请在步骤2输入表名
        if (string.IsNullOrEmpty(txt表名.Text))
        {
            lab删除数据.Text = "请在步骤2输入表名";

            return;
        }
        //检查单元格是否已经输入
        if (string.IsNullOrEmpty(txt单元格列名.Text))
        {
            lab删除数据.Text = "请输入要删除的列名,如不知道,请点击步骤4获取数据";

            return;
        }

        //检查单元格是否已经输入
        if (string.IsNullOrEmpty(txt数据行key.Text))
        {
            lab删除数据.Text = "请输入要删除的数据行key,如不知道,请点击步骤4获取数据";

            return;
        }


        string table = txt表名.Text;

        TTransport transport = null;

        try
        {
            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();



            string rowkey = txt数据行key.Text; ;
            string scol = txt单元格列名.Text;

            Dictionary<byte[], byte[]> encodedAttributes = new Dictionary<byte[], byte[]>();
            //删除数据
            DeleteCells(table, rowkey, false, new List<string>() { scol }, null, client);

            string xxx = string.Format("删除单元格数据:{0},数据{{ RowKey:{1},列:{2} }}<br/>", table, rowkey, scol);

            lab删除数据.Text = xxx + "请点击步骤4获取数据,查看删除状态";

            获取数据_Click(sender, e);


        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }



    protected void 删除数据行_Click(object sender, EventArgs e)
    {
        lab删除数据.Text = string.Empty;
        //请在步骤2输入表名
        if (string.IsNullOrEmpty(txt表名.Text))
        {
            lab删除数据.Text = "请在步骤2输入表名";

            return;
        }
        //检查单元格是否已经输入
        if (string.IsNullOrEmpty(txt单元格列名.Text))
        {
            lab删除数据.Text = "请输入要删除的列名,如不知道,请点击步骤4获取数据";

            return;
        }

        //检查单元格是否已经输入
        if (string.IsNullOrEmpty(txt数据行key.Text))
        {
            lab删除数据.Text = "请输入要删除的数据行key,如不知道,请点击步骤4获取数据";

            return;
        }


        string table = txt表名.Text;

        TTransport transport = null;
        try
        {
            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();



            string rowkey = txt数据行key.Text; ;
            string scol = txt单元格列名.Text;
            byte[] tableName = table.ToUTF8Bytes();
            byte[] row = rowkey.ToUTF8Bytes();

            Dictionary<byte[], byte[]> encodedAttributes = new Dictionary<byte[], byte[]>();
            //删除整行数据
            client.deleteAllRow(tableName, row, encodedAttributes);

            string xxx = string.Format("删除整行数据:{0},数据{{ RowKey:{1},列:{2} }}<br/>", table, rowkey, scol);

            lab删除数据.Text = xxx + "请点击步骤4获取数据,查看删除状态";

            获取数据_Click(sender, e);



        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }


    /// <summary>
    /// 删除单元数据,封装方法
    /// </summary>
    /// <param name="table"></param>
    /// <param name="rowKey"></param>
    /// <param name="writeToWal"></param>
    /// <param name="columns"></param>
    /// <param name="attributes"></param>
    /// <param name="client"></param>
    public void DeleteCells(string table, string rowKey, bool writeToWal, List<string> columns, Dictionary<string, string> attributes, Hbase.Client client)
    {
        byte[] tableName = table.ToUTF8Bytes();
        byte[] row = rowKey.ToUTF8Bytes();


        Dictionary<byte[], byte[]> encodedAttributes = new Dictionary<byte[], byte[]>();
        if (attributes != null)
        {
            foreach (var item in attributes)
            {
                encodedAttributes.Add(item.Key.ToUTF8Bytes(), item.Value.ToUTF8Bytes());
            }
        }
        List<Mutation> mutations = new List<Mutation>();
        foreach (string column in columns)
        {
            Mutation mutation = new Mutation();
            mutation.IsDelete = true;
            mutation.WriteToWAL = writeToWal;
            mutation.Column = column.ToUTF8Bytes();
            mutations.Add(mutation);
        }
        client.mutateRow(tableName, row, mutations, encodedAttributes);
    }

    #endregion



    /// <summary>
    /// 一次加入多行数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void 一次加入多行数据_Click(object sender, EventArgs e)
    {
        //请在步骤2输入表名
        if (string.IsNullOrEmpty(txt表名.Text))
        {
            lab添加数据状态.Text = "请在步骤2输入表名";

            return;
        }

        string table = txt表名.Text;

        byte[] tableName = table.ToUTF8Bytes();


        TTransport transport = null;
        try
        {
            //192.168.2.111:60010
            //实例化Socket连接
            transport = new TSocket("192.168.2.111", 9090);
            //实例化一个协议对象
            TProtocol tProtocol = new TBinaryProtocol(transport);
            //实例化一个Hbase的Client对象
            var client = new Hbase.Client(tProtocol);
            //打开连接
            transport.Open();


            //获取所有列



            #region 具体功能代码
            //批量提交
            var bat = new List<BatchMutation>();
            //value：列数据集合 目前是单列 
            #region 生成批量提交的数据集合
            var mu = new Mutation
            {
                /////////////////////////////////////////////////////////注意这里的列名表里必须有.
                Column = System.Text.Encoding.UTF8.GetBytes("Data:Collections"),
                Value = Guid.NewGuid().ToByteArray()
            };
            string row;

            row = string.Format("{0:d15}{1}{2}", 99999, "ABCDEF", DateTime.Now.ToString("yyyyMMddHHmmssfff"));

            Dictionary<byte[], byte[]> encodedAttributes = new Dictionary<byte[], byte[]>();

            var batch = new BatchMutation
            {
                Row = System.Text.Encoding.UTF8.GetBytes(row),
                Mutations = new List<Mutation>() { mu }
            };
            #endregion
            //添加到集合中
            bat.Add(batch);

            client.mutateRow(tableName, bat[0].Row, bat[0].Mutations, encodedAttributes);
            client.mutateRows(tableName, bat, encodedAttributes);

            #endregion



        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }




    }


}
