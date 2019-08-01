using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dirLL例用示例 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ConnectionString = "";
//        ConnectionString =
//@"DRIVER=MapR Drill ODBC Driver;
//AdvancedProperties={CastAnyToVarchar=true;HandshakeTimeout=5;QueryTimeout=180;TimestampTZDisplayTimezone=local;ExcludedSchemas=sys,INFORMATION_SCHEMA;NumberOfPrefetchBuffers=5};
//Catalog=DRILL;
//Schema=;
//AuthenticationType=No Authentication;
//ConnectionType=Direct;
//Host=192.168.2.113;
//Port=31010;
//";
     // ConnectionString = "DRIVER=MapR Drill ODBC Driver;Catalog=DRILL;Schema=hbase;ConnectionType=Direct;Host=192.168.2.113;Port=31010";
        ConnectionString = "DRIVER =MapR Drill ODBC Driver; AdvancedProperties ={ CastAnyToVarchar = true; HandshakeTimeout = 5; QueryTimeout = 180; TimestampTZDisplayTimezone = local; ExcludedSchemas = sys,INFORMATION_SCHEMA; NumberOfPrefetchBuffers = 5}; Catalog = DRILL; Schema =; AuthenticationType = No Authentication; ConnectionType = ZooKeeper; ZKQuorum = dataNode04:2181,dataNode03: 2181,dataNode02: 2181,nameNode: 2181; ZKClusterID = drillbits1; ";

        System.Data.Odbc.OdbcConnection conn = new System.Data.Odbc.OdbcConnection(ConnectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "SELECT * FROM `hbase`.`tab` ";
        System.Data.Odbc.OdbcDataAdapter ad = new System.Data.Odbc.OdbcDataAdapter(cmd);
        DataSet myds = new DataSet();
        ad.Fill(myds);



    }
}