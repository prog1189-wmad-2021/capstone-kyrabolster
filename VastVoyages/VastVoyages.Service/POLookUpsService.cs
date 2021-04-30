using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Repository;
using VastVoyages.Model;

namespace VastVoyages.Service
{
    public class POLookUpsService
    {
        private POLookUpsRepo repo = new POLookUpsRepo();

        public List<POStatusLookUpsDTO> GetPOStatus()
        {
            return repo.RetrievePOStatus();
        }

        public List<ItemStatusLookUpsDTO> GetItemStatus()
        {
            return repo.RetrieveItemStatus();
        }
    }
}
