using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.Domain.Abstract;

namespace Bonch.MinUI.Controllers
{
    public class ArrayController : Controller
    {
        IInfraStructureRepository _ifsReponsitory;

        public ArrayController(IInfraStructureRepository ifsRepository)
        {
            _ifsReponsitory = ifsRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
