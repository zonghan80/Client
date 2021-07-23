using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Client.Models;

namespace Client.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        private ClientDataEntities db = new ClientDataEntities();

        // GET: 客戶聯絡人
        public ActionResult Index(string sortOrder, string searchString)
        {
            var 客客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_desc" : "Phone";
            ViewBag.AllNameSortParm = sortOrder == "AllName" ? "AllName_desc" : "AllName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";
            ViewBag.MobileSortParm = sortOrder == "Mobile" ? "Mobile_desc" : "Mobile";

            var 客戶聯絡人 = from s in db.客戶聯絡人
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                客戶聯絡人 = 客戶聯絡人.Where(s => s.客戶資料.客戶名稱.Contains(searchString) || s.電話.Contains(searchString) || s.姓名.Contains(searchString)
                                       || s.手機.Contains(searchString) || s.職稱.Contains(searchString) || s.Email.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                case "Phone":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.電話);
                    break;
                case "Phone_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.電話);
                    break;
                case "AllName":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.姓名);
                    break;
                case "AllName_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.姓名);
                    break;
                case "Mobile":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.手機);
                    break;
                case "Mobile_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.手機);
                    break;
                case "Title":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.職稱);
                    break;
                case "Title_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.職稱);
                    break;
                case "Email":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.Email);
                    break;
                default:
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
            }
            return View(客戶聯絡人.ToList());
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            //var 客戶聯絡人資 = db.客戶聯絡人.Where(s => s.客戶資料.客戶名稱 != 客戶聯絡人.客戶資料.客戶名稱 && s.Email != 客戶聯絡人.Email).Count();

            //if (客戶聯絡人資 > 0)
            //{
            //    return HttpNotFound();
            //}

            if (ModelState.IsValid)
            {
                var ClientCall =
                    from cc in db.客戶聯絡人
                    where cc.客戶Id == 客戶聯絡人.客戶Id
                    select cc;
                if (ClientCall.Where(
                    e => e.Email == 客戶聯絡人.Email).Count() == 0)
                {
                    db.客戶聯絡人.Add(客戶聯絡人);
                    db.SaveChanges();
                }
                else
                {
                    TempData["ErrorAlert"] = "此Email已存在!";
                }
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var ClientCall =
                  from c in db.客戶聯絡人
                  where c.客戶Id == 客戶聯絡人.客戶Id
                  select c;
                if (ClientCall.Where(
                    e => e.Email == 客戶聯絡人.Email).Count() == 0)
                {
                    db.Entry(客戶聯絡人).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    TempData["ErrorAlert"] = "此Email已存在!";
                }
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人.IsDelete = true;
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
