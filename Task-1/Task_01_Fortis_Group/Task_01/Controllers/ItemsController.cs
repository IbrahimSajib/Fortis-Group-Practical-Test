using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_01.Models;

namespace Task_01.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemDbContext db;

        public ItemsController(ItemDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var itemDbContext = db.Items.Include(i => i.Category);
            return View(await itemDbContext.ToListAsync());
        }


        public IActionResult Insert()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return PartialView("_insert");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert([Bind("ItemName,CategoryId,Status")] ItemVM model)
        {
            if (ModelState.IsValid)
            {
                Item item = new Item()
                {
                    ItemName = model.ItemName,
                    CategoryId = model.CategoryId,
                    Status = model.Status
                };
                await db.Items.AddAsync(item);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return PartialView("_insert", model);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ItemVM itemVM = new ItemVM()
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                CategoryId = item.CategoryId,
                Status = item.Status
            };
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", itemVM.CategoryId);
            return PartialView("_update", itemVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("ItemId,ItemName,CategoryId,Status")] ItemVM model)
        {
            if (ModelState.IsValid)
            {
                Item item = new Item()
                {
                    ItemId = model.ItemId,
                    ItemName = model.ItemName,
                    CategoryId = model.CategoryId,
                    Status = model.Status
                };
                db.Items.Update(item);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return PartialView("_update", model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await db.Items.FindAsync(id);
            if (item != null)
            {
                db.Items.Remove(item);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
