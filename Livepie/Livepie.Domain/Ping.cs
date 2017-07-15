using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livepie.Domain
{
    public class Ping
    {
        PoiType _poiType;
        Location _location;

        public PoiType PoiType
        {
            get
            {
                return _poiType;
            }
        }

        public Location Location
        {
            get
            {
                return _location;
            }
        }

        public Ping(PoiType poiType, Location location)
        {
            _poiType = poiType;
            _location = location;
        }
    }
}
