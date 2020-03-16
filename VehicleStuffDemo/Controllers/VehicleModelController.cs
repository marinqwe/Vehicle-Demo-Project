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
            SortVehicleModel(ViewBag, sortBy, currentFilter, searchString, page);
            List<VehicleModel> models = await GetVehicleModelsAsync(sortBy, searchString);
            List<VehicleModelViewModel> modelsListDest = iMapper.Map<List<VehicleModel>, List<VehicleModelViewModel>>(models);
            var paginatedModelsList = ConvertToPaginatedResults(modelsListDest, page);

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
            var vehicleModelViewModel = iMapper.Map<VehicleModel, VehicleModelViewModel>(vehicleModel);
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
                return View(iMapper.Map<VehicleModel, VehicleModelViewModel>(vehicleModel));

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
            return View(iMapper.Map<VehicleModel, VehicleModelViewModel>(vehicleModel));
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

            return View(iMapper.Map<VehicleModel, VehicleModelViewModel>(vehicleModelToUpdate));
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
            return View(iMapper.Map<VehicleModel, VehicleModelViewModel>(vehicleModel));
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

        // Index methods
        private void SortVehicleModel(dynamic ViewBag, string sortBy, string currentFilter, string searchString, int? page)
        {
            // current sort by - keep sorting between pages
            ViewBag.CurrentSort = sortBy;
            // sort by
            ViewBag.SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            ViewBag.SortByAbrv = sortBy == "Abrv" ? "abrv_desc" : "Abrv";
            ViewBag.SortById = sortBy == "MakeId" ? "makeid_desc" : "MakeId";

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

        private async Task<List<VehicleModel>> GetVehicleModelsAsync(string sortBy, string searchString)
        {
            var models = await _vehicleService.GetVehicleModelList();
            // filter/find
            if (!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(m => m.Name.Contains(searchString)
                                    || m.Abrv.Contains(searchString)
                                    || m.MakeId.ToString().Contains(searchString));
            }
            // sort
            switch (sortBy)
            {
                case "name_desc":
                    models = models.OrderByDescending(v => v.Name);
                    break;

                case "Abrv":
                    models = models.OrderBy(v => v.Abrv);
                    break;

                case "abrv_desc":
                    models = models.OrderByDescending(v => v.Abrv);
                    break;

                case "MakeId":
                    models = models.OrderBy(v => v.MakeId);
                    break;

                case "makeid_desc":
                    models = models.OrderByDescending(v => v.MakeId);
                    break;

                default: // sort by name
                    models = models.OrderBy(v => v.Name);
                    break;
            }

            return models.ToList();
        }

        private IPagedList<VehicleModelViewModel> ConvertToPaginatedResults(List<VehicleModelViewModel> vehicleModelViewModelList, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return vehicleModelViewModelList.ToPagedList(pageNumber, pageSize);
        }
    }
}