using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Models.DAO
{
    public class TrainScheduleDao
    {

        TrainManagementEntities db = null;
        public TrainScheduleDao()
        {
            db = new TrainManagementEntities();
        }

        public TrainScheduleViewModel GetTrainScheduleDetail(decimal id)
        {
            var trainSchedule = db.tblTrainSchedules.Find(id);
            TrainScheduleViewModel trainScheduleViewModel = new TrainScheduleViewModel();
            trainScheduleViewModel.id = trainSchedule.id;
            trainScheduleViewModel.TrainId = trainSchedule.TrainId.Value;
            trainScheduleViewModel.StationId = trainSchedule.StationId.Value;
            trainScheduleViewModel.DepartureDate = trainSchedule.DepartureDate.Value.ToString("dd/MM/yyyy");
            trainScheduleViewModel.DepartureTime = trainSchedule.DepartureTime;
            return trainScheduleViewModel;
        }

        //
        public List<View_GetTrainSchedule> GetTrainScheduleById(decimal id)
        {
            var trainSchedule = db.View_GetTrainSchedule.Where(x => x.TrainId == id ).ToList();
            return trainSchedule;
        }

        public List<sp_GetTrainSchedule_Result> GetTrainScheduleClient(string departureStationName, string arrivalStationName, string departureDate)
        {
            DateTime DepartureDate = Convert.ToDateTime(departureDate, CultureInfo.InvariantCulture);
            var K = db.sp_GetTrainSchedule(departureStationName, arrivalStationName).ToList();
            var listTrainSchedule = db.sp_GetTrainSchedule(departureStationName, arrivalStationName).Where(x => x.DepartureDate == DepartureDate).ToList();
            return listTrainSchedule;
        }

        public List<sp_GetTrainScheduleList_Result> GetTrainScheduleList(string departureStationName, string arrivalStationName, string departureDate)
        {
            DateTime DepartureDate = Convert.ToDateTime(departureDate, CultureInfo.InvariantCulture);
            var K = db.sp_GetTrainScheduleList(departureStationName, arrivalStationName).ToList();
            var listTrainSchedule = db.sp_GetTrainScheduleList(departureStationName, arrivalStationName).Where(x => x.DepartureDate == DepartureDate).ToList();
            return listTrainSchedule;
        }

        public decimal Insert(TrainScheduleViewModel trainScheduleViewModel)
        {
            var tblTrainSchedule = new tblTrainSchedule();
            tblTrainSchedule.TrainId = trainScheduleViewModel.TrainId;
            tblTrainSchedule.StationId = trainScheduleViewModel.StationId;
            //Convert string to date time format
            DateTime DepartureDate = DateTime.ParseExact(trainScheduleViewModel.DepartureDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            tblTrainSchedule.DepartureDate = DepartureDate;
            tblTrainSchedule.DepartureTime = trainScheduleViewModel.DepartureTime;
            tblTrainSchedule.CreatedDate = DateTime.Now;
            db.tblTrainSchedules.Add(tblTrainSchedule);
            db.SaveChanges();
            return tblTrainSchedule.id;
        }

        public bool Update(TrainScheduleViewModel trainSchedule)
        {
            try
            {
                var tblTrainSchedule = db.tblTrainSchedules.Find(trainSchedule.id);
                tblTrainSchedule.StationId = trainSchedule.StationId;
                tblTrainSchedule.DepartureTime = trainSchedule.DepartureTime;
                DateTime departureDate = DateTime.ParseExact(trainSchedule.DepartureDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                tblTrainSchedule.DepartureDate = departureDate;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public bool Delete(decimal id)
        {
            try
            {
                var tblTrainSchedule = db.tblTrainSchedules.Find(id);
                db.tblTrainSchedules.Remove(tblTrainSchedule);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}