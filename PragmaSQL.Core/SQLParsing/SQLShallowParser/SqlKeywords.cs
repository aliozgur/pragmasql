/********************************************************************
  Class      : SqlKeywords
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public static class SqlKeywords
  {
    private static string _commaSperatedDataTypes = "sql_variant,datetime,smalldatetime,float,real,decimal"
      + ",money,smallmoney,bigint,int,smallint,tinyint,bit,ntext,text,image,timestamp"
      + ",uniqueidentifier,nvarchar,nchar,varchar,char,varbinary,binary";

    private static string _commaSperatedReservedWords = "add,except,percent,all,exec,plan,alter,execute,precision"
      + ",and,exists,primary,any,exit,print,as,fetch,proc,asc,file,procedure,authorization,fillfactor,public"
      + ",backup,for,raiserror,begin,foreign,read,between,freetext,readtext,break,freetexttable,reconfigure"
      + ",browse,from,references,bulk,full,replication,by,function,restore,cascade,goto,restrict,case,grant"
      + ",return,check,group,revokecheckpoint,having,right,close,holdlock,rollback,clustered,identity,rowcount"
      + ",coalesce,identity_insert,rowguidcol,collate,identitycol,rule,column,if,save,commit,in,schema"
      + ",compute,index,select,constraint,inner,session_user,contains,insert,set,containstable,intersect,setuser"
      + ",continue,into,shutdown,convert,is,some,create,join,statistics,cross,key,system_user,current,kill,table"
      + ",current_date,left,textsize,current_time,like,then,ties,current_timestamp,lineno,to,current_user"
      + ",load,top,cursor,national,tran,database,nocheck,transaction,dbcc,nonclustered,trigger,deallocate"
      + ",not,truncate,declare,null,tsequal,default,nullif,union,delete,of,unique,deny,off,update,desc,offsets"
      + ",updatetext,disk,on,use,distinct,open,user,distributed,opendatasource,values,double,openquery,varying"
      + ",drop,openrowset,view,dummy,openxml,waitfor,dump,option,when,else,or,where,end,order,while,errlvl"
      + ",outer,with,escape,over,writetext";



    private static string _commaSeperatedSelectEnd = "insert,update,delete,create,alter"
      + ",drop,truncate,exec,execute,if,while,declare,set,go,order,group"
      + ",raiserror,goto,exit,print,fetch,begin,with,union,return,else,end,open,fetch,;";

    private static IList<string> _dataTypes = new List<string>(_commaSperatedDataTypes.Split(','));
    public static IList<string> DataTypes
    {
      get { return _dataTypes; }
    }

    private static IList<string> _reservedWords = new List<string>(_commaSperatedReservedWords.Split(','));
    public static IList<string> Keywords
    {
      get { return _reservedWords; }
    }

    private static IList<string> _selectEnds = new List<string>(_commaSeperatedSelectEnd.Split(','));
    public static IList<string> SelectEnds
    {
      get { return _selectEnds; }
    }
  }
}
