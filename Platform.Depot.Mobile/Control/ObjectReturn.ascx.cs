using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Control_ObjectReturn : DepotControlSingle
{
    public void LoadDefaults(InMemoryReturn @return)
    {
        id.Text = @return.UseX.ToString();
        var usex = DataContext.DepotUseXRecord.FirstOrDefault(o => o.Id == @return.UseX);
        code.Text = @return.Code;
        time.Text = usex.Time.ToString("yyyy-MM-dd");
        name.Text = usex.Name;
        amount.Value = 1;
    }

    public InMemoryReturn PeekValue()
    {
        var result = new InMemoryReturn();
        result.Code = code.Text;
        result.Amount = amount.Value.HasValue ? (decimal)amount.Value.Value : (decimal?)null;
        result.OutAmount = outAmount.Value.HasValue ? (decimal)outAmount.Value.Value : (decimal?)null;
        result.Note = note.Text;
        result.UseX = id.Text.GlobalId();
        return result;
    }

    public int ItemIndex
    {
        get; set;
    }
}
