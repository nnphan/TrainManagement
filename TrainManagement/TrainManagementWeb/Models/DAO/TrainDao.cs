using System;
using System.Collections.Generic;
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