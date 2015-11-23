using System;
using System.Linq;
using System.Collections.Generic;
using HomeWork1.Models.ViewModels;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
	
namespace HomeWork1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(x => !x.是否刪除);
        }

        public 客戶資料VM OrderBy(客戶資料VM 客戶資料) 
        {
            switch (客戶資料.排序欄位)
            {
                case 客戶資料排序欄位.客戶名稱:

                    switch (客戶資料.排序方式)
	                {
                        case 排序方式.升冪:
                            客戶資料.客戶資料 = 客戶資料.客戶資料.OrderBy(x => x.客戶名稱).ToList();
                            break;
                        case 排序方式.降冪:
                            客戶資料.客戶資料 = 客戶資料.客戶資料.OrderByDescending(x => x.客戶名稱).ToList();
                            break;
	                }
    
                    break;
                case 客戶資料排序欄位.客戶分類:
                    switch (客戶資料.排序方式)
                    {
                        case 排序方式.升冪:
                            客戶資料.客戶資料 = 客戶資料.客戶資料.OrderBy(x => x.客戶分類).ToList();
                            break;
                        case 排序方式.降冪:
                            客戶資料.客戶資料 = 客戶資料.客戶資料.OrderByDescending(x => x.客戶分類).ToList();
                            break;
                    }
                    break;
            }

            return 客戶資料;
        }

        public MemoryStream GetAllDetailExcel()
        {
            IWorkbook wb = new XSSFWorkbook();
            ISheet ws = wb.CreateSheet();
            ws.CreateRow(0);
            ws.GetRow(0).CreateCell(0).SetCellValue("客戶分類");
            ws.GetRow(0).CreateCell(1).SetCellValue("客戶名稱");
            ws.GetRow(0).CreateCell(2).SetCellValue("統一編號");
            ws.GetRow(0).CreateCell(3).SetCellValue("電話");
            ws.GetRow(0).CreateCell(4).SetCellValue("傳真");
            ws.GetRow(0).CreateCell(5).SetCellValue("地址");
            ws.GetRow(0).CreateCell(6).SetCellValue("Email");
            int index = 1;
            foreach (var item in this.All())
            {
                ws.CreateRow(index);
                ws.GetRow(index).CreateCell(0).SetCellValue(item.客戶分類.ToString());
                ws.GetRow(index).CreateCell(1).SetCellValue(item.客戶名稱);
                ws.GetRow(index).CreateCell(2).SetCellValue(item.統一編號);
                ws.GetRow(index).CreateCell(3).SetCellValue(item.電話);
                ws.GetRow(index).CreateCell(4).SetCellValue(item.傳真);
                ws.GetRow(index).CreateCell(5).SetCellValue(item.地址);
                ws.GetRow(index).CreateCell(6).SetCellValue(item.Email);
                index++;
            }

            MemoryStream ms = new MemoryStream();
            wb.Write(ms);

            var file = ms.ToArray();
            var output = new MemoryStream();
            output.Write(file, 0, file.Length);
            output.Position = 0;
            
            // 為什麼無法直接回傳NPOI寫好的MemeroyStream，
            // 是因為wb.Write(ms)，應該會在裡面執行ms.Close()，導致無法取得關閉的串流
            //output.Close();
            
            return output;

            //return ms;
        } 
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}