using PeddleRealmWebApp.Models;
using System.Linq;
using System.Web.Http;

namespace PeddleRealmWebApp.Controllers.Api
{

    [Authorize]
    [RoutePrefix("api/item")]
    public class ItemController : ApiController
    {
        private ApplicationDbContext _context;

        public ItemController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var item = _context.Items.SingleOrDefault(i => i.Id == id);

            if (item != null)
            {
                if (item.IsDeleted)
                {
                    return NotFound();
                }

                item.IsDeleted = true;
                _context.SaveChanges();
            }


            return Ok();
        }
    }
}
