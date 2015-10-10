using System;
using System.Collections;
using Telerik.Web.UI;

namespace Homory.Model
{
    public abstract class HomoryCorePageWithGrid : HomoryCorePageWithNotify
    {

        protected virtual void grid_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (!(e.Item is GridDataItem) || e.Item.DataItem == null) return;
            var state = (e.Item.DataItem as dynamic).State;
            if (state == State.启用 || state == State.内置)
            {
                e.Item.ForeColor = HomoryCoreConstant.Color启用;
            }
            else if (state == State.停用)
            {
                e.Item.ForeColor = HomoryCoreConstant.Color停用;
            }
            else if (state == State.默认 || state == State.审核)
            {
                e.Item.ForeColor = HomoryCoreConstant.Color审核;
            }
        }

        protected virtual bool NotSet(Hashtable values, string name)
        {
            return values[name] == null || string.IsNullOrWhiteSpace(values[name].ToString());
        }

        protected virtual string Get(Hashtable values, string name, string defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            var result = values[name].ToString();
            return string.IsNullOrWhiteSpace(result) ? defaultValue : result;
        }

        protected virtual int Get(Hashtable values, string name, int defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            int result;
            return int.TryParse(values[name].ToString(), out result) ? result : defaultValue;
        }

        protected virtual bool? Get(Hashtable values, string name, bool? defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            bool result;
            return bool.TryParse(values[name].ToString(), out result) ? result : defaultValue;
        }

        protected virtual DateTime? Get(Hashtable values, string name, DateTime? defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            DateTime result;
            return DateTime.TryParse(values[name].ToString(), out result) ? result : defaultValue;
        }

        protected virtual Guid Get(Hashtable values, string name, Guid defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            Guid result;
            return Guid.TryParse(values[name].ToString(), out result) ? result : defaultValue;
        }

        protected virtual State Get(Hashtable values, string name, State defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            State result;
            return Enum.TryParse(values[name].ToString(), out result) ? ((int)result == -1 ? defaultValue : result) : defaultValue;
        }

        protected virtual BuildType Get(Hashtable values, string name, BuildType defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            BuildType result;
            return Enum.TryParse(values[name].ToString(), out result) ? ((int)result == -1 ? defaultValue : result) : defaultValue;
        }

        protected virtual ClassType Get(Hashtable values, string name, ClassType defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            ClassType result;
            return Enum.TryParse(values[name].ToString(), out result) ? ((int)result == -1 ? defaultValue : result) : defaultValue;
        }

        protected virtual ResourcePrizeRange Get(Hashtable values, string name, ResourcePrizeRange defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            ResourcePrizeRange result;
            return Enum.TryParse(values[name].ToString(), out result) ? ((int)result == -1 ? defaultValue : result) : defaultValue;
        }

        protected virtual ResourcePrizeLevel Get(Hashtable values, string name, ResourcePrizeLevel defaultValue)
        {
            if (NotSet(values, name)) return defaultValue;
            ResourcePrizeLevel result;
            return Enum.TryParse(values[name].ToString(), out result) ? ((int)result == -1 ? defaultValue : result) : defaultValue;
        }
    }
}
