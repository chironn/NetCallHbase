using HbaseThrift.HBase.Thrift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 试用aspx : System.Web.UI.Page
{
    /// <summary>
    /// 客户端
    /// </summary>
    HBaseThriftClient client = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //sss(5000);
        //sss();
        //sss();
      


        client = new HBaseThriftClient("192.168.2.111", 9090);
        try
        {
            client.Open();


            //CaseForUpdate(); // insert or update rows/cells
            //CaseForDeleteCells(); // delete cells
            //CaseForDeleteRow(); // delete rows
            // CaseForScan(); // scan rows
            // CaseForGet(); // get rows


            //所有数据表
            GridView1.DataSource = client.GetTables();
            GridView1.DataBind();


            // List<TRowResult> reslut = client.GetRows("TableName", new List<string>() { "Rows.key" }, new Dictionary<string, string>());

            //所有的表
            foreach (var item in client.GetTables())
            {
                //数据表的表
                foreach (var aa in client.GetRows(item, new List<string>() { "1", "2", "test" }, new Dictionary<string, string>()))
                {

                    Response.Write(aa);
                }


            }
        }
        finally
        {
            //关闭
            client.Close();
        }


    }


    ManualResetEvent wait = new ManualResetEvent(false);
    //测试线程的阻塞
    void sss(int xx = 16000)
    {

        #region 充电状态错误数据提交
        try
        {
            //创建新线程进行数据处理
            var thr = new System.Threading.Thread((cobj) =>
            {
                try
                {
                    Thread.Sleep(xx);
                }
                catch (Exception ex)
                {

                    return;
                }
                wait.Set();
            });

            //开始线程
            thr.Start();
            if (!wait.WaitOne(15 * 1000))
            {         
                thr.Abort();
            }
        }
        finally
        {
            //设置为无信号量状态
             wait.Reset();
        }


        #endregion
    }

    #region 示例代码
    static String RandomlyBirthday()
    {
        Random r = new Random();
        int year = 1900 + r.Next(100);
        int month = 1 + r.Next(12);
        int date = 1 + r.Next(30);
        return year + "-" + month.ToString().PadLeft(2, '0') + "-" + date.ToString().PadLeft(2, '0');
    }

    static String RandomlyGender()
    {
        Random r = new Random();
        int flag = r.Next(2);
        return flag == 0 ? "M" : "F";
    }

    static String RandomlyUserType()
    {
        Random r = new Random();
        int flag = 1 + r.Next(10);
        return flag.ToString();
    }


    public void CaseForUpdate()
    {
        bool writeToWal = false;
        Dictionary<String, String> attributes = new Dictionary<String, String>(0);
        string table = SetTable();
        // put kv pairs
        for (int i = 0; i < 10000000; i++)
        {
            string rowKey = i.ToString().PadLeft(4, '0');
            Dictionary<String, String> fieldNameValues = new Dictionary<String, String>();
            fieldNameValues.Add("info:birthday", RandomlyBirthday());
            fieldNameValues.Add("info:user_type", RandomlyUserType());
            fieldNameValues.Add("info:gender", RandomlyGender());
            client.Update(table, rowKey, writeToWal, fieldNameValues, attributes);
        }
    }

    public void CaseForDeleteCells()
    {
        bool writeToWal = false;
        Dictionary<String, String> attributes = new Dictionary<String, String>(0);
        String table = SetTable();
        // put kv pairs
        for (long i = 5; i < 10; i++)
        {
            String rowKey = i.ToString().PadLeft(4, '0');
            List<String> columns = new List<String>(0);
            columns.Add("info:birthday");
            client.DeleteCells(table, rowKey, writeToWal, columns, attributes);
        }
    }

    public void CaseForDeleteRow()
    {
        Dictionary<String, String> attributes = new Dictionary<String, String>(0);
        String table = SetTable();
        // delete rows
        for (long i = 5; i < 10; i++)
        {
            String rowKey = i.ToString().PadLeft(4, '0');
            client.DeleteRow(table, rowKey, attributes);
        }
    }

    public void CaseForScan()
    {
        Dictionary<String, String> attributes = new Dictionary<String, String>(0);
        String table = SetTable();
        String startRow = "0005";
        String stopRow = "0015";
        List<String> columns = new List<String>(0);
        columns.Add("info:birthday");
        int id = client.ScannerOpen(table, startRow, stopRow, columns, attributes);
        int nbRows = 2;
        List<TRowResult> results = client.ScannerGetList(id, nbRows);
        while (results != null)
        {
            foreach (TRowResult result in results)
            {
                client.IterateResults(result);
            }
            results = client.ScannerGetList(id, nbRows);
        }
        client.ScannerClose(id);

        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(results));
    }

    public void CaseForGet()
    {
        Dictionary<String, String> attributes = new Dictionary<String, String>(0);
        String table = SetTable();
        List<String> rows = new List<String>(0);
        rows.Add("0009");
        rows.Add("0098");
        rows.Add("0999");
        List<String> columns = new List<String>(0);
        columns.Add("info:birthday");
        columns.Add("info:gender");
        List<TRowResult> results = client.GetRowsWithColumns(table, rows, columns, attributes);
        foreach (TRowResult result in results)
        {
            client.IterateResults(result);
        }
    }

    private string SetTable(string table = "testtable")
    {
        return table;
    }
    #endregion
}


