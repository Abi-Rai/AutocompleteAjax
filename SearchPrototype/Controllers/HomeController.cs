using SearchPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchPrototype.Controllers
{
    public class HomeController : Controller
    {
        RecruitmentDbEntities dbContext;
        public HomeController()
        {
            this.dbContext = new RecruitmentDbEntities();
        }
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Search()
        {
            ViewModel model = new ViewModel(GetCategories(), GetSpecialities(), GetLocations());
            tblCompany company = dbContext.tblCompanies.First();
            Tuple<ViewModel, tblCompany> companyTuple = new Tuple<ViewModel, tblCompany>(model, company);
            return View(companyTuple);
        }


        //Main Ajax call method for autocomplete and filtering of dropdown selections.
        [HttpPost]
        public JsonResult GetCompapanyNames(string prefixText,string speciality,string category, string location)
        {
            int sSpeciality = ValidateSelection(speciality);
            int sCategory = ValidateSelection(category);
            int sLocation = ValidateSelection(location);
            IQueryable<tblCompany> companies = GetCompanies();
            
            /* specialities get filtered here, dropdown boxes with all selected are not filtered. */
            if (sSpeciality != -1){
                companies = companies.Where(c => c.tblSpecialitiesToCompanies.Select(s => s.intSpecialityID == sSpeciality).FirstOrDefault());
            }
            if (sCategory != -1){
                companies = companies.Where(c => c.intCategoryID.Equals(sCategory));
            }
            if (sLocation != -1){
                companies = companies.Where(c => c.tblLocationsToCompanies.Select(l => l.intLocationID == sLocation).FirstOrDefault());
            }
            var res = (from c in companies
                       where c.strName.ToLower().StartsWith(prefixText.ToLower())
                       select new
                       {
                           c.intID,
                           c.strName,
                       });

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // Partial view to bring up individual company details.
        [HttpPost]
        public ActionResult OnSubmitText(string companyName)
        {
            System.Diagnostics.Debug.WriteLine($"SubTextid: {companyName}");
            var companies = GetCompanies();
            tblCompany company = GetCompanies().FirstOrDefault(c => c.strName.ToLower().Equals(companyName.ToLower()));
            if (company==null) ViewBag.Message = "Error at Search Text Box, Company not found!";
            return PartialView("_CompanyDetails", company);
        }

        /* returns -1 if all is selected, else it returns int id of selection. */
        public int ValidateSelection(string id)
        {
            System.Diagnostics.Debug.WriteLine($"id: {id}");
            int val = id == "all" ? -1 : int.Parse(id);
            return val;
        }

        private IEnumerable<tblSpeciality> GetSpecialities()
        {
            return dbContext.tblSpecialities.ToList();
        }
        private IEnumerable<tblCategory> GetCategories()
        {
            return dbContext.tblCategories.ToList();
        }
        private IEnumerable<tblLocation> GetLocations()
        {
            return dbContext.tblLocations.ToList();
        }
        private IQueryable<tblCompany> GetCompanies()
        {
            return dbContext.tblCompanies;
        }
    }
}