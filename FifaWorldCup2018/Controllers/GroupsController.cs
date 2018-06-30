using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FifaWorldCup2018.DAL;
using FifaWorldCup2018.Models;

namespace FifaWorldCup2018.Controllers
{
    public class GroupsController : Controller
    {
        private IGroupRepository groupRepository;

        public GroupsController()
        {
            this.groupRepository = new GroupRepository(new FifaDbContext());
        }

        public GroupsController(IGroupRepository groupRepository) {
            this.groupRepository = groupRepository;
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var students = from s in groupRepository.GetGroups()
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students);
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            Group student = groupRepository.GetStudentByID(id);
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Student/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
           [Bind(Include = "Name")]
           Group group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    groupRepository.InsertGroup(group);
                    groupRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(group);
        }

        //
        // GET: /Student/Edit/5

        public ActionResult Edit(int id)
        {
            Group group = groupRepository.GetStudentByID(id);
            return View(group);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
           [Bind(Include = "Name")]
         Group group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    groupRepository.UpdateGroup(group);
                    groupRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }
            return View(group);
        }

        //
        // GET: /Student/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Group student = groupRepository.GetStudentByID(id);
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Group student = groupRepository.GetStudentByID(id);
                groupRepository.DeleteGroup(id);
                groupRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            groupRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}