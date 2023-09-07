using System;
using DragonBoyZ.Model.ModelBase;

namespace DragonBoyZ.Model.Option
{
    public class OptionItem : OptionBase
    {
        public OptionItem()
        {
            
        }

        public override object Clone()
        {
            return new OptionItem()
            {
                Id = Id,
                Param = Param
            };
        }
     
    }
}