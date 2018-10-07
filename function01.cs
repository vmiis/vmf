using System;
using System.IO;
using System.Xml;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
namespace VM{ public static partial class vmf{
//-----------------------------------------------------------------------------------------------------------
public static int parseInt(string n){
	int r=0;
	try{ r=int.Parse(n); }catch{}
	return r;
}
//-----------------------------------------------------------------------------------------------------------
public static double parseDouble(string n){
	double r=0;
	try{ r=double.Parse(n); }catch{}
	return r;
}
//-----------------------------------------------------------------------------------------------------------
public static void send_email(Dictionary<string, object> input,Dictionary<string, object> output,string user){
    output["bbb"]="abc";
}
//-----------------------------------------------------------------------------------------------------------
public static SqlParameter[] sql_input_parameters(Dictionary<string, object> input,string user){
	int i1=0; if(input.ContainsKey("i1")==true) i1=int.Parse((string)input["i1"]);
	int i2=0; if(input.ContainsKey("i2")==true) i2=int.Parse((string)input["i2"]);
	int i3=0; if(input.ContainsKey("i3")==true) i3=int.Parse((string)input["i3"]);
	int i4=0; if(input.ContainsKey("i4")==true) i4=int.Parse((string)input["i4"]);
	int i5=0; if(input.ContainsKey("i5")==true) i5=int.Parse((string)input["i5"]);
	int i6=0; if(input.ContainsKey("i6")==true) i6=int.Parse((string)input["i6"]);
	int i7=0; if(input.ContainsKey("i7")==true) i7=int.Parse((string)input["i7"]);

	double d1=0; if(input.ContainsKey("d1")==true) d1=double.Parse((string)input["d1"]);
	double d2=0; if(input.ContainsKey("d2")==true) d2=double.Parse((string)input["d2"]);
	double d3=0; if(input.ContainsKey("d3")==true) d3=double.Parse((string)input["d3"]);
	double d4=0; if(input.ContainsKey("d4")==true) d4=double.Parse((string)input["d4"]);
	double d5=0; if(input.ContainsKey("d5")==true) d5=double.Parse((string)input["d5"]);

	string s1=""; if(input.ContainsKey("s1")==true){ s1=(string)input["s1"];}
	string s2=""; if(input.ContainsKey("s2")==true){ s2=(string)input["s2"];}
	string s3=""; if(input.ContainsKey("s3")==true){ s3=(string)input["s3"];}
	string s4=""; if(input.ContainsKey("s4")==true){ s4=(string)input["s4"];}
	string s5=""; if(input.ContainsKey("s5")==true){ s5=(string)input["s5"];}

	DateTime t1=DateTime.Parse("01/01/1753"); if(input.ContainsKey("t1")==true) try{t1=DateTime.Parse((string)input["t1"]);}catch{}
	DateTime t2=DateTime.Parse("01/01/9999"); if(input.ContainsKey("t2")==true) try{t2=DateTime.Parse((string)input["t2"]);}catch{}

	int[] ii=new int[]{i1,i2,i3,i4,i5,i6,i7};
	double[] dd=new double[]{d1,d2,d3,d4,d5};
	string[] ss=new string[]{s1,s2,s3,s4,s5,user};
	DateTime[] tt=new DateTime[]{t1,t2};
	int iiLen=ii.Length;
	int ddLen=dd.Length;
	int ssLen=ss.Length;
	int ttLen=tt.Length;
	int NN=iiLen+ddLen+ssLen+ttLen;
	SqlParameter[] p=new SqlParameter[NN]; for(int i=0;i<p.Length;i++) p[i]=new SqlParameter();
	for(int i=0;i<iiLen;i++){ p[i].DbType=DbType.Int32;  						p[i].Value=ii[i];					}
	for(int i=0;i<ddLen;i++){ p[i+iiLen].DbType=DbType.Double; 					p[i+iiLen].Value=dd[i];				}
	for(int i=0;i<ssLen;i++){ p[i+iiLen+ddLen].DbType=DbType.String; 			p[i+iiLen+ddLen].Value=ss[i];		}
	for(int i=0;i<ttLen;i++){ p[i+iiLen+ddLen+ssLen].DbType=DbType.DateTime; 	p[i+iiLen+ddLen+ssLen].Value=tt[i];	}
	return p;
}
//-----------------------------------------------------------------------------------------------------------
public static object db_object(string cns,string sql,SqlParameter[] p){
	using(SqlConnection cnn=new SqlConnection(cns)){
		SqlCommand cmd=cnn.CreateCommand(); cmd.CommandText=sql;
		int iInt=1,iDouble=1,iS=1,iT=1;
		if(p!=null){
	   		for(int i=0;i<p.Length;i++){
	   			if(p[i].DbType==DbType.Int32){
		   			p[i].ParameterName="@I"+iInt;
	   				cmd.Parameters.Add(p[i]);
	   				iInt++;
	   			}
	   			if(p[i].DbType==DbType.Double){
		   			p[i].ParameterName="@D"+iDouble;
	   				cmd.Parameters.Add(p[i]);
	   				iDouble++;
	   			}
	   			if(p[i].DbType==DbType.String){
		   			p[i].ParameterName="@S"+iS;
	   				cmd.Parameters.Add(p[i]);
	   				iS++;
	   			}
	   			if(p[i].DbType==DbType.DateTime){
		   			p[i].ParameterName="@T"+iT;
	   				cmd.Parameters.Add(p[i]);
	   				iT++;
	   			}
			}
		}
		cnn.Open();	object r=cmd.ExecuteScalar(); cnn.Close();
		return r;
	}
}
//-----------------------------------------------------------------------------------------------------------
public static string table_owner(Dictionary<string, object> input, string user){
    string table=(string)input["sys_table"];
    string cns=(string)input["sys_cns"];
    string sql="select Author from "+table+" where pid=30 and uid=@I1";
    SqlParameter[] p=sql_input_parameters(input,user);
    string owner=(string)db_object(cns,sql,p);
    return owner;
}
//-----------------------------------------------------------------------------------------------------------
public static void test(Dictionary<string, object> input,Dictionary<string, object> output,string user){
    output["bbb"]="abc";
	//output["owner"]=table_owner(input,user);
}
//-----------------------------------------------------------------------------------------------------------
}}
