using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeWork1.Models;
using HomeWork1.Models.ViewModels;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Omu.ValueInjecter;
using System.IO;
using HomeWork1.Models.Interface;
using HomeWork1.ActionFilters;

namespace HomeWork1.Controllers
{
    public class 客戶資料Controller : BaseController
    {
        public ActionResult MyLayout()
        {
            return View();
        }
        public ActionResult IndexSurface()
        {
            return View(this.Repo客戶總覽.All().ToList());
        }

        [TimerFilter]
        // GET: 客戶資料
        public ActionResult Index()
        {
            客戶資料VM 客戶資料 = new 客戶資料VM();
            客戶資料.客戶資料 = this.Repo客戶資料.All().ToList();
            return View(客戶資料);
        }

        [HttpPost]
        public ActionResult Index(客戶資料VM 客戶資料)
        {
            if (客戶資料.分類查詢 != 客戶分類查詢.全部)
            {
                客戶分類 分類 = (客戶分類)客戶資料.分類查詢;
                客戶資料.客戶資料 = this.Repo客戶資料.All().Where(x => x.客戶分類 == 分類).ToList();
            }
            else
            {
                客戶資料.客戶資料 = this.Repo客戶資料.All().ToList();
            }

            客戶資料 = this.Repo客戶資料.OrderBy(客戶資料);

            return View(客戶資料);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = this.Repo客戶資料.Where(x => x.Id == id).FirstOrDefault();

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        [HttpPost]
        public ActionResult Details(int? id, FormCollection form)
        {
            IList<客戶聯絡人> details = new List<客戶聯絡人>();

            //string [] includeProperties = {"Id","職稱","手機","電話"};

            if (TryUpdateModel<IList<客戶聯絡人>>(details, "data"))
            {
                foreach (var item in details)
                {
                    var dbItem = this.Repo客戶聯絡人.Where(x => x.Id == item.Id).FirstOrDefault();
                    dbItem.InjectFrom(item);
                }

                this.Repo客戶聯絡人.UnitOfWork.Commit();
            }

            客戶資料 客戶資料 = this.Repo客戶資料.Where(x => x.Id == id).FirstOrDefault();

            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                this.Repo客戶資料.Add(客戶資料);
                this.Repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = this.Repo客戶資料.Where(x => x.Id == id).FirstOrDefault();

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection from, int? id)
        {
            var data = this.Repo客戶資料.Where(x => x.Id == id).FirstOrDefault();

            string[] includePropertie = { "客戶名稱", "統一編號", "電話", "傳真", "地址", "Email", "客戶分類" };
            if (TryUpdateModel<客戶資料>(data, includePropertie))
            {
                this.Repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(data);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = this.Repo客戶資料.Where(x => x.Id == id).FirstOrDefault();

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = this.Repo客戶資料.Where(x => x.Id == id).FirstOrDefault();
            客戶資料.是否刪除 = true;
            this.Repo客戶資料.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult DownLoadExcel()
        {
            MemoryStream file = this.Repo客戶資料.GetAllDetailExcel();
            return File(file.GetBuffer(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "客戶資料明細.xlsx");

            // 為什麼第一個參數需要file.GetBuffer，如果第一個參數是file會出現 "無法存取關閉的資料流"
            //return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "客戶資料明細.xlsx");
        }

        [HandleError(ExceptionType = typeof(HttpException), View = "Error")]
        public ActionResult MyErrorHandle()
        {
            throw new HttpException("就是丟Exception就是了");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Repo客戶資料.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
