using Inspection.Controllers;
using Inspection.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Inspection.Test
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            List<Models.Inspection> inspections = new List<Models.Inspection>()
            {
                new Models.Inspection
                {
                    ID                 =  51,
                    InspectionForm     = "COSHH",
                    SiteName           = "ODC",
                    SiteID             = "123",
                    AreaOfBusiness     = "kl",
                    AreaInspected      = "kl",
                    InspectedPerson    = "kl",
                    ActivityName       = "kl",
                    InspectionDate     = new DateTime(),
                    Created_By         = "John",
                    Created            = new DateTime(),
                    LastModifiedDate   = new DateTime(),
                    LastModifiedBy     = "John"

                }
            };

            List<Models.InspectionExt> inspectionExts = new List<Models.InspectionExt>()
            {
                new Models.InspectionExt()
                {
                    ID                 =  1029,
                    InspectionID = 51,
                    CompliantNo = 1,
                    Compliant = "Yes"
                }
            };

            List<Models.CompliantQuestion> compliantQuestions = new List<Models.CompliantQuestion>()
            {
                new Models.CompliantQuestion()
                {
                    ID                 =  1,
                    CompliantQues = "Vehicle and pedestrian routes are in good condition and a good state of repair.",
                    InspectionForm = "COSHH",
                    CompliantNo = 1
                }
            };



            var insMockSet = SetupMockEntity<Models.Inspection>(inspections);
            var insExtMockSet = SetupMockEntity<Models.InspectionExt>(inspectionExts);
            var cQuesMockSet = SetupMockEntity<Models.CompliantQuestion>(compliantQuestions);

            var context = new Mock<InspectionModelContext>();

            context.Setup(m => m.Inspections).Returns(insMockSet.Object);
            context.Setup(m => m.InspectionExts).Returns(insExtMockSet.Object);
            context.Setup(m => m.CompliantQuestions).Returns(cQuesMockSet.Object);
            // Arrange
            HomeController controller = new HomeController(context.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        private Mock<DbSet<T>> SetupMockEntity<T>(List<T> sourceList) where T : class
        {
            IQueryable<T> queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();

            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            dbSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(s => sourceList.Remove(s));
            //dbSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).Returns<object[]>(ids => Task.FromResult(sourceList.Find(t => t.Id == (int)ids[0])));
            return dbSet;
        }
    }
}
