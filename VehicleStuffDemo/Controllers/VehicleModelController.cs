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
using VehicleDataAccess.Helpers;
using VehicleDataAccess.Implementations;
using VehicleStuffDemo.ViewModels;

namespace VehicleStuffDemo.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelService _vehicleService;
        private readonly IMapper iMapper;

        public VehicleModelController()
        {
        }

        public VehicleModelController(IVehicleModelService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            iMapper = mapper;
        }

        // GET: VehicleModel
        public async Task<ActionResult> Index(string sortBy, string currentFilter, string searchString, int? page)
        {
            VehicleFilters filters = new VehicleFilters(searchString, currentFilter);
            VehicleSorting sorting = new VehicleSorting(sortBy);
            VehiclePaging paging = new VehiclePaging(page);

            var models = await _vehicleService.GetVehicleModelList(filters, sorting, paging);
            List<VehicleModelViewModel> modelsListDest = iMapper.Map<List<VehicleModelViewModel>>(models);
            var paginatedModelsList = new StaticPagedList<VehicleModelViewModel>(modelsListDest, paging.Page ?? 1, paging.ResultsPerPage, paging.TotalCount);

            UpdateView(ViewBag, filters, sorting, paging);

            return View(paginatedModelsList);
        }

        // GET: VehicleModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await _vehicleService.FindVehicleModel(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            var vehicleModelViewModel = iMapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(vehicleModelViewModel);
        }

        // GET: VehicleModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (!await _vehicleService.CreateVehicleModel(vehicleModel))
                return View(iMapper.Map<VehicleModelViewModel>(vehicleModel));

            return RedirectToAction("Index");
        }

        // GET: VehicleModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await _vehicleService.FindVehicleModel(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(iMapper.Map<VehicleModelViewModel>(vehicleModel));
        }

        // POST: VehicleModel/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVehicleModel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleModelToUpdate = await _vehicleService.FindVehicleModel(id);
            if (TryUpdateModel(vehicleModelToUpdate, "", new string[] { "MakeId", "Name", "Abrv" }))
            {
                try
                {
                    await _vehicleService.EditVehicleModel();
                    return RedirectToAction("Index");
                }
                catch (DataException dex)
                {
                    ModelState.AddModelError("ERR: ", dex);
                }
            }

            return View(iMapper.Map<VehicleModelViewModel>(vehicleModelToUpdate));
        }

        // GET: VehicleModel/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Failed to delete model. Please try again.";
            }
            VehicleModel vehicleModel = await _vehicleService.FindVehicleModel(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(iMapper.Map<VehicleModelViewModel>(vehicleModel));
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                VehicleModel vehicleModel = await _vehicleService.FindVehicleModel(id);
                await _vehicleService.DeleteVehicleModel(vehicleModel);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        private void UpdateView(dynamic ViewBag, VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            // current sort by - keep sorting between pages
            ViewBag.CurrentSort = sorting.SortBy;
            // sort by
            ViewBag.SortByName = String.IsNullOrEmpty(sorting.SortBy) ? "name_desc" : "";
            ViewBag.SortByAbrv = sorting.SortBy == "Abrv" ? "abrv_desc" : "Abrv";
            ViewBag.SortById = sorting.SortBy == "MakeId" ? "makeid_desc" : "MakeId";

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