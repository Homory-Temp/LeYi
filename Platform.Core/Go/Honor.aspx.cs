using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoHonor : HomoryCorePageWithGrid
    {
        private const string Right = "Honor";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInit();
                LogOp(OperationType.查询);
            }
        }

        private void LoadInit()
        {
            loading.InitialDelayTime = int.Parse("Busy".FromWebConfig());
         
            count1.Value = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditPublish").Value);
            count2.Value = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditComment").Value);
            count3.Value = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditReply").Value);
            count4.Value = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditRate").Value);
            count5.Value = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditFavourite").Value);
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grid.DataSource = HomoryContext.Value.PrizeCredit.OrderBy(o => o.PrizeRange).ThenBy(o => o.PrizeLevel).ToList();
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                try
                {
                    var values = command.NewValues;
                    if (NotSet(values, "Credit"))
                        continue;
                    var credit = Get(values, "Credit", 0);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Update:
                            var range = Get(values, "PrizeRange", ResourcePrizeRange.其他);
                            var level = Get(values, "PrizeLevel", ResourcePrizeLevel.其他);
                            HomoryContext.Value.PrizeCredit.Where(o => o.PrizeRange == range && o.PrizeLevel == level).Update(o => new PrizeCredit
                            {
                                Credit = credit
                            });
                            HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.编辑);
                            break;
                    }
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch
                // ReSharper restore EmptyGeneralCatchClause
                {
                }
            }
            Notify(panel, "操作成功", "success");
        }

        protected override string PageRight
        {
            get { return Right; }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditPublish").Value = count1.Value.Value.ToString();
            HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditComment").Value = count2.Value.Value.ToString();
            HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditReply").Value = count3.Value.Value.ToString();
            HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditRate").Value = count4.Value.Value.ToString();
            HomoryContext.Value.Dictionary.Single(o => o.Key == "CreditFavourite").Value = count5.Value.Value.ToString();
            HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
            Notify(panel, "操作成功", "success");
        }
    }
}
