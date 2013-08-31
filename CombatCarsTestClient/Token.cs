using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsTestClient
{
    class Token
    {
        //{"UserId":1,"TokenString":"48949b2c-5233-4f05-9aed-aa1d5e614bff","Issued":"2013-08-25T22:31:35.276248+02:00","ValidTo":"2013-08-25T22:36:35.276248+02:00"}

        public Token()
        {

        }

        public int UserId { get; set; }
        public string TokenString { get; set; }
        public DateTime Issued { get; set; }
        public DateTime ValidTo { get; set; }


    }
}
