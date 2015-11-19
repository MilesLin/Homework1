using HomeWork1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork1.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Repo客戶資料 = RepositoryHelper.Get客戶資料Repository();
            this.Repo客戶銀行資訊 = RepositoryHelper.Get客戶銀行資訊Repository();
            this.Repo客戶總覽 = RepositoryHelper.Get客戶總覽Repository();
            this.Repo客戶聯絡人 = RepositoryHelper.Get客戶聯絡人Repository();
        }

        public 客戶資料Repository Repo客戶資料 { get; private set; }

        public 客戶銀行資訊Repository Repo客戶銀行資訊 { get; private set; }

        public 客戶總覽Repository Repo客戶總覽 { get; private set; }

        public 客戶聯絡人Repository Repo客戶聯絡人 { get; private set; }

    }
}