using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Client.Models;

namespace Client.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        private ClientDataEntities db = new ClientDataEntities();

        // GET: 客戶銀行資訊
        public ActionResult Index(string sortOrder, string searchString)
        {
            var 客客戶銀行資訊 = db.客戶銀行資訊.Include(客 => 客.客戶資料);
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.BankNameSortParm = sortOrder == "BankName" ? "BankName_desc" : "BankName";
            ViewBag.BankNumSortParm = sortOrder == "BankNum" ? "BankNum_desc" : "BankNum";
            ViewBag.ABankNumSortParm = sortOrder == "ABankNum" ? "ABankNum_desc" : "ABankNum";
            ViewBag.AccNameSortParm = sortOrder == "AccName" ? "AccName_desc" : "AccName";
            ViewBag.AccNumSortParm = sortOrder == "AccNum" ? "AccNum_desc" : "AccNum";

            var 客戶銀行資訊 = from s in db.客戶銀行資訊
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                客戶銀行資訊 = 客戶銀行資訊.Where(s => s.銀行名稱.Contains(searchString) || s.客戶資料.客戶名稱.Contains(searchString) || s.銀行代碼.ToString().Contains(searchString)
                                       || s.帳戶號碼.Contains(searchString) || s.帳戶名稱.Contains(searchString) || s.分行代碼.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                case "BankName":
                    客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.銀行名稱);
                    break;
                case "BankName_desc":
                    客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.銀行名稱);
                    break;
                case "BankNum":
                    客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.銀行代碼);
                    break;
                case "BankNum_desc":
                    客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.銀行代碼);
                    break;
                case "AccNum":
                    客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.帳戶號碼);
                    break;
                case "AccNum_desc":
                    客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.帳戶號碼);
                    break;
                case "AccName":
                    客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.帳戶名稱);
                    break;
                case "AccName_desc":
                    客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.帳戶名稱);
                    break;
                case "ABankNum":
                    客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.分行代碼);
                    break;
                case "ABankNum_desc":
                    客戶銀行資訊 = 客戶銀行資訊.OrderByDescending(s => s.分行代碼);
                    break;
                default:
                    客戶銀行資訊 = 客戶銀行資訊.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
            }
            return View(客戶銀行資訊.ToList());
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(客戶銀行資訊);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶銀行資訊).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            客戶銀行資訊.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
