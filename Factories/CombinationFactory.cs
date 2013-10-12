﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PrestaSharp.Factories
{
    public class CombinationFactory : RestSharpFactory
    {
        public CombinationFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {

        }

        public Entities.combination Get(int CombinationId)
        {
            RestRequest request = this.RequestForGet("combinations", CombinationId, "combination");
            return this.Execute<Entities.combination>(request);
        }

        public Entities.combination Add(Entities.combination Combination)
        {
            Combination.id = null;
            RestRequest request = this.RequestForAdd("combinations", Combination);

            /*
             * Bug in the serializer with the serialization of id_product and minimal_quantity.
             * It´s needed to write again the value of id_product and minimal_quantity with the same value of the object Combination.
             */
            Entities.combination aux = this.Execute<Entities.combination>(request);
            aux.id_product = Combination.id_product;
            aux.minimal_quantity = Combination.minimal_quantity;
            return aux;
        }


        public void Update(Entities.combination Combination)
        {
            RestRequest request = this.RequestForUpdate("combinations", Combination.id, Combination);
            this.Execute<Entities.combination>(request);
        }

        public void Delete(Entities.combination Combination)
        {
            RestRequest request = this.RequestForDelete("combinations", Combination.id);
            this.Execute<Entities.combination>(request);
        }

        public List<int> GetIds()
        {
            RestRequest request = this.RequestForGet("combinations", null, "prestashop");
            return this.ExecuteForGetIds<List<int>>(request, "combination");
        }

        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public List<Entities.combination> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("combinations", "full", Filter, Sort, Limit, "combinations");
            return this.Execute<List<Entities.combination>>(request);
        }

        /// <summary>
        /// Get all combinations.
        /// </summary>
        /// <returns>A list of combinations</returns>
        public List<Entities.combination> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }
    }
}