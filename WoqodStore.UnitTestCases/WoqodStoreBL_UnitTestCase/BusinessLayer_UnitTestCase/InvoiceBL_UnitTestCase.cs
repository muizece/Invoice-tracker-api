using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WoqodStoreBL.BusinessLayer;

namespace WoqodStore.UnitTestCases.WoqodStoreBL_UnitTestCase.BusinessLayer_UnitTestCase
{

    public class InvoiceBL_UnitTestCase
    {
        private readonly InvoiceBL invoiceBL;

        public InvoiceBL_UnitTestCase()
        {
            invoiceBL = new InvoiceBL();
        }

        [Fact]
        public void ValidateRequestParameters_invalid_storeId_positive_response()
        {
            //Arrange

            int storedId = -1;
            long receiptNo = 12345;
            DateTime fromDate = new DateTime(2024,01,01);
            DateTime toDate= new DateTime(2024, 01, 02);


            //Act

            string result= invoiceBL.ValidateRequestParameters(receiptNo,storedId,fromDate,toDate);

            //Assert

            Assert.Equal("Please provide a valid storeId.", result);

        }


        [Fact]
        public void ValidateRequestParameters_invalid_storeId_negtive_response()
        {
            //Arrange

            int storedId = -1;
            long receiptNo = 12345;
            DateTime fromDate = new DateTime(2024, 01, 01);
            DateTime toDate = new DateTime(2024, 01, 02);


            //Act

            string result = invoiceBL.ValidateRequestParameters(receiptNo, storedId, fromDate, toDate);

            //Assert

            Assert.NotEqual("Some Negative  response", result);

        }

        [Fact]

        public void ValidateRequestParameters_invalid_receiptNo__positive_response()
        {
            //Arrange

            int storedId = 1;
            long receiptNo = -1;
            DateTime fromDate = new DateTime(2024, 01, 01);
            DateTime toDate = new DateTime(2024, 01, 02);


            //Act

            string result = invoiceBL.ValidateRequestParameters(receiptNo, storedId, fromDate, toDate);

            //Assert

            Assert.Equal("Please provide a valid receipt number.", result);

        }

    }
}
