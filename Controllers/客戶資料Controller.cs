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
    public class 客戶資料Controller : Controller
    {
        private ClientDataEntities db = new ClientDataEntities();

        // GET: 客戶資料
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_desc" : "Phone";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "Address_desc" : "Address";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.TaxIDSortParm = sortOrder == "TaxID" ? "TaxID_desc" : "TaxID";
            ViewBag.FaxSortParm = sortOrder == "Fax" ? "Fax_desc" : "Fax";

            var 客戶資料 = from s in db.客戶資料
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                客戶資料 = 客戶資料.Where(s => s.客戶名稱.Contains(searchString) || s.電話.Contains(searchString) || s.地址.Contains(searchString)
                                       || s.傳真.Contains(searchString) || s.統一編號.Contains(searchString) || s.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.客戶名稱);
                    break;
                case "Phone":
                    客戶資料 = 客戶資料.OrderBy(s => s.電話);
                    break;
                case "Phone_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.電話);
                    break;
                case "Address":
                    客戶資料 = 客戶資料.OrderBy(s => s.地址);
                    break;
                case "Address_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.地址);
                    break;
                case "Fax":
                    客戶資料 = 客戶資料.OrderBy(s => s.傳真);
                    break;
                case "Fax_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.傳真);
                    break;
                case "TaxID":
                    客戶資料 = 客戶資料.OrderBy(s => s.統一編號);
                    break;
                case "TaxID_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.統一編號);
                    break;
                case "Email":
                    客戶資料 = 客戶資料.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.Email);
                    break;
                default:
                    客戶資料 = 客戶資料.OrderBy(s => s.客戶名稱);
                    break;
            }
            return View(客戶資料.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,IsDelete")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
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
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,IsDelete")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
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
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料.IsDelete = true;
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
