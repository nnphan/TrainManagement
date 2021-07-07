using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Models.DAO
{
    public class StationDao
    {
        TrainManagementEntities db = null;
        public StationDao()
        {
            db = new TrainManagementEntities();
        }

        public decimal Insert(tblStation station)
        {
            db.tblStations.Add(station);
            db.SaveChanges();
            return station.Id;
        }

        public List<tblStation> GetListStation()
        {
            var station = db.tblStations.ToList();
            return station;
        }

        public List<StationJsonModel> GetListStationJson( string search)
        {

            List<StationJsonModel> allsearch = db.tblStations.Where(x => x.StationName.Contains(search)).Select(x => new StationJsonModel
            {
                Id = x.Id,
                Name = x.StationName
            }).ToList();

            return allsearch;
        }

        public tblStation GetStationDetail( decimal id)
        {
            var station = db.tblStations.Find(id);
            return station;
        }

        public tblStation GetStationByName(string name)
        {
            var station = db.tblStations.Where(x => x.StationName == name).FirstOrDefault();
            return station;
        }

        public bool Update(tblStation stationModel)
        {
            try
            {
                var station = db.tblStations.Find(stationModel.Id);
                station.StationName = stationModel.StationName;
                station.StationCode = stationModel.StationCode;
                station.RailwayDivisionName = stationModel.RailwayDivisionName;
                station.PositonDistance = stationModel.PositonDistance;
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
                var station = db.tblStations.Find(id);
                db.tblStations.Remove(station);
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