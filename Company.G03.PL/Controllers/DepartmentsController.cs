using Company.G03.BLL;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Reprositories;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentReprository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IDepartmentReprository departmentRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]//By Deafult
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//btmn3 ay request mn app khargy zy el postman msln
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)//IsValid deh btt2kd en el model bymatch el validation ele 3al properties zay eno required msln
            {
                await _unitOfWork.DepartmentReprository.AddAsync(model);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int? id ,string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();//error 400

            var department = await _unitOfWork.DepartmentReprository.GetAsync(id.Value);

            if(department == null)
                return NotFound();
            return View(ViewName, department);
        }

        public Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //    return BadRequest();//error 400

            //var department = _departmentRepository.Get(id.Value);

            //if (department == null)
            //    return NotFound();

            //return View(department);

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//btmn3 ay request mn app khargy zy el postman msln
        public async Task<IActionResult> Edit([FromRoute]int? id, Department model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest();

                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentReprository.Update(model);
                    var count = await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
           
            return View(model);
        }

        public Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //    return BadRequest();//error 400

            //var department = _departmentRepository.Get(id.Value);

            //if (department == null)
            //    return NotFound();
            //return View(department);
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//btmn3 ay request mn app khargy zy el postman msln
        public async Task<IActionResult> Delete([FromRoute] int? id, Department model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest();

                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentReprository.Delete(model);
                    var count = await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }
    }
}
