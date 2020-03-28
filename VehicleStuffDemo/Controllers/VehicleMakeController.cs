using AutoMapper;
using PagedList;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using VehicleDataAccess;
using VehicleDataAccess.Implementations;
using VehicleStuffDemo.ViewModels;
using VehicleDataAccess.Helpers;

namespace VehicleStuffDemo.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService _vehicleService;
        private readonly IMapper iMapper;

        public VehicleMakeController()
        {
        }

        public VehicleMakeController(IVehicleMakeService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            iMapper = mapper;
        }

        // GET: VehicleMake
        public async Task<ActionResult> Index(string sortBy, string currentFilter, string searchString, int? page)
        {
            VehicleMakeFilters filters = new VehicleMakeFilters(searchString, currentFilter);
            VehicleMakeSorting sorting = new VehicleMakeSorting(sortBy);
            VehicleMakePaging paging = new VehicleMakePaging(page);

            var vehicles = await _vehicleService.GetVehicleMakeListAsync(filters, sorting, paging);
            List<VehicleMakeViewModel> vehiclesListDest = iMapper.Map<List<VehicleMakeViewModel>>(vehicles);
            var paginatedVehiclesList = new StaticPagedList<VehicleMakeViewModel>(vehiclesListDest, paging.Page ?? 1, paging.ResultsPerPage, paging.TotalCount);

            UpdateView(ViewBag, filters, sorting, paging);

            return View(paginatedVehiclesList);
        }

        // GET: VehicleMake/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await _vehicleService.FindVehicleMakeAsync(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            var vehicleMakeViewModel = iMapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeViewModel);
        }

        // GET: VehicleMake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Abrv")] VehicleMake vehicleMake)
        {
            if (!await _vehicleService.CreateVehicle(vehicleMake))
                return View(iMapper.Map<VehicleMakeViewModel>(vehicleMake));

            return RedirectToAction("Index");
        }

        // GET: VehicleMake/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await _vehicleService.FindVehicleMakeAsync(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            var vehicleMakeViewModel = iMapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeViewModel);
        }

        // POST: VehicleMake/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVehicle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleToUpdate = await _vehicleService.FindVehicleMakeAsync(id);

            if (TryUpdateModel(vehicleToUpdate, "", new string[] { "Name", "Abrv" }))
            {
                try
                {
                    await _vehicleService.EditVehicle();
                    return RedirectToAction("Index");
                }
                catch (DataException dex)
                {
                    ModelState.AddModelError("ERR: ", dex);
                }
            }

            return View(iMapper.Map<VehicleMakeViewModel>(vehicleToUpdate));
        }

        // GET: VehicleMake/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Failed to delete item. Please try again.";
            }

            VehicleMake vehicleMake = await _vehicleService.FindVehicleMakeAsync(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            var vehicleMakeViewModel = iMapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeViewModel);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                VehicleMake vehicleMake = await _vehicleService.FindVehicleMakeAsync(id);
                await _vehicleService.DeleteVehicle(vehicleMake);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        // Index methods
        private void UpdateView(dynamic ViewBag, VehicleMakeFilters filters, VehicleMakeSorting sorting, VehicleMakePaging paging)
        {
            ViewBag.CurrentSort = sorting.SortBy;
            ViewBag.SortByName = sorting.SortByName;
            ViewBag.SortByAbrv = sorting.SortByAbrv;

            // paging - if searchString is updated, return to page 1
            if (filters.SearchString != null)
            {
                paging.Page = 1;
            }
            else // else keep the filter
            {
                filters.SearchString = filters.CurrentFilter;
            }
            // current filter - keeps filter between pages
            ViewBag.CurrentFilter = filters.SearchString;
        }
    }
}