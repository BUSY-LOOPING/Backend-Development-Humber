using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/q8")]
    [ApiController]
    public class Question8Controller : ControllerBase
    {
        /// <summary>
        /// Returns the checkout summary for an order of SquashFellows plushies.
        /// </summary>
        /// <param name="Small">The number of Small SquashFellows plushies ordered. Must be greater than or equal to 0.</param>
        /// <param name="Large">The number of Large SquashFellows plushies ordered. Must be greater than or equal to 0.</param>
        /// <returns>The checkout summary including the subtotal, tax, and total amount.</returns>
        /// <example>
        /// For example, if the order includes 2 Small and 1 Large plushies, the summary will be:
        /// 2 Small @ $25.50 = $51.00; 1 Large @ $40.50 = $40.50; Subtotal = $91.50; Tax = $11.90 HST; Total = $103.40
        /// </example>
        [HttpPost(template:"squashfellows")]
        [Consumes("application/x-www-form-urlencoded")] //**urlencoded**
        public string getCheckoutSummary([FromForm] int Small, [FromForm] int Large)  //***not FromBody but FromForm; FromBody can only be bound with 1 param
        {
            double totalForSmall = Small * 25.5;
            double totalForLarge = Large * 40.5;
            double subTotalWithoutTax = totalForSmall + totalForLarge;
            double tax = 0.13 * subTotalWithoutTax;
            double subTotalWithTax = subTotalWithoutTax + tax;
            return $"{Small} Small @ $25.50 = ${Math.Round(totalForSmall,2).ToString("F2")}; {Large} Large @ $40.50 = ${Math.Round(totalForLarge, 2).ToString("F2")}; Subtotal = ${Math.Round(subTotalWithoutTax, 2).ToString("F2")}; Tax = ${Math.Round(tax, 2).ToString("F2")} HST; Total = ${Math.Round(subTotalWithTax, 2).ToString("F2")}";
        }
    }
}
