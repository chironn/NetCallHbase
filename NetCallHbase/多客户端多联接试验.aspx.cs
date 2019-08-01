using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Thrift.Protocol;
using Thrift.Transport;

public partial class 多客户端多联接试验 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        测试多个联接(4);

        //打开4个TCP联接成功
        System.Threading.Thread.Sleep(50000);

        foreach (var item in conns)
        {
            if (item.Value != null) { }
            item.Value.Close();
            item.Value.Dispose();
        }

    }

    /// <summary>
    /// 联接集合
    /// </summary>
    Dictionary<int, TTransport> conns = new Dictionary<int, TTransport>(); 

    void 测试多个联接( int num)
    {
        TTransport transport = null;
        for (int i = 0; i < num; i++)
        {
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

                //添加到集合
                conns.Add(i, transport);
                //Label1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联接成功..";
            }
            catch (Exception ex)
            {
                // Label1.Text = "联接失败..";
                //Label1.Text += ex;
            }

            finally
            {
                // if (transport != null) { transport.Close(); }
            }

        }

    }
}