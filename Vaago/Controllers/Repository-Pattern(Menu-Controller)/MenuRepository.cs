using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaago.Models
{
    public class MenuRepository : IMenuRepository
    {
        private readonly VaagoProjectEntities1 _context;

        public MenuRepository(VaagoProjectEntities1 context)
        {
            _context = context;
        }

        public List<Menu> GetAllMenus()
        {
            return _context.Menus.ToList();
        }
    }
}
