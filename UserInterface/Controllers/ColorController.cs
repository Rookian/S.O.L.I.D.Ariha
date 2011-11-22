using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserInterface.Models;

namespace UserInterface.Controllers
{

    public class Color : IDropdownList
    {
        public virtual int Id { get; set; }
        public virtual string Text { get; set; }
    }

    public class ColorController : Controller
    {
        //
        // GET: /Color/

        public ActionResult Index()
        {

            IEnumerable<Color> colors = new List<Color> { new Color { Id = 1, Text = "Blue" }, new Color { Id = 2, Text = "Green" } };
            return View(colors);

        }

        //
        // GET: /Color/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Color/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Color/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Color/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Color/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Color/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Color/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
