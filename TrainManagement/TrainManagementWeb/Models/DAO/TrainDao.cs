using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;

namespace TrainManagementWeb.Models.DAO
{
    public class TrainDao
    {
        TrainManagementEntities db = null;
        public TrainDao()
        {
            db = new TrainManagementEntities();
        }

        public List<tblTrain> GetListTrain()
        {
            var trains = db.tblTrains.ToList();
            return trains;
        }

        public int GetSeatAvailable(string trainCode, int classSeat, string depDate)
        {
            int seatAvailable = 0;
            DateTime depDay = DateTime.ParseExact(depDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var seatBooking = db.sp_Count_SeatAvailable(trainCode, classSeat, depDay).FirstOrDefault();
            if(seatBooking == null)
            {
                seatBooking = 0;
            }
            var train = db.tblTrains.Where(x => x.TrainCode == trainCode).FirstOrDefault();
            var seatTotal1 = train.ACCoaches * train.ACSeats;
            var seatTotal2 = train.FirstClassCoaches * train.FirstClassSeats;
            var seatTotal3 = train.SleeperClassCoaches * train.SleeperClassSeats;
            if (classSeat == 1)
            {
                seatAvailable = seatTotal1.Value - seatBooking.Value;
            }
            else if (classSeat == 2)
            {
                seatAvailable = seatTotal2.Value - seatBooking.Value;
            }
            else
            {
                seatAvailable = seatTotal3.Value - seatBooking.Value;
            }
            return seatAvailable;
        }

        public int GetSeatBooked(string trainCode, int classSeat, string depDate)
        {
            int seatBooked = 0;
            DateTime depDay = DateTime.ParseExact(depDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var seatBooking = db.sp_Count_SeatAvailable(trainCode, classSeat, depDay).FirstOrDefault();
            if (seatBooking == null)
            {
                seatBooking = 0;
            }
            
            return seatBooking.Value;
        }



        public tblTrain GetTrainDetail(decimal id)
        {
            var train = db.tblTrains.Find(id);
            return train;
        }

        public bool Update(tblTrain trainUpdate)
        {
            try
            {
                var train = db.tblTrains.Find(trainUpdate.Id);
                train.TrainNo = trainUpdate.TrainNo;
                train.TrainCode = trainUpdate.TrainCode;
                train.TrainName  = trainUpdate.TrainName;
                train.Route = trainUpdate.Route;
                train.Status = trainUpdate.Status;
                train.ACCoaches = trainUpdate.ACCoaches;
                train.ACSeats = trainUpdate.ACSeats;
                train.FirstClassCoaches = trainUpdate.FirstClassCoaches;
                train.FirstClassSeats = trainUpdate.FirstClassSeats;
                train.SleeperClassCoaches = trainUpdate.SleeperClassCoaches;
                train.SleeperClassSeats = trainUpdate.SleeperClassSeats;
                train.Status = trainUpdate.Status;
                train.ProposedPrice = trainUpdate.ProposedPrice;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public decimal Insert(tblTrain trainInsertModel)
        {
            db.tblTrains.Add(trainInsertModel);
            db.SaveChanges();
            return trainInsertModel.Id;
        }
       

        public bool Delete(decimal id)
        {
            try
            {
                var train = db.tblTrains.Find(id);
                db.tblTrains.Remove(train);
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