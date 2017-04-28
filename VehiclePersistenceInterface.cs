using MitchellCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MitchellCodingChallenge
{
    public interface VehiclePersistenceInterface
    {
         List<Vehicle> getAllVehicles();
         Vehicle getVehicle(int id);
         int saveVehicle(Vehicle vehicleToSave);
         bool updateVehicle(int id, Vehicle vehicleToUpdate);
         bool deleteVehicle(int id);
         
    }
}
