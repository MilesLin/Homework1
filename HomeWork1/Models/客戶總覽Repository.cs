using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWork1.Models
{   
	public  class 客戶總覽Repository : EFRepository<客戶總覽>, I客戶總覽Repository
	{

	}

	public  interface I客戶總覽Repository : IRepository<客戶總覽>
	{

	}
}