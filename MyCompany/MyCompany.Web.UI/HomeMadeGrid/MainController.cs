using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GS.Extensions.DotNetExtensions;
using Turn5.AM.Core.Helpers;
using Turn5.AM.Models.Admin.MarketingInitiative;
using Turn5.AM.Services.ServiceContracts;

namespace Turn5.AM.Web.UI.Controllers.Admin
{
    public class MarketingInitiativeController : BaseAdminController
    {
        private readonly IMarketingInitiativeService _marketingInitiativeService;

        public MarketingInitiativeController(IMarketingInitiativeService marketingInitiativeService)
        {
            _marketingInitiativeService = marketingInitiativeService;
        }

        public ActionResult GetInitiatives(int? page = null, string sort = null, string direction = null)
        {
            var model = _marketingInitiativeService.GetMarketingInitiatives(page, sort, direction);

            if (page != null)
            {
                model.CurrentPage = (int)page;
            }

            var rowCount = model.InitiativesList[0].RowCount;
            var numberOfPages = (rowCount / model.PageSize);
            if (rowCount % model.PageSize != 0)
            {
                model.NumberOfPages = numberOfPages + 1;
            } 

            var urlHelper = new UrlHelper(Request.RequestContext, RouteTable.Routes);

            SetSortingPagingUrls(ref model, urlHelper);        
            
            return View("MarketingInitiative", model);
        }

        public ActionResult GetEditModal(int id)
        {
            var model = _marketingInitiativeService.GetMarketingInitiative(id);
            return View("_InitiativesModal", model);
        }

        [HttpPost]
        public ActionResult DeleteInitiative(int initiativeId)
        {
            _marketingInitiativeService.DeleteInitiative(initiativeId);

            var urlHelper = new UrlHelper(Request.RequestContext, RouteTable.Routes);
            return
                RedirectPermanent(urlHelper.MarketingInitiativesAdminUrl(null,
                    Convert.ToInt32(UrlHelpers.QueryStringValue("page")),
                    UrlHelpers.QueryStringValue("sort"),
                    UrlHelpers.QueryStringValue("direction")));
        }

        [HttpPost]
        public ActionResult SaveModal(FormCollection formCollection)
        {
            var initiative = new Initiative
            {
                Id = Convert.ToInt32(formCollection["id"]),
                Name = formCollection["name"],
                Generation = formCollection["generation"],
                Type = formCollection["type"],
                Url = formCollection["url"],
                AltText = formCollection["alttext"],
                CategoryBanner = formCollection["catbanner"],
                SearchBanner = formCollection["searchbanner"],
                ProductBanner = formCollection["prodbanner"],
                ProductIconOverlay = formCollection["prodicon"],
                HomeSlideImage = formCollection["homeslideimage"],
                HomeSlidePosition = Convert.ToInt32(formCollection["homeslidepos"]),
                StartDate = formCollection["startdate"].ToDateTime(),
                EndDate = formCollection["enddate"].ToDateTime(),
                IsEnabled = formCollection["enabled"].ToBoolean(),
                Priority = Convert.ToInt32(formCollection["priority"]),
            };

            _marketingInitiativeService.UpdateInitiative(initiative);

            var urlHelper = new UrlHelper(Request.RequestContext, RouteTable.Routes);
            return
                RedirectPermanent(urlHelper.MarketingInitiativesAdminUrl(null,
                    Convert.ToInt32(UrlHelpers.QueryStringValue("page")), 
                    UrlHelpers.QueryStringValue("sort"),
                    UrlHelpers.QueryStringValue("direction")));
        }

        public ActionResult GetCookie()
        {
            var model = new InitiativeCookie();
            var miCookie = Request.Cookies["MarketingInitiativeDate"];
            if (miCookie != null)
            {
                DateTime miCookieDate;
                DateTime.TryParse(miCookie.Value, out miCookieDate);
                model.InitiativeDate = Convert.ToDateTime(miCookieDate);
            }

            return View("MarketingInitiativeCookie", model);
        }

        [HttpPost]
        public ActionResult SetCookie(FormCollection formCollection)
        {
            var model = new InitiativeCookie();
            if (formCollection != null && formCollection.Count > 0)
            {
                Response.Cookies.Remove("MarketingInitiativeDate");
                model.InitiativeDate = Convert.ToDateTime(formCollection[0]);
                var cookie = new HttpCookie("MarketingInitiativeDate")
                {
                    Value = formCollection[0],
                    Expires = DateTime.Now.AddYears(2)
                };
                Response.Cookies.Add(cookie);
            }
            return RedirectPermanent("/mi/getcookie");
        }

        private void SetSortingPagingUrls(ref Initiatives model, UrlHelper urlHelper)
        {
            var currentPage = UrlHelpers.QueryStringValue("page").ToInt();

            var sort = UrlHelpers.QueryStringValue("sort");
            var direction = UrlHelpers.QueryStringValue("direction");

            var itemCount = model.InitiativesList[0] != null ? model.InitiativesList[0].RowCount : 0;
            var pageSize = model.PageSize;
            var lastPage = itemCount / pageSize;

            if (itemCount%pageSize != 0)
            {
                lastPage++;
            }

            var nextPage = 1;
            if (currentPage <= lastPage)
            {
                nextPage = currentPage + 1;
            }

            var previousPage = 1;
            if (currentPage > 1)
            {
                previousPage = currentPage - 1;
            }

            model.FirstPageUrl = urlHelper.MarketingInitiativesAdminUrl(null, 1, sort, direction);
            model.PreviousPageUrl = urlHelper.MarketingInitiativesAdminUrl(null, previousPage, sort, direction);
            model.NextPageUrl = itemCount == 0 ? string.Empty : urlHelper.MarketingInitiativesAdminUrl(null, nextPage, sort, direction);
            model.LastPageUrl = itemCount == 0 ? string.Empty : urlHelper.MarketingInitiativesAdminUrl(null, lastPage, sort, direction);

            var newSortingDirection = direction == "ASC" ? "DESC" : "ASC";
            model.SortNameUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "InitiativeName", newSortingDirection);
            model.SortGenerationUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "Generation", newSortingDirection);
            model.SortTypeUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "InitiativeType", newSortingDirection);
            model.SortUrlUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "InitiativeUrl", newSortingDirection);
            model.SortAltTextUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "InitiativeAltText", newSortingDirection);
            model.SortStartDateUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "StartDateTime", newSortingDirection);
            model.SortEndDateUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "EndDateTime", newSortingDirection);
            model.SortEnabledUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "IsEnabled", newSortingDirection);
            model.SortPriorityUrl = urlHelper.MarketingInitiativesAdminUrl(null, currentPage, "InitiativePriority", newSortingDirection);
        }

    }
}