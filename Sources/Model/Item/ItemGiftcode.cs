using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonBoyZ.Model.Option;

namespace DragonBoyZ.Model.Item
{
    public class ItemGiftcode
    {
        public short Id { get; set; }
        public int Quantity { get; set; }
        public List<OptionItem> Options { get; set; }
    }
}
