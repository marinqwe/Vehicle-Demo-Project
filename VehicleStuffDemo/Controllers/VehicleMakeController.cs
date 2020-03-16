using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using VehicleDataAccess;
using VehicleDataAccess.Implementations;
using VehicleStuffDemo.ViewModels;

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
            SortVehicleMake(ViewBag, sortBy, currentFilter, searchString, page);
            List<VehicleMake> vehicles = await GetVehiclesAsync(sortBy, searchString);
            List<VehicleMakeViewModel> vehiclesListDest = iMapper.Map<List<VehicleMake>, List<VehicleMakeViewModel>>(vehicles);
            var paginatedVehicleList = ConvertToPaginatedResults(vehiclesListDest, page);

            return View(paginatedVehicleList);
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
            var vehicleMakeViewModel = iMapper.Map<VehicleMake, VehicleMakeViewModel>(vehicleMake);
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
                return View(iMapper.Map<VehicleMake, VehicleMakeViewModel>(vehicleMake));

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
            var vehicleMakeViewModel = iMapper.Map<VehicleMake, VehicleMakeViewModel>(vehicleMake);
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

            return View(iMapper.Map<VehicleMake, VehicleMakeViewModel>(vehicleToUpdate));
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
            var vehicleMakeViewModel = iMapper.Map<VehicleMake, VehicleMakeViewModel>(vehicleMake);
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
        private void SortVehicleMake(dynamic ViewBag, string sortBy, string currentFilter, string searchString, int? page)
        {
            // current sort by - keep sorting between pages
            ViewBag.CurrentSort = sortBy;
            // sort by
            ViewBag.SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            ViewBag.SortByAbrv = sortBy == "Abrv" ? "abrv_desc" : "Abrv";

            // paging - if searchString is updated, return to page 1
            if (searchString != null)
            {
                page = 1;
            }
            else // else keep the filter
            {
                searchString = currentFilter;
            }
            // current filter - keeps filter between pages
            ViewBag.CurrentFilter = searchString;
        }

        private async Task<List<VehicleMake>> GetVehiclesAsync(string sortBy, string searchString)
        {
            var vehicles = await _vehicleService.GetVehicleMakeListAsync();
            // filter/find
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }

            // sort
            switch (sortBy)
            {
                case "name_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Name);
                    break;

                case "Abrv":
                    vehicles = vehicles.OrderBy(v => v.Abrv);
                    break;

                case "abrv_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Abrv);
                    break;

                default: // sort by name
                    vehicles = vehicles.OrderBy(v => v.Name);
                    break;
            }
            return vehicles.ToList();
        }

        private IPagedList<VehicleMakeViewModel> ConvertToPaginatedResults(List<VehicleMakeViewModel> vehicleMakeViewModelList, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return vehicleMakeViewModelList.ToPagedList(pageNumber, pageSize);
        }
    }
}