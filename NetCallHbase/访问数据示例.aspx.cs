using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Thrift.Protocol;
using Thrift.Transport;
using System.Text;
using System.Data;

public partial class 访问数据示例 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
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

            //判断 表是不是存在
            if (!client.getTableNames().Select(p => p.ToUTF8String()).Contains("abc"))
            {
                //创建数据表
                client.createTable("abc".ToUTF8Bytes(), new List<ColumnDescriptor>() {
                new ColumnDescriptor() { Name="你好中文" .ToUTF8Bytes(), BloomFilterVectorSize=30,},
                new ColumnDescriptor() { Name="aa" .ToUTF8Bytes(), BloomFilterVectorSize=30,}

                });

            }

            //向表里加数据
            #region 向表里加数据
            //代码错误.不会用
            //client.m("abc".ToUTF8Bytes(), new List<BatchMutation>() {
            //     new BatchMutation() { Mutations= new List<Mutation>() {
            //                        new Mutation() {  Column="你好中文".ToUTF8Bytes(), Value="我是,".ToUTF8Bytes() },
            //                        new Mutation() {  Column="aa".ToUTF8Bytes(), Value="我是,111".ToUTF8Bytes() },
            //                        new Mutation() {  Column="你好中文".ToUTF8Bytes(), Value="我是,Habse".ToUTF8Bytes() },

            //     }
            //     ,  Row = "aaaa_1".ToUTF8Bytes(),


            //      }

            //}, new Dictionary<byte[], byte[]>());
            client.mutateRow("abc".ToUTF8Bytes(), "aaaa_1".ToUTF8Bytes(), new List<Mutation>()
            {
                 new Mutation() { Column="你好中文:11".ToUTF8Bytes(), Value="abcdef".ToUTF8Bytes() }
            }, new Dictionary<byte[], byte[]>());

            #endregion


            //遍历结果集
            List<string> sss = new List<string>();

            //获取表名:
            foreach (var item in client.getTableNames())
            {
                var strs = System.Text.ASCIIEncoding.ASCII.GetString(item);
                sss.Add("表名:" + strs);
                //根据表名，RowKey名来获取结果集







                List<TRowResult> reslut = client.getRow(Encoding.UTF8.GetBytes(strs), Encoding.UTF8.GetBytes("aaaa_1"), null);

                foreach (var key in reslut)
                {
                    sss.Add(string.Format("&nbsp;&nbsp;RowKey:\n{0}", Encoding.UTF8.GetString(key.Row)));
                    //打印Qualifier和对应的Value
                    foreach (var k in key.Columns)
                    {
                        sss.Add(string.Format("&nbsp;&nbsp;&nbsp;Family:Qualifier:" + "\n" + Encoding.UTF8.GetString(k.Key)));
                        sss.Add(string.Format("&nbsp;&nbsp;&nbsp;Value:" + Encoding.UTF8.GetString(k.Value.Value)));

                    }

                }
            }
            GridView1.DataSource = sss;
            GridView1.DataBind();





        }

        catch (Exception exxx)

        {

            Response.Write(exxx);

        }

        finally

        {

            if (null != transport)

            {

                transport.Close();

            }

        }
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        string table = "abc";
        string key = "aaaK";

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

            foreach (var item in System.Linq.Enumerable.Range(1, 10))
            {
                string skey = (key + "_" + item);
                string scol = ("你好中文:" + item);
                string scol2 = ("你好中文:单元格2" + item);
                string svla = (Guid.NewGuid().ToString());
                client.mutateRow("abc".ToUTF8Bytes(), skey.ToUTF8Bytes(), new List<Mutation>()
            {
                 new Mutation() { Column=scol.ToUTF8Bytes(), Value=svla.ToUTF8Bytes() },
                 new  Mutation() { Column=scol2.ToUTF8Bytes(), Value=svla.ToUTF8Bytes() }
            }, new Dictionary<byte[], byte[]>());
                Response.Write(string.Format("添加tab:{0},数据{{ RowKey:{1} ,列{2},数据{3},列{4},数据{5} }}<br/>", table, skey, scol, svla, scol2, svla));
            }
        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }


    }

    /// <summary>
    /// 获取数据,列表行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button5_Click1(object sender, EventArgs e)
    {

        string table = "abc";
        string key = "aaaK";

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

            //遍历结果集
            List<string> sss = new List<string>();
            foreach (var item in System.Linq.Enumerable.Range(1, 10))
            {

                string skey = (key + "_" + item);
                string scol = ("你好中文:" + item);
                string svla = (Guid.NewGuid().ToString());
                ///////////////删除单元格数据
                byte[] tableName = table.ToUTF8Bytes();
                byte[] row = skey.ToUTF8Bytes();
                Dictionary<byte[], byte[]> encodedAttributes = new Dictionary<byte[], byte[]>();


                //获取表名:
                foreach (var itema in client.getTableNames())
                {
                    var strs = System.Text.ASCIIEncoding.ASCII.GetString(itema);
                    sss.Add("表名:" + strs);

                    //根据表名，RowKey名来获取结果集
                    List<TRowResult> reslut = client.getRow(Encoding.UTF8.GetBytes(strs), Encoding.UTF8.GetBytes(skey), null);
                    foreach (var keys in reslut)
                    {
                        sss.Add(string.Format("&nbsp;&nbsp;RowKey:\n{0}", Encoding.UTF8.GetString(keys.Row)));
                        //打印Qualifier和对应的Value
                        foreach (var k in keys.Columns)
                        {
                            sss.Add(string.Format("&nbsp;&nbsp;&nbsp;Family:Qualifier:" + "\n" + Encoding.UTF8.GetString(k.Key)));
                            sss.Add(string.Format("&nbsp;&nbsp;&nbsp;Value:" + Encoding.UTF8.GetString(k.Value.Value)));

                        }
                    }
                }


            }

            GridView1.DataSource = sss;
            GridView1.DataBind();
        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }


    /// <summary>
    /// 删除整行数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {

        string table = "abc";
        string key = "aaaK";

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

            foreach (var item in System.Linq.Enumerable.Range(1, 10))
            {

                string skey = (key + "_" + item);
                string scol = ("你好中文:" + item);
                string svla = (Guid.NewGuid().ToString());
                ///////////////删除整行数据
                byte[] tableName = table.ToUTF8Bytes();
                byte[] row = skey.ToUTF8Bytes();
                Dictionary<byte[], byte[]> encodedAttributes = new Dictionary<byte[], byte[]>();
                client.deleteAllRow(tableName, row, encodedAttributes);

                Response.Write(string.Format("删除整行数据:{0},数据{{ RowKey:{1} }}<br/>", table, skey));
            }
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

    /// <summary>
    /// 删除单元格数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button5_Click(object sender, EventArgs e)
    {
        string table = "abc";
        string key = "aaaK";

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

            foreach (var item in System.Linq.Enumerable.Range(1, 10))
            {

                string skey = (key + "_" + item);
                string scol = ("你好中文:" + item);
                string svla = (Guid.NewGuid().ToString());
                ///////////////删除单元格数据
                byte[] tableName = table.ToUTF8Bytes();
                byte[] row = skey.ToUTF8Bytes();
                Dictionary<byte[], byte[]> encodedAttributes = new Dictionary<byte[], byte[]>();

                client.mutateRow(tableName, row, new List<Mutation>() { new Mutation() { IsDelete = true, Column = scol.ToUTF8Bytes() } }, encodedAttributes);

                Response.Write(string.Format("删除单元格数据:{0},数据{{ RowKey:{1},列:{2} }}<br/>", table, skey, scol));
            }
        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }


    /// <summary>
    /// 用SCAN获取多行数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button6_Click(object sender, EventArgs e)
    {

        string table = "abc";
        string key = "aaaK";

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

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }

    }

    /// <summary>
    /// 用SCAN获取一行数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button7_Click(object sender, EventArgs e)
    {


        string table = "abc";

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
            ///////////////////////////////////////////获取单行数据
            var result2 = client.scannerGet(id);
    

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

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        finally
        {
            if (transport != null) { transport.Close(); }
        }
    }
}