using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_Code : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
        }
    }

    protected void all_ServerClick(object sender, EventArgs e)
    {
        if (_all.Value == "1")
        {
            tree.UncheckAllNodes();
            _all.Value = "0";
            all.Value = "全部选定";
        }
        else
        {
            tree.CheckAllNodes();
            _all.Value = "1";
            all.Value = "清除选定";
        }
        view.Rebind();
    }

    public class CodeObject
    {
        public DepotObjectCatalog ObjectCatalog { get; set; }
        public DepotInX InX { get; set; }
        public string CatalogPath { get; set; }
        public bool Single { get; set; }
        public bool Fixed { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public string Place { get; set; }
        public bool Consumable { get; set; }
        public string Code { get; set; }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var isVirtual = Depot.Featured(DepotType.固定资产库);
        var source = catalogs.Join(DataContext.DepotObjectCatalog.Where(o => o.IsVirtual == isVirtual && o.IsLeaf == true), o => o, o => o.CatalogId, (a, b) => b).ToList().Join(DataContext.DepotInX, o => o.ObjectId, o => o.ObjectId, (a, b) => new { DOC = a, DIX = b }).ToList().Select(o => new CodeObject { InX=o.DIX, ObjectCatalog = o.DOC }).ToList();
        source.ForEach(o =>
        {
            var obj = o.InX.DepotObject;
            o.CatalogPath = DataContext.ToCatalog(o.ObjectCatalog.CatalogId, o.ObjectCatalog.Level).Single();
            o.Name = obj.Name;
            o.Unit = obj.Unit;
            o.Specification = obj.Specification;
            o.Single = obj.Single;
            o.Fixed = obj.Fixed;
            o.Place = o.InX.Place;
            o.Consumable = obj.Consumable;
            o.Code = o.InX.Code;
        });
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }

    protected void pager_PageIndexChanged(object sender, Telerik.Web.UI.RadDataPagerPageIndexChangeEventArgs e)
    {
        view.Rebind();
    }
}
