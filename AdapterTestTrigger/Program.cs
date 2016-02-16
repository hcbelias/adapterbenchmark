using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterTestTrigger
{
    class Program
    {
        public enum BuyPlanType
        {
            WP,
            OP,
            AdvOP,
            CP,
            AdvCP,
        }

        #region DATA
        static BuyPlan[] buyPlanList = new BuyPlan[] {
            new BuyPlan { Id = "6d7f1205-dc9f-444c-bb66-e367501c6ae9", Type = BuyPlanType.WP },
            new BuyPlan { Id = "65eb0b19-4bd5-40bf-8b76-248ba0cd304f", Type = BuyPlanType.WP },
            new BuyPlan { Id = "a5c3e5f0-b3ed-491d-accc-4abc300a1d42", Type = BuyPlanType.WP },
            new BuyPlan { Id = "bb2d0b3a-aef6-4cc6-86a8-52e7865e41cf", Type = BuyPlanType.WP },
            new BuyPlan { Id = "f8ec66b8-6f39-44cf-986b-e0c4b8489230", Type = BuyPlanType.WP },
            new BuyPlan { Id = "3980c50c-f883-4ad4-ad85-485416dadef8", Type = BuyPlanType.WP },
            new BuyPlan { Id = "f75b77ef-839f-4c4d-9802-c087daaf2f1b", Type = BuyPlanType.WP },

            new BuyPlan { Id = "2de5508f-c576-477d-964d-86b3de7ab8ce", Type = BuyPlanType.CP },
            new BuyPlan { Id = "d12e8f1d-e26e-4ff6-a708-2bd46dbca30a", Type = BuyPlanType.CP },
            new BuyPlan { Id = "c9d7debf-ca68-48f1-b513-134e9679ebc2", Type = BuyPlanType.CP },
            new BuyPlan { Id = "5259f10d-fddf-4fea-9f02-14a20a70b6f1", Type = BuyPlanType.CP },
            new BuyPlan { Id = "c925023c-ba86-42c0-a32b-4aef64dc3aa9", Type = BuyPlanType.CP },
            new BuyPlan { Id = "0fd77189-a05a-4209-9259-56ade5cc57a1", Type = BuyPlanType.CP },
            new BuyPlan { Id = "b7d45867-2a12-4273-af42-11270d7eb958", Type = BuyPlanType.CP },

            new BuyPlan { Id = "91a18a46-d3d5-49a5-8338-4f9d34d8961f", Type = BuyPlanType.OP },
            new BuyPlan { Id = "4299f99b-05d5-42ad-a8f3-01589107a3a4", Type = BuyPlanType.OP },
            new BuyPlan { Id = "314181bd-280e-4d15-9e15-83301b92c307", Type = BuyPlanType.OP },
            new BuyPlan { Id = "bd589ef9-1ae4-45e8-8317-4cc80a52b370", Type = BuyPlanType.OP },
            new BuyPlan { Id = "94497930-ecbd-4fc3-a293-e01467295279", Type = BuyPlanType.OP },
            new BuyPlan { Id = "92682247-8b75-454f-afbc-702a494e8df8", Type = BuyPlanType.OP },

            new BuyPlan { Id = "cc867cc7-dc31-4c77-8e4b-cf520d6f0ce2", Type = BuyPlanType.AdvCP },
            new BuyPlan { Id = "bd2e4726-7f1b-489a-9141-6c73d8faeaba", Type = BuyPlanType.AdvCP },
            new BuyPlan { Id = "29f12987-aa24-4462-bb60-8d4986f499fb", Type = BuyPlanType.AdvCP },
            new BuyPlan { Id = "0370867e-2fd2-4fc3-8357-916594bb2488", Type = BuyPlanType.AdvCP },
            new BuyPlan { Id = "b5c6d2f6-7a63-454a-b5ef-e278f23b133e", Type = BuyPlanType.AdvCP },
            new BuyPlan { Id = "fb298a99-210c-4bad-8f3b-5516eeb3c067", Type = BuyPlanType.AdvCP },

            new BuyPlan { Id = "ffbba87c-b68f-4519-89ce-535704ed431c", Type = BuyPlanType.AdvOP },
            new BuyPlan { Id = "5dfc373b-0164-43b5-b5db-35a25f010d52", Type = BuyPlanType.AdvOP },
            new BuyPlan { Id = "718dee4c-12c6-4fc4-8b9a-5eb713911251", Type = BuyPlanType.AdvOP },
            new BuyPlan { Id = "6239dfcb-28e0-4889-bd43-80282409fcae", Type = BuyPlanType.AdvOP },
            new BuyPlan { Id = "b07c72d7-9a5b-42a1-afda-cce562c056d6", Type = BuyPlanType.AdvOP },
            new BuyPlan { Id = "00ef3019-b8ef-4a8c-9c6a-da142e8807a2", Type = BuyPlanType.AdvOP },
            new BuyPlan { Id = "99d736be-780c-48d9-b120-2c03473d363e", Type = BuyPlanType.AdvOP },
        };
        #endregion
        static void Main(string[] args)
        {
            var webClient = new System.Net.WebClient();
            foreach (var buyPlan in buyPlanList)
            {
                GetBuyPlanResult(webClient, buyPlan, true);
                GetBuyPlanResult(webClient, buyPlan, false);
            }
        }

        private static void GetBuyPlanResult(System.Net.WebClient webClient, BuyPlan buyPlan, bool aggregated)
        {
            var baseurl = "http://10.8.8.176/adapter/api/buy-plan-forecast/{0}/{1}";

            var urlPath = buyPlan.GetUrlByBuyPlanType(aggregated);
            var url = string.Format(baseurl, buyPlan.Id, urlPath);
            Console.WriteLine("URL: " + url);

            System.Diagnostics.Debug.WriteLine("URL: " + url);
            try
            {
                var result = webClient.DownloadString(url);


                
                System.Diagnostics.Debug.WriteLine(result);
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
                
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
        }

        public class BuyPlan
        {
            public string Id { get; set; }

            public BuyPlanType Type { get; set; }

            public string GetUrlByBuyPlanType(bool aggregated)
            {
                var urlPath = aggregated ? "aggregated" : "receipt-and-sales";
                var url = "";
                switch (this.Type)
                {
                    case BuyPlanType.WP:
                        url = String.Format("working-plan/{0}", urlPath);
                        break;
                    case BuyPlanType.OP:
                    case BuyPlanType.AdvOP:
                        url = String.Format("original-plan/{0}", urlPath);
                        break;
                    case BuyPlanType.CP:
                    case BuyPlanType.AdvCP:
                        url = String.Format("current-plan/{0}", urlPath);
                        break;
                }
                return url;
            }

        }
    }
}
