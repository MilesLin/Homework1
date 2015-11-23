using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.Models.Interface
{
    public interface I客戶聯絡人
    {
        int Id { get; set; }
        string 職稱 { get; set; }
        string 姓名 { get; set; }
        string 手機 { get; set; }
        string 電話 { get; set; }
    }
}
