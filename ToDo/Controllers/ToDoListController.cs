using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using ToDo.ViewModels;

namespace ToDo.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly ToDoContext _context;

        public ToDoListController(ToDoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var toDoList = await _context.ToDoLists.ToListAsync();

            IEnumerable<ToDoListVM> vms = (from t in toDoList select
                                          new ToDoListVM()
                                          {
                                              Id = t.Id,
                                              Content = t.Content,
                                              UserID = t.UserID,
                                              UserList = new SelectList(_context.Users.ToList(), "UserID", "UserName"),
                                              IsComplete = t.IsComplete
                                              
                                          }).ToList();
                
            
            

            return View(vms);
        }

        public IActionResult Create() 
        {
            var userList = new SelectList(_context.Users.ToList(), "UserID", "UserName");
            ViewBag.UserList = userList; 

            return View();
        }


        //Post /ToDoList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoList item)
        {
            if (ModelState.IsValid)
            {


                _context.Add(item);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The item has been added!";

                return RedirectToAction("Index");

            }

            return View(item);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var tdl = (from t in _context.ToDoLists.Where(t => t.Id == id)
                       select new ToDoListVM()
                       {
                           Id = t.Id,
                           Content = t.Content,
                           UserID = t.UserID,
                           UserList = new SelectList(_context.Users.ToList(), "UserID", "UserName"),
                           IsComplete = t.IsComplete
                       });

            ToDoListVM item = await tdl.FirstOrDefaultAsync();

            if (item == null) {
                return NotFound();
            }

            return View(item);

        }

        //Post /ToDoList/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(ToDoListVM item)
        {
            if (ModelState.IsValid) 
            {
                ToDoList tdl = await _context.ToDoLists.Where(t => t.Id == item.Id).FirstOrDefaultAsync();

                if (tdl == null) {
                    TempData["Error"] = "Item was not found.";
                    return View(item);
                }

                tdl.Content = item.Content;
                tdl.UserID = item.UserID;
                tdl.IsComplete = item.IsComplete;

                _context.Update(tdl);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The item has been updated!";

                return RedirectToAction("Index");
            }

            return View(item);

        
        
        }

        public async Task<IActionResult> Delete(int id)
        {

            ToDoList item = await _context.ToDoLists.FindAsync(id);

            if (item == null)
            {
                TempData["Success"] = "The item does not exist";
            }
            else
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The item has been deleted.";

            }
            return RedirectToAction("Index");


        }

    }
}
