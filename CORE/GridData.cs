using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaMEF
{
    public class GridData
    {
        public void Create_FILE_DT(string TableName)
        {
            if (GlobalValue.MasterDS.Tables[TableName] == null)
            {
                //FILE_INFO 테이블 생성
                DataTable fileDT = GlobalValue.MasterDS.Tables.Add(TableName);
                fileDT.Columns.AddRange(new DataColumn[]
                {
                new DataColumn { ColumnName = "ROW_ID",        Caption = "순서",     DataType = typeof(int)},
                new DataColumn { ColumnName = "FILE_NAME",     Caption = "Name",     DataType = typeof(string)},
                new DataColumn { ColumnName = "FILE_PATH",     Caption = "Path",     DataType = typeof(string)},
                new DataColumn { ColumnName = "FILE_SIZE",     Caption = "Size",     DataType = typeof(ulong)},
                new DataColumn { ColumnName = "FILE_EXT",     Caption = "Extension",     DataType = typeof(string)},
                new DataColumn { ColumnName = "FILE_C_TIME",     Caption = "Created Time",     DataType = typeof(DateTime)},
                new DataColumn { ColumnName = "FILE_A_TIME",     Caption = "Accessed Time",     DataType = typeof(DateTime)}
                });
                fileDT.PrimaryKey = new DataColumn[] { fileDT.Columns["ROW_ID"] }; //기본키 설정
            }
            
        }
    }
}