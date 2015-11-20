using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork1.Models.ViewModels
{
    public class 客戶資料VM
    {
        public List<客戶資料> 客戶資料 { get; set; }

        public 客戶分類查詢 分類查詢 { get; set; }

        public 排序方式 排序方式 { get; set; }

        public 客戶資料排序欄位 排序欄位 { get; set; }

    }

    public enum 客戶分類查詢 : byte
    {
        全部 = 9,
        一般客戶 = 0,
        VIP客戶 = 1,
        員工親屬 = 2
    }
}