using Kama.AppCore;
using Kama.FinancialAnalysis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Kama.FinancialAnalysis.DAL
{
    public class DistanceMeasurementDataSource : DataSource
    {
        public DistanceMeasurementDataSource(): base()
        {
        }

        public async Task<Result<IEnumerable<DistanceMeasurement>>> GetDistanceMeasurementsAsync(DistanceMeasurementVM model)
        {
            try
            {
                var result = (await pbl.GetDistanceMeasurementsAsync(
                    _session: (byte)model.Session,
                    _type: (byte)model.Type
                    )).ToListActionResult<DistanceMeasurement>();


                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result> AddAsync(List<DistanceMeasurement> model)
        {
            try
            {
                var result = (await pbl.AddDistanceMeasurementsAsync(
                    _json: new Dependency.ObjectSerializer().Serialize(model)
                    )).ToActionResult();


                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<DistanceMeasurement>>> ListAsync(DistanceMeasurementVM model)
        {
            try
            {
                var result = (await pbl.ListDistanceMeasurementAsync(
                    _session:(byte)model.Session,
                    _type: (byte)model.Type
                    )).ToListActionResult<DistanceMeasurement>();


                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}