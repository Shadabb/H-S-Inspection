using Inspection.Helpers;
using Inspection.Models;
using Inspection.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Inspection.Controllers
{

    public class HomeController : Controller
    {
        private readonly InspectionModelContext db;
        private AuditInspectionDetail Auditdetails;
        private readonly InpectionViewModel inspectionView = new InpectionViewModel();

        public HomeController()
        {
            db = new InspectionModelContext();
        }
        public HomeController(InspectionModelContext context)
        {
            context = new InspectionModelContext();
        }
        //[Authorize]
        //public async Task<ActionResult> Index()
        //{
        //    USerLoginDetails userLoginDetails = new USerLoginDetails();
        //    var user = await userLoginDetails.UserDetails();
        //    if (await userLoginDetails.AdGroupCheck(user))
        //    {
        //        var list = db.Inspections.OrderByDescending(d => d.ID).ToList();
        //        return View(list);
        //    };

        //    return View("AccessDenied");
        //}

        /// <summary>
        /// Without AD Group Auth
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            var list = db.Inspections.OrderByDescending(d => d.ID).ToList();
            var result = (from t1 in db.Inspections
                          join t2 in db.InspectionExts on t1.ID equals t2.InspectionID
                          join t3 in db.CompliantQuestions on t2.CompliantNo equals t3.CompliantNo.Value
                          where t1.InspectionForm == t3.InspectionForm || t3.InspectionForm == ""

                          select new MasterDetails()
                          {
                              InspectionId = t1.ID,
                              InspectionForm = t1.InspectionForm,
                              SiteName = t1.SiteName,
                              SiteId = t1.SiteID,
                              AreaOfBusiness = t1.AreaOfBusiness,
                              AreaInspected = t1.AreaInspected,
                              InspectedPerson = t1.InspectedPerson,
                              ActivityName = t1.ActivityName,
                              InspectionDate = t1.InspectionDate,
                              CreatedBy = t1.Created_By,
                              Created = t1.Created,
                              CompliantNo = t2.CompliantNo,
                              CompliantQues = t3.CompliantQues,
                              Compliant = t2.Compliant,
                              Remarks = t2.Remarks,
                              Severity = t2.Severity,
                              Assignee = t2.Assignee,
                              DueDate = t2.DueDate,
                              Status = t2.Status,
                              LastModifiedBy = t2.LastModifiedBy,
                              LastModifiedDate = t2.LastModifiedDate
                          }).ToList();

            var allRecords = new ViewModelAllRecord
            {
                MasterDetails = result,
                Inspection = list
            };

            return View(allRecords);
        }



        [Authorize]
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inspectionQuery = (from ins in db.Inspections
                                   join ext in db.InspectionExts on ins.ID equals ext.InspectionID
                                   join ques in db.CompliantQuestions on ext.CompliantNo equals ques.CompliantNo.Value
                                   where ins.ID == id && (ins.InspectionForm == ques.InspectionForm || ques.InspectionForm == "")
                                   select new
                                   {
                                       Inspect = ins,
                                       InspectExt = ext,
                                       Compliants = ques
                                   }
                             ).ToList();

            inspectionView.Inspections = db.Inspections.Find(id);
            inspectionView.InspectionCompliantData = new List<InspectionCompliant>();

            InspectionCompliant iComplaint = new InspectionCompliant();

            for (int i = 0; i < inspectionQuery.Count(); i++)
            {
                iComplaint = new InspectionCompliant();
                iComplaint.CompliantNo = int.Parse(inspectionQuery[i].Compliants.CompliantNo.ToString());
                iComplaint.CompliantQues = inspectionQuery[i].Compliants.CompliantQues.ToString();
                iComplaint.Compliant = inspectionQuery[i].InspectExt.Compliant.ToString();
                iComplaint.Remarks = inspectionQuery[i].InspectExt.Remarks.ToString();
                iComplaint.Severity = inspectionQuery[i].InspectExt.Severity.ToString();
                iComplaint.Assignee = inspectionQuery[i].InspectExt.Assignee.ToString();
                iComplaint.DueDate = inspectionQuery[i].InspectExt.DueDate.ToString();
                iComplaint.LastModifiedBy = inspectionQuery[i].InspectExt.LastModifiedBy;

                inspectionView.InspectionCompliantData.Add(iComplaint);
            }
            //if (inspection == null)
            //{
            //    return HttpNotFound();
            //}

            List<AuditInspectionDetail> lstAuditInspectionViewModel = new List<AuditInspectionDetail>();
            lstAuditInspectionViewModel = db.AuditInspectionDetails.Where(x => x.InspectionID == id).OrderByDescending(d => d.ModifiedDate).ToList();

            inspectionView.InspectionAuditViewModels = lstAuditInspectionViewModel;

            return View(inspectionView);

        }

        // GET: HSE_Responses/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inspectionQuery = (from ins in db.Inspections
                                   join ext in db.InspectionExts on ins.ID equals ext.InspectionID
                                   join ques in db.CompliantQuestions on ext.CompliantNo equals ques.CompliantNo.Value
                                   where ins.ID == id && (ins.InspectionForm == ques.InspectionForm || ques.InspectionForm == "")
                                   select new
                                   {
                                       Inspect = ins,
                                       InspectExt = ext,
                                       Compliants = ques
                                   }
                             ).ToList();

            inspectionView.Inspections = db.Inspections.Find(id);
            inspectionView.InspectionCompliantData = new List<InspectionCompliant>();

            InspectionCompliant iComplaint = new InspectionCompliant();

            for (int i = 0; i < inspectionQuery.Count(); i++)
            {
                iComplaint = new InspectionCompliant();
                iComplaint.InspectionCompliantID = inspectionQuery[i].InspectExt.ID;
                iComplaint.CompliantNo = int.Parse(inspectionQuery[i].Compliants.CompliantNo.ToString());
                iComplaint.CompliantQues = inspectionQuery[i].Compliants.CompliantQues.ToString();
                iComplaint.Compliant = inspectionQuery[i].InspectExt.Compliant.ToString();
                iComplaint.Remarks = inspectionQuery[i].InspectExt.Remarks.ToString();
                iComplaint.Severity = inspectionQuery[i].InspectExt.Severity.ToString();
                iComplaint.Assignee = inspectionQuery[i].InspectExt.Assignee.ToString();
                iComplaint.DueDate = inspectionQuery[i].InspectExt.DueDate.ToString();

                inspectionView.InspectionCompliantData.Add(iComplaint);
            }

            return View(inspectionView);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Edit(InpectionViewModel inspectionViewResult)
        {
            try
            {
                USerLoginDetails uld = new USerLoginDetails();
                var userDetails = await uld.UserDetails();
                var user = userDetails.DisplayName;

                Auditdetails = new AuditInspectionDetail();

                var inspectionResult = (from ins in db.Inspections
                                        where ins.ID == inspectionViewResult.Inspections.ID
                                        select ins).FirstOrDefault();

                DateTime dateTime = DateTime.Now;
                if (inspectionResult == null)
                {
                    throw new ArgumentNullException();
                }
                if (inspectionResult.SiteName != inspectionViewResult.Inspections.SiteName)
                {
                    Auditdetails.InspectionID = inspectionViewResult.Inspections.ID;
                    Auditdetails.TableName = "Inspection";
                    Auditdetails.FiledName = "Site Name";
                    Auditdetails.OldValue = string.IsNullOrEmpty(inspectionResult.SiteName) ? "" : inspectionResult.SiteName;
                    Auditdetails.NewValue = string.IsNullOrEmpty(inspectionViewResult.Inspections.SiteName) ? "" : inspectionViewResult.Inspections.SiteName;
                    Auditdetails.ModifiedBy = user;
                    Auditdetails.ModifiedDate = dateTime;
                    db.AuditInspectionDetails.Add(Auditdetails);
                    db.SaveChanges();
                }
                if (inspectionResult.SiteID != inspectionViewResult.Inspections.SiteID)
                {
                    Auditdetails.InspectionID = inspectionViewResult.Inspections.ID;
                    Auditdetails.TableName = "Inspection";
                    Auditdetails.FiledName = "Site ID";
                    Auditdetails.OldValue = string.IsNullOrEmpty(inspectionResult.SiteID) ? "" : inspectionResult.SiteID;
                    Auditdetails.NewValue = string.IsNullOrEmpty(inspectionViewResult.Inspections.SiteID) ? "" : inspectionViewResult.Inspections.SiteID;
                    Auditdetails.ModifiedBy = user;
                    Auditdetails.ModifiedDate = dateTime;
                    db.AuditInspectionDetails.Add(Auditdetails);
                    db.SaveChanges();
                }
                if (inspectionResult.AreaOfBusiness != inspectionViewResult.Inspections.AreaOfBusiness)
                {
                    Auditdetails.InspectionID = inspectionViewResult.Inspections.ID;
                    Auditdetails.TableName = "Inspection";
                    Auditdetails.FiledName = "Area of Business";
                    Auditdetails.OldValue = string.IsNullOrEmpty(inspectionResult.AreaOfBusiness) ? "" : inspectionResult.AreaOfBusiness;
                    Auditdetails.NewValue = string.IsNullOrEmpty(inspectionViewResult.Inspections.AreaOfBusiness) ? "" : inspectionViewResult.Inspections.AreaOfBusiness;
                    Auditdetails.ModifiedBy = user;
                    Auditdetails.ModifiedDate = dateTime;
                    db.AuditInspectionDetails.Add(Auditdetails);
                    db.SaveChanges();
                }
                if (inspectionResult.AreaInspected != inspectionViewResult.Inspections.AreaInspected)
                {
                    Auditdetails.InspectionID = inspectionViewResult.Inspections.ID;
                    Auditdetails.TableName = "Inspection";
                    Auditdetails.FiledName = "Area Inspected";
                    Auditdetails.OldValue = string.IsNullOrEmpty(inspectionResult.AreaInspected) ? "" : inspectionResult.AreaInspected;
                    Auditdetails.NewValue = string.IsNullOrEmpty(inspectionViewResult.Inspections.AreaInspected) ? "" : inspectionViewResult.Inspections.AreaInspected;
                    Auditdetails.ModifiedBy = user;
                    Auditdetails.ModifiedDate = dateTime;
                    db.AuditInspectionDetails.Add(Auditdetails);
                    db.SaveChanges();
                }
                if (inspectionResult.InspectedPerson != inspectionViewResult.Inspections.InspectedPerson)
                {
                    Auditdetails.InspectionID = inspectionViewResult.Inspections.ID;
                    Auditdetails.TableName = "Inspection";
                    Auditdetails.FiledName = "Inspected Person";
                    Auditdetails.OldValue = string.IsNullOrEmpty(inspectionResult.InspectedPerson) ? "" : inspectionResult.InspectedPerson;
                    Auditdetails.NewValue = string.IsNullOrEmpty(inspectionViewResult.Inspections.InspectedPerson) ? "" : inspectionViewResult.Inspections.InspectedPerson;
                    Auditdetails.ModifiedBy = user;
                    Auditdetails.ModifiedDate = dateTime;
                    db.AuditInspectionDetails.Add(Auditdetails);
                    db.SaveChanges();
                }

                if ((string.IsNullOrEmpty(inspectionResult.ActivityName) ? null : inspectionResult.ActivityName) != inspectionViewResult.Inspections.ActivityName)
                {
                    Auditdetails.InspectionID = inspectionViewResult.Inspections.ID;
                    Auditdetails.TableName = "Inspection";
                    Auditdetails.FiledName = "Activity Name";
                    Auditdetails.OldValue = string.IsNullOrEmpty(inspectionResult.ActivityName) ? "" : inspectionResult.ActivityName;
                    Auditdetails.NewValue = string.IsNullOrEmpty(inspectionViewResult.Inspections.ActivityName) ? "" : inspectionViewResult.Inspections.ActivityName;
                    Auditdetails.ModifiedBy = user;
                    Auditdetails.ModifiedDate = dateTime;
                    db.AuditInspectionDetails.Add(Auditdetails);
                    db.SaveChanges();
                }
                if (inspectionResult.InspectionDate.ToString("dd/MM/yyyy") != inspectionViewResult.Inspections.InspectionDate.ToString("dd/MM/yyyy"))
                {
                    Auditdetails.InspectionID = inspectionViewResult.Inspections.ID;
                    Auditdetails.TableName = "Inspection";
                    Auditdetails.FiledName = "Inspection Date";
                    Auditdetails.OldValue = inspectionResult.InspectionDate.ToString("dd/MM/yyyy");
                    Auditdetails.NewValue = inspectionViewResult.Inspections.InspectionDate.ToString("dd/MM/yyyy");
                    Auditdetails.ModifiedBy = user;
                    Auditdetails.ModifiedDate = dateTime;
                    db.AuditInspectionDetails.Add(Auditdetails);
                    db.SaveChanges();
                }

                inspectionResult.InspectionForm = inspectionResult.InspectionForm;
                inspectionResult.SiteName = inspectionViewResult.Inspections.SiteName;
                inspectionResult.SiteID = inspectionViewResult.Inspections.SiteID;
                inspectionResult.AreaOfBusiness = inspectionViewResult.Inspections.AreaOfBusiness;
                inspectionResult.AreaInspected = inspectionViewResult.Inspections.AreaInspected;
                inspectionResult.InspectedPerson = inspectionViewResult.Inspections.InspectedPerson;
                inspectionResult.ActivityName = inspectionViewResult.Inspections.ActivityName;
                inspectionResult.InspectionDate = inspectionViewResult.Inspections.InspectionDate;

                inspectionResult.LastModifiedBy = user;
                inspectionResult.LastModifiedDate = dateTime;

                db.SaveChanges();
                TempData["Success"] = "Updated sccessfully";
            }
            catch (Exception)
            {
                // ignored
            }

            return RedirectToAction("Edit/" + inspectionViewResult.Inspections.ID);
        }

        public static string ToString(DateTime? dt, string format)
            => dt == null ? "NULL" : ((DateTime)dt).ToString(format);

        [HttpPost]
        public async Task<ActionResult> UpdateInspectionResult(InspectionExt inspectionObj)
        {
            USerLoginDetails uld = new USerLoginDetails();
            var userDetails = await uld.UserDetails();
            var user = userDetails.DisplayName;

            Auditdetails = new AuditInspectionDetail();
            InspectionExt InspectionExtResult = db.InspectionExts.FirstOrDefault(s => s.ID.Equals(inspectionObj.ID));
            //var InspectionResult = db.Inspections.FirstOrDefault(s => s.ID.Equals(inspectionObj.InspectionID));

            DateTime dateTime = DateTime.Now;

            if (InspectionExtResult == null || inspectionObj == null)
            {
                throw new ArgumentNullException();
            }
            if (InspectionExtResult.Compliant != inspectionObj.Compliant)
            {
                Auditdetails.InspectionID = InspectionExtResult.InspectionID;
                Auditdetails.TableName = "InspectionExt";
                Auditdetails.FiledName = "Compliant";
                Auditdetails.OldValue = string.IsNullOrEmpty(InspectionExtResult.Compliant) ? "NULL" : InspectionExtResult.Compliant;
                Auditdetails.NewValue = string.IsNullOrEmpty(inspectionObj.Compliant) ? "NULL" : inspectionObj.Compliant;
                Auditdetails.ModifiedBy = user;
                Auditdetails.ModifiedDate = dateTime;
                Auditdetails.CompliantNo = inspectionObj.CompliantNo;
                db.AuditInspectionDetails.Add(Auditdetails);
                db.SaveChanges();
            }
            if (InspectionExtResult.Remarks != inspectionObj.Remarks)
            {
                Auditdetails.InspectionID = InspectionExtResult.InspectionID;
                Auditdetails.TableName = "InspectionExt";
                Auditdetails.FiledName = "Remarks";
                Auditdetails.OldValue = string.IsNullOrEmpty(InspectionExtResult.Remarks) ? "NULL" : InspectionExtResult.Remarks;
                Auditdetails.NewValue = string.IsNullOrEmpty(inspectionObj.Remarks) ? "NULL" : inspectionObj.Remarks;
                Auditdetails.ModifiedBy = user;
                Auditdetails.ModifiedDate = dateTime;
                Auditdetails.CompliantNo = inspectionObj.CompliantNo;
                db.AuditInspectionDetails.Add(Auditdetails);
                db.SaveChanges();
            }
            if (InspectionExtResult.Severity != inspectionObj.Severity)
            {
                Auditdetails.InspectionID = InspectionExtResult.InspectionID;
                Auditdetails.TableName = "InspectionExt";
                Auditdetails.FiledName = "Severity";
                Auditdetails.OldValue = string.IsNullOrEmpty(InspectionExtResult.Severity) ? "NULL" : InspectionExtResult.Severity;
                Auditdetails.NewValue = string.IsNullOrEmpty(inspectionObj.Severity) ? "NULL" : inspectionObj.Severity;
                Auditdetails.ModifiedBy = user;
                Auditdetails.ModifiedDate = dateTime;
                Auditdetails.CompliantNo = inspectionObj.CompliantNo;
                db.AuditInspectionDetails.Add(Auditdetails);
                db.SaveChanges();
            }
            if (InspectionExtResult.Assignee != inspectionObj.Assignee)
            {
                Auditdetails.InspectionID = InspectionExtResult.InspectionID;
                Auditdetails.TableName = "InspectionExt";
                Auditdetails.FiledName = "Assignee";
                Auditdetails.OldValue = string.IsNullOrEmpty(InspectionExtResult.Assignee) ? "NULL" : InspectionExtResult.Assignee;
                Auditdetails.NewValue = string.IsNullOrEmpty(inspectionObj.Assignee) ? "NULL" : inspectionObj.Assignee;
                Auditdetails.ModifiedBy = user;
                Auditdetails.ModifiedDate = dateTime;
                Auditdetails.CompliantNo = inspectionObj.CompliantNo;
                db.AuditInspectionDetails.Add(Auditdetails);
                db.SaveChanges();
            }
            if (InspectionExtResult.DueDate != inspectionObj.DueDate)
            {
                Auditdetails.InspectionID = InspectionExtResult.InspectionID;
                Auditdetails.TableName = "InspectionExt";
                Auditdetails.FiledName = "DueDate";
                Auditdetails.OldValue = string.IsNullOrEmpty(InspectionExtResult.DueDate) ? "NULL" : InspectionExtResult.DueDate;
                Auditdetails.NewValue = string.IsNullOrEmpty(inspectionObj.DueDate) ? "NULL" : inspectionObj.DueDate;
                Auditdetails.ModifiedBy = user;
                Auditdetails.ModifiedDate = dateTime;
                Auditdetails.CompliantNo = inspectionObj.CompliantNo;
                db.AuditInspectionDetails.Add(Auditdetails);
                db.SaveChanges();
            }
            if (InspectionExtResult.Status != inspectionObj.Status)
            {
                Auditdetails.InspectionID = InspectionExtResult.InspectionID;
                Auditdetails.TableName = "InspectionExt";
                Auditdetails.FiledName = "Status";
                Auditdetails.OldValue = string.IsNullOrEmpty(InspectionExtResult.Status) ? "NULL" : InspectionExtResult.Status;
                Auditdetails.NewValue = string.IsNullOrEmpty(inspectionObj.Status) ? "NULL" : inspectionObj.Status;
                Auditdetails.ModifiedBy = user;
                Auditdetails.ModifiedDate = dateTime;
                Auditdetails.CompliantNo = inspectionObj.CompliantNo;
                db.AuditInspectionDetails.Add(Auditdetails);
                db.SaveChanges();
            }
            if (InspectionExtResult.ClosureDate != inspectionObj.ClosureDate)
            {
                Auditdetails.InspectionID = InspectionExtResult.InspectionID;
                Auditdetails.TableName = "InspectionExt";
                Auditdetails.FiledName = "ClosureDate";
                Auditdetails.OldValue = string.IsNullOrEmpty(InspectionExtResult.ClosureDate) ? "NULL" : InspectionExtResult.ClosureDate;
                Auditdetails.NewValue = string.IsNullOrEmpty(inspectionObj.ClosureDate) ? "NULL" : inspectionObj.ClosureDate;
                Auditdetails.ModifiedBy = user;
                Auditdetails.ModifiedDate = dateTime;
                Auditdetails.CompliantNo = inspectionObj.CompliantNo;
                db.AuditInspectionDetails.Add(Auditdetails);
                db.SaveChanges();
            }
            if (InspectionExtResult.UpdatedComments != inspectionObj.UpdatedComments)
            {
                Auditdetails.InspectionID = InspectionExtResult.InspectionID;
                Auditdetails.TableName = "InspectionExt";
                Auditdetails.FiledName = "UpdatedComments";
                Auditdetails.OldValue = string.IsNullOrEmpty(InspectionExtResult.UpdatedComments) ? "NULL" : InspectionExtResult.UpdatedComments;
                Auditdetails.NewValue = string.IsNullOrEmpty(inspectionObj.UpdatedComments) ? "NULL" : inspectionObj.UpdatedComments;
                Auditdetails.ModifiedBy = user;
                Auditdetails.ModifiedDate = dateTime;
                Auditdetails.CompliantNo = inspectionObj.CompliantNo;
                db.AuditInspectionDetails.Add(Auditdetails);
                db.SaveChanges();
            }

            InspectionExtResult.LastModifiedBy = user;
            InspectionExtResult.LastModifiedDate = dateTime;

            UpdateModel(InspectionExtResult);
            db.SaveChanges();

            TempData["Success"] = "Updated sccessfully";
            return Json("Result");
        }

        [Authorize]
        public ActionResult ActionTab()
        {
            var list = db.Inspections.OrderByDescending(d => d.ID).ToList();
            var result = (from t1 in db.Inspections
                          join t2 in db.InspectionExts on t1.ID equals t2.InspectionID
                          join t3 in db.CompliantQuestions on t2.CompliantNo equals t3.CompliantNo.Value
                          where t1.InspectionForm == t3.InspectionForm || t3.InspectionForm == ""

                          select new MasterDetails()
                          {
                              InspectionId = t1.ID,
                              InspectionExtId = t2.ID,
                              InspectionForm = t1.InspectionForm,
                              SiteName = t1.SiteName,
                              SiteId = t1.SiteID,
                              AreaOfBusiness = t1.AreaOfBusiness,
                              AreaInspected = t1.AreaInspected,
                              InspectedPerson = t1.InspectedPerson,
                              ActivityName = t1.ActivityName,
                              InspectionDate = t1.InspectionDate,
                              CreatedBy = t1.Created_By,
                              Created = t1.Created,
                              CompliantNo = t2.CompliantNo,
                              CompliantQues = t3.CompliantQues,
                              Compliant = t2.Compliant,
                              Remarks = t2.Remarks,
                              Severity = t2.Severity,
                              Assignee = t2.Assignee,
                              DueDate = t2.DueDate,
                              Status = t2.Status,
                              ClosureDate = t2.ClosureDate,
                              UpdatedComments = t2.UpdatedComments

                          }).ToList();

            var allRecords = new ViewModelAllRecord
            {
                MasterDetails = result,
                Inspection = list
            };

            return View(allRecords);
        }
    }
}