using System;
using System.Linq;
using System.Collections.Generic;
using HomeWork1.Models.ViewModels;
	
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
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}