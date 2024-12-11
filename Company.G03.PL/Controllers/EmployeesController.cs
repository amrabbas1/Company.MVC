using AutoMapper;
using Company.G03.BLL;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Company.G03.PL.Helper;
using Company.G03.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace Company.G03.PL.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {

        //private readonly IEmployeeReprository _employeeReprository;
        //private readonly IDepartmentReprository _departmentReprository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(
            //IEmployeeReprository employeeReprository,
            //IDepartmentReprository departmentReprository,
            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            _unitOfWork = unitOfWork;
            //_employeeReprository = employeeReprository;
            //_departmentReprository = departmentReprository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchInput)
        {
            var employees = Enumerable.Empty<Employee>();
            var employeesViewModels = new Collection<EmployeeViewModel>();
            if (string.IsNullOrEmpty(searchInput))
            {
                employees = await _unitOfWork.EmployeeReprository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeReprository.GetByNameAsync(searchInput);
            }

            //Auto mapping
            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);//from EmployeeViewModel to employee

            #region View Dictionary
            string Message = "Hello World";

            //View Dictionary : [Extra information] Transfer data from action to view [one way]

            //1.ViewData : property inherited from controller -- Dictionary

            ViewData["Message"] = Message + "From ViewData";

            //2.ViewBag : property inherited from controller -- Dynamic

            ViewBag.Hamada = Message + "From ViewBag";

            //3.TempData : property inherited from controller -- Dictionary
            //Transfer data from request to another request

            TempData["Message01"] = Message + "From TempData";
            #endregion

            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentReprository.GetAllAsync();//Extra Information

            ViewData["Departments"] = departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//btmn3 ay request mn app khargy zy el postman msln
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if(model.Image != null)
                model.ImageName = DocumentSettings.Upload(model.Image, "images");
            //Casting from employeeviewmodel to model
            ////Manual mapping
            //Employee employee = new Employee()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Age = model.Age,
            //    Address  = model.Address,
            //    Salary = model.Salary,
            //    PhoneNumber = model.PhoneNumber,
            //    Email = model.Email,
            //    IsDeleted = model.IsDeleted,
            //    DateOfCreation = model.DateOfCreation,
            //    HiringDate = model.HiringDate,
            //    WorkForId = model.WorkForId,
            //    WorkFor = model.WorkFor
            //};

            //Auto Mapping
            var employee = _mapper.Map<Employee>(model);

            if (ModelState.IsValid)//IsValid deh btt2kd en el model bymatch el validation ele 3al properties zay eno required msln
            {
                await _unitOfWork.EmployeeReprository.AddAsync(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();//error 400

            var employee = await _unitOfWork.EmployeeReprository.GetAsync(id.Value);

            if (employee == null)
                return NotFound();
            //Auto Mapping
            var result = _mapper.Map<EmployeeViewModel>(employee);
            return View(ViewName, result);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //    return BadRequest();//error 400

            //var department = _departmentRepository.Get(id.Value);

            //if (department == null)
            //    return NotFound();

            //return View(department);

            var departments = await _unitOfWork.DepartmentReprository.GetAllAsync();//Extra Information

            ViewData["Departments"] = departments;

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//btmn3 ay request mn app khargy zy el postman msln
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (model.ImageName != null)
                    DocumentSettings.Delete(model.ImageName, "images");

                if (model.Image != null)
                    model.ImageName = DocumentSettings.Upload(model.Image, "images");

                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,
                //    IsDeleted = model.IsDeleted,
                //    DateOfCreation = model.DateOfCreation,
                //    HiringDate = model.HiringDate,
                //    WorkForId = model.WorkForId,
                //    WorkFor = model.WorkFor
                //};

                    //Auto Mapping
                var employee = _mapper.Map<Employee>(model);

                if (id != model.Id)
                    return BadRequest();

                if (ModelState.IsValid)
                {
                    _unitOfWork.EmployeeReprository.Update(employee);
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
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                
                ////Manual Mapping
                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,
                //    IsDeleted = model.IsDeleted,
                //    DateOfCreation = model.DateOfCreation,
                //    HiringDate = model.HiringDate,
                //    WorkForId = model.WorkForId,
                //    WorkFor = model.WorkFor
                //};

                //Auto Mapping
                var employee = _mapper.Map<Employee>(model);

                if (id != model.Id)
                    return BadRequest();

                if (ModelState.IsValid)
                {
                    _unitOfWork.EmployeeReprository.Delete(employee);
                    var count = await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        if (model.ImageName != null)
                            DocumentSettings.Delete(model.ImageName, "images");

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
