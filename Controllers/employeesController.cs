using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class employeesController : Controller
    {
        private EmpoyeesDBEntities1 db = new EmpoyeesDBEntities1();

        // GET: employees
        public ActionResult Index()
        {
            return View(db.employees.ToList());
        }

        // GET: employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: employees/Create
        public ActionResult Create(int id = 0)
        {
            employee emp = new employee();
            var lastemployee = db.employees.OrderByDescending(c => c.ID).FirstOrDefault();

            if (id != 0)
            {
                emp = db.employees.Where(x => x.ID == id).FirstOrDefault<employee>();
            }
            else if (lastemployee == null)
            {
                emp.EmployeeNo = "SD0001";
            }
            else
            {
                emp.EmployeeNo = "SD0" + (Convert.ToInt32(lastemployee.EmployeeNo.Substring(5, lastemployee.EmployeeNo.Length - 5)) + 1).ToString("D3");
            }
            return View(emp);

        }

        // POST: employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "ID,EmployeeNo,FirstName,LastName,BirthDate,ContactNo,EmailAddress")] employee employee)
        {
            if (ModelState.IsValid)
            {
               

                db.employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            return View(employee);
        }

        // GET: employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeNo,FirstName,LastName,BirthDate,ContactNo,EmailAddress")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employee employee = db.employees.Find(id);
            db.employees.Remove(employee);
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
