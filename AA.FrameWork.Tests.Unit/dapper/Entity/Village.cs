using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AA.Dapper.Test
{
    public class Village
    {

        /// <summary>
        /// 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public System.Guid Id
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string VillageName
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string VillageAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Province
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Area
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProvinceCode
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CityCode
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime GmtCreate
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime GmtModified
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Contact
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContactTel
        {
            get;
            set;
        }

    }
}




