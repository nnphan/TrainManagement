using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainManagementWeb.Models.DAO;

namespace TrainManagementWeb.Areas.Admin.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet]
        public ActionResult GetBookingList()
        {

            BookingDao bookingDao = new BookingDao();
            var bookingList = bookingDao.GetBookingListForAdmin();

            return View(bookingList);
        }


        /// <summary>
        /// Method Export Excel
        /// </summary>
        public void ExportExcel()
        {
            BookingDao bookingDao = new BookingDao();
            var bookingList = bookingDao.GetBookingListForAdmin();
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "User Name";
            Sheet.Cells["B1"].Value = "Phone Number";
            Sheet.Cells["C1"].Value = "Passport Id";
            Sheet.Cells["D1"].Value = "PRN";
            Sheet.Cells["E1"].Value = "Status";
            Sheet.Cells["F1"].Value = "Booking Day";
            Sheet.Cells["G1"].Value = "Train code";
            Sheet.Cells["H1"].Value = "Class";
            Sheet.Cells["I1"].Value = "Price";
            Sheet.Cells["J1"].Value = "Quantity";
            Sheet.Cells["K1"].Value = "Start Station Name";
            Sheet.Cells["L1"].Value = "End Station Name";
            Sheet.Cells["M1"].Value = "Departure day";
            Sheet.Cells["N1"].Value = "Departure time";
            Sheet.Cells["O1"].Value = "Refund Money";
            int row = 2;
            foreach (var item in bookingList)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.UserName;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.PhoneNumber;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.PassportId;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.PRN;
                if (item.isCancel == true)
                {
                    Sheet.Cells[string.Format("E{0}", row)].Value = "Cancelled";
                }
                else
                {
                    Sheet.Cells[string.Format("E{0}", row)].Value = "Available";
                }
                Sheet.Cells[string.Format("F{0}", row)].Value = item.CreatedDate.Value.ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("G{0}", row)].Value = item.traincode;
                if (item.Class == 1)
                {
                    Sheet.Cells[string.Format("H{0}", row)].Value = "AC Coach";
                }
                else if(item.Class == 2)
                {
                    Sheet.Cells[string.Format("H{0}", row)].Value = "First Class Coach";
                }
                else
                {
                    Sheet.Cells[string.Format("H{0}", row)].Value = "Sleeper Class Coach";
                }
                Sheet.Cells[string.Format("I{0}", row)].Value = item.price.Value.ToString("N0");
                Sheet.Cells[string.Format("J{0}", row)].Value = item.quantity;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.startstationname;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.endstationname;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.depday.Value.ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("N{0}", row)].Value = item.deptime;
                if (item.RefuncMoney == null)
                {
                    Sheet.Cells[string.Format("O{0}", row)].Value = "0";
                }
                else
                {
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.RefuncMoney.Value.ToString("N0"); ;
                }
                
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
           
        }

    }
}